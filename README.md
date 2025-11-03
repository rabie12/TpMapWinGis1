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

                // ✅ handle when type is null or when it's a string
                boolean isStringType = "string".equals(p.getType()) || p.getType() == null;

                if (isStringType && Boolean.TRUE.equals(p.getNullable())) {
                    Schema<?> stringSchema = new Schema<>().type("string");
                    Schema<?> nullSchema = new Schema<>().type("null");

                    // Replace type/nullable with oneOf
                    p.setOneOf(List.of(stringSchema, nullSchema));
                    p.setType(null);
                    p.setNullable(null);
                }
            });
        });
    }
}