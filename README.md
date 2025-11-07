package eu.olkypay.bankInfo.model;

import jakarta.persistence.*;
import lombok.*;

import java.util.ArrayList;
import java.util.List;


@Entity
@Table(name = "bank_agency")
@NoArgsConstructor
@Data
@AllArgsConstructor
public class BankAgency {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    @Column(name = "id", updatable = false, nullable = false)
    private Long id;
    @Column(name = "country_iso_2")
    private String countryIso2;
    @Column(name = "bank_code")
    private String bankCode;
    @Column(name = "branch_code")
    private String branchCode;
    @Column(name = "branch_name")
    private String branchName;
    @Column(name = "bank_and_branch_code")
    private String bankAndBranchCode;
    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "bank_info_id")
    private BankInfo bankInfo;
    @OneToMany(mappedBy = "bankAgency", cascade = CascadeType.ALL)
    private List<IbanSearchHistory> searchHistories = new ArrayList<>();

}


"id","bank_and_branch_code","bank_code","branch_code","branch_name","country_iso_2","bank_info_id"
177845,,SRLG,"040011",,GB,37105
177846,,SRLG,"040049",,GB,37105
177847,,BARC,"250868",,GB,108
177848,,"01005","03331",,IT,3824

do the same bankagency
