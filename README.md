Good catch 👍 — the error:

'BusinessRegistryApiException' is abstract; cannot be instantiated

…happens because your BusinessRegistryApiException class is declared as abstract, so you can’t create an instance of it directly (with new BusinessRegistryApiException(...)).

⸻

✅ 2 Ways to Fix It

Option 1: Make it a concrete class

If you only need one general-purpose exception type, simply remove the abstract keyword.

✅ Fixed version:

public class BusinessRegistryApiException extends RuntimeException {
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

✅ Now you can call:

throw new BusinessRegistryApiException(
        HttpStatus.NOT_FOUND,
        "BusinessRegistryApi",
        "Company not found with SIREN " + siren
);


⸻

Option 2: Keep it abstract, but create concrete subclasses

If your design intends to have specific exception types (e.g. CompanyNotFoundException, ExternalApiException), then keep the base abstract and define concrete ones.

Example:

public abstract class BusinessRegistryApiException extends RuntimeException {
    private final HttpStatus status;
    private final String error;

    public BusinessRegistryApiException(HttpStatus status, String error, String message) {
        super(message);
        this.status = status;
        this.error = error;
    }

    public String getError() {
        return "error" + error;
    }

    public HttpStatus getStatus() {
        return status;
    }
}

Then create a subclass:

public class CompanyNotFoundException extends BusinessRegistryApiException {
    public CompanyNotFoundException(String siren) {
        super(HttpStatus.NOT_FOUND, "BusinessRegistryApi", "Company not found with SIREN " + siren);
    }
}

And throw it like:

throw new CompanyNotFoundException(siren);


⸻

✅ Recommended Approach

If you just want one general exception for all registry-related errors → Option 1 (remove abstract) is clean and simple.

If you foresee different causes (e.g., Not Found, Bad Response, Timeout, etc.) and want fine-grained error types → Option 2 with subclasses is more scalable.

⸻

Would you like me to show how the GlobalExceptionHandler would look if you go with Option 2 (with multiple specialized exceptions)?