    public CompanyDTO getDataForCountry(String country, String siren) throws EntityNotFoundException, BadRequestException {
        if (siren == null) {
            log.warn("Siren is null");
            throw new BadRequestException("No company selected");
        }
        if (country == null || !(country.equals("FR") || country.equals("LU") || country.equals("GB")|| country.equals("CH"))) {
            log.warn("Country is null or invalid");
            throw new BadRequestException("Country is null or invalid");
        }
        if (!RegexCompanyNumber.companyNumberRegexMatch(country, siren)) {
            throw new BadRequestException("The company number format is incorrect");
        }
        Optional<LegalEntity> legalEntity = companyRepository.findById(siren);
        if (legalEntity.isPresent() && legalEntity.get().getCountry() != null && legalEntity.get().getCountry().equals(country)) {
            log.info("The company {} is already present in the DB", siren);
            if (legalEntity.get().getCreatedAt() == null) {
                legalEntity.get().setCreatedAt(LocalDateTime.now().minusHours(1));
            }

            if (ChronoUnit.MINUTES.between(legalEntity.get().getCreatedAt(), LocalDateTime.now()) <= DIFF_DATE_MAX) {
                log.info("Company {}'s stored in db has been returned", siren);
                return companyMapper.legalEntityToCompanyDTO(legalEntity.get());
            }
            else {
                log.info("Company {}'s information will be refreshed", siren);
            }
        }
        List<Connector> connectors = connectorRepository.findActiveConnectorByCountry(country, "REFERENTIAL");
        if (connectors.isEmpty()) {
            log.warn("No referential connector for {}", country);
            throw new EntityNotFoundException("No referential connector for " + country);
        }
        try {
            LegalEntity company = new LegalEntity();
            for (Connector connector : connectors) {
                log.info("Connector {}", connector.getServiceName());
                String beanName = connector.getServiceName();
                if (connector.getApiToken() == null || connector.getApiToken().getExpiratedAt() == null || connector.getApiToken().getExpiratedAt().compareTo(LocalDateTime.now()) < 0 || connector.getApiToken().getToken().isEmpty()) {
                    log.info("Token generation ...");
                    tokenService.generateToken(connector);
                }
                ConnectorInterface serviceConnector = (ConnectorInterface) context.getBean(beanName, ConnectorInterface.class);
                CompanyDTO companyDTO = serviceConnector.getData(connector, siren);

                company = companyMapper.companyDtoToCompany(company, companyDTO);
            }
            if (company.getIdentifier() == null) {
                throw new EntityNotFoundException("Company " + siren + " not found");
            }
            company.setCountry(country);
            company.setCreatedAt(LocalDateTime.now());
            companyRepository.save(company);
            return companyMapper.legalEntityToCompanyDTO(company);
        } catch (Exception exception) {
            throw new BadRequestException(exception.getMessage());
        }
    }
