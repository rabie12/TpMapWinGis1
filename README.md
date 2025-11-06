databaseChangeLog:
  - changeSet:
      id: 01-populate-bankinfo.yaml
      author: RHI
      changes:
        - sqlFile:
            path: classpath:db/changelog/data/bank_info.sql
            relativeToChangelogFile: false
            endDelimiter: ";"

        - sqlFile:
            path: classpath:db/changelog/data/bank_agency.sql
            relativeToChangelogFile: false
            endDelimiter: ";"


	databaseChangeLog:
  - include:
      file: db/changelog/baseline.yaml
  - include:
      file: db/changelog/changeSet/01-populate-bankinfo.yaml


	i m not able to lunch this i make it on seperate changeset to check but its executed
