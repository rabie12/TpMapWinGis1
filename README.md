Can't parse numeric value [can_do_core_sdd] using formatter
DATA TOO long adress1

this is what i want to import fril an old db 

databaseChangeLog:
  - changeSet:
      id: 1-baseline-schema
      author: RHI
      changes:
        - createTable:
            tableName: users
            columns:
              - column:
                  name: username
                  type: varchar(255)
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: password
                  type: varchar(255)
              - column:
                  name: enabled
                  type: boolean

        - createTable:
            tableName: authorities
            columns:
              - column:
                  name: id
                  type: bigint
                  autoIncrement: true
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: username
                  type: varchar(255)
              - column:
                  name: authority
                  type: varchar(255)

        - addForeignKeyConstraint:
            baseTableName: authorities
            baseColumnNames: username
            referencedTableName: users
            referencedColumnNames: username
            constraintName: fk_authorities_user

        - createTable:
            tableName: bank_info
            columns:
              - column:
                  name: id
                  type: bigint
                  autoIncrement: true
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: bic
                  type: varchar(50)
              - column:
                  name: name
                  type: varchar(255)
              - column:
                  name: institution
                  type: varchar(255)
              - column:
                  name: address1
                  type: longtext
              - column:
                  name: location
                  type: varchar(255)
              - column:
                  name: can_do_sct
                  type: boolean
              - column:
                  name: can_do_core_sdd
                  type: boolean
              - column:
                  name: can_do_b2b_sdd
                  type: boolean
              - column:
                  name: country_iso_2
                  type: varchar(10)
              - column:
                  name: created_at
                  type: datetime
              - column:
                  name: updated_at
                  type: datetime
              - column:
                  name: search_result
                  type: longtext
        - createTable:
            tableName: bank_agency
            columns:
              - column:
                  name: id
                  type: bigint
                  autoIncrement: true
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: country_iso_2
                  type: varchar(10)
              - column:
                  name: bank_code
                  type: varchar(50)
              - column:
                  name: branch_code
                  type: varchar(50)
              - column:
                  name: branch_name
                  type: varchar(255)
              - column:
                  name: bank_and_branch_code
                  type: varchar(100)
              - column:
                  name: bank_info_id
                  type: bigint

        - addForeignKeyConstraint:
            baseTableName: bank_agency
            baseColumnNames: bank_info_id
            referencedTableName: bank_info
            referencedColumnNames: id
            constraintName: fk_agency_bankinfo

        - createTable:
            tableName: iban_search_history
            columns:
              - column:
                  name: id
                  type: char(36)
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: iban
                  type: varchar(255)
              - column:
                  name: result
                  type: varchar(255)
              - column:
                  name: response_details
                  type: longtext
              - column:
                  name: created_at
                  type: datetime
              - column:
                  name: updated_at
                  type: datetime
              - column:
                  name: bank_agency_id
                  type: bigint

        - addForeignKeyConstraint:
            baseTableName: iban_search_history
            baseColumnNames: bank_agency_id
            referencedTableName: bank_agency
            referencedColumnNames: id
            constraintName: fk_history_agency

        - createTable:
            tableName: spring_properties
            columns:
              - column:
                  name: id
                  type: bigint
                  autoIncrement: true
                  constraints:
                    primaryKey: true
                    nullable: false
              - column:
                  name: prop_key
                  type: varchar(255)
              - column:
                  name: prop_value
                  type: varchar(255)

  - changeSet:
      id: 2-init-db-data
      author: RHI
      changes:
        - insert:
            tableName: spring_properties
            columns:
              - column:
                  name: prop_key
                  value: sepa.url
              - column:
                  name: prop_value
                  value: https://rest.sepatools.eu

        - insert:
            tableName: spring_properties
            columns:
              - column:
                  name: prop_key
                  value: sepa.username
              - column:
                  name: prop_value
                  value: ibancalculatorolkypay

        - insert:
            tableName: spring_properties
            columns:
              - column:
                  name: prop_key
                  value: sepa.secret
              - column:
                  name: prop_value
                  value: 4u\\Z*4.(+ZK%P<E5mA

        - insert:
            tableName: users
            columns:
              - column:
                  name: username
                  value: tournesol
              - column:
                  name: password
                  value: $2a$12$7p4J5DYvDEP1MKbhw5WuA.gmfIqEi5Ukj/BgWF/spz23J7Oa2c4sO
              - column:
                  name: enabled
                  valueBoolean: true

        - insert:
            tableName: users
            columns:
              - column:
                  name: username
                  value: bitbang
              - column:
                  name: password
                  value: $2a$12$7p4J5DYvDEP1MKbhw5WuA.gmfIqEi5Ukj/BgWF/spz23J7Oa2c4sO
              - column:
                  name: enabled
                  valueBoolean: true

        - insert:
            tableName: authorities
            columns:
              - column:
                  name: username
                  value: tournesol
              - column:
                  name: authority
                  value: OLKY_ADMIN

        - insert:
            tableName: authorities
            columns:
              - column:
                  name: username
                  value: bitbang
              - column:
                  name: authority
                  value: OLKY_ADMIN
    
long text fix adresse 1 but i dont how to fix to boolean value this is a part of script that i want to start :

INSERT INTO external_bank_data.bank_info (address1,bic,can_dob2b_sdd,can_do_core_sdd,can_do_sct,country_iso2,created_at,institution,location,name,search_result,updated_at) VALUES
	 ('AEGIDIENTORPLATZ 1','SPKHDE2HXXX',1,1,1,'DE','2015-04-01 16:56:07.000000','SPARKASSE HANNOVER','HANNOVER','Sparkasse Hannover',NULL,'2025-10-23 11:52:02.094260'),
	 ('28, PLACE RIHOUR','NORDFRPPXXX',1,1,1,'FR','2015-04-01 16:56:07.000000','CREDIT DU NORD','LILLE','AG FLANDRES',NULL,'2025-10-23 11:52:02.094260'),
	 ('','SOGEFRPPXXX',1,1,1,'FR','2015-04-01 16:56:08.000000','Societe Generale','PUTEAUX','Societe Generale',NULL,'2025-10-23 11:52:02.094260'),
	 ('6 AVENUE DE PROVENCE','CMCIFRPPXXX',1,1,1,'FR','2015-04-01 16:56:08.000000','CM - CIC BANQUES','PARIS','CM - CIC BANQUES',NULL,'2025-10-23 11:52:02.094260'),
	 ('1 ROND POINT DE LA NATION','CEPAFRPP213',1,1,1,'FR','2015-04-01 16:56:08.000000','CAISSE D''EPARGNE DE BOURGOGNE FRANCHE-COMTE','DIJON','AG SIEGE',NULL,'2025-10-23 11:52:02.094260'),
	 ('33 RUE DES TROIS FONTANOT','CCOPFRPPXXX',1,1,1,'FR','2015-04-01 16:56:09.000000','CREDIT COOPERATIF','NANTERRE','AG SIEGE',NULL,'2025-10-23 11:52:02.094260'),
	 ('','AGRIFRPP883',1,1,1,'FR','2015-04-01 16:56:09.000000','CREDIT AGRICOLE','ROUEN','AG SITE BOIS GUIL',NULL,'2025-10-23 11:52:02.094260'),
	 ('RUE LEON LAVAL 4','CCRALULLXXX',1,1,1,'LU','2015-04-01 16:56:11.000000','BANQUE RAIFFEISEN','LEUDELANGE','',NULL,'2025-10-23 11:52:02.094260'),
	 ('1 CHURCHILL PLACE','BARCGB22XXX',1,1,1,'GB','2015-04-01 16:56:12.000000','BARCLAYS BANK PLC','LONDON','BARCLAYS BANK PLC',NULL,'2025-10-23 11:52:02.094260'),
	 ('38 PLACE DE LA GARE','CCPLLULLXXX',1,1,1,'FR','2015-04-01 16:56:13.000000','ENTREPRISE DES P ET T','LUXEMBOURG','',NULL,'2025-10-23 11:52:02.094260');

	 
@Table(name = "bank_info")
@NoArgsConstructor
@Data
@AllArgsConstructor
@Entity
public class BankInfo {

    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    @Column(name = "id", updatable = false, nullable = false)
    private Long id;

    private String bic;
    private String name;
    private String institution;
    private String address1;
    private String location;
    private Boolean canDoSct;
    private Boolean canDoCoreSdd;
    private Boolean canDoB2bSdd;
    private LocalDateTime createdAt;
    private LocalDateTime updatedAt;
    private String countryIso2;
    @OneToMany(mappedBy = "bankInfo", cascade = CascadeType.ALL)
    private List<BankAgency> bankAgencies = new ArrayList<>();
    @Lob
    @Column(name = "searchResult", columnDefinition = "LONGTEXT")
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

    public BankInfo(String bic, String name, String institution, String address1, String location, Boolean canDoCoreSdd, Boolean canDoSct, Boolean canDoB2bSdd, String countryIso2) {
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
i think that there is an issue with the boolean value
