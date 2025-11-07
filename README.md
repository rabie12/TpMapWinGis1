@Entity
@Table(name = "bank_info")
@NoArgsConstructor
@Data
@AllArgsConstructor
public class BankInfo {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", updatable = false, nullable = false)
    private Long id;

    @Column(name = "bic")
    private String bic;

    @Column(name = "name")
    private String name;

    @Column(name = "institution")
    private String institution;

    @Column(name = "address1", columnDefinition = "LONGTEXT")
    private String address1;

    @Column(name = "location")
    private String location;

    @Column(name = "can_do_sct")
    private Boolean canDoSct;

    @Column(name = "can_do_core_sdd")
    private Boolean canDoCoreSdd;

    @Column(name = "can_do_b2b_sdd")
    private Boolean canDoB2bSdd;

    @Column(name = "created_at")
    private LocalDateTime createdAt;

    @Column(name = "updated_at")
    private LocalDateTime updatedAt;

    @Column(name = "country_iso_2")
    private String countryIso2;

    @OneToMany(mappedBy = "bankInfo", cascade = CascadeType.ALL)
    private List<BankAgency> bankAgencies = new ArrayList<>();

    @Lob
    @Column(name = "search_result", columnDefinition = "LONGTEXT")
    private String searchResult;

    @PrePersist
    protected void onCreate() {
        this.createdAt = LocalDateTime.now();
        this.updatedAt = LocalDateTime.now();
    }

    @PreUpdate
    protected void onUpdate() {
        this.updatedAt = LocalDateTime.now();
    }

    public BankInfo(String bic, String name, String institution, String address1, String location,
                    Boolean canDoCoreSdd, Boolean canDoSct, Boolean canDoB2bSdd, String countryIso2) {
        this.bic = bic;
        this.name = name;
        this.institution = institution;
        this.address1 = address1;
        this.location = location;
        this.canDoCoreSdd = canDoCoreSdd;
        this.canDoSct = canDoSct;
        this.canDoB2bSdd = canDoB2bSdd;
        this.countryIso2 = countryIso2;
    }
}