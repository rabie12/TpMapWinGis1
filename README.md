{
  "timeStamp": "2025-10-28T09:20:24.8186945",
  "message": "Identifier of entity 'eu.olkypay.business_registry.model.company.LegalEntity' must be manually assigned before calling 'persist()'",
  "httpStatus": 400
}

@Table(name = "legal_entity")
public class LegalEntity implements Serializable {
    @Id
    @NotNull (message = "Identifier can't be null ")
    private String identifier;
    @NotNull  (message = "RCS can't be null ")
    private String rcs;
    private String status;
    private String country;
    private String legalName;
    private String legalForm;
    private Long capital;
    private String activityCode; // a mettre en enum ??
    private LocalDate registrationDate;
    private String registrationCountry;
    @OneToOne(cascade =  {CascadeType.PERSIST, CascadeType.MERGE})
    @JoinColumn(name = "address", referencedColumnName = "id")
    private Address address;
    @OneToMany(fetch = FetchType.EAGER, mappedBy = "legalEntityParent", cascade =  {CascadeType.PERSIST, CascadeType.MERGE})
    private List<Representative> representatives = new ArrayList<>();
    @OneToMany(fetch = FetchType.EAGER, mappedBy = "legalEntityParent", cascade =  {CascadeType.PERSIST, CascadeType.MERGE})
    private List<BeneficialOwner> beneficialOwners = new ArrayList<>();
    @OneToMany(fetch = FetchType.EAGER, mappedBy = "parentCompany", cascade = {CascadeType.PERSIST, CascadeType.MERGE})
    private List<LegalEntity> secondaryOffices = new ArrayList<>();
    @ManyToOne
    @ToString.Exclude
    @JsonIgnore
    @JoinColumn(name = "legal_entity_parent_id")
    private LegalEntity parentCompany;
    private String legalEntityIdentifier;
    private String intracommunityVATNumber;
    @OneToMany(fetch = FetchType.EAGER, mappedBy = "legalEntityParent", cascade =  {CascadeType.PERSIST, CascadeType.MERGE})
    private List<Document> documents = new ArrayList<>();
    @OneToMany(mappedBy = "legalEntity", cascade =  {CascadeType.PERSIST, CascadeType.MERGE})
    @JsonIgnore
    @ToString.Exclude
    private List<Alert> alerts;
    @ManyToMany (mappedBy ="legalEntities")
    @JsonIgnore
    @ToString.Exclude
    private List<AppClient> appClients;
    public LegalEntity(String identifier, String legalName, String intracommunityVATNumber) {
        this.identifier = identifier;
        this.legalName = legalName;
        this.intracommunityVATNumber = intracommunityVATNumber;
    }


IT4S THE ERROR THAT Identifier of entity 'eu.olkypay.business_registry.model.company.LegalEntity' must be manually assigned before calling 'persist()
