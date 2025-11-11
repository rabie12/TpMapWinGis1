Got it ✅ — you want to validate that all fields in your SctDebitData class are mandatory before your /generate API executes the PDF generation logic.

Here’s how to do that cleanly in Spring Boot, using Jakarta Bean Validation (formerly javax validation).

⸻

✅ Step 1: Add validation annotations to your model

import jakarta.validation.constraints.*;
import java.math.BigDecimal;

public class SctDebitData {

    @NotNull(message = "orderId is mandatory")
    private Integer orderId;

    @NotBlank(message = "txId is mandatory")
    private String txId;

    @NotBlank(message = "executionDate is mandatory")
    private String executionDate;

    @NotBlank(message = "endToEndId is mandatory")
    private String endToEndId;

    @NotBlank(message = "outName is mandatory")
    private String outName;

    @NotBlank(message = "outAddress is mandatory")
    private String outAddress;

    @NotBlank(message = "outIban is mandatory")
    private String outIban;

    @NotBlank(message = "supplierName is mandatory")
    private String supplierName;

    @NotBlank(message = "supplierIban is mandatory")
    private String supplierIban;

    @NotBlank(message = "controlKey is mandatory")
    private String controlKey;

    @NotNull(message = "amount is mandatory")
    @DecimalMin(value = "0.01", message = "amount must be greater than 0")
    private BigDecimal amount;

    @NotBlank(message = "label is mandatory")
    private String label;

    // Getters and Setters
}


⸻

✅ Step 2: Enforce validation in your controller

You simply need to add the @Valid annotation and optionally @Validated at the class level (if not already used).

import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/sct-debit")
@Validated
public class SctDebitController {

    private final SctDebitPdfWriter _sctDebitPdfWriter;
    private static final Logger LOGGER = LoggerFactory.getLogger(SctDebitController.class);

    public SctDebitController(SctDebitPdfWriter sctDebitPdfWriter) {
        this._sctDebitPdfWriter = sctDebitPdfWriter;
    }

    @PostMapping(value = "/generate", produces = MediaType.APPLICATION_PDF_VALUE)
    public ResponseEntity<byte[]> generate(@Valid @RequestBody SctDebitData data) throws Exception {
        byte[] pdf = _sctDebitPdfWriter.generatePdf(data);

        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_PDF);

        String filename = String.format("Avis-debit-%d.pdf", data.getOrderId());
        headers.setContentDispositionFormData(filename, filename);
        headers.setCacheControl("must-revalidate, post-check=0, pre-check=0");

        LOGGER.info("PDF generated successfully for order <{}>", data.getOrderId());
        return new ResponseEntity<>(pdf, headers, HttpStatus.OK);
    }
}


⸻

✅ Step 3: (Optional but recommended) Add a global exception handler for validation errors

This ensures clients get a clear, structured JSON response when fields are missing.

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import java.util.HashMap;
import java.util.Map;

@RestControllerAdvice
public class ValidationExceptionHandler {

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<Map<String, String>> handleValidationExceptions(MethodArgumentNotValidException ex) {
        Map<String, String> errors = new HashMap<>();
        ex.getBindingResult().getFieldErrors().forEach(error ->
            errors.put(error.getField(), error.getDefaultMessage())
        );
        return new ResponseEntity<>(errors, HttpStatus.BAD_REQUEST);
    }
}


⸻

🧪 Example invalid request → Response

POST /sct-debit/generate

{
  "orderId": null,
  "txId": "",
  "amount": 0
}

Response (HTTP 400):

{
  "orderId": "orderId is mandatory",
  "txId": "txId is mandatory",
  "executionDate": "executionDate is mandatory",
  "endToEndId": "endToEndId is mandatory",
  "outName": "outName is mandatory",
  "outAddress": "outAddress is mandatory",
  "outIban": "outIban is mandatory",
  "supplierName": "supplierName is mandatory",
  "supplierIban": "supplierIban is mandatory",
  "controlKey": "controlKey is mandatory",
  "amount": "amount must be greater than 0",
  "label": "label is mandatory"
}


⸻

Would you like me to include an example of a unit test (MockMvc) that checks the validation behavior of this endpoint?