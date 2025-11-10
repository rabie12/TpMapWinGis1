public Optional<IbanSearchHistoryDTO> validateIban(String iban) {
    String ibanFormatted = iban.replaceAll("\\s+", "");

    try {
        IbanUtil.validate(ibanFormatted);
    } catch (IbanFormatException ex) {
        throw new IbanFormatException("Invalid IBAN format: " + iban, ex);
    }

    // Retrieve existing search history
    Optional<IbanSearchHistory> ibanSearchHistory = searchHistoryRepo.findByIban(ibanFormatted);

    // Parse IBAN and try to get bank/branch info
    Iban ibanValue = Iban.valueOf(ibanFormatted);
    BankAgency bankAgency = bankAgencyRepo
            .findByBranchCodeAndBankCodeAndCountryIso2(
                    ibanValue.getBranchCode(),
                    ibanValue.getBankCode(),
                    ibanValue.getCountryCode().name()
            )
            .orElse(null);

    if (bankAgency != null) {
        log.info("IBAN found in database and is up-to-date.");
        IbanSearchHistoryDTO dto = new IbanSearchHistoryDTO();
        dto.setIban(iban);
        dto.setBankInfoDTO(bankInfoMapper.toDto(bankAgency.getBankInfo()));
        dto.setBankAgencyDTO(bankAgencyMapper.toDTO(bankAgency));
        dto.setResult("VALID");
        return Optional.of(dto);
    }

    if (ibanSearchHistory.isPresent()) {
        IbanSearchHistory existingRecord = ibanSearchHistory.get();
        boolean isUpToDate = existingRecord.getUpdatedAt()
                .isAfter(LocalDateTime.now().minusDays(300));

        if (isUpToDate) {
            log.info("IBAN found in database and is up-to-date.");
            return Optional.of(searchHistorymapper.toDTO(existingRecord));
        }

        log.info("IBAN found in database but outdated. Calling external API for an update.");
        return updateFromExternalApi(ibanFormatted, existingRecord);
    }

    log.info("IBAN not found in database. Calling external API.");
    return fetchFromExternalApi(ibanFormatted);
}