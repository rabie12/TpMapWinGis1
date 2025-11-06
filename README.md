i did it still the same issue and ive try to put my csv on the same directpry still have the same issue can ;
  - changeSet:
      id: 3-load-bank-data
      author: RHI
      changes:
        - loadData:
            tableName: bank_info
            separator: ","
            file: bank_info.csv
            encoding: UTF-8
            columns:
