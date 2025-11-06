Perfect! Let’s make a fully working Spring Boot + Liquibase setup using your tables and bank data. This version avoids all path issues by using classpath references and ensures the change sets execute in the right order.

⸻

Folder structure

src
 └── main
     └── resources
         └── db
             └── changelog
                 ├── master.yaml
                 ├── baseline.yaml
                 ├── changeSet
                 │    └── 01-populate-bankinfo.yaml
                 └── data
                      ├── bank_info.sql
                      └── bank_agency.sql


⸻

1️⃣ master.yaml

This is the top-level changelog Spring Boot will use:

databaseChangeLog:
  - include:
      file: db/changelog/baseline.yaml
      relativeToChangelogFile: false

  - include:
      file: db/changelog/changeSet/01-populate-bankinfo.yaml
      relativeToChangelogFile: false


⸻

2️⃣ baseline.yaml

Creates all tables:

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


⸻

3️⃣ 01-populate-bankinfo.yaml

Loads your SQL files:

databaseChangeLog:
  - changeSet:
      id: 01-populate-bankinfo
      author: RHI
      changes:
        - sqlFile:
            path: classpath:db/changelog/data/bank_info.sql
            relativeToChangelogFile: false
            endDelimiter: ";"

        - sqlFile:
            path: classpath:db/changelog/data/bank_agency.sql
            relativeToChangelogFile: false
            endDelimiter: ";"

✅ Important: both SQL files must be under src/main/resources/db/changelog/data/.

⸻

4️⃣ Spring Boot configuration

In application.properties:

spring.liquibase.change-log=classpath:db/changelog/master.yaml

	•	This ensures SpringLiquibase uses the master file, which includes both baseline and population change sets.

⸻

5️⃣ Verify packaging

After mvn clean package, run:

jar tf target/*.jar | grep db/changelog/data/

You should see:

db/changelog/data/bank_info.sql
db/changelog/data/bank_agency.sql

If they appear, everything is correctly packaged.

⸻

With this setup:
	•	Tables are created first (baseline.yaml)
	•	Data is inserted second (01-populate-bankinfo.yaml)
	•	No NullPointerException or file not found errors
	•	Works fully in Spring Boot

⸻

If you want, I can also convert your CSV content into SQL inserts for bank_info.sql and bank_agency.sql so it’s ready to run.

Do you want me to do that?