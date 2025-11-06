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
pringframework.beans.factory.BeanCreationException: Error creating bean with name 'entityManagerFactory' defined in class path resource [org/springframework/boot/autoconfigure/orm/jpa/HibernateJpaConfiguration.class]: Failed to initialize dependency 'liquibase' of LoadTimeWeaverAware bean 'entityManagerFactory': Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.ChangeLogParseException: Error parsing classpath:/db/changelog/db.changelog-master.yaml : Error parsing db/changelog/baseline.yaml : Error parsing db/changelog/baseline.yaml: Unknown change type 'columns'. Check for spelling or capitalization errors and missing extensions such as liquibase-commercial.
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:328) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:207) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractApplicationContext.finishBeanFactoryInitialization(AbstractApplicationContext.java:970) ~[spring-context-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractAppl


	ca you review 
