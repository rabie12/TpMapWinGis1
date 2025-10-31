spring.application.name=business-registry
server.port=${APPLICATION_PORT}
spring.datasource.url=${DB_URL}
spring.datasource.username=${DB_USERNAME}
spring.datasource.password=${DB_PASSWORD}
spring.datasource.driver-class-name=org.mariadb.jdbc.Driver
spring.jpa.properties.hibernate.dialect=org.hibernate.dialect.MariaDBDialect
spring.jpa.hibernate.ddl-auto=none

server.error.include-message=always
management.endpoints.web.exposure.include=*
#management.endpoint.health.probes.enabled=true
management.endpoint.health.show-details=always
management.health.db.enabled=true
management.health.health.enabled=true
management.health.diskSpace.enabled=false
management.health.ssl.enabled=false
management.health.ping.enabled=false
spring.liquibase.change-log= classpath:db/changelog/db.changelog-master.yaml
springdoc.api-docs.path=/swagger
springdoc.api-docs.version=OPENAPI_3_1
springdoc.model-converters.nullable-properties=true



<?xml version="1.0" encoding="UTF-8"?>
<project xmlns="http://maven.apache.org/POM/4.0.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 https://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>
    <parent>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-starter-parent</artifactId>
        <version>4.0.0-M1</version>
        <relativePath/> <!-- lookup parent from repository -->
    </parent>

    <groupId>eu.olkypay</groupId>
    <artifactId>business-registry</artifactId>
    <version>0.0.1-SNAPSHOT</version>
    <name>business-registry</name>
    <description>Business Registry</description>
    <packaging>jar</packaging>

    <distributionManagement>
        <repository>
            <id>central</id>
            <name>olky-local-releases</name>
            <url>${env.JFROG_PLATFORM_REPO}/artifactory/libs-release-local</url>
        </repository>
        <snapshotRepository>
            <id>snapshots</id>
            <name>olky-local-snapshots</name>
            <url>${env.JFROG_PLATFORM_REPO}/artifactory/libs-snapshot-local</url>
        </snapshotRepository>
    </distributionManagement>

    <properties>
        <java.version>21</java.version>
        <org.mapstruct.version>1.6.3</org.mapstruct.version>
    </properties>

    <dependencies>
        <dependency>
            <groupId>io.jsonwebtoken</groupId>
            <artifactId>jjwt-api</artifactId>
            <version>0.13.0</version>
        </dependency>
        <dependency>
            <groupId>org.mapstruct</groupId>
            <artifactId>mapstruct</artifactId>
            <version>${org.mapstruct.version}</version>
        </dependency>
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-security</artifactId>
        </dependency>
        <dependency>
            <groupId>io.jsonwebtoken</groupId>
            <artifactId>jjwt-impl</artifactId>
            <version>0.11.5</version>
        </dependency>
        <dependency>
            <groupId>io.jsonwebtoken</groupId>
            <artifactId>jjwt-jackson</artifactId>
            <version>0.11.5</version>
        </dependency>
        <dependency>
            <groupId>commons-codec</groupId>
            <artifactId>commons-codec</artifactId>
            <version>1.15</version>
        </dependency>
        <dependency>
            <groupId>com.fasterxml.jackson.dataformat</groupId>
            <artifactId>jackson-dataformat-xml</artifactId>
            <version>2.11.1</version>
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
            <artifactId>spring-boot-starter-data-jpa</artifactId>
        </dependency>
        <dependency>
            <groupId>com.fasterxml.jackson.core</groupId>
            <artifactId>jackson-databind</artifactId>
        </dependency>
        <dependency>
            <groupId>org.liquibase</groupId>
            <artifactId>liquibase-core</artifactId>
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
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-test</artifactId>
            <scope>test</scope>
        </dependency>
        <dependency>
            <groupId>org.mockito</groupId>
            <artifactId>mockito-core</artifactId>
            <version>5.12.0</version>
        </dependency>
        <dependency>
            <groupId>org.testng</groupId>
            <artifactId>testng</artifactId>
            <version>7.1.0</version>
            <scope>test</scope>
        </dependency>
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-actuator</artifactId>
        </dependency>
        <dependency>
            <groupId>org.springdoc</groupId>
            <artifactId>springdoc-openapi-starter-webmvc-ui</artifactId>
            <version>3.0.0-M1</version>
        </dependency>
    </dependencies>

    <build>
        <finalName>${project.artifactId}</finalName>
        <plugins>
            <plugin>
                <groupId>org.apache.maven.plugins</groupId>
                <artifactId>maven-compiler-plugin</artifactId>
                <version>3.11.0</version>
                <configuration>
                    <source>${java.version}</source>
                    <target>${java.version}</target>
                    <annotationProcessorPaths>
                        <path>
                            <groupId>org.projectlombok</groupId>
                            <artifactId>lombok</artifactId>
                            <version>1.18.34</version>
                        </path>
                        <path>
                            <groupId>org.mapstruct</groupId>
                            <artifactId>mapstruct-processor</artifactId>
                            <version>${org.mapstruct.version}</version>
                        </path>
                    </annotationProcessorPaths>
                </configuration>
            </plugin>
            <plugin>
                <groupId>org.springframework.boot</groupId>
                <artifactId>spring-boot-maven-plugin</artifactId>
            </plugin>
        </plugins>
    </build>

</project>


@AllArgsConstructor
@NoArgsConstructor
@Schema(description = "Represents an address of a company or person")
public class AddressDTO {

    @Schema(
            description = "First address line (nullable)",
            type = "string",
            nullable = true,
            example = "23 RUE JEAN DIDIER"
    )
    private String addressLine1;

    @Schema(
            description = "Second address line (nullable)",
            type = "string",
            nullable = true,
            example = "Résidence Les Lilas"
    )
    private String addressLine2;

    @Schema(
            description = "Third address line (nullable)",
            type = "string",
            nullable = true,
            example = "null"
    )
    private String addressLine3;

    @Schema(
            description = "ZIP code (nullable)",
            type = "string",
            nullable = true,
            example = "75015"
    )
    private String zipCode;

    @Schema(
            description = "City (nullable)",
            type = "string",
            nullable = true,
            example = "Paris"
    )
    private String city;

    @Schema(
            description = "Country (nullable, ISO 2)",
            type = "string",
            nullable = true,
            example = "FR"
    )
    private String country;


    public AddressDTO(String city) {
        this.city = city;
    }
}


but still not bien able to hve : { "type": ["string", "null"], "example": "75015" }

