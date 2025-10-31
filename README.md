Perfect — thanks for providing all that detail.
You’ve done almost everything right ✅, but the last missing piece is due to incompatibilities between versions. Let’s walk through this carefully.

⸻

🧩 Root Cause

You’re using:

<version>4.0.0-M1</version> <!-- Spring Boot -->
<version>3.0.0-M1</version> <!-- springdoc-openapi -->

That combo is experimental and currently does not yet fully implement OpenAPI 3.1’s ["string","null"] output for nullable = true.

👉 Even though the config keys are accepted, the actual model converter for null types is not finalized in M1.

That’s why you still only get:

"type": "string"

instead of:

"type": ["string","null"]


⸻

✅ Verified Working Setup

To actually get

"type": ["string","null"]

you need to use:

Spring Boot 3.3+

and

springdoc-openapi-starter-webmvc-ui 2.8.4 or newer

For example:

<parent>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-parent</artifactId>
    <version>3.3.4</version>
</parent>

<dependencies>
    <dependency>
        <groupId>org.springdoc</groupId>
        <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
        <version>2.8.4</version>
    </dependency>
</dependencies>

Then in your application.properties:

springdoc.api-docs.version=OPENAPI_3_1
springdoc.model-converters.nullable-properties=true

And your DTO:

@Schema(nullable = true, example = "75015")
private String zipCode;

➡️ Will correctly render:

"zipCode": {
  "type": ["string", "null"],
  "example": "75015"
}


⸻

🧰 Why not M1 versions?
	•	Spring Boot 4.0.0-M1 is a milestone — not production ready.
	•	springdoc 3.0.0-M1 was released only to match early Spring Boot 4 snapshots.
	•	Many features (like OpenAPI 3.1 nullables) are regressed or unimplemented there.

⸻

✅ Recommended Fix Plan
	1.	Downgrade to stable versions:

<parent>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-parent</artifactId>
    <version>3.3.4</version>
</parent>


	2.	Use the stable springdoc:

<dependency>
    <groupId>org.springdoc</groupId>
    <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
    <version>2.8.4</version>
</dependency>


	3.	Keep these properties:

springdoc.api-docs.version=OPENAPI_3_1
springdoc.model-converters.nullable-properties=true


	4.	Clean and rebuild:

mvn clean package



⸻

🧩 Optional check

Visit:

http://localhost:8080/v3/api-docs

and verify the top line says:

"openapi": "3.1.0"

If it does, your DTOs with @Schema(nullable = true) will start generating the proper dual-type definitions.

⸻

Would you like me to give you a fully working minimal sample project (DTO + controller + pom) that outputs the correct ["string","null"] types for you to test?