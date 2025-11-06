Perfect ✅ — Liquibase supports bulk data loading from CSV files via the loadData change type.
You can easily load your bank_info and bank_agency tables automatically during migration — no need for long YAML insert lists.

Here’s how to integrate your two CSV imports cleanly into your Liquibase changelog 👇

⸻

🧩 1. Folder structure

Make sure your project looks like this (typical setup):

src/
 └── main/
     └── resources/
         └── db/
             └── changelog/
                 ├── db.changelog-master.yaml
                 ├── baseline.yaml
                 ├── data/
                 │   ├── bank_info.csv
                 │   └── bank_agency.csv

✅ Keep your CSV files in a data/ subfolder inside your changelog directory.

⸻

🧾 2. Example CSV format

bank_info.csv

id,bic,name,institution,address1,location,can_do_sct,can_do_core_sdd,can_do_b2b_sdd,country_iso_2,created_at,updated_at,search_result
1,BICCODE1,Bank A,Inst A,123 Street,Paris,true,true,false,FR,2025-01-01 10:00:00,2025-01-01 10:00:00,sample data
2,BICCODE2,Bank B,Inst B,456 Road,Berlin,true,false,true,DE,2025-01-01 10:00:00,2025-01-01 10:00:00,sample data

bank_agency.csv

id,country_iso_2,bank_code,branch_code,branch_name,bank_and_branch_code,bank_info_id
1,FR,BNKA,0001,Paris Branch,BNKA0001,1
2,DE,BNKB,0002,Berlin Branch,BNKB0002,2

⚠️ Column names in the CSV must match the table column names in your Liquibase schema.

⸻

🧱 3. Add a new Liquibase changeset

At the end of your baseline.yaml, add this:

  - changeSet:
      id: 3-load-bank-data
      author: olkypay
      changes:
        - loadData:
            file: db/changelog/data/bank_info.csv
            tableName: bank_info
            separator: ","
            encoding: UTF-8

        - loadData:
            file: db/changelog/data/bank_agency.csv
            tableName: bank_agency
            separator: ","
            encoding: UTF-8

🔹 file: is a classpath-relative path
🔹 separator: is optional (default is ,)
🔹 You can also add quotchar: '"' if your CSV has quoted text.

⸻

🚀 4. Run the migration

Then execute:

./mvnw liquibase:update

Liquibase will:
	1.	Create all tables (from previous changeSets),
	2.	Load CSV data into bank_info and bank_agency.

⸻

🧠 Tip

If your CSV has a header but you don’t want to include certain columns (e.g. auto-generated IDs), simply remove them from the CSV or mark them as autoIncrement in Liquibase — Liquibase will skip empty values for those.

⸻

Would you like me to show you an example of CSVs with auto-increment IDs handled automatically (so you don’t have to include id in the CSV)?