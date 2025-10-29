Perfect 🔥 — you’re thinking like a pro dev here.
You’re absolutely right: there are two valid and professional approaches to handling this situation. Let’s go through both clearly so you can apply them (and explain them to your team confidently).

⸻

🧩 1️⃣ Add the Alert table directly into your baseline

Since you already know your model and want a clean baseline (no need to apply dozens of incremental changeSets), you can merge all existing changes + the new alert entity into a single baseline.yaml.

Here’s what you’d do:

✅ Step-by-step
	1.	Keep your current database schema that already includes all entities (legal_entity, connector, app_client, etc.).
	2.	Add this directly to your baseline changelog (the consolidated file you’ll version as your new starting point):

- changeSet:
    id: 020-create-alert-table
    author: ILS
    changes:
      - createTable:
          tableName: alert
          columns:
            - column:
                constraints:
                  nullable: false
                  primaryKey: true
                name: id
                type: VARCHAR(255)
            - column:
                constraints:
                  nullable: false
                name: origin
                type: VARCHAR(255)
            - column:
                constraints:
                  nullable: false
                name: type
                type: VARCHAR(255)
            - column:
                name: legal_entity_id
                type: VARCHAR(255)
            - column:
                name: content
                type: LONGTEXT
            - column:
                name: created_at
                type: datetime(6)
            - column:
                name: updated_at
                type: datetime(6)
      - addForeignKeyConstraint:
          baseTableName: alert
          baseColumnNames: legal_entity_id
          constraintName: fk_alert_legal_entity
          referencedTableName: legal_entity
          referencedColumnNames: identifier

Now your baseline has everything in one place — ready to apply on a fresh DB or used as a reset point.

⸻

🧩 2️⃣ Use Liquibase Consolidate Command (official way)

If you want Liquibase to automatically merge all changeSets into one file (your new baseline), you can use:

liquibase changelog:consolidate

🧠 What it does:
	•	Scans your existing database changelog history.
	•	Generates one new changelog file representing your current database structure.
	•	Marks all existing changeSets as applied (baseline).
	•	Perfect for moving from “many incremental changeSets” → “one clean baseline file”.

⚙️ Typical workflow:
	1.	Make sure your DB is up-to-date:

liquibase update


	2.	Run consolidation:

liquibase changelog:consolidate --changelog-file=new-baseline.yaml


	3.	Liquibase will create a single YAML file (e.g. new-baseline.yaml) containing all the DDL that represents your current schema — including the alert table if it’s in your DB.
	4.	Once done, you can remove all old incremental changelogs and just keep:

db/changelog/new-baseline.yaml


	5.	Future changes will be added incrementally on top of that baseline.

⸻

🧩 How to Explain Both to Your Team

Approach	Description	When to Use
Manual Baseline Update	You edit your baseline manually and add the new table.	Best during active development when you control the schema manually.
Liquibase Consolidate	Liquibase auto-generates a single baseline changelog from your DB.	Best when your schema is stable, and you want to clean your changelog folder.


⸻

✅ Recommendation

Since you’re in a dev environment, I suggest:
	1.	Add the alert table manually into your baseline now (so you can test and continue developing fast).
	2.	Later, when your schema stabilizes, run changelog:consolidate to rebuild a clean single baseline for your team — and commit that as version 1.0 of your database.

⸻

Would you like me to generate the final merged baseline.yaml file (including the Alert table) so you can use it directly before consolidation?