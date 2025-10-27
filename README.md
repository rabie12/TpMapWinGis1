public abstract class BusinessRegistryApiException extends RuntimeException {
    private final HttpStatus status;
    private final String error;
    private static final String errorPrefix = "error";

    public BusinessRegistryApiException(HttpStatus status, String error) {
        this.status = status;
        this.error = error;
    }

    public BusinessRegistryApiException(HttpStatus status, String error, String message) {
        super(message);
        this.status = status;
        this.error = error;
    }

    public String getError() {
        return errorPrefix + error;
    }

    public HttpStatus getStatus() {
        return status;
    }
}
'BusinessRegistryApiException' is abstract; cannot be instantiated

fix it
