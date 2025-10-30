
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




          fir         "address": {
          "id": 0,
          "addressLine1": "string",
          "addressLine2": "string",
          "addressLine3": "string",



          i want to raplace this value by dans la definition du swagger tu peux passer 

type
 - 'null'
 - string


i v ried this and it doesnt work :

          public class AddressDTO {
    @Schema(nullable = true, example = "23 RUE JEAN DIDIER", defaultValue = "null")
    private String addressLine1;
    @Schema(nullable = true, example = "LUXEMBOURG", defaultValue = "null")
    private String addressLine2;
    @Schema(nullable = true, example = "LUXEMBOURG", defaultValue = "null")
    private String addressLine3;


    
