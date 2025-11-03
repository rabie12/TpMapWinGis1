package eu.olkypay.business_registry.dto.company;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Setter
@AllArgsConstructor
@NoArgsConstructor
@JsonAutoDetect(fieldVisibility = JsonAutoDetect.Visibility.ANY)
@Schema(description = "Represents an address of a company or person")
public class AddressDTO {

    @Schema(
            type = "string",
            nullable = true,
            requiredMode = Schema.RequiredMode.AUTO,
            example = "23 RUE JEAN DIDIER"
    )
    private String addressLine1;

    @Schema(
            type = "string",
            nullable = true,
            example = "Résidence Les Lilas"

    )
    private String addressLine2;

    @Schema(
            type = "string",
            nullable = true,
            example = "null"
    )
    private String addressLine3;

    @Schema(
            type = "string",
            nullable = true,
            example = "75015"
    )
    private String zipCode;

    @Schema(
            type = "string",
            nullable = true,
            example = "Paris"
    )
    private String city;

    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    private String country;

    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getAddressLine1() {
        return addressLine1;
    }
    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getAddressLine2() {
        return addressLine2;
    }
    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getAddressLine3() {
        return addressLine3;
    }
    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getZipCode() {
        return zipCode;
    }
    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getCity() {
        return city;
    }

    @Schema(
            type = "string",
            nullable = true,
            example = "FR"
    )
    public String getCountry() {
        return country;
    }

    public AddressDTO(String city) {
        this.city = city;
    }
}
ive already add this but it doesnt work as im working with latest version of spring
