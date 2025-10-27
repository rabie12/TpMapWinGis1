Got it ✅ — thanks for sharing the full mapper and DTO.
The root cause of your compile-time MapStruct error is now crystal clear:

⸻

🔍 Root Cause

MapStruct doesn’t know which constructor to use when generating the method
CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity)
because your CompanyDTO has two matching constructors besides the no-args one:

public CompanyDTO(String identifier, String legalName, String intracommunityVATNumber)
public CompanyDTO(String identifier)

Even though you also have:

@NoArgsConstructor

MapStruct still gets confused — it inspects all constructors and finds multiple that could theoretically match the mapping.

⸻

💣 Why @NoArgsConstructor alone doesn’t fix it

@NoArgsConstructor generates a no-arg constructor at compile time, but MapStruct still sees multiple non-ambiguous constructors and fails to choose automatically.

⸻

✅ The Cleanest Fix: Add an Explicit @ObjectFactory in CompanyMapper

Tell MapStruct exactly which constructor to use by adding a factory method.

Add this inside your CompanyMapper interface:

import org.mapstruct.ObjectFactory;

@Mapper(componentModel = "spring", nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS)
public interface CompanyMapper {

    @ObjectFactory
    default CompanyDTO createCompanyDTO() {
        return new CompanyDTO(); // Force use of the no-arg constructor
    }

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);

    LegalEntity companyDTOToLegalEntity(CompanyDTO companyDTO);

    // ... (your existing default mapping methods)
}


⸻

🧩 Why This Works
	•	The @ObjectFactory method tells MapStruct:
“Whenever you need a new CompanyDTO, use this method to create it.”
	•	This completely avoids the constructor ambiguity.
	•	You don’t need to remove or modify any of your existing constructors.

⸻

✅ Optional (if using MapStruct ≥ 1.6)

If your project uses MapStruct 1.6 or newer, you can alternatively annotate the constructor you want MapStruct to prefer:

@Default
public CompanyDTO() {}

…but the @ObjectFactory solution is compatible with all versions and is generally safer in Spring-based projects.

⸻

🧱 Final Working Summary

CompanyDTO.java (no change needed):

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

CompanyMapper.java (add @ObjectFactory method):

@Mapper(componentModel = "spring", nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS)
public interface CompanyMapper {

    @ObjectFactory
    default CompanyDTO createCompanyDTO() {
        return new CompanyDTO();
    }

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);
    LegalEntity companyDTOToLegalEntity(CompanyDTO companyDTO);

    // ... rest of your existing default methods
}


⸻

After adding that, your ambiguous constructor error will disappear ✅.

Would you like me to show the full modified version of your CompanyMapper file (ready to paste in) with the fix inserted in the right place?