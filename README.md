Merci — super clair. On va diagnostiquer proprement et te donner des solutions concrètes. Tu as fait ce qu’il fallait côté @Schema, mais il y a 3 causes fréquentes quand la doc OpenAPI n’affiche pas nullable/oneOf comme attendu :
	1.	Le JSON OpenAPI généré n’inclut pas l’annotation (donc c’est un problème de découverte / version).
	2.	Swagger-UI affiche différemment (il peut masquer nullable: true visuellement).
	3.	Tu utilises une mauvaise version de springdoc / swagger-core ou Spring Boot incompatible.

Je te propose une démarche de vérification + 3 solutions (diagnostic → correctifs rapides → solution globale).

⸻

1) Diagnostic rapide — regarde le JSON OpenAPI brut

Ouvre dans ton navigateur :

http://localhost:8080/v3/api-docs

(ou /v3/api-docs.yaml si tu préfères)

Recherche le champ addressLine1 (Ctrl/Cmd+F).
Tu dois voir soit nullable: true soit oneOf pour ce champ. Exemples attendus :

a) avec nullable :

"addressLine1": {
  "type": "string",
  "nullable": true,
  "example": "23 RUE JEAN DIDIER"
}

b) ou avec oneOf :

"addressLine1": {
  "oneOf": [
    { "type": "string" },
    { "type": "null" }
  ],
  "example": "23 RUE JEAN DIDIER"
}

→ Dis-moi si tu trouves nullable/oneOf dans ce JSON ou non. C’est la source de vérité : si c’est présent dans /v3/api-docs alors le problème est uniquement l’affichage dans Swagger-UI.

⸻

2) Si /v3/api-docs N’affiche pas nullable / oneOf pour tes champs

Ca veut dire que tes @Schema ne sont pas pris en compte par le générateur. Vérifie :
	•	Que tu utilises springdoc-openapi (pas springfox). Dans pom.xml tu dois avoir pour Spring Boot 2.x :
org.springdoc:springdoc-openapi-ui:1.6.x
ou pour Spring Boot 3 :
org.springdoc:springdoc-openapi-starter-webmvc-ui:2.x.
	•	Vérifie la version effective :

mvn dependency:tree | grep springdoc

Dis-moi la version si tu veux que je vérifie la compatibilité.

	•	Vérifie que CompanyDTO / AddressDTO apparaissent bien dans la spec (tes endpoints utilisent CompanyDTO dans la signature du contrôleur) — tu l’as fait, donc normalement oui.

Si la dépendance est OK mais toujours rien, essaye de remplacer oneOf par type + nullable dans @Schema (plus fiable) :

@Schema(type = "string", nullable = true, example = "23 RUE JEAN DIDIER")
private String addressLine1;


⸻

3) Si /v3/api-docs contient nullable mais Swagger UI n’affiche pas comme tu veux

C’est juste un problème d’affichage UI : Swagger UI peut montrer type: string et une note “nullable” ailleurs. Le client TypeScript (openapi-generator) lira par contre nullable: true correctement. Donc vérifie le fichier /v3/api-docs — c’est ça qui compte pour les SDK.

⸻

4) Solution programmatique globale (si tu veux forcer tous les string en nullable)

Si tu veux garantir que toutes les propriétés type: string deviennent nullable: true (pratique en dev) — ajoute un OpenApiCustomiser bean :

import org.springdoc.core.customizers.OpenApiCustomiser;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import io.swagger.v3.oas.models.media.Schema;

@Configuration
public class OpenApiConfig {

    @Bean
    public OpenApiCustomiser nullableStringsCustomizer() {
        return openApi -> {
            if (openApi.getComponents() == null || openApi.getComponents().getSchemas() == null) return;
            openApi.getComponents().getSchemas().forEach((name, schema) -> {
                if (schema.getProperties() == null) return;
                schema.getProperties().forEach((propName, propSchema) -> {
                    if (propSchema instanceof Schema) {
                        Schema<?> s = (Schema<?>) propSchema;
                        if ("string".equals(s.getType()) && Boolean.FALSE.equals(s.getNullable())) {
                            s.setNullable(true);
                        }
                    }
                });
            });
        };
    }
}

C’est puissant — utilisable en dev pour garantir l’API doc correspond à string | null. (Ne pas forcer en prod sans concertation.)

⸻

5) Exemple concret de DTO (garanti compatible)

Utilise type = "string", nullable = true (plutôt que oneOf) — c’est le plus simple et fiable :

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AddressDTO {

    @Schema(type = "string", nullable = true, example = "23 RUE JEAN DIDIER")
    private String addressLine1;

    @Schema(type = "string", nullable = true, example = "LUXEMBOURG")
    private String addressLine2;

    @Schema(type = "string", nullable = true, example = "BAT A")
    private String addressLine3;

    @Schema(type = "string", nullable = true, example = "75000")
    private String zipCode;

    @Schema(type = "string", nullable = true, example = "Paris")
    private String city;

    @Schema(type = "string", nullable = true, example = "FR")
    private String country;
}

Et CompanyDTO idem (tu as déjà fait ça).

⸻

6) Checklist rapide à exécuter maintenant
	1.	Ouvre http://localhost:8080/v3/api-docs et recherche addressLine1. Dis-moi ce que tu trouves.
	2.	Si absent : indique la version de springdoc dans ton pom.xml.
	3.	Si présent mais Swagger UI n’affiche pas comme tu veux → OK, la spec est correcte, tu peux générer un client TypeScript.
	4.	En dev, si tu veux forcer globalement : colle le OpenApiCustomiser ci-dessus.

⸻

Donne-moi le résultat de la recherche dans /v3/api-docs (copie-colle le fragment JSON pour address ou dis la version springdoc), je te dis exactement quel correctif appliquer au code ou à la configuration.