

this is my property file
server.port=${APPLICATION_PORT}
spring.datasource.url=jdbc:mariadb://localhost:3306/business-registry
spring.datasource.username=root
spring.datasource.password=root
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

ive already update my app.properties file but ihave the issue below should update somwhere elese:

[ERROR] Failed to execute goal org.liquibase:liquibase-maven-plugin:4.31.1:changelogSync (default-cli) on project business-registry: The database URL has not been specified either as a parameter or in a properties file.spring.application.name=business-registry


