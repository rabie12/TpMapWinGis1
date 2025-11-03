    @Override
    public void customise(OpenAPI openApi) {
        if (openApi == null || openApi.getComponents() == null) return;
        Map<String, Schema> schemas = openApi.getComponents().getSchemas();
        if (schemas == null) return;
        schemas.values().forEach(schema -> {
            if (schema.getProperties() == null) return;
            schema.getProperties().forEach((k, v) -> {
                if (!(v instanceof Schema)) return;
                Schema<?> p = (Schema<?>) v;
                if ("string".equals(p.getType()) && Boolean.TRUE.equals(p.getNullable())) {
                    Schema<?> s1 = new Schema<>().type("string");
                    Schema<?> s2 = new Schema<>().type("null");
                    p.setOneOf(List.of(s1, s2));
                    p.setType(null);
                    p.setNullable(null);
                }
            });
        });
    }
}

class JsonSchema {
    class Schema {
        type: [string]
        format: null
        $ref: null
        description: null
        title: null
        multipleOf: null
        maximum: null
        exclusiveMaximum: null
        minimum: null
        exclusiveMinimum: null
        maxLength: null
        minLength: null
        pattern: null
        maxItems: null
        minItems: null
        uniqueItems: null
        maxProperties: null
        minProperties: null
        required: null
        not: null
        properties: null
        additionalProperties: null
        nullable: null
        readOnly: null
        writeOnly: null
        example: null
        externalDocs: null
        deprecated: null
        discriminator: null
        xml: null
        patternProperties: null
        contains: null
        $id: null
        $anchor: null
        $schema: null
        $vocabulary: null
        $dynamicAnchor: null
        const: null
        contentEncoding: null
        contentMediaType: null
        contentSchema: null
        propertyNames: null
        unevaluatedProperties: null
        maxContains: null
        minContains: null
        additionalItems: null
        unevaluatedItems: null
        _if: null
        _else: null
        then: null
        dependentRequired: null
        dependentSchemas: null
        $comment: null
        prefixItems: null
    }
}


when p.getType() its null and it never change the value of schema on my swagger can i review pleacs
