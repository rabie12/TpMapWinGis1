{"openapi":"3.1.0","info":{"title":"API Documentation","version":"1.0"},"servers":[{"url":"http://localhost:8080","description":"Generated server url"}],"security":[{"Api-Key":[]}],"paths":{"/auth/appClient":{"post":{"tags":["authentication-controller"],"summary":"Create an app Client","operationId":"addAppClient","requestBody":{"content":{"application/json":{"schema":{"$ref":"#/components/schemas/RequestBodyCreateAppClient"}}},"required":true},"responses":{"200":{"description":"App Client created","content":{"*/*":{"schema":{"$ref":"#/components/schemas/AppClient"}}}}}}},"/api/country/{country}/company/{id}/follow":{"post":{"tags":["registry-controller"],"summary":"Follow new company","operationId":"followCompany","parameters":[{"name":"country","in":"path","required":true,"schema":{"type":"string"}},{"name":"id","in":"path","required":true,"schema":{"type":"string"}},{"name":"Api-Key","in":"header","required":true,"schema":{"type":"string"}}],"responses":{"200":{"description":"Company followed","content":{"*/*":{"schema":{"type":"string"}}}}}}},"/api/connector":{"post":{"tags":["registry-controller"],"summary":"Create or update connector","operationId":"addConnector","requestBody":{"content":{"application/json":{"schema":{"$ref":"#/components/schemas/Connector"}}},"required":true},"responses":{"201":{"description":"Connector created/updated","content":{"*/*":{"schema":{"$ref":"#/components/schemas/Connector"}}}}}}},"/health/connectors":{"get":{"tags":["health-controller"],"operationId":"getHealthConnector","responses":{"200":{"description":"OK","content":{"*/*":{"schema":{"$ref":"#/components/schemas/Health"}}}}}}},"/api/country/{country}/company/{id}":{"get":{"tags":["registry-controller"],"summary":"Get company's data ","operationId":"getDataForCountry","parameters":[{"name":"country","in":"path","required":true,"schema":{"type":"string"}},{"name":"id","in":"path","required":true,"schema":{"type":"string"}}],"responses":{"200":{"description":"Company's data returned","content":{"*/*":{"schema":{"$ref":"#/components/schemas/LegalEntity"}}}}}}},"/api/connector/{id}":{"get":{"tags":["registry-controller"],"summary":"Get connector ","operationId":"getConnector","parameters":[{"name":"id","in":"path","required":true,"schema":{"type":"integer","format":"int64"}}],"responses":{"200":{"description":"Connector's data","content":{"*/*":{"schema":{"$ref":"#/components/schemas/Connector"}}}}}}},"/api/alert":{"get":{"tags":["registry-controller"],"summary":"Get new alert for appClient","operationId":"getAlerts","parameters":[{"name":"Api-Key","in":"header","required":true,"schema":{"type":"string"}}],"responses":{"200":{"description":"List of alerts","content":{"*/*":{"schema":{"type":"array","items":{"$ref":"#/components/schemas/AlertDTO"}}}}}}}}},"components":{"schemas":{"RequestBodyCreateAppClient":{"properties":{"name":{"type":"string","oneOf":[{},{}]},"callBackUrl":{"type":"string","oneOf":[{},{}]}},"required":["name"]},"Address":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"addressLine1":{"type":"string","oneOf":[{},{}]},"addressLine2":{"type":"string","oneOf":[{},{}]},"addressLine3":{"type":"string","oneOf":[{},{}]},"zipCode":{"type":"string","oneOf":[{},{}]},"city":{"type":"string","oneOf":[{},{}]},"country":{"type":"string","oneOf":[{},{}]}}},"AppClient":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"apiKey":{"type":"string","format":"uuid","oneOf":[{},{}]},"name":{"type":"string","oneOf":[{},{}]},"createdAt":{"type":"string","format":"date-time","oneOf":[{},{}]},"updatedAt":{"type":"string","format":"date-time","oneOf":[{},{}]},"legalEntities":{"type":"array","items":{"$ref":"#/components/schemas/LegalEntity"},"oneOf":[{},{}]},"callBackUrl":{"type":"string","oneOf":[{},{}]},"secretKey":{"type":"string","oneOf":[{},{}]}},"required":["name"]},"BeneficialOwner":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"percentageOfOwnership":{"type":"integer","format":"int32","oneOf":[{},{}]},"natureOfOwnership":{"type":"string","oneOf":[{},{}]},"naturalPerson":{"$ref":"#/components/schemas/NaturalPerson","oneOf":[{},{}]},"legalEntity":{"oneOf":[{},{}],"required":["identifier","rcs"]},"startDate":{"type":"string","format":"date","oneOf":[{},{}]},"endDate":{"type":"string","format":"date","oneOf":[{},{}]}}},"Document":{"properties":{"identifier":{"type":"string","oneOf":[{},{}]},"type":{"type":"string","enum":["Acte","Bilan"],"oneOf":[{},{}]},"name":{"type":"string","oneOf":[{},{}]},"creationDate":{"type":"string","format":"date-time","oneOf":[{},{}]},"updatedDate":{"type":"string","format":"date-time","oneOf":[{},{}]},"details":{"type":"string","oneOf":[{},{}]}}},"LegalEntity":{"properties":{"identifier":{"type":"string","oneOf":[{},{}]},"rcs":{"type":"string","oneOf":[{},{}]},"status":{"type":"string","oneOf":[{},{}]},"country":{"type":"string","oneOf":[{},{}]},"legalName":{"type":"string","oneOf":[{},{}]},"legalForm":{"type":"string","oneOf":[{},{}]},"capital":{"type":"integer","format":"int64","oneOf":[{},{}]},"activityCode":{"type":"string","oneOf":[{},{}]},"registrationDate":{"type":"string","format":"date","oneOf":[{},{}]},"registrationCountry":{"type":"string","oneOf":[{},{}]},"address":{"$ref":"#/components/schemas/Address","oneOf":[{},{}]},"representatives":{"type":"array","items":{"$ref":"#/components/schemas/Representative"},"oneOf":[{},{}]},"beneficialOwners":{"type":"array","items":{"$ref":"#/components/schemas/BeneficialOwner"},"oneOf":[{},{}]},"secondaryOffices":{"type":"array","items":{"$ref":"#/components/schemas/LegalEntity"},"oneOf":[{},{}]},"legalEntityIdentifier":{"type":"string","oneOf":[{},{}]},"intracommunityVATNumber":{"type":"string","oneOf":[{},{}]},"documents":{"type":"array","items":{"$ref":"#/components/schemas/Document"},"oneOf":[{},{}]},"createdAt":{"type":"string","format":"date-time","oneOf":[{},{}]},"updatedAt":{"type":"string","format":"date-time","oneOf":[{},{}]}},"required":["identifier","rcs"]},"NaturalPerson":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"firstName":{"type":"string","oneOf":[{},{}]},"lastName":{"type":"string","oneOf":[{},{}]},"maidenName":{"type":"string","oneOf":[{},{}]},"birthDate":{"type":"string","format":"date","oneOf":[{},{}]},"birthCity":{"type":"string","oneOf":[{},{}]},"birthCountry":{"type":"string","oneOf":[{},{}]},"nationality":{"type":"string","oneOf":[{},{}]},"address":{"$ref":"#/components/schemas/Address","oneOf":[{},{}]}}},"Representative":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"role":{"type":"string","oneOf":[{},{}]},"naturalPerson":{"$ref":"#/components/schemas/NaturalPerson","oneOf":[{},{}]},"legalEntity":{"$ref":"#/components/schemas/LegalEntity","oneOf":[{},{}]}}},"Connector":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"type":{"type":"array","items":{"$ref":"#/components/schemas/ConnectorType"},"oneOf":[{},{}]},"active":{"type":"boolean","oneOf":[{},{}]},"country":{"type":"string","oneOf":[{},{}]},"apiUrl":{"type":"string","oneOf":[{},{}]},"credentials":{"$ref":"#/components/schemas/Credentials","oneOf":[{},{}]},"apiToken":{"$ref":"#/components/schemas/Token","oneOf":[{},{}]},"priority":{"type":"integer","format":"int32","oneOf":[{},{}]},"serviceName":{"type":"string","oneOf":[{},{}]},"createdDate":{"type":"string","format":"date-time","oneOf":[{},{}]},"updateDate":{"type":"string","format":"date-time","oneOf":[{},{}]}},"required":["active","country","serviceName"]},"ConnectorType":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"name":{"type":"string","oneOf":[{},{}]}}},"Credentials":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"apiKey":{"type":"string","oneOf":[{},{}]},"loginUrl":{"type":"string","oneOf":[{},{}]},"username":{"type":"string","oneOf":[{},{}]},"password":{"type":"string","oneOf":[{},{}]}}},"Token":{"properties":{"id":{"type":"integer","format":"int64","oneOf":[{},{}]},"token":{"type":"string","oneOf":[{},{}]},"refreshToken":{"type":"string","oneOf":[{},{}]},"createdAt":{"type":"string","format":"date-time","oneOf":[{},{}]},"expiratedAt":{"type":"string","format":"date-time","oneOf":[{},{}]}}},"Health":{"properties":{"details":{"type":"object","additionalProperties":{},"oneOf":[{},{}]}}},"AlertDTO":{"properties":{"id":{"type":"string","oneOf":[{},{}]},"origin":{"type":"string","oneOf":[{},{}]},"type":{"type":"string","enum":["LOCATION","ACTIVITY","STRIKING_OFF","BOARD_OF_DIRECTORS","UBO","COMPANY_NAME","LEGAL_FORM","SHARE_CAPITAL","ANNUAL_ACCOUNT","UPDATE_IDENTIFICATION_NUMBER","STATUTORY_AUDITOR","AGM_NOTICE","OTHER"],"oneOf":[{},{}]},"legalEntity":{"$ref":"#/components/schemas/LegalEntity","oneOf":[{},{}]},"content":{"type":"string","oneOf":[{},{}]},"createdAt":{"type":"string","format":"date-time","oneOf":[{},{}]},"updatedAt":{"type":"string","format":"date-time","oneOf":[{},{}]}},"required":["id","origin","type"]}},"securitySchemes":{"Api-Key":{"type":"apiKey","name":"Api-Key","in":"header"}}}}


@Component
public class OpenApiNullablePostProcessor implements OpenApiCustomizer {
    @Override
    public void customise(OpenAPI openApi) {
        if (openApi == null || openApi.getComponents() == null) return;

        Map<String, Schema> schemas = openApi.getComponents().getSchemas();
        if (schemas == null) return;

        schemas.values().forEach(schema -> {
            if (schema.getProperties() == null) return;

            schema.getProperties().forEach((name, prop) -> {
                if (!(prop instanceof Schema<?> p)) return;
                boolean isStringType = "string".equals(p.getType()) || p.getType() == null;
                if (isStringType) {
                    Schema<?> stringSchema = new Schema<>().type("string");
                    Schema<?> nullSchema = new Schema<>().type("null");
                    p.setOneOf(List.of(stringSchema, nullSchema));
                    p.setType(null);
                    p.setNullable(null);
                }
            });
        });
    }
}


and in my swagger-ui page :

{
  "identifier": "string",
  "rcs": "string",
  "status": "string",
  "country": "string",
  "legalName": "string",
  "legalForm": "string",
  "capital": 9007199254740991,
  "activityCode": "string",
  "registrationDate": "2025-11-03",
  "registrationCountry": "string",
  "address": {
    "id": 9007199254740991,
    "addressLine1": "string",
    "addressLine2": "string",
    "addressLine3": "string",
    "zipCode": "string",
    "city": "string",
    "country": "string"
  },
  "representatives": [
    {
      "id": 9007199254740991,
      "role": "string",
      "naturalPerson": {
        "id": 9007199254740991,
        "firstName": "string",
        "lastName": "string",
        "maidenName": "string",
        "birthDate": "2025-11-03",
        "birthCity": "string",
        "birthCountry": "string",
        "nationality": "string",
        "address": {
          "id": 9007199254740991,
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
      "id": 9007199254740991,
      "percentageOfOwnership": 1073741824,
      "natureOfOwnership": "string",
      "naturalPerson": {
        "id": 9007199254740991,
        "firstName": "string",
        "lastName": "string",
        "maidenName": "string",
        "birthDate": "2025-11-03",
        "birthCity": "string",
        "birthCountry": "string",
        "nationality": "string",
        "address": {
          "id": 9007199254740991,
          "addressLine1": "string",
          "addressLine2": "string",
          "addressLine3": "string",
          "zipCode": "string",
          "city": "string",
          "country": "string"
        }
      },
      "legalEntity": {},
      "startDate": "2025-11-03",
      "endDate": "2025-11-03"
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
      "creationDate": "2025-11-03T08:44:23.855Z",
      "updatedDate": "2025-11-03T08:44:23.855Z",
      "details": "string"
    }
  ],
  "createdAt": "2025-11-03T08:44:23.855Z",
  "updatedAt": "2025-11-03T08:44:23.855Z"
}


i want to replace on my swagger definition 'string'  type by :
 - 'null'
 - string
as you see upper it change it all the type on my api-docs because of getType returning null
