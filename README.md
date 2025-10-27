C:\Users\RHABACHI\IdeaProjects\business-registry\src\main\java\eu\olkypay\business_registry\mapper\CompanyMapper.java:18:16
java: Ambiguous constructors found for creating eu.olkypay.business_registry.dto.company.CompanyDTO: CompanyDTO(java.lang.String, java.lang.String, java.lang.String), CompanyDTO(java.lang.String). Either declare parameterless constructor or annotate the default constructor with an annotation named @Default.


package eu.olkypay.business_registry.dto.company;

import eu.olkypay.business_registry.dto.AlertDTO;
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


package eu.olkypay.business_registry.mapper;

import eu.olkypay.business_registry.dto.AlertDTO;
import eu.olkypay.business_registry.dto.company.*;
import eu.olkypay.business_registry.model.Alert;
import eu.olkypay.business_registry.model.company.*;
import org.mapstruct.Mapper;
import org.mapstruct.NullValueCheckStrategy;
import org.mapstruct.NullValuePropertyMappingStrategy;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

@Mapper(componentModel = "spring", nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS)
public interface CompanyMapper {

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);

    LegalEntity companyDTOToLegalEntity(CompanyDTO companyDTO);

    default LegalEntity companyDtoToCompany(LegalEntity company, CompanyDTO companyDTO) {

        if (company.getIdentifier() == null && companyDTO.getIdentifier() != null) {
            company.setIdentifier(companyDTO.getIdentifier());
        }
        if (company.getRcs() == null && companyDTO.getRcs() != null) {
            company.setRcs(companyDTO.getRcs());
        }
        if (company.getStatus() == null && companyDTO.getStatus() != null) {
            company.setStatus(companyDTO.getStatus());
        }
        if (company.getLegalName() == null && companyDTO.getLegalName() != null) {
            company.setLegalName(companyDTO.getLegalName());
        }
        if (company.getLegalForm() == null && companyDTO.getLegalForm() != null) {
            company.setLegalForm(companyDTO.getLegalForm());
        }
        if (company.getCapital() == null && companyDTO.getCapital() != null) {
            company.setCapital(companyDTO.getCapital());
        }
        if (company.getActivityCode() == null && companyDTO.getActivityCode() != null) {
            company.setActivityCode(companyDTO.getActivityCode());
        }
        if (company.getRegistrationDate() == null && companyDTO.getRegistrationDate() != null) {
            company.setRegistrationDate(companyDTO.getRegistrationDate());
        }
        if (company.getRegistrationCountry() == null && companyDTO.getRegistrationCountry() != null) {
            company.setRegistrationCountry(companyDTO.getRegistrationCountry());
        }
        if (company.getLegalEntityIdentifier() == null && companyDTO.getLegalEntityIdentifier() != null) {
            company.setLegalEntityIdentifier(companyDTO.getLegalEntityIdentifier());
        }
        if (company.getIntracommunityVATNumber() == null && companyDTO.getIntracommunityVATNumber() != null) {
            company.setIntracommunityVATNumber(companyDTO.getIntracommunityVATNumber());
        }
        if (company.getAddress() == null && companyDTO.getAddress() != null) {
            company.setAddress(addressDtoToAdress(companyDTO.getAddress()));
        }
        if ((company.getRepresentatives() == null || company.getRepresentatives().isEmpty()) && companyDTO.getRepresentatives() != null) {
            for (RepresentativeDTO representativeDTO : companyDTO.getRepresentatives()) {
                company.addRepresentative(representativeDtoToRepresentative(representativeDTO));
            }
        }
        if ((company.getBeneficialOwners() == null || company.getBeneficialOwners().isEmpty()) && companyDTO.getBeneficialOwners() != null) {
            for (BeneficialOwnerDTO beneficialOwnerDTO : companyDTO.getBeneficialOwners()) {
                company.addBeneficialOwner(beneficialOwnerDtoToBeneficialOwner(beneficialOwnerDTO));
            }
        }
        if ((company.getSecondaryOffices() == null || company.getSecondaryOffices().isEmpty()) && companyDTO.getSecondaryOffices() != null) {
            for (CompanyDTO companyDto : companyDTO.getSecondaryOffices()) {
                company.addSecondaryOffice(companyDTOToLegalEntity(companyDto));
            }
        }
        if (companyDTO.getDocuments() != null) {
            for (DocumentDTO documentDTO : companyDTO.getDocuments()) {
                company.addDocument(documentDtoToDocument(documentDTO));
            }
        }
        if (companyDTO.getAlerts() != null) {
            for (AlertDTO alertDTO : companyDTO.getAlerts()) {
                company.addAlert(alertDTOToAlert(alertDTO));
            }
        }
        return company;
    }

    default Address addressDtoToAdress(AddressDTO addressDTO) {
        Address address = new Address();
        if (Objects.equals(addressDTO.getAddressLine1(), "")) {
            address.setAddressLine1(null);
        }
        else {
            address.setAddressLine1(addressDTO.getAddressLine1());
        }
        address.setAddressLine2(addressDTO.getAddressLine2());
        address.setAddressLine3(addressDTO.getAddressLine3());
        address.setCountry(addressDTO.getCountry());
        address.setCity(addressDTO.getCity());
        address.setZipCode(addressDTO.getZipCode());
        return address;
    }

    default Representative representativeDtoToRepresentative(RepresentativeDTO representativeDTO) {
        Representative representative = new Representative();
        if (representativeDTO.getRole() != null) {
            representative.setRole(representativeDTO.getRole());
            if (representativeDTO.getNaturalPerson() != null) {
                representative.setNaturalPerson(naturalPersonDtoToNaturalPerson(representativeDTO.getNaturalPerson()));
            }
            else if (representativeDTO.getLegalEntity() != null) {
                representative.setLegalEntity(companyDTOToLegalEntity(representativeDTO.getLegalEntity()));
            }
        }
        return representative;
    }

    default Document documentDtoToDocument(DocumentDTO documentDTO) {
        Document document = new Document();
        document.setIdentifier(documentDTO.getIdentifier());
        document.setType(documentDTO.getType());
        document.setName(documentDTO.getName());
        document.setCreationDate(documentDTO.getCreationDate());
        document.setUpdatedDate(documentDTO.getUpdatedDate());
        document.setDetails(documentDTO.getDetails());
        return document;
    }

    default Alert alertDTOToAlert(AlertDTO alertDTO) {
        Alert alert = new Alert();
        alert.setId(alertDTO.getId());
        alert.setOrigin(alertDTO.getOrigin());
        alert.setType(alertDTO.getType());
        alert.setContent(alertDTO.getContent());
        alert.setCreatedAt(alertDTO.getCreatedAt());
        return alert;
    }

    default BeneficialOwner beneficialOwnerDtoToBeneficialOwner(BeneficialOwnerDTO beneficialOwnerDTO) {
        BeneficialOwner beneficialOwner = new BeneficialOwner();
        beneficialOwner.setPercentageOfOwnership(beneficialOwnerDTO.getPercentageOfOwnership());
        beneficialOwner.setNatureOfOwnership(beneficialOwnerDTO.getNatureOfOwnership());
        if (beneficialOwnerDTO.getNaturalPerson() != null) {
            beneficialOwner.setNaturalPerson(naturalPersonDtoToNaturalPerson(beneficialOwnerDTO.getNaturalPerson()));
        }
        if (beneficialOwnerDTO.getLegalEntity() != null) {
            beneficialOwner.setLegalEntity(companyDTOToLegalEntity(beneficialOwnerDTO.getLegalEntity()));
        }
        beneficialOwner.setStartDate(beneficialOwnerDTO.getStartDate());
        beneficialOwner.setEndDate(beneficialOwnerDTO.getEndDate());
        return beneficialOwner;
    }

    default NaturalPerson naturalPersonDtoToNaturalPerson(NaturalPersonDTO naturalPersonDTO) {
        NaturalPerson naturalPerson = new NaturalPerson();
        naturalPerson.setFirstName(naturalPersonDTO.getFirstName());
        naturalPerson.setLastName(naturalPersonDTO.getLastName());
        naturalPerson.setMaidenName(naturalPersonDTO.getMaidenName());
        naturalPerson.setBirthDate(naturalPersonDTO.getBirthDate());
        naturalPerson.setBirthCity(naturalPersonDTO.getBirthCity());
        naturalPerson.setBirthCountry(naturalPersonDTO.getBirthCountry());
        naturalPerson.setNationality(naturalPersonDTO.getNationality());
        if (naturalPersonDTO.getAddress() != null) {
            naturalPerson.setAddress(addressDtoToAdress(naturalPersonDTO.getAddress()));
        }
        return naturalPerson;
    }

}

