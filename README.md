Error starting ApplicationContext. To display the condition evaluation report re-run your application with 'debug' enabled.
2025-11-07T15:25:15.955+01:00 ERROR 8116 --- [bankInfo] [  restartedMain] o.s.boot.SpringApplication               : Application run failed

org.springframework.beans.factory.BeanCreationException: Error creating bean with name 'entityManagerFactory' defined in class path resource [org/springframework/boot/autoconfigure/orm/jpa/HibernateJpaConfiguration.class]: Failed to initialize dependency 'liquibase' of LoadTimeWeaverAware bean 'entityManagerFactory': Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/changeSet/001-populate-bankinfo.yaml::3-load-bankinfo-data::RHI:
     Reason: liquibase.exception.DatabaseException: java.sql.SQLSyntaxErrorException: (conn=153) Unknown column 'can_dob2b_sdd' in 'INSERT INTO'
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
Caused by: org.springframework.beans.factory.BeanCreationException: Error creating bean with name 'liquibase' defined in class path resource [org/springframework/boot/autoconfigure/liquibase/LiquibaseAutoConfiguration$LiquibaseConfiguration.class]: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/changeSet/001-populate-bankinfo.yaml::3-load-bankinfo-data::RHI:
     Reason: liquibase.exception.DatabaseException: java.sql.SQLSyntaxErrorException: (conn=153) Unknown column 'can_dob2b_sdd' in 'INSERT INTO'
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1812) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.doCreateBean(AbstractAutowireCapableBeanFactory.java:601) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.createBean(AbstractAutowireCapableBeanFactory.java:523) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.lambda$doGetBean$0(AbstractBeanFactory.java:339) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.DefaultSingletonBeanRegistry.getSingleton(DefaultSingletonBeanRegistry.java:346) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:337) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.getBean(AbstractBeanFactory.java:202) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractBeanFactory.doGetBean(AbstractBeanFactory.java:315) ~[spring-beans-6.2.3.jar:6.2.3]
	... 13 common frames omitted
Caused by: liquibase.exception.LiquibaseException: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/changeSet/001-populate-bankinfo.yaml::3-load-bankinfo-data::RHI:
     Reason: liquibase.exception.DatabaseException: java.sql.SQLSyntaxErrorException: (conn=153) Unknown column 'can_dob2b_sdd' in 'INSERT INTO'
	at liquibase.integration.spring.SpringLiquibase.afterPropertiesSet(SpringLiquibase.java:272) ~[liquibase-core-4.30.0.jar:na]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.invokeInitMethods(AbstractAutowireCapableBeanFactory.java:1859) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1808) ~[spring-beans-6.2.3.jar:6.2.3]
	... 20 common frames omitted
Caused by: liquibase.exception.CommandExecutionException: liquibase.exception.LiquibaseException: liquibase.exception.MigrationFailedException: Migration failed for changeset db/changelog/changeSet/001-populate-bankinfo.yaml::3-load-bankinfo-data::RHI:
     Reason: liquibase.exception.DatabaseException: java.sql.SQLSyntaxErrorException: (conn=153) Unknown column 'can_dob2b_sdd' in 'INSERT INTO'
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
	at liquibase.integration.spring.SpringLiquibase.afterPropertiesSet(Sprin
