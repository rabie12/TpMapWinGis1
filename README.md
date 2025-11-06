Ah! Now we have everything — the changelog, folder structure, CSV content, and the exact error. I can explain precisely what’s happening and give a working solution.

The root cause is Spring Boot + Liquibase cannot resolve CSV files relative to a YAML changelog using relativeToChangelogFile: true in many setups, especially when running from the IDE or a packaged JAR. This is why you keep hitting:

Cannot invoke "String.startsWith(String)" because "relativePath" is null


⸻

✅ Correct solution

1. Use classpath-based paths instead of relativeToChangelogFile:
Your CSVs are currently here:

src/main/resources/db/changelog/data/bank_info.csv
src/main/resources/db/changelog/data/bank_agency.csv

Build output (JAR) will contain:

db/changelog/data/bank_info.csv
db/changelog/data/bank_agency.csv

So, in your YAML, you must reference them via classpath:, not relative paths:

- changeSet:
    id: 3-load-bank-data
    author: RHI
    changes:
      - loadData:
          tableName: bank_info
          file: classpath:db/changelog/data/bank_info.csv
          separator: ","
          encoding: UTF-8

      - loadData:
          tableName: bank_agency
          file: classpath:db/changelog/data/bank_agency.csv
          separator: ","
          encoding: UTF-8

Do not use relativeToChangelogFile: true. Remove it entirely.

⸻

2. Optional: Remove the columns block
Your CSV has headers. Liquibase can automatically map columns based on headers. You only need the columns: block if you want to explicitly define types. To simplify:

- loadData:
    tableName: bank_info
    file: classpath:db/changelog/data/bank_info.csv
    separator: ","
    encoding: UTF-8


⸻

3. Verify CSVs are in the JAR

jar tf target/*.jar | grep bank_info.csv

You should see:

db/changelog/data/bank_info.csv
db/changelog/data/bank_agency.csv

If not, move the CSVs under src/main/resources/db/changelog/data/ and rebuild.

⸻

4. Key points

Setting	Correct Value
Path prefix	classpath:
relativeToChangelogFile	remove entirely
CSV location	src/main/resources/db/changelog/data/
File name	must exactly match (case-sensitive)


⸻

Once you do this, Spring Boot + Liquibase will successfully load your CSVs without NullPointerException.

⸻

If you want, I can rewrite your entire baseline.yaml including users, spring_properties, and CSV loading with these fixes — ready to run in Spring Boot. This will eliminate all the relativePath issues.

Do you want me to do that?