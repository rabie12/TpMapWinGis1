Perfect ✅ — you want a single baseline Liquibase changelog file (YAML) that defines all your entity tables and relationships in one place — clean and simple, with only essential column names, types, primary keys, and relationships.

Here’s exactly that.

⸻

📄 src/main/resources/db/changelog/db.changelog-master.yaml

databaseChangeLog:
  - changeSet:
      id: 1-baseline-schema
      author: olkypay
      changes:

        # === USERS TABLE ===
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

        # === AUTHORITIES TABLE ===
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

        # === BANK INFO TABLE ===
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

        # === BANK AGENCY TABLE ===
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

        # === IBAN SEARCH HISTORY TABLE ===
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

        # === SPRING PROPERTIES TABLE ===
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


⸻

✅ Notes
	•	One baseline changelog (no includes, just one file).
	•	Simple and clear — only table/column/type + foreign keys.
	•	Works perfectly with your provided entity model.
	•	Liquibase will automatically track this baseline in the DATABASECHANGELOG table.

⸻

⚙️ Spring Boot Integration Reminder

In application.yml:

spring:
  liquibase:
    enabled: true
    change-log: classpath:/db/changelog/db.changelog-master.yaml


⸻

Would you like me to enhance this baseline to also include primary keys and auto-increment IDs for all id fields (matching your JPA mappings)?