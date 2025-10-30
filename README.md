springdoc.api-docs.enabled=true
springdoc.api-docs.path=/v3/api-docs
springdoc.model-converters.nullable-properties=true
springdoc.api-docs.version=3.1.0


Ah — now I understand exactly what’s happening. Thank you for sending the full OpenAPI JSON. The issue is not in your DTO annotations; it’s in how SpringDoc interprets @Schema(nullable = true) with OpenAPI 3.1.

Here’s the analysis and the fix:

⸻

🔹 Problem
	1.	SpringDoc by default outputs OpenAPI 3.0-style schemas even if your OpenAPI JSON says "openapi":"3.1.0".
	2.	For nullable string fields, SpringDoc will not emit "type":["string","null"] unless you explicitly tell it to allow nullable types.
	3.	In your generated JSON, all addressLine1/2/3 still appear as "type": "string" without nullable: true.

⸻

🔹 What works

Option 1: Ensure OpenAPI 3.1 mode with nullable properties

Add these in your application.properties:

springdoc.api-docs.enabled=true
springdoc.api-docs.path=/v3/api-docs
springdoc.model-converters.nullable-properties=true
springdoc.api-docs.version=3.1.0

	•	springdoc.model-converters.nullable-properties=true is key: it tells SpringDoc to emit nullable: true and type: ["string","null"] for fields annotated with @Schema(nullable = true).

⸻

Option 2: Explicit oneOf in @Schema

Your current DTO:

@Schema(
        description = "First address line or null",
        oneOf = {String.class, Object.class},
        example = "23 RUE JEAN DIDIER"
)
private String addressLine1;

	•	oneOf = {String.class, Object.class} does not work reliably.
	•	Instead, use:

@Schema(description = "First address line or null", nullable = true, example = "23 RUE JEAN DIDIER")
private String addressLine1;

	•	Keep nullable = true and remove oneOf. SpringDoc 2.9+ + nullable-properties=true will generate the correct "type":["string","null"].

⸻

Option 3: Programmatic customization (if 2.9.0 + nullable-properties still doesn’t work)

@Bean
public PropertyCustomizer nullableStringCustomizer() {
    return (property, type) -> {
        if ("string".equals(property.getType())) {
            property.setNullable(true);
        }
        return property;
    };
}

	•	This ensures all string fields in your DTOs are marked nullable in OpenAPI.

⸻

🔹 Recommended Fix Steps
	1.	Update application.properties with OpenAPI 3.1 + nullable-properties.
	2.	In your DTOs, remove oneOf and just use nullable = true.
	3.	Restart your Spring Boot app and check /v3/api-docs.
	4.	You should see something like:

"addressLine1": {
  "type": ["string","null"],
  "example": "23 RUE JEAN DIDIER",
  "description": "First address line or null"
}


⸻

If you want, I can write a full corrected AddressDTO + CompanyDTO ready for SpringDoc 2.9.0 with OpenAPI 3.1 that will produce all your address and nested string fields as nullable in Swagger.

Do you want me to do that?