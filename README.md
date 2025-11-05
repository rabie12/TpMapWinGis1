Perfect ✅ — below is your final complete Liquibase baseline changelog:
	•	All id columns are now autoIncrement: true
	•	All foreign key relationships are preserved
	•	The spring_properties table includes your three insert statements

This version is ready to drop into:
src/main/resources/db/changelog/db.changelog-master.yaml

⸻

📄 db.changelog-master.yaml

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
                  constraints:
                    primaryKey: true
                    nullable: false
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

        # === BANK INFO TABLE ===
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

        # === BANK AGENCY TABLE ===
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

        # === IBAN SEARCH HISTORY TABLE ===
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

        # === SPRING PROPERTIES TABLE ===
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
      id: 2-insert-spring-properties
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


⸻

⚙️ application.yml (to enable Liquibase)

spring:
  liquibase:
    enabled: true
    change-log: classpath:/db/changelog/db.changelog-master.yaml


⸻

✅ Summary

Table	ID Type	Auto Increment	FK
users	username (PK)	❌	—
authorities	bigint	✅	→ users.username
bank_info	bigint	✅	—
bank_agency	bigint	✅	→ bank_info.id
iban_search_history	char(36) (UUID)	❌	→ bank_agency.id
spring_properties	bigint	✅	— (plus 3 inserted rows)


⸻

Would you like me to also generate a Liquibase rollback section (to drop tables and data safely)?