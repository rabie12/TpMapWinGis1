Excellent — you want a cleaner YAML Liquibase changelog, where we only declare columns and types (without constraints like nullable, primaryKey, etc. — except where necessary).

Here’s the simplified version of your Liquibase changelog in YAML format for all your entities.

⸻

📄 src/main/resources/db/changelog/db.changelog-master.yaml

databaseChangeLog:
  - include:
      file: db/changelog/db.changelog-1.0-init.yaml


⸻

📄 src/main/resources/db/changelog/db.changelog-1.0-init.yaml

databaseChangeLog:
  - changeSet:
      id: 1-create-users
      author: olkypay
      changes:
        - createTable:
            tableName: users
            columns:
              - column:
                  name: username
                  type: varchar(255)
              - column:
                  name: password
                  type: varchar(255)
              - column:
                  name: enabled
                  type: boolean

  - changeSet:
      id: 2-create-authorities
      author: olkypay
      changes:
        - createTable:
            tableName: authorities
            columns:
              - column:
                  name: id
                  type: bigint
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

  - changeSet:
      id: 3-create-bank-info
      author: olkypay
      changes:
        - createTable:
            tableName: bank_info
            columns:
              - column:
                  name: id
                  type: bigint
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

  - changeSet:
      id: 4-create-bank-agency
      author: olkypay
      changes:
        - createTable:
            tableName: bank_agency
            columns:
              - column:
                  name: id
                  type: bigint
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

  - changeSet:
      id: 5-create-iban-search-history
      author: olkypay
      changes:
        - createTable:
            tableName: iban_search_history
            columns:
              - column:
                  name: id
                  type: char(36)
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

  - changeSet:
      id: 6-create-spring-properties
      author: olkypay
      changes:
        - createTable:
            tableName: spring_properties
            columns:
              - column:
                  name: id
                  type: bigint
              - column:
                  name: prop_key
                  type: varchar(255)
              - column:
                  name: prop_value
                  type: varchar(255)

Absolutely — let’s continue the Liquibase YAML changelog from where it cut off 👇

⸻

🧱 db.changelog-1.0-init.yaml (continued)

              - column:
                  name: search_result
                  type: longtext

  - changeSet:
      id: 4-create-bank-agency
      author: olkypay
      changes:
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
              - column: { name: country_iso_2, type: varchar(10) }
              - column: { name: bank_code, type: varchar(50) }
              - column: { name: branch_code, type: varchar(50) }
              - column: { name: branch_name, type: varchar(255) }
              - column: { name: bank_and_branch_code, type: varchar(100) }
              - column: { name: bank_info_id, type: bigint }

        - addForeignKeyConstraint:
            baseTableName: bank_agency
            baseColumnNames: bank_info_id
            referencedTableName: bank_info
            referencedColumnNames: id
            constraintName: fk_agency_bankinfo

  - changeSet:
      id: 5-create-iban-search-history
      author: olkypay
      changes:
        - createTable:
            tableName: iban_search_history
            columns:
              - column:
                  name: id
                  type: char(36)
                  constraints:
                    primaryKey: true
                    nullable: false
              - column: { name: iban, type: varchar(255) }
              - column: { name: result, type: varchar(255) }
              - column: { name: response_details, type: longtext }
              - column: { name: created_at, type: datetime }
              - column: { name: updated_at, type: datetime }
              - column: { name: bank_agency_id, type: bigint }

        - addForeignKeyConstraint:
            baseTableName: iban_search_history
            baseColumnNames: bank_agency_id
            referencedTableName: bank_agency
            referencedColumnNames: id
            constraintName: fk_history_agency

  - changeSet:
      id: 6-create-spring-properties
      author: olkypay
      changes:
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
                  constraints:
                    nullable: false
                    unique: true
              - column:
                  name: prop_value
                  type: varchar(255)
                  constraints:
                    nullable: false


⸻

✅ Final Liquibase Setup Summary

Folder structure:

src/main/resources/
└── db/
    └── changelog/
        ├── db.changelog-master.yaml
        └── db.changelog-1.0-init.yaml

application.yml

spring:
  liquibase:
    enabled: true
    change-log: classpath:/db/changelog/db.changelog-master.yaml

pom.xml

<dependency>
  <groupId>org.liquibase</groupId>
  <artifactId>liquibase-core</artifactId>
</dependency>


⸻

🧠 Notes & Best Practices
	•	Run your app once — Liquibase will automatically create all tables and track applied changes in the DATABASECHANGELOG tables.
	•	You can add future migrations by creating new changelog files like:

db.changelog-1.1-add-indexes.yaml
db.changelog-1.2-new-table.yaml

and include them in db.changelog-master.yaml.

⸻

Would you like me to also generate a rollback section for each change set (so you can revert schema changes safely)?
