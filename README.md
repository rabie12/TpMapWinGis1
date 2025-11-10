       BankAgency bankAgency = bankAgencyRepo
                .findByBankCodeAndCountryIso2(ibanInfo.getBranchCode(), ibanInfo.getBankCode(), ibanInfo.getCountry())
                .orElse(null);
                je veux faire ça mais je pense qu'il faut corriger findByBankCodeAndCountryIso2 je pense 

                @Repository
public interface BankAgencyRepository extends JpaRepository<BankAgency, Long> {

    @Query("SELECT ba FROM BankAgency ba " +
            "JOIN ba.bankInfo bi " +
            "WHERE ((:branchCode IS NULL AND ba.branchCode IS NULL) " +
            "OR (:branchCode IS NOT NULL AND ba.branchCode = :branchCode)) " +
            "AND ba.bankCode = :bankCode " +
            "AND ba.countryIso2 = :country")
    Optional<BankAgency> findByBranchCodeAndBankCodeAndCountryIso2(
            @Param("branchCode") String branchCode,
            @Param("bankCode") String bankCode,
            @Param("country") String country
    );

    @Query("SELECT ba FROM BankAgency ba " +
            "JOIN ba.bankInfo bi " +
            "WHERE + ba.bankCode = :bankCode " +
            "AND ba.countryIso2 = :country")
    Optional<BankAgency> findByBankCodeAndCountryIso2(
            @Param("bankCode") String bankCode,
            @Param("country") String country
    );
}

    je dois faire une demande d'ouverutre de port aussi pour l'env en uat peux tu m'adier à rédiger une peite demande aussi

                
