    @GetMapping("/country/{country}/company/{id}")
    public ResponseEntity<CompanyDTO> getDataForCountry(@PathVariable("country") String country, @PathVariable("id") String id) throws IOException, EntityNotFoundException {
        CompanyDTO company = registryService.getDataForCountry(country, id);
        return new ResponseEntity<>(company, HttpStatus.OK);
    }

	this my api return :

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
      "creationDate": "2025-10-30T09:05:51.585Z",
      "updatedDate": "2025-10-30T09:05:51.585Z",
      "details": "string"
    }
  ],
  "createdAt": "2025-10-30T09:05:51.585Z",
  "updatedAt": "2025-10-30T09:05:51.585Z"
}
