Perfect — this is a very precise diagnosis. 👏
You’re 100 % right: because getType() is null for many fields (especially for nested $ref types), your customizer ends up applying the transformation everywhere, even where it shouldn’t.

Let’s fix it cleanly.

⸻

🧠 Goal

You only want to modify nullable string fields,
so that in your OpenAPI JSON you see:

"zipCode": {
  "type": ["string", "null"],
  "example": "75015"
}

instead of

"zipCode": {
  "type": "string"
}


⸻

🧩 Why the current version fails

Your code:

boolean isStringType = "string".equals(p.getType()) || p.getType() == null;

Because many nested schemas or $ref-based properties have p.getType() == null,
this condition becomes true for everything,
and you end up transforming every property, including integers and objects.

⸻

✅ Corrected OpenApiNullablePostProcessor

Here’s a refined version that works correctly and selectively:

package eu.olkypay.business_registry.configuration;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.media.Schema;
import org.springdoc.core.customizers.OpenApiCustomizer;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.Map;

@Component
public class OpenApiNullablePostProcessor implements OpenApiCustomizer {

    @Override
    public void customise(OpenAPI openApi) {
        if (openApi == null || openApi.getComponents() == null) return;
        Map<String, Schema> schemas = openApi.getComponents().getSchemas();
        if (schemas == null) return;

        schemas.values().forEach(schema -> {
            if (schema.getProperties() == null) return;

            schema.getProperties().forEach((name, prop) -> {
                if (!(prop instanceof Schema<?> p)) return;

                // ✅ Only apply to actual string types, not arrays or $ref objects
                boolean isStringType = "string".equals(p.getType());
                boolean isNullable =
                        Boolean.TRUE.equals(p.getNullable())
                                || (p.getDescription() != null && p.getDescription().toLowerCase().contains("nullable"))
                                || "null".equals(String.valueOf(p.getExample()));

                if (isStringType && isNullable) {
                    Schema<?> stringSchema = new Schema<>().type("string");
                    Schema<?> nullSchema = new Schema<>().type("null");
                    p.setOneOf(List.of(stringSchema, nullSchema));
                    p.setType(null);
                    p.setNullable(null);
                }
            });
        });
    }
}


⸻

🧱 Example usage

DTO:

public class Address {

    @Schema(description = "First line of the address")
    private String addressLine1;

    @Schema(description = "Second line (nullable)", nullable = true, example = "null")
    private String addressLine2;

    @Schema(description = "ZIP code (nullable)", nullable = true)
    private String zipCode;

    @Schema(description = "City")
    private String city;
}

Generated Swagger (OpenAPI 3.1):

"zipCode": {
  "oneOf": [
    { "type": "string" },
    { "type": "null" }
  ]
},
"city": {
  "type": "string"
}


⸻

🧩 Optional improvement (less intrusive)

If you still see too many fields transformed,
you can further restrict it to only fields explicitly annotated with nullable = true by using the model name:

if (isStringType && Boolean.TRUE.equals(p.getNullable())) {
    ...
}

and remove all heuristic checks (description, example).

That will produce the cleanest output.

⸻

✅ Summary

Problem	Cause	Fixed by
Every field turned into oneOf	p.getType() == null matched all fields	Restrict to p.getType().equals("string")
Nullable ignored	nullable not set by SpringDoc	Added fallback checks (description, example)
Desired output missing		Rewrites only nullable strings to oneOf: [string, null]


⸻

Would you like me to show a version that also supports nullable integers and booleans (not just strings)?