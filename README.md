    public CompanyDTO getCompany(Connector connector, String siren) throws JsonProcessingException {
        log.info(" Search company {} with {}", siren, connector.getServiceName());
        WebClient webClient = createWebClient(connector);

        String json = webClient.get()
                .uri("/company/{siren}", siren)
                .retrieve()
                .onStatus(HttpStatusCode::is4xxClientError, clientResponse -> Mono.empty())
                .bodyToMono(String.class)
                .block();

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
        log.warn("Error with the API call of Companies House {}", json);
        return new CompanyDTO();

    }

	when json is null i have this exception :
	{
  "timeStamp": "2025-10-27T09:38:30.5457431",
  "message": "Cannot invoke \"String.contains(java.lang.CharSequence)\" because \"json\" is null",
  "httpStatus": 400
}

i want to return clear exception compony not found or something similar, i ahve alkready implement this globalException handler annotated : public class GlobalExceptionHandler {
    private static final Logger logger = LoggerFactory.getLogger(GlobalExceptionHandler.class);

    @ExceptionHandler(BusinessRegistryApiException.class)
    public ResponseEntity<ErrorEntity> handleTradingApiException(BusinessRegistryApiException exception) {
        logger.error("Entity Not found");
//        ErrorEntity error = ErrorEntity.builder()
//                .timeStamp(LocalDateTime.now())
//                .message(exception.getMessage())
//                .errorAuthor(exception.getError())
//                .httpStatus(exception.getStatus().value())
//                .build();
        ErrorEntity error = new ErrorEntity(
                LocalDateTime.now(),
                exception.getMessage(),
                exception.getError(),
                exception.getStatus().value()
        );
        return new ResponseEntity<>(error, exception.getStatus());
    }

    @ExceptionHandler(BadRequestException.class)
    public ResponseEntity<ErrorEntity> badRequestHandler(BadRequestException exception) {

        ErrorEntity error = ErrorEntity.builder()
                .timeStamp(LocalDateTime.now())
                .message(exception.getMessage())
                .httpStatus(HttpStatus.BAD_REQUEST.value())
                .build();

        return ResponseEntity.status(HttpStatus.NOT_FOUND.value()).body(error);
    }

    @ExceptionHandler(RuntimeException.class)
    public ResponseEntity<ErrorEntity> runtimeExceptionHandler(RuntimeException exception) {

        ErrorEntity error = ErrorEntity.builder()
                .timeStamp(LocalDateTime.now())
                .message(exception.getMessage())
                .httpStatus(HttpStatus.FORBIDDEN.value())
                .build();

        log.info("{}", exception.getMessage());
        return ResponseEntity.status(HttpStatus.FORBIDDEN.value()).body(error);
    }

}


give me some proposition proposition to handle correclty exception and disply meaninfull message
	
