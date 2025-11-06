	... 20 common frames omitted
Caused by: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::3-load-bank-data::RHI:
     Reason: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null
	at liquibase.command.CommandScope.lambda$execute$6(CommandScope.java:278) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:179) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.CommandScope.execute(CommandScope.java:219) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Liquibase.lambda$update$0(Liquibase.java:216) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Liquibase.runInScope(Liquibase.java:1329) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Liquibase.update(Liquibase.java:205) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Liquibase.update(Liquibase.java:188) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.integration.spring.SpringLiquibase.performUpdate(SpringLiquibase.java:310) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.integration.spring.SpringLiquibase.lambda$afterPropertiesSet$0(SpringLiquibase.java:262) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.integration.spring.SpringLiquibase.afterPropertiesSet(SpringLiquibase.java:255) ~[liquibase-core-4.30.0.jar:na]
	... 22 common frames omitted
Caused by: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::3-load-bank-data::RHI:
     Reason: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null
	at liquibase.changelog.ChangeLogIterator.run(ChangeLogIterator.java:155) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.AbstractUpdateCommandStep.lambda$run$0(AbstractUpdateCommandStep.java:114) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.AbstractUpdateCommandStep.run(AbstractUpdateCommandStep.java:112) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.UpdateCommandStep.run(UpdateCommandStep.java:100) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.CommandScope.lambda$execute$6(CommandScope.java:231) ~[liquibase-core-4.30.0.jar:na]
	... 40 common frames omitted
Caused by: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::3-load-bank-data::RHI:
     Reason: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:821) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.visitor.UpdateVisitor.executeAcceptedChange(UpdateVisitor.java:126) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.visitor.UpdateVisitor.visit(UpdateVisitor.java:70) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.lambda$run$0(ChangeLogIterator.java:133) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.lambda$run$1(ChangeLogIterator.java:122) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:260) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:264) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeLogIterator.run(ChangeLogIterator.java:91) ~[liquibase-core-4.30.0.jar:na]
	... 48 common frames omitted
Caused by: java.lang.RuntimeException: java.lang.NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null
	at liquibase.change.core.LoadDataChange.generateStatements(LoadDataChange.java:482) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.lambda$addSqlMdc$2(ChangeSet.java:1624) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.addSqlMdc(ChangeSet.java:1623) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:774) ~[liquibase-core-4.30.0.jar:na]
	... 63 common frames omitted
Caused by: java.lang.NullPointerException: Cannot invoke "String.matches(String)" because "searchPath" is null
	at liquibase.integration.spring.SpringResourceAccessor.finalizeSearchPath(SpringResourceAccessor.java:185) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.integration.spring.SpringResourceAccessor.getAll(SpringResourceAccessor.java:42) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.resource.ResourceAccessor.get(ResourceAccessor.java:234) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.change.core.LoadDataChange.generateStatements(LoadDataChange.java:399) ~[liquibase-core-4.30.0.jar:na]
	... 70 common frames omitted

Disconnected from the target VM, address: '127.0.0.1:51326', transport: 'socket'

Process finished with exit code 0
it's working for this example

databaseChangeLog:
  - changeSet:
      id: 008-add-formejuriqueINPI-table.yaml
      author: ILS
      changes:
        - createTable:
            columns:
              - column:
                  constraints:
                    nullable: false
                    primaryKey: true
                  name: code
                  type: INT
              - column:
                  name: label
                  type: VARCHAR(400)
            tableName: INPI_legal_form
        - loadData:
            tableName: INPI_legal_form
            separator: ;
            relativeToChangelogFile : true
            file : legalForm_matching.csv
            columns:
              - column:
                  header: Code
                  name: code
                  type: INT
              - column:
                  header: Libelle
                  name: label
                  type: VARCHAR(400)
should i add columns ? if yes adjust my baseline
