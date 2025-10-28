Excellent — let’s clean up and optimize your getDataForCountry() method while keeping the same logic, making it more readable, efficient, and safe (especially around persistence and country handling).

I’ll also make sure the country is automatically uppercased if not null.

⸻

✅ Optimized Version

Here’s the improved method with comments explaining each change:

public CompanyDTO getDataForCountry(String country, String siren)
        throws EntityNotFoundException, BadRequestException {

    // --- 1️⃣ Validate inputs early ---
    if (siren == null || siren.isBlank()) {
        log.warn("Siren is null or blank");
        throw new BadRequestException("No company selected");
    }

    if (country == null) {
        log.warn("Country is null");
        throw new BadRequestException("Country is null or invalid");
    }

    // Always uppercase the country to standardize
    country = country.toUpperCase(Locale.ROOT);

    if (!Set.of("FR", "LU", "GB", "CH").contains(country)) {
        log.warn("Invalid country code: {}", country);
        throw new BadRequestException("Country is invalid");
    }

    if (!RegexCompanyNumber.companyNumberRegexMatch(country, siren)) {
        throw new BadRequestException("The company number format is incorrect");
    }

    // --- 2️⃣ Check existing company in DB ---
    Optional<LegalEntity> existingEntityOpt = companyRepository.findById(siren);
    if (existingEntityOpt.isPresent()) {
        LegalEntity existingEntity = existingEntityOpt.get();
        String existingCountry = existingEntity.getCountry();

        if (country.equalsIgnoreCase(existingCountry)) {
            log.info("Company {} is already present in the DB", siren);

            if (existingEntity.getCreatedAt() == null) {
                existingEntity.setCreatedAt(LocalDateTime.now().minusHours(1));
            }

            long minutesSinceCreation = ChronoUnit.MINUTES.between(
                    existingEntity.getCreatedAt(),
                    LocalDateTime.now()
            );

            if (minutesSinceCreation <= DIFF_DATE_MAX) {
                log.info("Company {}'s stored data returned (age: {} minutes)", siren, minutesSinceCreation);
                return companyMapper.legalEntityToCompanyDTO(existingEntity);
            }

            log.info("Company {}'s information will be refreshed", siren);
        }
    }

    // --- 3️⃣ Fetch connector(s) ---
    List<Connector> connectors = connectorRepository.findActiveConnectorByCountry(country, "REFERENTIAL");
    if (connectors.isEmpty()) {
        log.warn("No referential connector for {}", country);
        throw new EntityNotFoundException("No referential connector for " + country);
    }

    // --- 4️⃣ Retrieve and map company data ---
    LegalEntity company = new LegalEntity();

    for (Connector connector : connectors) {
        log.info("Using connector: {}", connector.getServiceName());

        // Refresh token if needed
        if (connector.getApiToken() == null ||
            connector.getApiToken().getExpiratedAt() == null ||
            connector.getApiToken().getExpiratedAt().isBefore(LocalDateTime.now()) ||
            connector.getApiToken().getToken().isEmpty()) {

            log.info("Generating new API token for connector {}", connector.getServiceName());
            tokenService.generateToken(connector);
        }

        ConnectorInterface serviceConnector =
                (ConnectorInterface) context.getBean(connector.getServiceName(), ConnectorInterface.class);

        CompanyDTO connectorData = serviceConnector.getData(connector, siren);
        company = companyMapper.companyDtoToCompany(company, connectorData);
    }

    // --- 5️⃣ Final validation & persistence ---
    if (company.getIdentifier() == null) {
        throw new EntityNotFoundException("Company " + siren + " not found");
    }

    company.setCountry(country);
    company.setCreatedAt(LocalDateTime.now());

    companyRepository.save(company);

    return companyMapper.legalEntityToCompanyDTO(company);
}


⸻

🔧 Key Improvements

🧠 1. Input Validation Simplified
	•	All null / invalid checks moved to the top for readability.
	•	Unified and normalized country to uppercase with:

country = country.toUpperCase(Locale.ROOT);



⚙️ 2. Cleaner DB lookup
	•	Simplified Optional handling.
	•	Moved duplicated log.info() and date logic into a single block.

🧩 3. Connector handling streamlined
	•	Loop logic simplified with clear logs.
	•	Token refresh check made more readable with .isBefore().

💾 4. Safer persistence
	•	Keeps your same save logic but makes sure mapping is done cleanly.
	•	No logic changes — just clearer structure.

📜 5. Logging improvements
	•	More consistent, meaningful logging (with minutesSinceCreation, connector names, etc.)

⸻

⚠️ Optional Improvement (Recommended)

If you keep seeing that “Identifier must be manually assigned” error:
	•	Ensure companyMapper.companyDtoToCompany() sets identifier correctly from companyDTO (not null, not overwritten by null).
	•	If the company already exists, load it from DB and update instead of creating a new one:

LegalEntity company = companyRepository.findById(siren).orElse(new LegalEntity());



That prevents accidental “new entity” persistence attempts for existing IDs.

⸻

Would you like me to include that “update-or-insert” pattern in the final optimized method?
It would prevent Hibernate from ever trying to persist() a duplicate ID again.