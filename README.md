Excellent — you’re 95% there ✅
You already have the @Schema(oneOf = {String.class, Object.class}) pattern correctly in place for AddressDTO.
However, the reason your Swagger still doesn’t show type: ["string", "null"] or “nullable” everywhere is that:
	1.	You didn’t yet annotate the rest of your DTO fields (CompanyDTO and nested ones).
	2.	Swagger/OpenAPI won’t infer nullable automatically — it only appears when you explicitly annotate each nullable property.

Let’s fix that cleanly 👇

⸻

✅ Fixed AddressDTO

You did great — just a small enhancement: add nullable = true for consistency and make all fields explicit.

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AddressDTO {

    @Schema(
        description = "First address line or null",
        nullable = true,
        oneOf = {String.class, Object.class},
        example = "23 RUE JEAN DIDIER"
    )
    private String addressLine1;

    @Schema(
        description = "Second address line or null",
        nullable = true,
        oneOf = {String.class, Object.class},
        example = "Appartement 4B"
    )
    private String addressLine2;

    @Schema(
        description = "Third address line or null",
        nullable = true,
        oneOf = {String.class, Object.class},
        example = "null"
    )
    private String addressLine3;

    @Schema(nullable = true, example = "75000")
    private String zipCode;

    @Schema(nullable = true, example = "Paris")
    private String city;

    @Schema(nullable = true, example = "FR")
    private String country;

    public AddressDTO(String city) {
        this.city = city;
    }
}


⸻

✅ Fixed CompanyDTO

Here’s your CompanyDTO updated so all nullable fields are explicitly marked, including nested DTOs and lists.
This ensures that Swagger/OpenAPI reflects your expected structure exactly like your JSON.

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CompanyDTO {

    @Schema(nullable = true, example = "123456789")
    private String identifier;

    @Schema(nullable = true, example = "RCS Paris 123456")
    private String rcs;

    @Schema(nullable = true, example = "My Company S.A.")
    private String legalName;

    @Schema(nullable = true, example = "Société Anonyme")
    private String legalForm;

    @Schema(nullable = true, example = "ACTIVE")
    private String status;

    @Schema(nullable = true, example = "1000000")
    private Long capital;

    @Schema(nullable = true, example = "6202A")
    private String activityCode;

    @Schema(nullable = true, example = "2020-01-01")
    private LocalDate registrationDate;

    @Schema(nullable = true, example = "FR")
    private String registrationCountry;

    @Schema(nullable = true, implementation = AddressDTO.class)
    private AddressDTO address;

    @Schema(nullable = true)
    private List<RepresentativeDTO> representatives = new ArrayList<>();

    @Schema(nullable = true)
    private List<BeneficialOwnerDTO> beneficialOwners = new ArrayList<>();

    @Schema(nullable = true)
    private List<CompanyDTO> secondaryOffices = new ArrayList<>();

    @Schema(nullable = true)
    private CompanyDTO principalOffice;

    @Schema(nullable = true, example = "5493001KJTIIGC8Y1R12")
    private String legalEntityIdentifier;

    @Schema(nullable = true, example = "FR12345678901")
    private String intracommunityVATNumber;

    @Schema(nullable = true)
    private List<DocumentDTO> documents = new ArrayList<>();

    @Schema(nullable = true)
    private List<AlertDTO> alerts = new ArrayList<>();

    public CompanyDTO(String identifier, String legalName, String intracommunityVATNumber) {
        this.identifier = identifier;
        this.legalName = legalName;
        this.intracommunityVATNumber = intracommunityVATNumber;
    }

    public CompanyDTO(String identifier) {
        this.identifier = identifier;
    }
}


⸻

🧠 What will change in Swagger:

Before:

addressLine1:
  type: string

After:

addressLine1:
  oneOf:
    - type: string
    - type: "null"
  example: "23 RUE JEAN DIDIER"

And for simple nullable = true fields:

rcs:
  type: string
  nullable: true
  example: "RCS Paris 123456"


⸻

Would you like me to also show you how to annotate RepresentativeDTO and BeneficialOwnerDTO consistently (so your Swagger schema matches the full JSON tree)?