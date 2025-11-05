package eu.olkypay.bankInfo.model;


import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "authorities")
@Data
@NoArgsConstructor
public class Authority {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "username", nullable = false)
    private User user;

    @Column(nullable = false)
    private String authority;
}
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
package eu.olkypay.bankInfo.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.UuidGenerator;

import java.time.LocalDateTime;
import java.util.UUID;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class IbanSearchHistory {
    @Id
    @UuidGenerator
    @Column(name = "id", updatable = false, nullable = false)
    private UUID id;
    private String iban;
    private String result;
    @Lob
    @Column(name = "responseDetails", columnDefinition = "LONGTEXT")
    private String responseDetails;
    @Column(updatable = false)
    private LocalDateTime createdAt;

    @Column(nullable = false)
    private LocalDateTime updatedAt;

    @PrePersist
    protected void onCreate() {
        this.createdAt = LocalDateTime.now();
        this.updatedAt = LocalDateTime.now();
    }

    @PreUpdate
    protected void onUpdate() {
        this.updatedAt = LocalDateTime.now();
    }
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "bank_agency_id")
    private BankAgency bankAgency;

    public IbanSearchHistory(String iban, String result, String responseDetails, LocalDateTime updatedAt, BankAgency bankAgency) {
        this.iban = iban;
        this.result = result;
        this.responseDetails = responseDetails;
        this.updatedAt = updatedAt;
        this.bankAgency = bankAgency;
    }
}

package eu.olkypay.bankInfo.model;

import jakarta.persistence.*;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "spring_properties")
@NoArgsConstructor
@Data
@AllArgsConstructor
public class SpringProperty {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "prop_key", unique = true, nullable = false)
    private String propKey;

    @Column(name = "prop_value", nullable = false)
    private String propValue;

}

package eu.olkypay.bankInfo.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.Set;

@Entity
@Table(name = "users")
@Getter
@Setter
@NoArgsConstructor
public class User {

    @Id
    private String username;

    @Column(nullable = false)
    private String password;

    @Column(nullable = false)
    private boolean enabled = true;

    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, fetch = FetchType.EAGER)
    private Set<Authority> authorities;

}

I want to imprment the liquibase for this db below my properties adjust it and then use

server:
  port: 9005
spring:
  application:
    name: bankInfo
  datasource:
    url: ${DB_URL}
    username: ${DB_USERNAME}
    password: ${DB_PASSWORD}
    driver-class-name: org.mariadb.jdbc.Driver
    hikari:
      pool-name: AtkHikariPool
      maximum-pool-size: '10'
      minimum-idle: '5'
      idle-timeout: '30000'
      max-lifetime: '1800000'
      connection-timeout: '30000'
      leak-detection-threshold: '200000'
      validation-timeout: '5000'
  jpa:
    properties:
      hibernate:
        dialect: org.hibernate.dialect.MariaDBDialect
    show-sql: true
    generate-ddl: false
    hibernate:
      ddl-auto: update
  springdoc:
    api-docs:
      path: /bankinfo-docs
      enabled: true
logging:
  level:
    org:
      springframework:
        security: DEBUG

        



<?xml version="1.0" encoding="UTF-8"?>
<project xmlns="http://maven.apache.org/POM/4.0.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 https://maven.apache.org/xsd/maven-4.0.0.xsd">
	<modelVersion>4.0.0</modelVersion>
	<packaging>jar</packaging>
	<parent>
		<groupId>org.springframework.boot</groupId>
		<artifactId>spring-boot-starter-parent</artifactId>
		<version>3.4.3</version>
		<relativePath/> <!-- lookup parent from repository -->
	</parent>
	<groupId>eu.olkypay</groupId>
	<artifactId>bankInfo</artifactId>
	<version>0.0.1-SNAPSHOT</version>
	<name>bankInfo</name>
	<description>Bank Info</description>
	<url/>
	<licenses>
		<license/>
	</licenses>
	<developers>
		<developer/>
	</developers>
	<scm>
		<connection/>
		<developerConnection/>
		<tag/>
		<url/>
	</scm>
	<properties>
		<java.version>21</java.version>
		<org.mapstruct.version>1.6.3</org.mapstruct.version>
		<project.build.finalName>bank-info</project.build.finalName>
	</properties>

	<distributionManagement>
		<repository>
			<id>central</id>
			<name>olky-local-releases</name>
			<url>https://newartifactory.olkypay.local:443/artifactory/libs-release-local</url>
		</repository>
		<snapshotRepository>
			<id>snapshots</id>
			<name>olky-local-snapshot</name>
			<url>https://newartifactory.olkypay.local:443/artifactory/libs-snapshot-local</url>
		</snapshotRepository>
	</distributionManagement>

	<dependencies>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-data-jpa</artifactId>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-validation</artifactId>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-web</artifactId>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-webflux</artifactId>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-web-services</artifactId>
		</dependency>
		<dependency>
			<groupId>com.neovisionaries</groupId>
			<artifactId>nv-i18n</artifactId>
			<version>1.29</version>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-security</artifactId>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-devtools</artifactId>
			<scope>runtime</scope>
			<optional>true</optional>
		</dependency>
		<dependency>
			<groupId>org.mariadb.jdbc</groupId>
			<artifactId>mariadb-java-client</artifactId>
			<scope>runtime</scope>
		</dependency>
		<dependency>
			<groupId>org.projectlombok</groupId>
			<artifactId>lombok</artifactId>
			<optional>true</optional>
		</dependency>
		<dependency>
			<groupId>org.mapstruct</groupId>
			<artifactId>mapstruct</artifactId>
			<version>${org.mapstruct.version}</version>
		</dependency>
		<dependency>
			<groupId>org.mapstruct</groupId>
			<artifactId>mapstruct-processor</artifactId>
			<version>${org.mapstruct.version}</version>
			<scope>provided</scope>
		</dependency>

		<dependency>
			<groupId>org.projectlombok</groupId>
			<artifactId>lombok-mapstruct-binding</artifactId>
			<version>0.2.0</version>
			<scope>provided</scope>
		</dependency>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-test</artifactId>
			<scope>test</scope>
		</dependency>
		<dependency>
			<groupId>org.iban4j</groupId>
			<artifactId>iban4j</artifactId>
			<version>3.2.11-RELEASE</version>
		</dependency>
		<!-- https://mvnrepository.com/artifact/io.projectreactor/reactor-core -->
		<dependency>
			<groupId>io.projectreactor</groupId>
			<artifactId>reactor-test</artifactId>
			<scope>test</scope>
		</dependency>
		<dependency>
			<groupId>org.springdoc</groupId>
			<artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
			<version>2.8.4</version>
		</dependency>
		<dependency>
			<groupId>org.springdoc</groupId>
			<artifactId>springdoc-openapi-starter-webflux-ui</artifactId>
			<version>2.8.4</version>
		</dependency>
		<dependency>
			<groupId>org.mockito</groupId>
			<artifactId>mockito-core</artifactId>
			<version>5.7.0</version>
			<scope>test</scope>
		</dependency>

		<dependency>
			<groupId>org.mockito</groupId>
			<artifactId>mockito-inline</artifactId>
			<version>5.2.0</version>
			<scope>test</scope>
		</dependency>
	</dependencies>

	<build>
		<finalName>${project.build.finalName}</finalName>
		<plugins>
			<plugin>
				<groupId>org.apache.maven.plugins</groupId>
				<artifactId>maven-compiler-plugin</artifactId>
				<configuration>
					<annotationProcessorPaths>
						<path>
							<groupId>org.springframework.boot</groupId>
							<artifactId>spring-boot-configuration-processor</artifactId>
						</path>
						<path>
							<groupId>org.projectlombok</groupId>=
							<artifactId>lombok</artifactId>
							<version>${lombok.version}</version>
						</path>
						<path>
							<groupId>org.mapstruct</groupId>
							<artifactId>mapstruct-processor</artifactId>
							<version>${org.mapstruct.version}</version>
						</path>
					</annotationProcessorPaths>
					<source>21</source>
					<target>21</target>
					<compilerArgs>--enable-preview</compilerArgs>
				</configuration>
			</plugin>
			<plugin>
				<groupId>org.springframework.boot</groupId>
				<artifactId>spring-boot-maven-plugin</artifactId>
				<configuration>
					<excludes>
						<exclude>
							<groupId>org.projectlombok</groupId>
							<artifactId>lombok</artifactId>
						</exclude>
					</excludes>
				</configuration>
			</plugin>
			<plugin>
			<groupId>org.apache.maven.plugins</groupId>
			<artifactId>maven-surefire-plugin</artifactId>
			<version>3.0.0-M7</version>

			</plugin>
		</plugins>
	</build>

</project>
adjust on pom as well
