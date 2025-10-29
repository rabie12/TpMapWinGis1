Très bonne question 👏
Oui — tu as tout à fait raison : il y a une différence de comportement entre Swagger/OpenAPI côté Java (Spring) et côté TypeScript (ou générateur client) concernant la nullabilité des champs String.

Voici ce qui se passe en détail 👇

⸻

🧩 1. En Java (Spring Boot + Swagger/OpenAPI)

Quand tu écris :

private String name;

➡️ Par défaut, Swagger considère ce champ comme “nullable = false” et “required = false”.
Autrement dit :
	•	Tu peux omettre le champ dans le JSON (non requis)
	•	Mais si tu l’envoies avec null, certains validateurs ne le traitent pas comme une valeur valide
(et le mapping ou la validation @NotNull peut échouer).

Exemple :

@Schema(description = "Company name")
private String name;

→ Swagger générera quelque chose comme :

name:
  type: string

Mais sans indiquer qu’il accepte null.

⸻

🧩 2. En TypeScript (client généré depuis OpenAPI)

Les générateurs TypeScript (comme openapi-generator) interprètent souvent :
	•	type: string sans nullable → name?: string
	•	type: string, nullable: true → name?: string | null

Donc, en TypeScript, si ton OpenAPI ne marque pas explicitement un champ comme nullable,
il sera seulement optionnel (undefined), pas null.

⸻

🧩 3. Dans ton cas concret

Tu as reçu :

{
  "message": "[Invalid Name: Empty name, Invalid Name: Name is NULL]"
}

➡️ Cela montre que ton backend Java rejette à la fois :
	•	un name vide ("")
	•	et un name null

👉 Donc dans ton modèle Java ou ta validation (@NotNull, @NotBlank, ou @Size), tu imposes une contrainte stricte.
Swagger, lui, n’indique pas nullable: true, donc le client TypeScript ne sait pas qu’il ne doit pas envoyer null.

⸻

✅ Solution propre

Si tu veux que Swagger/OpenAPI reflète correctement la nullabilité du champ :

@Schema(nullable = true, description = "Company name (can be null)")
private String name;

Et si le champ doit être obligatoire :

@Schema(nullable = false, required = true, description = "Company name (required)")
@NotBlank
private String name;

Cela générera un schéma plus fidèle pour ton client TypeScript.

⸻

💡 En résumé

Langage / Outil	String par défaut	Null explicite accepté ?
Java (Spring)	nullable en théorie, mais Swagger ne le marque pas	❌ (sauf @Schema(nullable = true))
TypeScript (OpenAPI)	`string	undefined`
Hibernate Validation	dépend de @NotNull, @NotBlank	❌ si contrainte présente


⸻

Souhaites-tu que je te montre comment ajuster ton modèle Alert ou LegalEntity pour que Swagger et TypeScript aient le même comportement vis-à-vis des champs nullable ?