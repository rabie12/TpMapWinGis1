import io.swagger.v3.oas.annotations.media.Schema;

public class AddressDTO {

    @Schema(
        description = "First address line or null",
        oneOf = {String.class, Object.class},
        example = "23 RUE JEAN DIDIER"
    )
    private String addressLine1;

    @Schema(
        description = "Second address line or null",
        oneOf = {String.class, Object.class},
        example = "LUXEMBOURG"
    )
    private String addressLine2;

    @Schema(
        description = "Third address line or null",
        oneOf = {String.class, Object.class},
        example = "null"
    )
    private String addressLine3;
}