Excellent réflexe d’avoir partagé la stacktrace complète 💪 — le problème que tu rencontres :

java.lang.NoSuchMethodError: 'void org.springframework.web.method.ControllerAdviceBean.<init>(java.lang.Object)'

est un conflit de version entre springdoc-openapi et Spring Boot 3.3+ (et donc Spring Framework 6.1+).

⸻

💥 Cause exacte

springdoc-openapi-starter-webmvc-ui:2.6.0 appelle une ancienne signature du constructeur de
org.springframework.web.method.ControllerAdviceBean qui a été modifiée dans Spring 6.1 / Boot 3.3+.

➡️ Donc la version 2.6.0 n’est plus compatible avec ton projet (tu es en Java 21, donc forcément sur Spring Boot 3.3.x).

⸻

✅ Solution : utiliser la dernière version stable compatible Spring Boot 3.3+

Remplace ta dépendance actuelle :

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
    <version>2.6.0</version>
</dependency>

par la plus récente (par ex. 2.8.4 ou 2.8.8 selon Maven Central — les deux corrigent ce bug).

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
    <version>2.8.8</version>
</dependency>

✅ 2.8.8 est la seule version actuellement compatible avec Spring Boot 3.3+.

⸻

⚙️ Ensuite : pour gérer le nullable

Comme 2.8.x a réintroduit le bug sur nullable, on va contourner en forçant Swagger à marquer certains champs comme nullable manuellement via un customizer.

Ajoute ce bean dans ta configuration :

import io.swagger.v3.oas.models.media.Schema;
import org.springdoc.core.customizers.OpenApiCustomiser;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SwaggerNullableFixConfig {

    @Bean
    public OpenApiCustomiser makeStringsNullable() {
        return openApi -> {
            if (openApi.getComponents() == null || openApi.getComponents().getSchemas() == null) return;

            openApi.getComponents().getSchemas().forEach((name, schema) -> {
                if (schema.getProperties() == null) return;

                schema.getProperties().forEach((propName, propSchema) -> {
                    if (propSchema instanceof Schema<?> property && "string".equals(property.getType())) {
                        property.setNullable(true);
                    }
                });
            });
        };
    }
}

✅ Ce bean passe tous les champs string de tous tes DTOs en nullable dans la spec OpenAPI.
Les clients TypeScript ou React verront donc bien :

"type": "string",
"nullable": true


⸻

🧹 Résumé complet

Étape	Action
🔄	Mets à jour springdoc-openapi-starter-webmvc-ui vers 2.8.8
🧠	Supprime les versions anciennes du jar dans ton repo local (~/.m2/repository/org/springdoc/...)
⚙️	Ajoute la classe SwaggerNullableFixConfig ci-dessus
🔍	Redémarre ton app et vérifie sur http://localhost:8080/v3/api-docs


⸻

Souhaites-tu que je te donne un pom.xml corrigé complet (avec les dépendances cohérentes pour Spring Boot 3.3.3, Java 21, Swagger 2.8.8 et MapStruct 1.6.3) ?
Cela t’éviterait tout conflit à long terme.