Thanks for sharing your pom.xml. From a SpringDoc/OpenAPI perspective, your dependencies look mostly correct, but there are a few points to check/fix regarding your nullable string issue and general SpringDoc integration. Here’s a detailed analysis:

⸻

1️⃣ SpringDoc Version
	•	You upgraded to 2.9.0 which is good — it handles nullable = true better than 2.8.x.
	•	Make sure you don’t have an older SpringDoc jar somewhere in your classpath (2.6.0 appeared in your error logs earlier). That could cause NoSuchMethodError.

✅ Action: Run mvn dependency:tree | grep springdoc to confirm only 2.9.0 is used.

⸻

2️⃣ Nullable Strings
	•	Using @Schema(nullable = true, example="...") is correct.
	•	SpringDoc 2.x doesn’t generate "type":["string","null"] unless you either:
	1.	Use nullable = true and OpenAPI 3.1 mode.
	2.	Or use a PropertyCustomizer (as I suggested earlier) to mark all strings nullable.

@Bean
public PropertyCustomizer nullableStringCustomizer() {
    return (property, type) -> {
        if ("string".equals(property.getType())) {
            property.setNullable(true);
        }
        return property;
    };
}


⸻

3️⃣ OpenAPI 3.1 Mode

SpringDoc defaults to OpenAPI 3.0. If you want proper "string | null" arrays, you should enable OpenAPI 3.1:

springdoc.api-docs.version=3.1.0
springdoc.api-docs.enabled=true
springdoc.api-docs.path=/v3/api-docs
springdoc.model-converters.nullable-properties=true

	•	The last property (nullable-properties) is key for generating "string | null" in the schema.

⸻

4️⃣ Check Swagger UI
	•	The JSON in /v3/api-docs should now show your string fields as:

"addressLine1": {
  "type": "string",
  "nullable": true,
  "example": "23 RUE JEAN DIDIER"
}

	•	If Swagger UI still shows just "string", the nullable flag is being ignored, probably because the OpenAPI spec version is still 3.0 in your config.

⸻

5️⃣ Misc Notes on Your pom.xml
	•	Dependencies for Spring Boot 3.5.3, Java 21, Lombok, MapStruct are fine.
	•	You have both spring-boot-starter-web and spring-boot-starter-webflux. Unless you are using reactive endpoints, you can remove WebFlux to reduce complexity.
	•	Liquibase and MariaDB dependencies are okay.
	•	Make sure your annotation processors (lombok + mapstruct) are working correctly.

⸻

✅ Summary of actions to fix nullable strings:
	1.	Confirm SpringDoc 2.9.0 is the only version in your project (dependency:tree).
	2.	Add a PropertyCustomizer bean to force nullable strings.
	3.	Optional: Enable OpenAPI 3.1 mode with springdoc.model-converters.nullable-properties=true.
	4.	Rebuild and restart your app, check /v3/api-docs.

⸻

If you want, I can provide a full working example of CompanyDTO + AddressDTO with SpringDoc 2.9.0 that correctly outputs all nullable strings as "string | null" in Swagger.

Do you want me to do that?