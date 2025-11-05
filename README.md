Excellent question — yes ✅, Liquibase fully supports inserting initial data into your tables right after they are created.

You can do this directly in the same changelog file (or in a separate changeset), using the insert change type.
Liquibase will ensure it runs after the table is created and will only execute once.

⸻

✅ Updated spring_properties section with inserts

Here’s how you can extend your existing baseline changelog:

  - changeSet:
      id: 6-create-spring-properties
      author: RHI
      changes:
        - createTable:
            tableName: spring_properties
            columns:
              - column:
                  name: id
                  type: bigint
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
      id: 7-insert-spring-properties
      author: RHI
      changes:
        - insert:
            tableName: spring_properties
            columns:
              - column:
                  name: id
                  valueNumeric: 1
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
                  name: id
                  valueNumeric: 2
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
                  name: id
                  valueNumeric: 3
              - column:
                  name: prop_key
                  value: sepa.secret
              - column:
                  name: prop_value
                  value: 4u\\Z*4.(+ZK%P<E5mA


⸻

💡 Notes
	•	You can safely use value: for strings and valueNumeric: for numbers.
	•	Liquibase will escape the backslash automatically in YAML; no extra quoting needed.
	•	This data is tracked like schema changes — Liquibase won’t re-insert unless you remove the changeset entry from the DATABASECHANGELOG table.

⸻

🧩 Optional — For auto-increment IDs

If your spring_properties.id should auto-increment, you can skip the id columns in the inserts and let the DB generate them:

- insert:
    tableName: spring_properties
    columns:
      - column:
          name: prop_key
          value: sepa.url
      - column:
          name: prop_value
          value: https://rest.sepatools.eu


⸻

Would you like me to show you a complete merged version of your changelog (baseline + inserts + autoIncrement IDs)?