Parfait 👍
Tu veux donc que la méthode findByBankCodeAndCountryIso2 puisse retourner plusieurs résultats, mais que ton code récupère le premier seulement (ou null s’il n’y en a aucun).

Voici les 3 façons possibles de le faire proprement, selon ce que tu préfères 👇

⸻

✅ Option 1 — Laisser la méthode retourner une liste et prendre le premier en Java

C’est la méthode la plus simple et lisible.

Repository :

@Query("SELECT ba FROM BankAgency ba " +
       "JOIN ba.bankInfo bi " +
       "WHERE ba.bankCode = :bankCode " +
       "AND ba.countryIso2 = :country")
List<BankAgency> findByBankCodeAndCountryIso2(
        @Param("bankCode") String bankCode,
        @Param("country") String country
);

Service ou appel :

List<BankAgency> agencies = bankAgencyRepo.findByBankCodeAndCountryIso2(
        ibanInfo.getBankCode(),
        ibanInfo.getCountry()
);

BankAgency bankAgency = agencies.isEmpty() ? null : agencies.get(0);

👉 C’est clair, simple et ne dépend pas d’un comportement implicite.

⸻

✅ Option 2 — Limiter directement dans la requête (JPA + Pageable)

Tu peux demander à JPA de ne retourner qu’un seul résultat (le premier).

Repository :

@Query("SELECT ba FROM BankAgency ba " +
       "JOIN ba.bankInfo bi " +
       "WHERE ba.bankCode = :bankCode " +
       "AND ba.countryIso2 = :country")
List<BankAgency> findFirstByBankCodeAndCountryIso2(
        @Param("bankCode") String bankCode,
        @Param("country") String country,
        Pageable pageable
);

Appel :

BankAgency bankAgency = bankAgencyRepo
        .findFirstByBankCodeAndCountryIso2(
            ibanInfo.getBankCode(),
            ibanInfo.getCountry(),
            PageRequest.of(0, 1)
        )
        .stream()
        .findFirst()
        .orElse(null);


⸻

✅ Option 3 — Utiliser la convention Spring Data (sans @Query)

Si tu n’as pas besoin du JOIN, tu peux même te passer de @Query :

Repository :

Optional<BankAgency> findFirstByBankCodeAndCountryIso2(String bankCode, String countryIso2);

👉 Spring Data JPA va générer tout seul une requête du type :

SELECT * FROM bank_agency WHERE bank_code = ? AND country_iso2 = ? LIMIT 1

Appel :

BankAgency bankAgency = bankAgencyRepo
        .findFirstByBankCodeAndCountryIso2(ibanInfo.getBankCode(), ibanInfo.getCountry())
        .orElse(null);


⸻

🟢 Recommandation
Si tu n’as pas besoin du JOIN ba.bankInfo bi dans cette requête, la solution 3 est la plus élégante et idiomatique Spring.

Souhaites-tu que je te montre comment adapter cette méthode avec le JOIN et la limite à 1 (si tu veux le garder) ?