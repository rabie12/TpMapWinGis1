    public BicValidationResponse getBicFromIban(String iban) {
        Iban ibanValue = null;
        BicValidationResponse response = new BicValidationResponse();
        response.setIban(iban);
        try {
            ibanValue = Iban.valueOf(iban);
        } catch (IbanFormatException | InvalidCheckDigitException | UnsupportedCountryException e) {
            if (LOGGER.isDebugEnabled()) {
                LOGGER.debug("Invalid iban provided", e);
            }
            response.setStatus("INVALID_IBAN");

            return response;
        }

        response.setIban(iban);
        if (iban.isEmpty()) {
            return response;
        }
        List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name()
        );

        BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);
        if (bankAgency == null || bankAgency.getBankInfo() == null) {
            log.debug("Unknown bankCode=<{}> branchCode=<{}> country=<{}>", ibanValue.getBranchCode(), ibanValue.getBranchCode(),
                    ibanValue.getCountryCode().name());
            response.setStatus("INVALID_IBAN");
            return response;
        }
        response.setBic(bankAgency.getBankInfo().getBic());
        response.setStatus("VALID");
        return response;
    }

    can you refector this
