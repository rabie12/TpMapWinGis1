Bien sûr 👍 voici le lien complet officiel vers la documentation SpringDoc OpenAPI 3 (v3.0 et suivantes) :

👉 📘 Documentation officielle :
🔗 https://springdoc.org/v2

(Oui, même pour la version 3.x de Spring Boot, le site reste /v2, car il correspond à la génération “Spring Boot 3 compatible”).

⸻

🔹 Points clés de la doc :

🧩 Dépendance Maven (Spring Boot 3.x compatible)

<dependency>
  <groupId>org.springdoc</groupId>
  <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
  <version>2.8.8</version>
</dependency>

🧭 URL par défauts :

Description	URL
OpenAPI JSON	http://localhost:8080/v3/api-docs
Swagger UI	http://localhost:8080/swagger-ui/index.html


⸻

⚙️ Exemple de config basique compatible Spring Boot 3.3.x :

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springdoc.core.models.GroupedOpenApi;

@Configuration
public class SwaggerConfig {

    @Bean
    public GroupedOpenApi publicApi() {
        return GroupedOpenApi.builder()
                .group("business-registry")
                .pathsToMatch("/api/**")
                .build();
    }
}


⸻

🪄 Pour activer le support nullable (optionnel)

Tu peux ajouter ton customizer (comme discuté avant) :

import io.swagger.v3.oas.models.media.Schema;
import org.springdoc.core.customizers.OpenApiCustomizer;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SwaggerNullableFixConfig {

    @Bean
    public OpenApiCustomizer makeStringsNullable() {
        return openApi -> {
            if (openApi.getComponents() == null || openApi.getComponents().getSchemas() == null)
                return;

            openApi.getComponents().getSchemas().forEach((name, schema) -> {
                if (schema.getProperties() != null) {
                    schema.getProperties().forEach((propName, propSchema) -> {
                        if (propSchema instanceof Schema<?> property) {
                            if ("string".equals(property.getType())) {
                                property.setNullable(true);
                            }
                        }
                    });
                }
            });
        };
    }
}


⸻

💡 Bonus : Si tu veux activer la doc OpenAPI uniquement en dev :

springdoc:
  api-docs:
    enabled: true
  swagger-ui:
    enabled: true

et tu peux désactiver en prod :

springdoc:
  api-docs:
    enabled: false
  swagger-ui:
    enabled: false


⸻

Souhaites-tu que je te donne un exemple complet fonctionnel (pom.xml + config + endpoint) pour ton projet actuel (avec Spring Boot 3.3 + Swagger 2.8.8 + nullable fix) ?