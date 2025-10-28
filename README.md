Perfect! Since you’re in development and want a single consolidated baseline, I can produce a ready-to-use baseline.yaml that includes all your current changeSets in the correct order (tables → foreign keys → indexes → additional columns). This way you can remove all previous separate changeSet files and reference just this baseline.

Here’s the merged and cleaned baseline:

databaseChangeLog:

  # -------------------------------
  # 1️⃣ Company Table
  # -------------------------------
  - changeSet:
      id: baseline-001-company
      author: ILLINARES
      changes:
        - createTable:
            tableName: company
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: siren
                  type: VARCHAR(255)
              - column:
                  name: documents
                  type: VARCHAR(255)
              - column:
                  name: country
                  type: VARCHAR(255)
              - column:
                  name: intracommunityvatnumber
                  type: VARCHAR(255)
              - column:
                  name: main_address
                  type: VARCHAR(255)
              - column:
                  name: name
                  type: VARCHAR(255)

  # -------------------------------
  # 2️⃣ Connector Table
  # -------------------------------
  - changeSet:
      id: baseline-002-connector
      author: ILLINARES
      changes:
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
                  defaultValueComputed: 'NULL'
                  name: created_date
                  type: datetime(6)
              - column:
                  name: service_name
                  type: VARCHAR(255)
              - column:
                  defaultValueComputed: 'NULL'
                  name: update_date
                  type: datetime(6)
              - column:
                  name: priority
                  type: INT
              - column:
                  constraints:
                    unique: true
                  defaultValueComputed: 'NULL'
                  name: api_token_id
                  type: BIGINT
              - column:
                  constraints:
                    unique: true
                  defaultValueComputed: 'NULL'
                  name: credentials_id
                  type: BIGINT

  # -------------------------------
  # 3️⃣ Credentials Table
  # -------------------------------
  - changeSet:
      id: baseline-003-credentials
      author: ILLINARES
      changes:
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
                  name: password
                  type: VARCHAR(255)
              - column:
                  name: username
                  type: VARCHAR(255)

  # -------------------------------
  # 4️⃣ Person Table
  # -------------------------------
  - changeSet:
      id: baseline-004-person
      author: ILLINARES
      changes:
        - createTable:
            tableName: person
            columns:
              - column:
                  autoIncrement: true
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: id
                  type: BIGINT
              - column:
                  name: address
                  type: VARCHAR(255)
              - column:
                  defaultValueComputed: 'NULL'
                  name: first_name
                  type: VARBINARY(255)
              - column:
                  name: last_name
                  type: VARCHAR(255)
              - column:
                  defaultValueComputed: 'NULL'
                  name: phone_number
                  type: INT
              - column:
                  name: company
                  type: VARCHAR(255)

        # Index for company FK
        - createIndex:
            tableName: person
            indexName: idx_person_company
            columns:
              - column:
                  name: company

  # -------------------------------
  # 5️⃣ Token Table
  # -------------------------------
  - changeSet:
      id: baseline-005-token
      author: ILLINARES
      changes:
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
                  defaultValueComputed: 'NULL'
                  name: created_at
                  type: datetime(6)
              - column:
                  defaultValueComputed: 'NULL'
                  name: expirated_at
                  type: datetime(6)
              - column:
                  name: refresh_token
                  type: VARCHAR(255)
              - column:
                  name: token
                  type: LONGTEXT

  # -------------------------------
  # 6️⃣ Foreign Keys
  # -------------------------------
  - changeSet:
      id: baseline-006-fks
      author: ILLINARES
      changes:
        # person → company
        - addForeignKeyConstraint:
            baseTableName: person
            baseColumnNames: company
            referencedTableName: company
            referencedColumnNames: siren
            constraintName: fk_person_company
            onDelete: RESTRICT
            onUpdate: RESTRICT

        # connector → credentials
        - addForeignKeyConstraint:
            baseTableName: connector
            baseColumnNames: credentials_id
            referencedTableName: credentials
            referencedColumnNames: id
            constraintName: fk_connector_credentials
            onDelete: RESTRICT
            onUpdate: RESTRICT

        # connector → token
        - addForeignKeyConstraint:
            baseTableName: connector
            baseColumnNames: api_token_id
            referencedTableName: token
            referencedColumnNames: id
            constraintName: fk_connector_token
            onDelete: RESTRICT
            onUpdate: RESTRICT


⸻

✅ Notes
	1.	ChangeSet IDs are unique (baseline-001, baseline-002, …).
	2.	Order matters: tables → indexes → foreign keys.
	3.	Development environment safe: you can delete all old 6–14 changeSet files and only reference this baseline.
	4.	For existing dev DBs, run:

liquibase changelogSync

to mark all baseline changeSets as applied without touching the schema.
	5.	Future changes should not modify this baseline. Add new changeSets in separate files.

⸻

If you want, I can also merge your earlier 001–019 changeSets (including legal_entity, address, alert, representative, etc.) into a complete dev baseline in one full file, ready to use.

Do you want me to do that next?