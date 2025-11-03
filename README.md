Perfect — let’s make Swagger UI actually show that your fields can be "null" ✅

Since Swagger UI does not render oneOf: [string, null] properly, the right approach is to use nullable: true (OpenAPI 3.0+ supported and Swagger UI recognizes it visually).

⸻

✅ Option 1 – Use nullable: true instead of oneOf

Change your OpenApiNullablePostProcessor logic like this:

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

                // Only apply to strings
                if ("string".equals(p.getType())) {
                    p.setNullable(true);      // ✅ Swagger UI shows this clearly
                }
            });
        });
    }
}

🔍 Result:
Swagger UI will now render it like:

legalName:
  type: string
  nullable: true

and when you open the example body, Swagger UI will display:

"legalName": "string"

and also mark that it can be null (tooltip + schema description).

⸻

🧭 Option 2 – Keep oneOf and help Swagger UI visualize it

If you must keep oneOf (for strict OpenAPI 3.1 semantics), Swagger UI won’t show null in the example —
but you can help users by adding an example:

if ("string".equals(p.getType()) || p.getType() == null) {
    p.setType("string");
    p.setExample("string or null");  // 👁 visible in UI
    p.setNullable(true);             // ✅ UI highlights nullable
}

This gives you both machine-readable and visually helpful behavior.

⸻

✅ Recommended final setup

If your goal is for developers to see in Swagger UI that a field may be null:

p.setNullable(true);

is the correct and supported way.
Swagger UI clearly labels such fields as “nullable” in both the schema and the sample.

⸻

Would you like me to show you what the Swagger UI output will look like (screenshot-style example of the new section)?