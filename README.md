{
  "identifier": "string",
  "rcs": "string",
  "status": "string",
  "country": "string",
  "legalName": "string",
  "legalForm": "string",
  "capital": 0,
  "activityCode": "string",
  "registrationDate": "2025-10-30",
  "registrationCountry": "string",
  "address": {
    "id": 0,
    "addressLine1": "string",
    "addressLine2": "string",
    "addressLine3": "string",
    "zipCode": "string",
    "city": "string",
    "country": "string"
  },
  "representatives": [
    {
      "id": 0,
      "role": "string",
      "naturalPerson": {
        "id": 0,
        "firstName": "string",
        "lastName": "string",
        "maidenName": "string",
        "birthDate": "2025-10-30",
        "birthCity": "string",
        "birthCountry": "string",
        "nationality": "string",
        "address": {
          "id": 0,
          "addressLine1": "string",
          "addressLine2": "string",
          "addressLine3": "string",
          "zipCode": "string",
          "city": "string",
          "country": "string"
        }
      },
      "legalEntity": "string"
    }
  ],
  "beneficialOwners": [
    {
      "id": 0,
      "percentageOfOwnership": 0,
      "natureOfOwnership": "string",
      "naturalPerson": {
        "id": 0,
        "firstName": "string",
        "lastName": "string",
        "maidenName": "string",
        "birthDate": "2025-10-30",
        "birthCity": "string",
        "birthCountry": "string",
        "nationality": "string",
        "address": {
          "id": 0,
          "addressLine1": "string",
          "addressLine2": "string",
          "addressLine3": "string",
          "zipCode": "string",
          "city": "string",
          "country": "string"
        }
      },
      "legalEntity": {},
      "startDate": "2025-10-30",
      "endDate": "2025-10-30"
    }
  ],
  "secondaryOffices": [
    "string"
  ],
  "legalEntityIdentifier": "string",
  "intracommunityVATNumber": "string",
  "documents": [
    {
      "identifier": "string",
      "type": "Acte",
      "name": "string",
      "creationDate": "2025-10-30T13:55:57.756Z",
      "updatedDate": "2025-10-30T13:55:57.756Z",
      "details": "string"
    }
  ],
  "createdAt": "2025-10-30T13:55:57.756Z",
  "updatedAt": "2025-10-30T13:55:57.756Z"
}



i did it but i still have this: 
this is my companyDTO and AdressDTO coul you check if something are missing :


@Data
@AllArgsConstructor
@NoArgsConstructor
public class CompanyDTO {
    private String identifier;
    private String rcs;
    private String legalName;
    private String legalForm;
    private String status;
    private Long capital;
    private String activityCode;
    private LocalDate registrationDate;
    private String registrationCountry;
    private AddressDTO address;
    private List<RepresentativeDTO> representatives = new ArrayList<>();
    private List<BeneficialOwnerDTO> beneficialOwners = new ArrayList<>();
    private List<CompanyDTO> secondaryOffices = new ArrayList<>();
    private CompanyDTO principalOffice;
    private String legalEntityIdentifier;
    private String intracommunityVATNumber;
    private List<DocumentDTO> documents = new ArrayList<>();
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

package eu.olkypay.business_registry.dto.company;

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
    private String zipCode;
    private String city;
    private String country;

    public AddressDTO(String city) {
        this.city = city;
    }
}

