Ah — now we can pinpoint exactly what’s going wrong. This is a classic Spring Boot + Liquibase + CSV issue, and the root cause is how SpringLiquibase resolves relative paths. Let’s break it down carefully.

⸻

Problem analysis
	1.	Your changelog is here:

src/main/resources/db/changelog/baseline.yaml

	2.	Your CSVs are here:

src/main/resources/db/changelog/data/bank_info.csv
src/main/resources/db/changelog/data/bank_agency.csv

✅ This is the correct folder structure.
	3.	Your loadData in YAML:

file: data/bank_info.csv
relativeToChangelogFile: true

✅ Looks correct at first glance.
	4.	Error:

Cannot invoke "String.startsWith(String)" because "relativePath" is null

This means SpringResourceAccessor cannot resolve the file path — usually because it’s interpreting your resource as a FileSystemResource instead of a classpath resource, which happens when:
	•	You run your app from IDE with non-standard working directory, or
	•	Liquibase tries to resolve the file before it’s in the classpath, or
	•	There’s a mix of file: prefix, classpath: prefix, or relative paths.

⸻

What actually works in Spring Boot
	1.	Use classpath-based paths for Spring Boot, instead of relying on relativeToChangelogFile.
	2.	Remove relativeToChangelogFile: true and prefix the file with classpath:.

Yes, this is counterintuitive, but SpringLiquibase behaves differently than the CLI.

⸻

Fixed YAML loadData

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

Notes:
	•	Remove relativeToChangelogFile: true.
	•	Use full classpath from src/main/resources.
	•	Don’t specify columns manually unless you need type conversion. Liquibase can infer columns from CSV headers.

⸻

Why this works
	•	SpringBoot’s SpringLiquibase uses SpringResourceAccessor.
	•	relativeToChangelogFile: true only works reliably with CLI or direct filesystem, not always inside Spring Boot.
	•	Using classpath: ensures it loads resources from the JAR/resources, not from the IDE filesystem.

⸻

Checklist to ensure success
	1.	CSVs are in:

src/main/resources/db/changelog/data/

	2.	Build your project (mvn clean package) to make sure CSVs are in the JAR:

jar tf target/*.jar | grep bank_info.csv

Should show:

db/changelog/data/bank_info.csv
db/changelog/data/bank_agency.csv

	3.	YAML uses classpath::

file: classpath:db/changelog/data/bank_info.csv

	4.	Restart your Spring Boot app. ✅ Should work now.

⸻

If you want, I can rewrite your full baseline.yaml including both your inserts and CSV loading, ready-to-run in Spring Boot, so you don’t hit any more path issues.

Do you want me to do that?