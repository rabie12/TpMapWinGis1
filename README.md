That NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null almost never comes from your YAML syntax or CSV content—it’s a Liquibase classpath resolution bug that appears when Spring Boot’s SpringLiquibase bean can’t resolve the path you gave to loadData.
Here’s how to make it work reliably in Spring Boot + Liquibase 4.30.

⸻

✅ 1. Put the CSVs in the right place

src/
 └── main/
     └── resources/
         └── db/
             └── changelog/
                 ├── db.changelog-master.yaml
                 ├── baseline.yaml
                 └── data/
                     ├── bank_info.csv
                     └── bank_agency.csv

Make sure both files are inside src/main/resources, and that the resources folder is correctly marked as a Resources Root in your IDE so it’s on the runtime classpath.

⸻

✅ 2. Reference them with a class-path URL, not a relative path

Change your loadData blocks to use an absolute classpath URI:

  - changeSet:
      id: 3-load-bank-data
      author: RHI
      changes:
        - loadData:
            file: "classpath:/db/changelog/data/bank_info.csv"
            tableName: bank_info
            separator: ","
            encoding: UTF-8

        - loadData:
            file: "classpath:/db/changelog/data/bank_agency.csv"
            tableName: bank_agency
            separator: ","
            encoding: UTF-8

Notice the slash right after classpath: —
classpath:/db/... → not classpath:db/....

That leading / is what Spring’s SpringResourceAccessor expects to correctly build the searchPath.

⸻

✅ 3. Double-check your Spring Boot config

In application.yml (or .properties):

spring:
  liquibase:
    change-log: classpath:/db/changelog/db.changelog-master.yaml

Again, include the leading /.

⸻

✅ 4. If you still hit the same stack trace

Add an explicit context to Liquibase so it resolves resources from its own accessor instead of Spring’s:

  - changeSet:
      id: 3-load-bank-data
      author: RHI
      context: load-data
      changes:
        - loadData:
            file: "classpath:/db/changelog/data/bank_info.csv"
            tableName: bank_info
            separator: ","
            encoding: UTF-8
        - loadData:
            file: "classpath:/db/changelog/data/bank_agency.csv"
            tableName: bank_agency
            separator: ","
            encoding: UTF-8

Then run:

./mvnw liquibase:update -Dliquibase.contexts=load-data


⸻

✅ 5. Quick sanity test

Before running the full Spring Boot app, test the changelog directly with the CLI to confirm the CSVs are discoverable:

./mvnw liquibase:update \
  -Dliquibase.changeLogFile=src/main/resources/db/changelog/db.changelog-master.yaml \
  -Dspring.config.location=classpath:/application.yml

If that works, Spring Boot will too.

⸻

🧠 TL;DR
	•	Use classpath:/... (with the slash).
	•	Ensure CSVs are in src/main/resources.
	•	Keep the same path style in both application.yml and YAML changeSet.

Once you rebuild and restart, Liquibase should print:

INFO  Successfully released change log lock
INFO  ChangeSet db/changelog/baseline.yaml::3-load-bank-data::RHI ran successfully

and your bank tables will be populated.