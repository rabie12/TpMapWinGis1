Ah — now it’s crystal clear. The problem isn’t your YAML syntax anymore — it’s a classpath/resource resolution issue with Spring Boot + Liquibase. Even when you switch to sqlFile, SpringBoot cannot find your files at runtime. Here’s why and how to fix it definitively.

⸻

1️⃣ Key cause
	•	SpringLiquibase runs inside Spring Boot and only sees files that are actually in the runtime classpath.
	•	Files like bank_info.csv or bank_agency.sql must be under src/main/resources and referenced via full classpath.
	•	Paths like bank_agency.sql or db/changelog/data/bank_agency.sql without classpath: will fail in Spring Boot.

Even relativeToChangelogFile: false won’t help if the file is missing from the classpath.

⸻

2️⃣ Correct approach with SQL files

Assume this folder structure:

src
 └── main
     └── resources
         └── db
             └── changelog
                 ├── baseline.yaml
                 └── sql
                     ├── bank_info.sql
                     └── bank_agency.sql

Files must be physically in resources so Spring Boot packs them into the JAR.

⸻

3️⃣ Update YAML to use classpath explicitly

- changeSet:
    id: 3-load-bank-data
    author: RHI
    changes:
      - sqlFile:
          path: classpath:db/changelog/sql/bank_info.sql
          relativeToChangelogFile: false
          endDelimiter: ";"

      - sqlFile:
          path: classpath:db/changelog/sql/bank_agency.sql
          relativeToChangelogFile: false
          endDelimiter: ";"

✅ Notes:
	1.	classpath: is mandatory — tells SpringLiquibase to look inside the JAR/resources.
	2.	Remove any other prefixes like file:.
	3.	Do not mix relativeToChangelogFile with classpath:.

⸻

4️⃣ Verify the files are packaged

Run:

mvn clean package
jar tf target/*.jar | grep bank_agency.sql

Expected output:

db/changelog/sql/bank_agency.sql
db/changelog/sql/bank_info.sql

	•	If nothing appears, the files are not in the JAR, which explains why SpringLiquibase cannot find them.

⸻

5️⃣ Optional debug: check classpath at runtime

Add this snippet to any Spring Boot component:

System.out.println(getClass().getClassLoader().getResource("db/changelog/sql/bank_info.sql"));

	•	Should print something like: jar:file:/path/to/target/app.jar!/db/changelog/sql/bank_info.sql
	•	If it prints null, the file is not on the runtime classpath — that’s the core problem.

⸻

6️⃣ Bottom line
	•	Move your CSV/SQL files into src/main/resources.
	•	Use classpath: prefix in YAML.
	•	Remove relativeToChangelogFile.
	•	Verify files are actually packaged in the JAR.

Once this is done, both loadData (CSV) and sqlFile (SQL) will work in Spring Boot.

⸻

If you want, I can generate a full working baseline.yaml for your project that:
	•	Creates all tables
	•	Inserts your static data
	•	Loads bank info and bank agency via SQL
	•	Guaranteed to work in Spring Boot without any path issues.

Do you want me to create that?