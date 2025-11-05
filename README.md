databaseChangeLog:
  - changeSet:
      id: 1-create-users
      author: RHI
      changes:
        - createTable:
            tableName: users
            columns:
              - column:
                  name: username
                  type: varchar(255)
              - column:
                  name: password
                  type: varchar(255)
              - column:
                  name: enabled
                  type: boolean

  - changeSet:
      id: 2-create-authorities
      author: RHI
      changes:
        - createTable:
            tableName: authorities
            columns:
              - column:
                  name: id
                  type: bigint
              - column:
                  name: username
                  type: varchar(255)
              - column:
                  name: authority
                  type: varchar(255)
        - addForeignKeyConstraint:
            baseTableName: authorities
            baseColumnNames: username
            referencedTableName: users
            referencedColumnNames: username
            constraintName: fk_authorities_user

  - changeSet:
      id: 3-create-bank-info
      author: RHI
      changes:
        - createTable:
            tableName: bank_info
            columns:
              - column:
                  name: id
                  type: bigint
              - column:
                  name: bic
                  type: varchar(50)
              - column:
                  name: name
                  type: varchar(255)
              - column:
                  name: institution
                  type: varchar(255)
              - column:
                  name: address1
                  type: varchar(255)
              - column:
                  name: location
                  type: varchar(255)
              - column:
                  name: can_do_sct
                  type: boolean
              - column:
                  name: can_do_core_sdd
                  type: boolean
              - column:
                  name: can_do_b2b_sdd
                  type: boolean
              - column:
                  name: country_iso_2
                  type: varchar(10)
              - column:
                  name: created_at
                  type: datetime
              - column:
                  name: updated_at
                  type: datetime
              - column:
                  name: search_result
                  type: longtext

  - changeSet:
      id: 4-create-bank-agency
      author: RHI
      changes:
        - createTable:
            tableName: bank_agency
            columns:
              - column:
                  name: id
                  type: bigint
              - column:
                  name: country_iso_2
                  type: varchar(10)
              - column:
                  name: bank_code
                  type: varchar(50)
              - column:
                  name: branch_code
                  type: varchar(50)
              - column:
                  name: branch_name
                  type: varchar(255)
              - column:
                  name: bank_and_branch_code
                  type: varchar(100)
              - column:
                  name: bank_info_id
                  type: bigint
        - addForeignKeyConstraint:
            baseTableName: bank_agency
            baseColumnNames: bank_info_id
            referencedTableName: bank_info
            referencedColumnNames: id
            constraintName: fk_agency_bankinfo

  - changeSet:
      id: 5-create-iban-search-history
      author: RHI
      changes:
        - createTable:
            tableName: iban_search_history
            columns:
              - column:
                  name: id
                  type: char(36)
              - column:
                  name: iban
                  type: varchar(255)
              - column:
                  name: result
                  type: varchar(255)
              - column:
                  name: response_details
                  type: longtext
              - column:
                  name: created_at
                  type: datetime
              - column:
                  name: updated_at
                  type: datetime
              - column:
                  name: bank_agency_id
                  type: bigint
        - addForeignKeyConstraint:
            baseTableName: iban_search_history
            baseColumnNames: bank_agency_id
            referencedTableName: bank_agency
            referencedColumnNames: id
            constraintName: fk_history_agency

  - changeSet:
      id: 6-create-spring-properties
      author: RHI
      changes:
        - createTable:
            tableName: spring_properties
            columns:
              - column:
                  name: id
                  type: bigint
              - column:
                  name: prop_key
                  type: varchar(255)
              - column:
                  name: prop_value
                  type: varchar(255)


	2025-11-05T15:51:09.874+01:00  WARN 30536 --- [bankInfo] [  restartedMain] o.m.jdbc.message.server.ErrorPacket      : Error: 1005-HY000: Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed")
2025-11-05T15:51:09.874+01:00 ERROR 30536 --- [bankInfo] [  restartedMain] liquibase.changelog                      : ChangeSet db/changelog/baseline.yaml::2-create-authorities::RHI encountered an exception.

liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
	at liquibase.executor.jvm.JdbcExecutor$ExecuteStatementCallback.doInStatement(JdbcExecutor.java:497) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor.execute(JdbcExecutor.java:83) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor.execute(JdbcExecutor.java:185) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.AbstractExecutor.execute(AbstractExecutor.java:141) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.database.AbstractJdbcDatabase.executeStatements(AbstractJdbcDatabase.java:1189) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:777) ~[liquibase-core-4.30.0.jar:na]
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
	at liquibase.command.core.AbstractUpdateCommandStep.lambda$run$0(AbstractUpdateCommandStep.java:114) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.lambda$child$0(Scope.java:194) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:203) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:193) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.Scope.child(Scope.java:172) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.AbstractUpdateCommandStep.run(AbstractUpdateCommandStep.java:112) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.UpdateCommandStep.run(UpdateCommandStep.java:100) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.CommandScope.lambda$execute$6(CommandScope.java:231) ~[liquibase-core-4.30.0.jar:na]
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
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.invokeInitMethods(AbstractAutowireCapableBeanFactory.java:1859) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1808) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.doCreateBean(AbstractAutowireCapableBeanFactory.java:601) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.createBean(AbstractAutowireCapableBeanFactory.java:523) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.lambda$doGetBean$0(AbstractBeanFactory.java:339) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.DefaultSingletonBeanRegistry.getSingleton(DefaultSingletonBeanRegistry.java:346) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:337) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:202) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:315) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:207) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractApplicationContext.finishBeanFactoryInitialization(AbstractApplicationContext.java:970) ~[spring-context-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractApplicationContext.refresh(AbstractApplicationContext.java:627) ~[spring-context-6.2.3.jar:6.2.3]
	at org.springframework.boot.web.servlet.context.ServletWebServerApplicationContext.refresh(ServletWebServerApplicationContext.java:146) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.refresh(SpringApplication.java:752) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.refreshContext(SpringApplication.java:439) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:318) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1361) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1350) ~[spring-boot-3.4.3.jar:3.4.3]
	at eu.olkypay.bankInfo.BankInfoApplication.main(BankInfoApplication.java:13) ~[classes/:na]
	at java.base/jdk.internal.reflect.DirectMethodHandleAccessor.invoke(DirectMethodHandleAccessor.java:103) ~[na:na]
	at java.base/java.lang.reflect.Method.invoke(Method.java:580) ~[na:na]
	at org.springframework.boot.devtools.restart.RestartLauncher.run(RestartLauncher.java:50) ~[spring-boot-devtools-3.4.3.jar:3.4.3]
Caused by: java.sql.SQLException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed")
	at org.mariadb.jdbc.export.ExceptionFactory.createException(ExceptionFactory.java:306) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.export.ExceptionFactory.create(ExceptionFactory.java:378) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.message.ClientMessage.readPacket(ClientMessage.java:189) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readPacket(StandardClient.java:1235) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResults(StandardClient.java:1174) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResponse(StandardClient.java:1093) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.execute(StandardClient.java:1017) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.executeInternal(Statement.java:1034) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:1163) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:502) ~[mariadb-java-client-3.4.1.jar:na]
	at com.zaxxer.hikari.pool.ProxyStatement.execute(ProxyStatement.java:94) ~[HikariCP-5.1.0.jar:na]
	at com.zaxxer.hikari.pool.HikariProxyStatement.execute(HikariProxyStatement.java) ~[HikariCP-5.1.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor$ExecuteStatementCallback.doInStatement(JdbcExecutor.java:491) ~[liquibase-core-4.30.0.jar:na]
	... 68 common frames omitted

2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : UPDATE SUMMARY
2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : Run:                          1
2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : Previously run:               0
2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : Filtered out:                 0
2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : -------------------------------
2025-11-05T15:51:09.883+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : Total change sets:            6
2025-11-05T15:51:09.884+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.util                           : Update summary generated
2025-11-05T15:51:09.887+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.command                        : Update command encountered an exception.
2025-11-05T15:51:09.889+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.lockservice                    : Successfully released change log lock
2025-11-05T15:51:09.892+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.command                        : Logging exception.
2025-11-05T15:51:09.893+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.ui                             : ERROR: Exception Details
2025-11-05T15:51:09.893+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.ui                             : ERROR: Exception Primary Class:  SQLException
2025-11-05T15:51:09.893+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.ui                             : ERROR: Exception Primary Reason:  (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed")
2025-11-05T15:51:09.893+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.ui                             : ERROR: Exception Primary Source:  MariaDB 11.7.2-MariaDB-log
2025-11-05T15:51:09.893+01:00  INFO 30536 --- [bankInfo] [  restartedMain] liquibase.command                        : Command execution complete
2025-11-05T15:51:10.514+01:00  WARN 30536 --- [bankInfo] [  restartedMain] ConfigServletWebServerApplicationContext : Exception encountered during context initialization - cancelling refresh attempt: org.springframework.beans.factory.BeanCreationException: Error creating bean with name 'entityManagerFactory' defined in class path resource [org/springframework/boot/autoconfigure/orm/jpa/HibernateJpaConfiguration.class]: Failed to initialize dependency 'liquibase' of LoadTimeWeaverAware bean 'entityManagerFactory': Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
2025-11-05T15:51:10.514+01:00  INFO 30536 --- [bankInfo] [  restartedMain] com.zaxxer.hikari.HikariDataSource       : AtkHikariPool - Shutdown initiated...
2025-11-05T15:51:10.518+01:00  INFO 30536 --- [bankInfo] [  restartedMain] com.zaxxer.hikari.HikariDataSource       : AtkHikariPool - Shutdown completed.
2025-11-05T15:51:10.528+01:00  INFO 30536 --- [bankInfo] [  restartedMain] o.apache.catalina.core.StandardService   : Stopping service [Tomcat]
2025-11-05T15:51:10.543+01:00  INFO 30536 --- [bankInfo] [  restartedMain] .s.b.a.l.ConditionEvaluationReportLogger : 

Error starting ApplicationContext. To display the condition evaluation report re-run your application with 'debug' enabled.
2025-11-05T15:51:10.560+01:00 ERROR 30536 --- [bankInfo] [  restartedMain] o.s.boot.SpringApplication               : Application run failed

org.springframework.beans.factory.BeanCreationException: Error creating bean with name 'entityManagerFactory' defined in class path resource [org/springframework/boot/autoconfigure/orm/jpa/HibernateJpaConfiguration.class]: Failed to initialize dependency 'liquibase' of LoadTimeWeaverAware bean 'entityManagerFactory': Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:328) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:207) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractApplicationContext.finishBeanFactoryInitialization(AbstractApplicationContext.java:970) ~[spring-context-6.2.3.jar:6.2.3]
	at org.springframework.context.support.AbstractApplicationContext.refresh(AbstractApplicationContext.java:627) ~[spring-context-6.2.3.jar:6.2.3]
	at org.springframework.boot.web.servlet.context.ServletWebServerApplicationContext.refresh(ServletWebServerApplicationContext.java:146) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.refresh(SpringApplication.java:752) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.refreshContext(SpringApplication.java:439) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:318) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1361) ~[spring-boot-3.4.3.jar:3.4.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1350) ~[spring-boot-3.4.3.jar:3.4.3]
	at eu.olkypay.bankInfo.BankInfoApplication.main(BankInfoApplication.java:13) ~[classes/:na]
	at java.base/jdk.internal.reflect.DirectMethodHandleAccessor.invoke(DirectMethodHandleAccessor.java:103) ~[na:na]
	at java.base/java.lang.reflect.Method.invoke(Method.java:580) ~[na:na]
	at org.springframework.boot.devtools.restart.RestartLauncher.run(RestartLauncher.java:50) ~[spring-boot-devtools-3.4.3.jar:3.4.3]
Caused by: org.springframework.beans.factory.BeanCreationException: Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1812) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.doCreateBean(AbstractAutowireCapableBeanFactory.java:601) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.createBean(AbstractAutowireCapableBeanFactory.java:523) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.lambda$doGetBean$0(AbstractBeanFactory.java:339) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.DefaultSingletonBeanRegistry.getSingleton(DefaultSingletonBeanRegistry.java:346) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:337) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:202) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:315) ~[spring-beans-6.2.3.jar:6.2.3]
	... 13 common frames omitted
Caused by: liquibase.exception.LiquibaseException: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
	at liquibase.integration.spring.SpringLiquibase.afterPropertiesSet(SpringLiquibase.java:272) ~[liquibase-core-4.30.0.jar:na]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.invokeInitMethods(AbstractAutowireCapableBeanFactory.java:1859) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1808) ~[spring-beans-6.2.3.jar:6.2.3]
	... 20 common frames omitted
Caused by: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
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
Caused by: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
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
Caused by: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/baseline.yaml::2-create-authorities::RHI:
     Reason: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
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
Caused by: liquibase.exception.DatabaseException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed") [Failed SQL: (1005) ALTER TABLE external_bank_data.authorities ADD CONSTRAINT fk_authorities_user FOREIGN KEY (username) REFERENCES external_bank_data.users (username)]
	at liquibase.executor.jvm.JdbcExecutor$ExecuteStatementCallback.doInStatement(JdbcExecutor.java:497) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor.execute(JdbcExecutor.java:83) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor.execute(JdbcExecutor.java:185) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.executor.AbstractExecutor.execute(AbstractExecutor.java:141) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.database.AbstractJdbcDatabase.executeStatements(AbstractJdbcDatabase.java:1189) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.changelog.ChangeSet.execute(ChangeSet.java:777) ~[liquibase-core-4.30.0.jar:na]
	... 63 common frames omitted
Caused by: java.sql.SQLException: (conn=150) Can't create table `external_bank_data`.`authorities` (errno: 150 "Foreign key constraint is incorrectly formed")
	at org.mariadb.jdbc.export.ExceptionFactory.createException(ExceptionFactory.java:306) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.export.ExceptionFactory.create(ExceptionFactory.java:378) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.message.ClientMessage.readPacket(ClientMessage.java:189) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readPacket(StandardClient.java:1235) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResults(StandardClient.java:1174) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.readResponse(StandardClient.java:1093) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.client.impl.StandardClient.execute(StandardClient.java:1017) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.executeInternal(Statement.java:1034) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:1163) ~[mariadb-java-client-3.4.1.jar:na]
	at org.mariadb.jdbc.Statement.execute(Statement.java:502) ~[mariadb-java-client-3.4.1.jar:na]
	at com.zaxxer.hikari.pool.ProxyStatement.execute(ProxyStatement.java:94) ~[HikariCP-5.1.0.jar:na]
	at com.zaxxer.hikari.pool.HikariProxyStatement.execute(HikariProxyStatement.java) ~[HikariCP-5.1.0.jar:na]
	at liquibase.executor.jvm.JdbcExecutor$ExecuteStatementCallback.doInStatement(JdbcExecutor.java:491) ~[liquibase-core-4.30.0.jar:na]
	... 68 common frames omitted



	
