databaseChangeLog:
- changeSet:
    id: 1753783246709-6
    author: ILLINARES (generated)
    changes:
    - createTable:
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
        tableName: company
- changeSet:
    id: 1753783246709-7
    author: ILLINARES (generated)
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
        tableName: connector
- changeSet:
    id: 1753783246709-8
    author: ILLINARES (generated)
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
        tableName: credentials
- changeSet:
    id: 1753783246709-9
    author: ILLINARES (generated)
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
        tableName: person
- changeSet:
    id: 1753783246709-10
    author: ILLINARES (generated)
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
        tableName: token
- changeSet:
    id: 1753783246709-11
    author: ILLINARES (generated)
    changes:
    - createIndex:
        associatedWith: ''
        columns:
        - column:
            name: company
        indexName: FKbpbwe3hfsg98qqo3luvktsq69
        tableName: person
- changeSet:
    id: 1753783246709-12
    author: ILLINARES (generated)
    changes:
    - addForeignKeyConstraint:
        baseColumnNames: company
        baseTableName: person
        constraintName: FKbpbwe3hfsg98qqo3luvktsq69
        deferrable: false
        initiallyDeferred: false
        onDelete: RESTRICT
        onUpdate: RESTRICT
        referencedColumnNames: siren
        referencedTableName: company
        validate: true
- changeSet:
    id: 1753783246709-13
    author: ILLINARES (generated)
    changes:
    - addForeignKeyConstraint:
        baseColumnNames: credentials_id
        baseTableName: connector
        constraintName: FKnee9arsfrk9kt8wekuvla3y2d
        deferrable: false
        initiallyDeferred: false
        onDelete: RESTRICT
        onUpdate: RESTRICT
        referencedColumnNames: id
        referencedTableName: credentials
        validate: true
- changeSet:
    id: 1753783246709-14
    author: ILLINARES (generated)
    changes:
    - addForeignKeyConstraint:
        baseColumnNames: api_token_id
        baseTableName: connector
        constraintName: FKpad8su3iuv9phakkurpidyvw7
        deferrable: false
        initiallyDeferred: false
        onDelete: RESTRICT
        onUpdate: RESTRICT
        referencedColumnNames: id
        referencedTableName: token
        validate: true

this is my baseline apply all changeset in my baseline so i can remove them and refer difrectly to baseline as im still in dev environements
