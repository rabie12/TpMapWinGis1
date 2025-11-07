package eu.olkypay.bankInfo.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;


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
    @Column(name = "address1", columnDefinition = "LONGTEXT")
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


}

INSERT INTO external_bank_data.bank_info (id, bic, name, institution, address1, location, can_do_sct, can_do_core_sdd, can_do_b2b_sdd, country_iso_2, created_at, updated_at, search_result) VALUES(0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL); voila à quoi ressemble un instrt
est-ceje dois modifier lechanmps ici pour l'adapter à la db
