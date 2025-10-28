Parfait 👏 — tu veux donc des exemples réels ou réalistes de numéros d’identification d’entreprises (SIREN, RCS, Company Number, UID) pour tester tes APIs pour les 4 pays : 🇫🇷 France, 🇱🇺 Luxembourg, 🇬🇧 Royaume-Uni et 🇨🇭 Suisse.

Voici un tableau complet et prêt à l’emploi 👇

⸻

✅ Exemples de numéros d’entreprises valides pour tests API

Pays	Type d’identifiant	Exemple valide	Exemple invalide	Remarques
🇫🇷 France (FR)	SIREN	552100554	55210055 (8 chiffres)	9 chiffres uniquement. Ex : 552100554 = L’Oréal.
🇫🇷 France (FR)	SIRET (SIREN + NIC)	55210055400013	5521005540001	Facultatif si tu veux tester le niveau établissement.
🇱🇺 Luxembourg (LU)	RCS Luxembourg	B123456	123456	Format : une lettre (A ou B) suivie de 3–6 chiffres.
🇬🇧 Royaume-Uni (GB)	Company Number	01234567	1234567A (mauvais format)	8 caractères. Ex : 01234567 = test Companies House.
🇬🇧 Royaume-Uni (GB)	Company Number (écossais)	SC123456	SC12345	Certaines régions ont un préfixe (SC, NI, OC etc.).
🇨🇭 Suisse (CH)	UID (CHE)	CHE-105.688.587	CHE105688587	Format officiel : CHE-XXX.XXX.XXX. Ex : Nestlé = CHE-105.909.215.


⸻

💡 Astuce pour tes tests API

Tu peux créer un petit JSON de test par pays, par exemple :

🇫🇷 France

{
  "country": "FR",
  "identifier": "552100554"
}

🇱🇺 Luxembourg

{
  "country": "LU",
  "identifier": "B123456"
}

🇬🇧 Royaume-Uni

{
  "country": "GB",
  "identifier": "SC123456"
}

🇨🇭 Suisse

{
  "country": "CH",
  "identifier": "CHE-105.688.587"
}


⸻

🧩 Bonus : expressions régulières simples pour valider

Si tu veux renforcer ton validateur RegexCompanyNumber.companyNumberRegexMatch() :

switch (country) {
    case "FR": return siren.matches("^[0-9]{9}$");
    case "LU": return siren.matches("^[A-Z]{1}[0-9]{3,6}$");
    case "GB": return siren.matches("^[A-Z0-9]{6,8}$");
    case "CH": return siren.matches("^CHE-[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}$");
    default: return false;
}


⸻

Souhaites-tu que je te génère aussi un jeu de données JSON complet (plusieurs sociétés par pays) pour tester des appels batch (par ex. /companies/import ou /companies/search)?