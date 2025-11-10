    public Optional<IbanSearchHistoryDTO> validateIban(String iban) {
        String ibanFormatted = iban.replaceAll("\\s+", "");
        Iban ibanValue = null;
        try {
            IbanUtil.validate(ibanFormatted);
        } catch (IbanFormatException ex) {
            throw new IbanFormatException("Invalid IBAN format: " + iban, ex);
        }
        Optional<IbanSearchHistory> ibanSearchHistory = searchHistoryRepo.findByIban(ibanFormatted);
        IbanSearchHistoryDTO ibanSearchHistoryDTO = new IbanSearchHistoryDTO();
        ibanValue = Iban.valueOf(ibanFormatted);
        BankAgency bankAgency = bankAgencyRepo
                    .findByBranchCodeAndBankCodeAndCountryIso2(ibanValue.getBranchCode(), ibanValue.getBankCode(),
                            ibanValue.getCountryCode().name()).orElse(null);

            if (bankAgency != null) {
                log.info("IBAN found in database and is up-to-date.");
                ibanSearchHistoryDTO.setIban(iban);
                ibanSearchHistoryDTO.setBankInfoDTO(bankInfoMapper.toDto(bankAgency.getBankInfo()));
                ibanSearchHistoryDTO.setBankAgencyDTO(bankAgencyMapper.toDTO(bankAgency));
                ibanSearchHistoryDTO.setResult("VALID");
                return Optional.of(ibanSearchHistoryDTO);
            }

        if (ibanSearchHistory.isPresent()) {
            IbanSearchHistory existingRecord = ibanSearchHistory.get();
            if (existingRecord.getUpdatedAt().isAfter(LocalDateTime.now().minusDays(300))) {
                log.info("IBAN found in database and is up-to-date.");
                return Optional.of(searchHistorymapper.toDTO(existingRecord));
            } else {
                log.info("IBAN found in database but outdated. Calling external API for an update.");
                return updateFromExternalApi(ibanFormatted, existingRecord);
            }
        } else {
            log.info("IBAN not found in database. Calling external API");
            return fetchFromExternalApi(ibanFormatted);
        }
    }

    optimize this, avoid this         Iban ibanValue = null;
 if yyou can
