Yes — you’ve provided a Liquibase changelog (YAML) and the error stack trace showing a ChangeLogParseException with the message:

Unknown change type 'columns' ... check for spelling or capitalization errors

This error means Liquibase doesn’t recognize the structure of your YAML file — specifically, the nesting around columns:. Let’s review and fix it carefully.

⸻

🧩 Root Cause

In several places (for example, under insert:), you have this incorrect structure:

- insert:
    tableName: spring_properties
    columns:
      - column:
          name: prop_key
          value: sepa.url
      - column:
          name: prop_value
          value: https://rest.sepatools.eu

✅ This part is fine.

❌ However, elsewhere (like the later inserts for users), the indentation or dash placement is broken:

- insert:
  tableName: users
  columns:
    - column:
        name: username
        value: tournesol

Notice that tableName and columns are not properly indented under insert: — Liquibase treats columns as a top-level key, not part of the insert object.
This triggers:

Unknown change type 'columns'

⸻

✅ Corrected Version (key structure fixed)

Here’s the corrected snippet for the problematic part — properly aligned:

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


⸻

🧠 Summary of Fixes
	1.	Ensure consistent indentation (2 spaces per level recommended).
	2.	Every change (createTable, insert, etc.) must start with - under changes:.
	3.	Inside insert, both tableName and columns must be indented under insert:.
	4.	Avoid mixing tabs and spaces — Liquibase YAML parsing is sensitive to that.

⸻

✅ Next Step

After correcting indentation, run:

mvn clean spring-boot:run

or

./mvnw liquibase:update

You should see Liquibase applying your baseline successfully.

⸻

If you’d like, I can produce a fully corrected and validated Liquibase YAML file (ready to copy–paste) — would you like me to do that?