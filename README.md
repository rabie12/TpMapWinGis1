mvn liquibase:changelogSync \
  -Dliquibase.url=jdbc:mariadb://localhost:3306/business-registry \
  -Dliquibase.username=root \
  -Dliquibase.password=root \
  -Dliquibase.changeLogFile=src/main/resources/db/changelog/db.changelog-master.yaml