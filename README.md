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
                  type: varchar(255)
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

  - changeSet:
      id: 3-load-bank-data
      author: RHI
      changes:
        - loadData:
            tableName: bank_info
            separator: ","
            relativeToChangelogFile: true
            file: data/bank_info.csv
            encoding: UTF-8
            columns:
              - column:
                  header: id
                  name: id
                  type: NUMERIC
              - column:
                  header: bic
                  name: bic
                  type: VARCHAR(50)
              - column:
                  header: name
                  name: name
                  type: VARCHAR(255)
              - column:
                  header: institution
                  name: institution
                  type: VARCHAR(255)
              - column:
                  header: address1
                  name: address1
                  type: VARCHAR(255)
              - column:
                  header: location
                  name: location
                  type: VARCHAR(255)
              - column:
                  header: can_do_sct
                  name: can_do_sct
                  type: BOOLEAN
              - column:
                  header: can_do_core_sdd
                  name: can_do_core_sdd
                  type: BOOLEAN
              - column:
                  header: can_do_b2b_sdd
                  name: can_do_b2b_sdd
                  type: BOOLEAN
              - column:
                  header: country_iso_2
                  name: country_iso_2
                  type: VARCHAR(10)
              - column:
                  header: created_at
                  name: created_at
                  type: DATETIME
              - column:
                  header: updated_at
                  name: updated_at
                  type: DATETIME
              - column:
                  header: search_result
                  name: search_result
                  type: CLOB

        - loadData:
            tableName: bank_agency
            separator: ","
            relativeToChangelogFile: true
            file: data/bank_agency.csv
            encoding: UTF-8
            columns:
              - column:
                  header: id
                  name: id
                  type: NUMERIC
              - column:
                  header: country_iso_2
                  name: country_iso_2
                  type: VARCHAR(10)
              - column:
                  header: bank_code
                  name: bank_code
                  type: VARCHAR(50)
              - column:
                  header: branch_code
                  name: branch_code
                  type: VARCHAR(50)
              - column:
                  header: branch_name
                  name: branch_name
                  type: VARCHAR(255)
              - column:
                  header: bank_and_branch_code
                  name: bank_and_branch_code
                  type: VARCHAR(100)
              - column:
                  header: bank_info_id
                  name: bank_info_id
                  type: NUMERIC

    	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.AbstractUpdateCommandStep.run(AbstractUpdateCommandStep.java:112) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.UpdateCommandStep.run(UpdateCommandStep.java:100) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.CommandScope.lambda$execute$6(CommandScope.java:231) ~[liquibase-core-4.30.0.jar:na]
	... 40 common frames omitted
Caused by: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::3-load-bank-data::RHI:
     Reason: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.startsWith(String)" because "relativePath" is null
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:821) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.visitor.UpdateVisitor.executeAcceptedChange(UpdateVisitor.java:126) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.visitor.UpdateVisitor.visit(UpdateVisitor.java:70) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.lambda$run$0(ChangeLogIterator.java:133) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.lambda$run$1(ChangeLogIterator.java:122) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:260) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:264) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.run(ChangeLogIterator.java:91) ~[liquibase-core-4.30.0.jar:na]
	... 48 common frames omitted
Caused by: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.startsWith(String)" because "relativePath" is null
	at liquibase.change.core.LoadDataChange.generateStatements(LoadDataChange.java:482) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.lambda$addSqlMdc$2(ChangeSet.java:1624) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.addSqlMdc(ChangeSet.java:1623) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:774) ~[liquibase-core-4.30.0.jar:na]
	... 63 common frames omitted
Caused by: java.lang.NullPointerException: Cannot invoke "String.startsWith(String)" because "relativePath" is null
	at org.springframework.util.StringUtils.applyRelativePath(StringUtils.java:689) ~[spring-core-6.2.3.jar:6.2.3]
	at org.springframework.core.io.FileSystemResource.createRelative(FileSystemResource.java:366) ~[spring-core-6.2.3.jar:6.2.3]
	at liquibase.integration.spring.SpringResource.resolveSibling(SpringResource.java:44) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.change.core.LoadDataChange.generateStatements(LoadDataChange.java:397) ~[liquibase-core-4.30.0.jar:na]
	... 70 common frames omitted

Disconnected from the target VM, address: '127.0.0.1:64615', transport: 'socket'

Process finished with exit code 0
my changelog and baseline are here : src\main\resources\db\changelog my csv are here src\main\resources\db\changelog\data and it still not working
