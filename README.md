public BicValidationResponse getBicFromIban(String iban) {
    BicValidationResponse response = new BicValidationResponse();
    response.setIban(iban);

    if (iban == null || iban.isEmpty()) {
        response.setStatus("INVALID_IBAN");
        return response;
    }

    Iban ibanValue;
    try {
        ibanValue = Iban.valueOf(iban);
    } catch (IbanFormatException | InvalidCheckDigitException | UnsupportedCountryException e) {
        LOGGER.debug("Invalid IBAN provided: {}", iban, e);
        response.setStatus("INVALID_IBAN");
        return response;
    }

    List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
            ibanValue.getBankCode(),
            ibanValue.getCountryCode().name()
    );

    if (agencies.isEmpty()) {
        LOGGER.debug("Unknown bankCode=<{}> branchCode=<{}> country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getBranchCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    BankAgency bankAgency = agencies.get(0);
    if (bankAgency.getBankInfo() == null || bankAgency.getBankInfo().getBic() == null) {
        LOGGER.debug("No BIC found for bankCode=<{}>, country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    response.setBic(bankAgency.getBankInfo().getBic());
    response.setStatus("VALID");
    return response;
}