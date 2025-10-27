✅ The error you’re seeing:

Ambiguous constructors found for creating eu.olkypay.business_registry.dto.company.CompanyDTO:
CompanyDTO(java.lang.String, java.lang.String, java.lang.String),
CompanyDTO(java.lang.String).
Either declare parameterless constructor or annotate the default constructor with an annotation named @Default.

is a MapStruct mapping ambiguity problem — it cannot decide which constructor to use when instantiating a CompanyDTO.

⸻

💡 Why it happens

You have multiple constructors in CompanyDTO:

public CompanyDTO(String identifier, String legalName, String intracommunityVATNumber) { ... }
public CompanyDTO(String identifier) { ... }

MapStruct, when mapping from an entity to a DTO (e.g. LegalEntity → CompanyDTO), tries to pick a constructor.
Since both constructors could technically match (depending on what fields are available), it fails with the ambiguous constructor error.

Even though you also have @NoArgsConstructor, MapStruct ignores it by default unless you tell it to use that one.

⸻

✅ Solution Options

Option 1: Tell MapStruct to use the no-args constructor

Add the following annotation to your @Mapper:

@Mapper(
    componentModel = "spring",
    nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS,
    nullValuePropertyMappingStrategy = NullValuePropertyMappingStrategy.IGNORE
)

Then annotate the no-arg constructor with MapStruct’s @Default (if available) or @NoArgsConstructor(force = true) depending on your Lombok setup:

@NoArgsConstructor
@AllArgsConstructor
public class CompanyDTO {
    ...
}

👉 If you’re using MapStruct ≥ 1.6, you can use:

@Default
public CompanyDTO() {}

Otherwise, just rely on @NoArgsConstructor and add:

@Mapping(target = "<field>", ignore = true)

for all unmapped fields if needed.

⸻

Option 2: Remove ambiguous constructors

If you don’t really need both extra constructors, simplify to only one:

public CompanyDTO(String identifier, String legalName, String intracommunityVATNumber) { ... }

or

public CompanyDTO(String identifier) { ... }

This is often the cleanest fix — MapStruct won’t get confused anymore.

⸻

Option 3: Use a factory method

You can tell MapStruct explicitly how to create your DTOs:

@Mapper(componentModel = "spring")
public interface CompanyMapper {
    @ObjectFactory
    default CompanyDTO createCompanyDTO() {
        return new CompanyDTO();
    }

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);
}

This removes ambiguity by explicitly defining the constructor used.

⸻

✅ Recommended Fix

The simplest and safest approach:

Keep all constructors and just tell MapStruct to use the default one:

@Data
@NoArgsConstructor
@AllArgsConstructor
public class CompanyDTO {
    // fields...

    public CompanyDTO(String identifier, String legalName, String intracommunityVATNumber) { ... }
    public CompanyDTO(String identifier) { ... }
}

And in your mapper interface, add the factory:

@Mapper(componentModel = "spring", nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS)
public interface CompanyMapper {

    @ObjectFactory
    default CompanyDTO createCompanyDTO() {
        return new CompanyDTO();
    }

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);
}


⸻

Would you like me to show you the exact final version of your two files (CompanyDTO and CompanyMapper) with the minimal working fix applied?