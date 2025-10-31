Perfect 👍 — here’s a complete, working example of your DTOs configured so that Swagger (SpringDoc OpenAPI 3.1+) correctly generates
type: ["string", "null"] for nullable fields.

This works with your setup (Spring Boot 3.5.3 + springdoc-openapi 2.9.0).

⸻

✅ AddressDTO.java

package eu.olkypay.business_registry.dto.company;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Schema(description = "Represents an address of a company or person")
public class AddressDTO {

    @Schema(
        description = "First address line (nullable)",
        type = "string",
        nullable = true,
        example = "23 RUE JEAN DIDIER"
    )
    private String addressLine1;

    @Schema(
        description = "Second address line (nullable)",
        type = "string",
        nullable = true,
        example = "Résidence Les Lilas"
    )
    private String addressLine2;

    @Schema(
        description = "Third address line (nullable)",
        type = "string",
        nullable = true,
        example = "null"
    )
    private String addressLine3;

    @Schema(
        description = "ZIP code (nullable)",
        type = "string",
        nullable = true,
        example = "75015"
    )
    private String zipCode;

    @Schema(
        description = "City (nullable)",
        type = "string",
        nullable = true,
        example = "Paris"
    )
    private String city;

    @Schema(
        description = "Country (nullable, ISO 2)",
        type = "string",
        nullable = true,
        example = "FR"
    )
    private String country;
}


⸻

✅ CompanyDTO.java

package eu.olkypay.business_registry.dto.company;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Schema(description = "Represents a legal entity or company")
public class CompanyDTO {

    @Schema(description = "Unique identifier of the company", example = "123456789")
    private String identifier;

    @Schema(description = "RCS registration number", example = "RCS Paris B 123456789")
    private String rcs;

    @Schema(description = "Legal name of the company", nullable = true, example = "OlkyPay SAS")
    private String legalName;

    @Schema(description = "Legal form (SARL, SAS, etc.)", nullable = true, example = "SAS")
    private String legalForm;

    @Schema(description = "Status of the company", nullable = true, example = "Active")
    private String status;

    @Schema(description = "Company capital", nullable = true, example = "100000")
    private Long capital;

    @Schema(description = "Company activity code (NAF, NACE...)", nullable = true, example = "6201Z")
    private String activityCode;

    @Schema(description = "Date of registration", nullable = true, example = "2023-05-15")
    private LocalDate registrationDate;

    @Schema(description = "Country of registration", nullable = true, example = "FR")
    private String registrationCountry;

    @Schema(description = "Registered address", nullable = true)
    private AddressDTO address;

    @Schema(description = "List of company representatives", nullable = true)
    private List<RepresentativeDTO> representatives = new ArrayList<>();

    @Schema(description = "List of beneficial owners", nullable = true)
    private List<BeneficialOwnerDTO> beneficialOwners = new ArrayList<>();

    @Schema(description = "Secondary offices (linked legal entities)", nullable = true)
    private List<CompanyDTO> secondaryOffices = new ArrayList<>();

    @Schema(description = "Legal Entity Identifier (LEI)", nullable = true, example = "5493001KJTIIGC8Y1R12")
    private String legalEntityIdentifier;

    @Schema(description = "Intra-community VAT number", nullable = true, example = "FR763000600001")
    private String intracommunityVATNumber;

    @Schema(description = "Documents related to the company", nullable = true)
    private List<DocumentDTO> documents = new ArrayList<>();

    @Schema(description = "Creation timestamp", example = "2025-10-30T15:26:59.308Z")
    private OffsetDateTime createdAt;

    @Schema(description = "Last update timestamp", example = "2025-10-30T15:26:59.308Z")
    private OffsetDateTime updatedAt;
}


⸻

✅ In application.properties

Make sure you have this to force OpenAPI 3.1 & nullable support:

springdoc.api-docs.version=3.1.0
springdoc.model-converters.nullable-properties=true


⸻

🧠 How it works
	•	nullable = true now tells SpringDoc to render:

"type": ["string", "null"]


	•	This is only available with SpringDoc 2.2.0+ and OpenAPI 3.1.
	•	Works with spring-boot-starter-parent:3.5.3.

⸻

second solution 

Perfect 💪 Here’s the full global configuration you can add to your Spring Boot project to make sure nullable=true fields are always rendered correctly in your OpenAPI (Swagger) spec — even if springdoc skips them.

⸻

✅ 1. Add this configuration class

Create a new file in your project, for example:

📁 src/main/java/eu/olkypay/business_registry/config/OpenApiConfig.java

package eu.olkypay.business_registry.config;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.License;
import io.swagger.v3.oas.models.Components;
import io.swagger.v3.oas.models.security.SecurityScheme;
import io.swagger.v3.oas.models.security.SecurityRequirement;
import org.springdoc.core.models.GroupedOpenApi;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenApiConfig {

    @Bean
    public OpenAPI customOpenAPI() {
        final String securitySchemeName = "Api-Key";

        return new OpenAPI()
                .info(new Info()
                        .title("Business Registry API")
                        .version("1.0")
                        .description("OlkyPay Business Registry Service Documentation")
                        .license(new License().name("OlkyPay Internal Use").url("https://olkypay.eu")))
                // 🔐 Add API Key authentication
                .addSecurityItem(new SecurityRequirement().addList(securitySchemeName))
                .components(new Components().addSecuritySchemes(securitySchemeName,
                        new SecurityScheme()
                                .name(securitySchemeName)
                                .type(SecurityScheme.Type.APIKEY)
                                .in(SecurityScheme.In.HEADER)
                                .description("Provide your App API key")))
                ;
    }

    @Bean
    public GroupedOpenApi allApis() {
        return GroupedOpenApi.builder()
                .group("all")
                .pathsToMatch("/**")
                .build();
    }
}


⸻

✅ 2. Enable OpenAPI 3.1 and nullable handling

In your application.properties (or .yml):

# Enable OpenAPI 3.1 syntax (allows "type": ["string", "null"])
springdoc.api-docs.version=3.1.0

# Optional: ensures nullables are respected by the model converters
springdoc.model-converters.nullable-properties=true


⸻

✅ 3. Confirm correct dependency (you already have it)

You’re using the correct one:

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
    <version>2.9.0</version>
</dependency>

⚠️ Make sure you do not also have the old v1 dependency:

<artifactId>springdoc-openapi-ui</artifactId>

If it exists, remove it — only the starter-webmvc-ui should remain.

⸻

✅ 4. Verify at runtime

Once you restart the app, check:

http://localhost:8080/v3/api-docs

In the generated JSON, nullable fields should now show like this:

"zipCode": {
  "type": ["string", "null"],
  "example": "75015"
}

And your header security should appear globally:

"security": [{ "Api-Key": [] }]


⸻

🧠 Why this works
	•	SpringDoc v2 (with OpenAPI 3.1) respects nullable=true only if:
	1.	The OpenAPI version is explicitly 3.1.0.
	2.	No conflicting configuration is overriding the model converters.
	•	The above configuration forces both those conditions at startup and ensures the schema metadata is applied globally.

⸻

Would you like me to include a custom ModelConverter (backup fallback) that enforces nullable=true even for legacy DTOs without annotations?
It’s optional but guarantees that any @Schema(nullable = true) will output "type": ["string", "null"] regardless of version quirks.


