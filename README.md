databaseChangeLog:
  - changeSet:
      id: 1-baseline-schema
      author: olkypay
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
      author: olkypay
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
                  value: 1

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
                value: 1

        - insert:
            tableName: authorities
            columns:
              - column:
                name: authority
                value: OLKY_ADMIN
              - column:
                name: username
                value: tournesol

        - insert:
            tableName: authorities
            columns:
              - column:
                name: authority
                value: OLKY_ADMIN
              - column:
                name: username
                value: bitbang

fix it here this :
Caused by: java.sql.SQLSyntaxErrorException: (conn=36) You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near 'null, enabled) VALUES (NULL, '1')' at line 1
	at org.mariadb.jdbc.export.ExceptionFactory.createException(ExceptionFactory.java:289) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.export.ExceptionFactory.create(ExceptionFactory.java:378) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.message.ClientMessage.readPacket(ClientMessage.java:189) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readPacket(StandardClient.java:1235) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResults(StandardClient.java:1174) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResponse(StandardClient.java:1093) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.execute(StandardClient.java:1017) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.executeInternal(Statement.java:1034) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:1163) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:502) ~[mariadb-java-client-3.4.1.jar:na]
	at com.zaxxer.hikari.pool.ProxyStatement.execute(ProxyStatement.java:94) ~[HikariCP-5.1.0.jar:na]
	at com.zaxxer.hikari.pool.HikariProxyStatement.execute(HikariProxyStatement.java) ~[HikariCP-5.1.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor$ExecuteStatementCallback.doInStatement(JdbcExecutor.java:491) ~[liquibase-core-4.30.0.jar:na]
	... 68 common frames omitted

Disconnected from the target VM, address: '127.0.0.1:63335', transport: 'socket'

Process finished with exit code 0
i have this issue  can you fix all the baseline 
