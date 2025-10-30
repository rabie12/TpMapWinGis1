To properly enable SpringDoc OpenAPI 3.1 features (including nullable fields handling), you need to update your Maven dependencies. Since you’re currently on springdoc-openapi-starter-webmvc-ui 2.9.0, here’s what I recommend:

⸻

1️⃣ Add/Update SpringDoc OpenAPI Starter

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
    <version>2.9.0</version>
</dependency>

This is the main dependency that includes Swagger UI and OpenAPI JSON generation.

⸻

2️⃣ Optional: SpringDoc OpenAPI Core (if customizing)

If you want advanced customizations (nullable, property customizers):

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-common</artifactId>
    <version>2.9.0</version>
</dependency>

	•	This is usually pulled transitively by the starter, but you can include it explicitly if you plan to use customizers.

⸻

3️⃣ Optional: OpenAPI 3.1 Support

SpringDoc 2.9+ supports OpenAPI 3.1. To make sure nullable strings are generated correctly, configure in application.properties:

springdoc.api-docs.enabled=true
springdoc.api-docs.path=/v3/api-docs
springdoc.model-converters.nullable-properties=true
springdoc.api-docs.version=3.1.0


⸻

✅ That’s all you need for your POM. No other dependency is required for OpenAPI 3.1 + nullable fields.

⸻

If you want, I can write a full working AddressDTO + CompanyDTO with correct Swagger annotations that will show type: ["string","null"] for all string fields. This will fix the issue you still see in your docs.

Do you want me to do that?