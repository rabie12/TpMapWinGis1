I want to reduce the number of changeset : 

databaseChangeLog:
  - include:
      file: db/changelog/baseline.yaml
  - include:
      file: db/changelog/changeSet/001-add-appclient-table.yaml
  - include:
      file: db/changelog/changeSet/002-delete-company-and-person-table.yaml
  - include:
      file : db/changelog/changeSet/003-add-legalentity-table.yaml
  - include:
      file : db/changelog/changeSet/007-fix-some-bugs.yaml
  - include:
      file: db/changelog/changeSet/004-add-connector-type-field.yaml
  - include:
      file: db/changelog/changeSet/005-add-alert-table.yaml
  - include:
      file: db/changelog/changeSet/006-add-app-legal-entity-link.yaml
  - include:
      file: db/changelog/changeSet/008-add-formejuriqueINPI-table.yaml
  - include:
      file: db/changelog/changeSet/009-add-country-field-legalentity.yaml
  - include:
      file: db/changelog/changeSet/010-add-call-back-url-field.yaml
  - include :
      file: db/changelog/changeSet/011-add-secret-key-field.yaml
  - include :
      file : db/changelog/changeSet/012-rename-table-formejuridiqueINPI.yaml
  - include :
      file : db/changelog/changeSet/013-change-type-capital.yaml
  - include :
      file : db/changelog/changeSet/014-delete-unique-constraint-representative.yaml
  - include :
      file : db/changelog/changeSet/015-add-status-fiel-in-company.yaml
  - include :
      file : db/changelog/changeSet/016-add-createdAt-and-updatedAt-field-in-company.yaml
  - include :
      file : db/changelog/changeSet/017-delete-unique-constraint-document.yaml
  - include :
      file : db/changelog/changeSet/018-delete-unique-constraint-beneficial-owner.yaml
  - include:
      file: db/changelog/changeSet/019-change-type-alert-content.yaml


    databaseChangeLog:
  - changeSet:
      id: 001-add-appclient-table.yaml
      author: ILS
      changes:
        - createTable:
            tableName: app_client
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: api_key
                  type: UUID
              - column:
                  name: name
                  type: VARCHAR(255)
              - column:
                  defaultValueComputed: 'NULL'
                  name: created_at
                  type: datetime(6)
              - column:
                  defaultValueComputed: 'NULL'
                  name: updated_at
                  type: datetime(6)

    databaseChangeLog:
  - changeSet:
      id: 002-delete-company-and-person-table.yaml
      author: ILS
      changes:
        - dropTable:
            tableName: person
        - dropTable:
            tableName: company
    databaseChangeLog:
  - changeSet:
      id: 003-add-legalentity-table.yaml
      author: ILS
      changes:
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
                  type: INT
              - column:
                  name: registration_country
                  type: VARCHAR(255)
              - column:
                  name: activity_code
                  type: VARCHAR(255)
              - column:
                  defaultValueComputed: 'NULL'
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
                  constraints:
                    unique: true
                    foreignKeyName: fk_address_legal_entity
                    references: address(id)
                  defaultValueComputed: 'NULL'
                  name: address
                  type: BIGINT
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
                  defaultValueComputed: 'NULL'
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
                  constraints:
                    unique: true
                    foreignKeyName: fk_address_natural_person
                    references: address(id)
                  defaultValueComputed: 'NULL'
                  name: address
                  type: BIGINT
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
                  defaultValueComputed: 'NULL'
                  name: creation_date
                  type: datetime(6)
              - column:
                  defaultValueComputed: 'NULL'
                  name: updated_date
                  type: datetime(6)
              - column:
                  constraints:
                    unique: true
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
              - addForeignKeyConstraint:
                  baseTableName: document
                  baseColumnNames: legal_entity_id
                  constraintName: fk_document_legal_entity
                  referencedTableName: legal_entity
                  referencedColumnNames: identifier
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
                  constraints:
                    unique: true
                    foreignKeyName: fk_natural_person_representative
                    references: natural_person(id)
                  defaultValueComputed: 'NULL'
                  name: natural_person_id
                  type: BIGINT
              - column:
                  constraints:
                    unique: true
                    foreignKeyName: fk_legal_entity_representative
                    references: legal_entity(identifier)
                  defaultValueComputed: 'NULL'
                  name: legal_entity_id
                  type: VARCHAR(255)
              - column:
                  constraints:
                    unique: true
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
              - addForeignKeyConstraint:
                  baseColumnNames: legal_entity_parent_id
                  baseTableName: representative
                  constraintName: fk_representative_legal_entity_parent
                  referencedColumnNames: identifier
                  referencedTableName: legal_entity

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
                  defaultValueComputed: 'NULL'
                  name: start_date
                  type: datetime(6)
              - column:
                  defaultValueComputed: 'NULL'
                  name: end_date
                  type: datetime(6)
              - column:
                  constraints:
                    unique: true
                    foreignKeyName: fk_natural_person_beneficial_owner
                    references: natural_person(id)
                  defaultValueComputed: 'NULL'
                  name: natural_person_id
                  type: BIGINT
              - column:
                  constraints:
                    unique: true
                    foreignKeyName: fk_legal_entity_beneficial_owner
                    references: legal_entity(identifier)
                  defaultValueComputed: 'NULL'
                  name: legal_entity_id
                  type: VARCHAR(255)
              - column:
                  constraints:
                    unique: true
                  defaultValueComputed: 'NULL'
                  name: legal_entity_parent_id
                  type: VARCHAR(255)
              - addForeignKeyConstraint:
                  baseColumnNames: legal_entity_parent_id
                  baseTableName: beneficial_owner
                  constraintName: fk_beneficial_owner_legal_entity_parent
                  referencedColumnNames: identifier
                  referencedTableName: legal_entity

    databaseChangeLog:
  - changeSet:
      id: 004-add-connector-type-field.yaml
      author: ILS
      changes:
      - createTable:
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
          tableName: connector_type
      - createTable:
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
          tableName: connector_connector_type
      - addPrimaryKey:
          tableName: connector_connector_type
          constraintName: pk_connector_connector_type
          columnNames: connector_id, connector_type_id
      - insert:
          tableName: connector_type
          columns:
            - column:
                name: name
                value : "REFERENTIAL"
      - insert:
          tableName: connector_type
          columns:
            - column:
                name: name
                value: "MONITORING"

        databaseChangeLog:
  - changeSet:
      id: 005-add-alert-table.yaml
      author: ILS
      changes:
        - createTable:
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: VARCHAR(255)
              - column:
                  name: origin
                  type: VARCHAR(255)
              - column:
                  name: type
                  type: VARCHAR(255)
              - column:
                  name: content
                  type: VARCHAR(1000)
              - column:
                  name: created_at
                  type: datetime(6)
              - column:
                  name: updated_at
                  type: datetime(6)
              - column:
                  name: legal_entity_id
                  type: VARCHAR(255)
              - addForeignKeyConstraint:
                  baseTableName: alert
                  baseColumnNames: legal_entity_id
                  constraintName: fk_alert_legal_entity
                  referencedTableName: legal_entity
                  referencedColumnNames: identifier
            tableName: alert

databaseChangeLog:
  - changeSet:
      id: 006-add-app-legal-entity-link.yaml
      author: ILS
      changes:
        - createTable:
            columns:
              - column:
                  name: app_client_id
                  type: BIGINT
                  constraints:
                    foreignKeyName: fk_app_client_id
                    references: app_client(id)
              - column:
                  name: legal_entity_id
                  type: VARCHAR(255)
                  constraints:
                    foreignKeyName: fk_legal_entity_id
                    references: legal_entity(identifier)
            tableName: legal_entity_monitoring
        - addPrimaryKey:
            tableName: legal_entity_monitoring
            constraintName: pk_legal_entity_monitoring
            columnNames: app_client_id, legal_entity_id
    databaseChangeLog:
  - changeSet:
      id: 007-fix-some-bugs.yaml
      author: ILS
      changes:
        - addColumn:
            tableName : connector
            columns:
              - column:
                  name: active
                  type: BOOLEAN
        - addColumn:
            tableName: app_client
            columns:
                - column:
                    constraints:
                      nullable: false
                    name: id
                    type: BIGINT
        - dropPrimaryKey:
            tableName: app_client
        - addPrimaryKey:
            tableName: app_client
            columnNames: id
        - addAutoIncrement:
            tableName: app_client
            columnName: id
            columnDataType: BIGINT
    databaseChangeLog:
  - changeSet:
      id: 008-add-formejuriqueINPI-table.yaml
      author: ILS
      changes:
        - createTable:
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
            tableName: INPI_legal_form
        - loadData:
            tableName: INPI_legal_form
            separator: ;
            relativeToChangelogFile : true
            file : legalForm_matching.csv
            columns:
              - column:
                  header: Code
                  name: code
                  type: INT
              - column:
                  header: Libelle
                  name: label
                  type: VARCHAR(400)

    databaseChangeLog:
  - changeSet:
      id: 009-add-country-field-legalentity.yaml
      author: ILS
      changes:
        - addColumn :
            tableName: legal_entity
            columns:
              - column:
                  name : country
                  type: VARCHAR(255)

    databaseChangeLog:
  - changeSet:
      id: 010-add-call-back-url-field.yaml
      author: ILS
      changes:
        - addColumn :
            tableName: app_client
            columns:
              - column:
                  name : call_back_url
                  type: VARCHAR(255)
    databaseChangeLog:
  - changeSet:
      id: 011-add-secret-key-field.yaml
      author: ILS
      changes:
        - addColumn :
            tableName: app_client
            columns:
              - column:
                  name : secret_key
                  type: UUID
    databaseChangeLog:
  - changeSet:
      id: 012-rename-table-formejuridiqueINPI.yaml
      author: ILS
      changes:
        - renameTable:
            newTableName: inpi_legal_form
            oldTableName: INPI_legal_form
    databaseChangeLog:
  - changeSet:
      id: 013-change-type-capital.yaml
      author: ILS
      changes:
          - modifyDataType:
              columnName: capital
              newDataType: BIGINT
              tableName: legal_entity
      databaseChangeLog:
    - changeSet:
        id: 014-delete-unique-constraint-representative.yaml
        author: ILS
        changes:
          - dropForeignKeyConstraint:
                baseTableName: representative
                constraintName: fk_natural_person_representative
          - dropForeignKeyConstraint:
              baseTableName: representative
              constraintName: fk_legal_entity_representative

          - dropUniqueConstraint:
              constraintName: natural_person_id
              tableName: representative
          - dropUniqueConstraint:
              constraintName: legal_entity_id
              tableName: representative
          - dropUniqueConstraint:
              constraintName: legal_entity_parent_id
              tableName: representative

          - addForeignKeyConstraint:
              baseColumnNames: natural_person_id
              baseTableName: representative
              constraintName: fk_natural_person_representative
              referencedColumnNames: id
              referencedTableName: natural_person

          - addForeignKeyConstraint:
              baseColumnNames: legal_entity_id
              baseTableName: representative
              constraintName: fk_legal_entity_representative
              referencedColumnNames: identifier
              referencedTableName: legal_entity
          - addForeignKeyConstraint:
              baseColumnNames: legal_entity_parent_id
              baseTableName: representative
              constraintName: fk_representative_legal_entity_parent
              referencedColumnNames: identifier
              referencedTableName: legal_entity
      i will send the remaining 


    


