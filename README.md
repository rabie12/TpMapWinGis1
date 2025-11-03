    @Override
    public void customise(OpenAPI openApi) {
        if (openApi == null || openApi.getComponents() == null) return;
        Map<String, Schema> schemas = openApi.getComponents().getSchemas();
        if (schemas == null) return;
        schemas.values().forEach(schema -> {
            if (schema.getProperties() == null) return;
            schema.getProperties().forEach((name, prop) -> {
                if (!(prop instanceof Schema<?> p)) return;
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
                    p.setNullable(true);
                }
            });
        });
    }


    can you adjust it here according to what you ve sent on the last one
