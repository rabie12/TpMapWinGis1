Excellent — you’ve identified the real issue: json can be null when the WebClient request returns nothing (for example, a 404 or other client error). The exception happens because .contains() is called on null.

Let’s make this robust and return a meaningful, consistent error (like “Company not found”) that will be handled cleanly by your GlobalExceptionHandler.

⸻

✅ Goal

If json (or any response) is null, throw a custom exception (BusinessRegistryApiException or similar) with a clear message and appropriate HTTP status (404 Not Found).

⸻

💡 Step-by-step solution

1. Update your getCompany() method

Here’s an improved version of your code with proper null handling, logging, and exception throwing:

public CompanyDTO getCompany(Connector connector, String siren) throws JsonProcessingException {
    log.info("Searching company {} with {}", siren, connector.getServiceName());
    WebClient webClient = createWebClient(connector);

    String json = webClient.get()
            .uri("/company/{siren}", siren)
            .retrieve()
            .onStatus(HttpStatusCode::is4xxClientError, clientResponse -> {
                log.warn("Company {} not found in {}", siren, connector.getServiceName());
                return Mono.error(new BusinessRegistryApiException(
                        "Company not found with SIREN " + siren,
                        "BusinessRegistryApi",
                        HttpStatus.NOT_FOUND
                ));
            })
            .bodyToMono(String.class)
            .block();

    if (json == null || json.isEmpty()) {
        throw new BusinessRegistryApiException(
                "Empty response from business registry for SIREN " + siren,
                "BusinessRegistryApi",
                HttpStatus.NOT_FOUND
        );
    }

    String jsonOfficers = webClient.get()
            .uri("/company/{siren}/officers", siren)
            .retrieve()
            .onStatus(HttpStatusCode::is4xxClientError, clientResponse -> Mono.empty())
            .bodyToMono(String.class)
            .block();

    String jsonBO = webClient.get()
            .uri("/company/{siren}/persons-with-significant-control", siren)
            .retrieve()
            .onStatus(HttpStatusCode::is4xxClientError, clientResponse -> Mono.empty())
            .bodyToMono(String.class)
            .block();

    if (!json.contains("\"message\":\"4")) {
        return findInformationCompany(json, jsonOfficers, jsonBO);
    }

    log.warn("Error with the API call of Companies House: {}", json);
    throw new BusinessRegistryApiException(
            "Error with Companies House API response for " + siren,
            "CompaniesHouseAPI",
            HttpStatus.BAD_GATEWAY
    );
}


⸻

2. Define your custom exception class

If you haven’t already, make sure your BusinessRegistryApiException looks like this:

public class BusinessRegistryApiException extends RuntimeException {

    private final String error;
    private final HttpStatus status;

    public BusinessRegistryApiException(String message, String error, HttpStatus status) {
        super(message);
        this.error = error;
        this.status = status;
    }

    public String getError() {
        return error;
    }

    public HttpStatus getStatus() {
        return status;
    }
}


⸻

3. Make sure your GlobalExceptionHandler maps it clearly

You already have this handler:

@ExceptionHandler(BusinessRegistryApiException.class)
public ResponseEntity<ErrorEntity> handleTradingApiException(BusinessRegistryApiException exception) {
    logger.error("Entity not found or registry API error: {}", exception.getMessage());

    ErrorEntity error = new ErrorEntity(
            LocalDateTime.now(),
            exception.getMessage(),
            exception.getError(),
            exception.getStatus().value()
    );

    return new ResponseEntity<>(error, exception.getStatus());
}


⸻

✅ 4. Example output

If the company is not found, the API will now return a clean, readable JSON:

{
  "timeStamp": "2025-10-27T09:38:30.5457431",
  "message": "Company not found with SIREN 123456789",
  "errorAuthor": "BusinessRegistryApi",
  "httpStatus": 404
}


⸻

⚙️ Optional — add centralized response checking

If you have several similar methods, extract a helper to avoid repeating null checks:

private String getJsonResponse(WebClient webClient, String uriTemplate, String siren, String serviceName) {
    String response = webClient.get()
            .uri(uriTemplate, siren)
            .retrieve()
            .onStatus(HttpStatusCode::is4xxClientError, clientResponse -> Mono.error(
                    new BusinessRegistryApiException(
                            "Company not found with SIREN " + siren,
                            serviceName,
                            HttpStatus.NOT_FOUND
                    )))
            .bodyToMono(String.class)
            .block();

    if (response == null || response.isEmpty()) {
        throw new BusinessRegistryApiException(
                "Empty response from " + serviceName + " for SIREN " + siren,
                serviceName,
                HttpStatus.BAD_GATEWAY
        );
    }

    return response;
}

Then simply call:

String json = getJsonResponse(webClient, "/company/{siren}", siren, connector.getServiceName());
String jsonOfficers = getJsonResponse(webClient, "/company/{siren}/officers", siren, connector.getServiceName());


⸻

Would you like me to refactor your full method with this helper included (cleaner and reusable)?