Excellent — you’ve already got a fairly large and well-structured BankInfoValidationService, but you’re absolutely right:
👉 there’s duplicate IBAN validation logic scattered across several methods (getBicFromIban, validateIbanAndBic, and validateIban).

Let’s fix that by extracting a reusable IBAN validation utility method and refactoring your service to use it cleanly.

⸻

🧩 Step 1 — Create a Common Utility Method

You can extract IBAN validation into a separate helper class, e.g.
eu.olkypay.bankInfo.util.IbanValidatorUtil.

✅ IbanValidatorUtil.java

package eu.olkypay.bankInfo.util;

import lombok.extern.slf4j.Slf4j;
import org.iban4j.*;

@Slf4j
public final class IbanValidatorUtil {

    private IbanValidatorUtil() {
        // Prevent instantiation
    }

    /**
     * Validates an IBAN string and returns a parsed {@link Iban} object.
     * Throws a custom exception or returns null depending on configuration.
     *
     * @param iban the IBAN string to validate
     * @return valid Iban instance if valid, null otherwise
     */
    public static Iban validate(String iban) {
        if (iban == null || iban.trim().isEmpty()) {
            log.debug("IBAN is null or empty");
            return null;
        }

        try {
            IbanUtil.validate(iban);
            return Iban.valueOf(iban);
        } catch (IbanFormatException | InvalidCheckDigitException | UnsupportedCountryException e) {
            log.debug("Invalid IBAN provided: {}", iban, e);
            return null;
        }
    }
}


⸻

🧩 Step 2 — Refactor Your Service Methods

Now in your BankInfoValidationService, you replace the inline IBAN validation logic with a single call to IbanValidatorUtil.validate(iban).

🔧 Example Refactor: getBicFromIban

public BicValidationResponse getBicFromIban(String iban) {
    BicValidationResponse response = new BicValidationResponse();
    response.setIban(iban);

    Iban ibanValue = IbanValidatorUtil.validate(iban);
    if (ibanValue == null) {
        response.setStatus("INVALID_IBAN");
        return response;
    }

    List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
            ibanValue.getBankCode(),
            ibanValue.getCountryCode().name()
    );

    if (agencies.isEmpty()) {
        log.debug("Unknown bankCode=<{}> branchCode=<{}> country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getBranchCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    BankAgency bankAgency = agencies.get(0);
    if (bankAgency.getBankInfo() == null || bankAgency.getBankInfo().getBic() == null) {
        log.debug("No BIC found for bankCode=<{}>, country=<{}>",
                ibanValue.getBankCode(),
                ibanValue.getCountryCode().name());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    response.setBic(bankAgency.getBankInfo().getBic());
    response.setStatus("VALID");
    return response;
}


⸻

🔧 Example Refactor: validateIbanAndBic

public BicValidationResponse validateIbanAndBic(String bic, String iban, IbanInfoUtilsDTO ibanInfo) {
    BicValidationResponse response = new BicValidationResponse();
    response.setBic(bic);
    response.setIban(iban);

    Iban ibanValue = IbanValidatorUtil.validate(iban);
    if (ibanValue == null) {
        response.setStatus("INVALID_IBAN");
        return response;
    }

    List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
            ibanValue.getBankCode(),
            ibanValue.getCountryCode().name()
    );

    BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);
    if (bankAgency == null) {
        log.debug("Unknown Bank Agency Info bankCode=<{}> branchCode=<{}> country=<{}>",
                ibanInfo.getBankCode(), ibanInfo.getBranchCode(), ibanInfo.getCountry());
        response.setStatus("INVALID_IBAN");
        return response;
    }

    String expectedBic = bankAgency.getBankInfo().getBic();
    String normalizedExpected = normalizeBic(expectedBic);
    String normalizedReceived = normalizeBic(bic);

    if (!normalizedExpected.equals(normalizedReceived)) {
        log.info("Received Bic=<{}> Expected=<{}>", bic, expectedBic);
        response.setStatus("BIC_MISMATCH");
        return response;
    }

    response.setStatus("VALID");
    return response;
}

private String normalizeBic(String bic) {
    return (bic != null && bic.length() > 8 && bic.endsWith("XXX")) ? bic.substring(0, 8) : bic;
}


⸻

🔧 Example Refactor: validateIban

public Optional<IbanSearchHistoryDTO> validateIban(String iban) {
    String ibanFormatted = iban.replaceAll("\\s+", "");
    Iban ibanValue = IbanValidatorUtil.validate(ibanFormatted);
    if (ibanValue == null) {
        throw new IbanFormatException("Invalid IBAN format: " + iban);
    }

    List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
            ibanValue.getBankCode(),
            ibanValue.getCountryCode().name()
    );

    // (rest of your method stays unchanged)
    ...
}


⸻

📦 Step 3 — Summary of Changes

Change	Description
New Class	IbanValidatorUtil under eu.olkypay.bankInfo.util
Methods Refactored	getBicFromIban, validateIbanAndBic, validateIban
Benefits	✅ Removes duplication  ✅ Improves readability  ✅ Easier to maintain and test  ✅ Centralized logging and validation logic


⸻

Would you like me to also show a JUnit test class for the new IbanValidatorUtil (to make sure IBAN validation is rock-solid and reusable across services)?