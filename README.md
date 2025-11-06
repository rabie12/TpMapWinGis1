
  - changeSet:
      id: 2-init-db-data
      author: RHI
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
                  valueBoolean: true

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
                  valueBoolean: true

        - insert:
            tableName: authorities
            columns:
              - column:
                  name: username
                  value: tournesol
              - column:
                  name: authority
                  value: OLKY_ADMIN

        - insert:
            tableName: authorities
            columns:
              - column:
                  name: username
                  value: bitbang
              - column:
                  name: authority
                  value: OLKY_ADMIN
                   what should i update on this changeset
