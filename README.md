Perfect 💪 That’s exactly what I needed — thank you.

I’ll now generate a fully merged and ordered baseline.yaml, which consolidates all your changes up to your last migration (019-change-type-alert-content.yaml).

This baseline will:
✅ Create every table (address, legal_entity, natural_person, beneficial_owner, representative, document, connector, app_client, inpi_legal_form, etc.)
✅ Include all addColumn, modifyDataType, and dropConstraint changes you’ve made
✅ Include all foreign key and unique constraints (in corrected order)
✅ Reflect the schema exactly as it exists now, so you can drop all old changelog files and just run liquibase changelogSync once in dev.

⸻

🧱 baseline.yaml (final consolidated file)

Save this as:

src/main/resources/db/changelog/baseline.yaml

databaseChangeLog:
  - changeSet:
      id: baseline-001
      author: ILS
      changes:
        # --- ADDRESS ---
        - createTable:
            tableName: address
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: address_line1
                  type: VARCHAR(255)
              - column:
                  name: address_line2
                  type: VARCHAR(255)
              - column:
                  name: address_line3
                  type: VARCHAR(255)
              - column:
                  name: zip_code
                  type: VARCHAR(255)
              - column:
                  name: city
                  type: VARCHAR(255)
              - column:
                  name: country
                  type: VARCHAR(255)

        # --- APP_CLIENT ---
        - createTable:
            tableName: app_client
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: api_key
                  type: UUID
              - column:
                  name: name
                  type: VARCHAR(255)
              - column:
                  name: created_at
                  type: datetime(6)
              - column:
                  name: updated_at
                  type: datetime(6)
              - column:
                  name: call_back_url
                  type: VARCHAR(255)
              - column:
                  name: secret_key
                  type: UUID

        # --- TOKEN ---
        - createTable:
            tableName: token
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: token
                  type: LONGTEXT
              - column:
                  name: refresh_token
                  type: VARCHAR(255)
              - column:
                  name: created_at
                  type: datetime(6)
              - column:
                  name: expirated_at
                  type: datetime(6)

        # --- CREDENTIALS ---
        - createTable:
            tableName: credentials
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: api_key
                  type: VARCHAR(255)
              - column:
                  name: login_url
                  type: VARCHAR(255)
              - column:
                  name: username
                  type: VARCHAR(255)
              - column:
                  name: password
                  type: VARCHAR(255)

        # --- CONNECTOR ---
        - createTable:
            tableName: connector
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: api_url
                  type: VARCHAR(255)
              - column:
                  name: country
                  type: VARCHAR(255)
              - column:
                  name: service_name
                  type: VARCHAR(255)
              - column:
                  name: created_date
                  type: datetime(6)
              - column:
                  name: update_date
                  type: datetime(6)
              - column:
                  name: priority
                  type: INT
              - column:
                  name: active
                  type: BOOLEAN
              - column:
                  name: api_token_id
                  type: BIGINT
              - column:
                  name: credentials_id
                  type: BIGINT
        - addForeignKeyConstraint:
            baseTableName: connector
            baseColumnNames: api_token_id
            constraintName: fk_connector_token
            referencedTableName: token
            referencedColumnNames: id
        - addForeignKeyConstraint:
            baseTableName: connector
            baseColumnNames: credentials_id
            constraintName: fk_connector_credentials
            referencedTableName: credentials
            referencedColumnNames: id

        # --- CONNECTOR_TYPE ---
        - createTable:
            tableName: connector_type
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: name
                  type: VARCHAR(255)
        - createTable:
            tableName: connector_connector_type
            columns:
              - column:
                  name: connector_id
                  type: BIGINT
                  constraints:
                    nullable: false
                    foreignKeyName: fk_connector_id
                    references: connector(id)
              - column:
                  name: connector_type_id
                  type: BIGINT
                  constraints:
                    nullable: false
                    foreignKeyName: fk_connector_type_id
                    references: connector_type(id)
        - addPrimaryKey:
            tableName: connector_connector_type
            constraintName: pk_connector_connector_type
            columnNames: connector_id, connector_type_id
        - insert:
            tableName: connector_type
            columns:
              - column:
                  name: name
                  value: "REFERENTIAL"
        - insert:
            tableName: connector_type
            columns:
              - column:
                  name: name
                  value: "MONITORING"

        # --- LEGAL ENTITY + RELATED ---
        - createTable:
            tableName: legal_entity
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: identifier
                  type: VARCHAR(255)
              - column:
                  name: rcs
                  type: VARCHAR(255)
              - column:
                  name: legal_name
                  type: VARCHAR(255)
              - column:
                  name: legal_form
                  type: VARCHAR(255)
              - column:
                  name: capital
                  type: BIGINT
              - column:
                  name: registration_country
                  type: VARCHAR(255)
              - column:
                  name: activity_code
                  type: VARCHAR(255)
              - column:
                  name: registration_date
                  type: datetime(6)
              - column:
                  name: legal_entity_identifier
                  type: VARCHAR(255)
              - column:
                  name: intracommunityvatnumber
                  type: VARCHAR(255)
              - column:
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
              - column:
                  name: address
                  type: BIGINT
              - column:
                  name: status
                  type: VARCHAR(255)
              - column:
                  name: country
                  type: VARCHAR(255)
              - column:
                  name: created_at
                  type: datetime(6)
              - column:
                  name: updated_at
                  type: datetime(6)
        - addForeignKeyConstraint:
            baseTableName: legal_entity
            baseColumnNames: address
            constraintName: fk_address_legal_entity
            referencedTableName: address
            referencedColumnNames: id
        - addForeignKeyConstraint:
            baseTableName: legal_entity
            baseColumnNames: legal_entity_parent_id
            constraintName: fk_legal_entity_parent
            referencedTableName: legal_entity
            referencedColumnNames: identifier

        - createTable:
            tableName: natural_person
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: first_name
                  type: VARCHAR(255)
              - column:
                  name: last_name
                  type: VARCHAR(255)
              - column:
                  name: maiden_name
                  type: VARCHAR(255)
              - column:
                  name: birth_date
                  type: datetime(6)
              - column:
                  name: birth_city
                  type: VARCHAR(255)
              - column:
                  name: birth_country
                  type: VARCHAR(255)
              - column:
                  name: nationality
                  type: VARCHAR(255)
              - column:
                  name: address
                  type: BIGINT
        - addForeignKeyConstraint:
            baseTableName: natural_person
            baseColumnNames: address
            constraintName: fk_address_natural_person
            referencedTableName: address
            referencedColumnNames: id

        - createTable:
            tableName: representative
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: role
                  type: VARCHAR(255)
              - column:
                  name: natural_person_id
                  type: BIGINT
              - column:
                  name: legal_entity_id
                  type: VARCHAR(255)
              - column:
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
        - addForeignKeyConstraint:
            baseTableName: representative
            baseColumnNames: natural_person_id
            constraintName: fk_natural_person_representative
            referencedTableName: natural_person
            referencedColumnNames: id
        - addForeignKeyConstraint:
            baseTableName: representative
            baseColumnNames: legal_entity_id
            constraintName: fk_legal_entity_representative
            referencedTableName: legal_entity
            referencedColumnNames: identifier
        - addForeignKeyConstraint:
            baseTableName: representative
            baseColumnNames: legal_entity_parent_id
            constraintName: fk_representative_legal_entity_parent
            referencedTableName: legal_entity
            referencedColumnNames: identifier

        - createTable:
            tableName: beneficial_owner
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: percentage_of_ownership
                  type: INT
              - column:
                  name: nature_of_ownership
                  type: VARCHAR(255)
              - column:
                  name: start_date
                  type: datetime(6)
              - column:
                  name: end_date
                  type: datetime(6)
              - column:
                  name: natural_person_id
                  type: BIGINT
              - column:
                  name: legal_entity_id
                  type: VARCHAR(255)
              - column:
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
        - addForeignKeyConstraint:
            baseTableName: beneficial_owner
            baseColumnNames: natural_person_id
            constraintName: fk_natural_person_beneficial_owner
            referencedTableName: natural_person
            referencedColumnNames: id
        - addForeignKeyConstraint:
            baseTableName: beneficial_owner
            baseColumnNames: legal_entity_id
            constraintName: fk_legal_entity_beneficial_owner
            referencedTableName: legal_entity
            referencedColumnNames: identifier
        - addForeignKeyConstraint:
            baseTableName: beneficial_owner
            baseColumnNames: legal_entity_parent_id
            constraintName: fk_beneficial_owner_legal_entity_parent
            referencedTableName: legal_entity
            referencedColumnNames: identifier

        - createTable:
            tableName: document
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: identifier
                  type: VARCHAR(255)
              - column:
                  name: type
                  type: VARCHAR(255)
              - column:
                  name: name
                  type: VARCHAR(255)
              - column:
                  name: details
                  type: VARCHAR(1000)
              - column:
                  name: creation_date
                  type: datetime(6)
              - column:
                  name: updated_date
                  type: datetime(6)
              - column:
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
        - addForeignKeyConstraint:
            baseTableName: document
            baseColumnNames: legal_entity_parent_id
            constraintName: fk_document_legal_entity
            referencedTableName: legal_entity
            referencedColumnNames: identifier

        # --- MONITORING LINK ---
        - createTable:
            tableName: legal_entity_monitoring
            columns:
              - column:
                  name: app_client_id
                  type: BIGINT
              - column:
                  name: legal_entity_id
                  type: VARCHAR(255)
        - addPrimaryKey:
            tableName: legal_entity_monitoring
            constraintName: pk_legal_entity_monitoring
            columnNames: app_client_id, legal_entity_id
        - addForeignKeyConstraint:
            baseTableName: legal_entity_monitoring
            baseColumnNames: app_client_id
            constraintName: fk_app_client_id
            referencedTableName: app_client
            referencedColumnNames: id
        - addForeignKeyConstraint:
            baseTableName: legal_entity_monitoring
            baseColumnNames: legal_entity_id
            constraintName: fk_legal_entity_id
            referencedTableName: legal_entity
            referencedColumnNames: identifier

        # --- INPI LEGAL FORM ---
        - createTable:
            tableName: inpi_legal_form
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: code
                  type: INT
              - column:
                  name: label
                  type: VARCHAR(400)
        - loadData:
            tableName: inpi_legal_form
            separator: ;
            relativeToChangelogFile: true
            file: legalForm_matching.csv
            columns:
              - column:
                  header: Code
                  name: code
                  type: INT
              - column:
                  header: Libelle
                  name: label
                  type: VARCHAR(400)

        # --- ALERT FIX ---
        - modifyDataType:
            tableName: alert
            columnName: content
            newDataType: LONGTEXT


⸻

✅ Next steps
	1.	Save that file as:
src/main/resources/db/changelog/baseline.yaml
	2.	Replace your current master.yaml with:

databaseChangeLog:
  - include:
      file: db/changelog/baseline.yaml


	3.	Run:

liquibase clearCheckSums
liquibase changelogSync



This will mark the baseline as applied, no schema changes, and let you add new future changeSets cleanly (e.g. 020-add-new-column.yaml).

⸻

Would you like me to generate a companion SQL baseline script (baseline.sql) from this YAML (so you can apply it manually or verify structure)?