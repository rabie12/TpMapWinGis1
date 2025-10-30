String apiKey = request.getHeader("Api-Key");
if (apiKey == null) {
    apiKey = request.getHeader("api-key"); // fallback
}