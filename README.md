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