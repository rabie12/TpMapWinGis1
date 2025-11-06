ore locations can be added with the 'searchPath' parameter., db/changelog/baseline.yaml::3-load-bank-data::RHI

	at liquibase.integration.spring.SpringLiquibase.afterPropertiesSet(SpringLiquibase.java:272) ~[liquibase-core-4.30.0.jar:na]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.invokeInitMethods(AbstractAutowireCapableBeanFactory.java:1859) ~[spring-beans-6.2.3.jar:6.2.3]
	at org.springframework.beans.factory.support.AbstractAutowireCapableBeanFactory.initializeBean(AbstractAutowireCapableBeanFactory.java:1808) ~[spring-beans-6.2.3.jar:6.2.3]
	... 20 common frames omitted
Caused by: liquibase.exception.CommandExecutionException: liquibase.exception.ValidationFailedException: Validation Failed:
     2 changes have validation failures
          The file bank_info.sql was not found in the configured search path:
    - Spring classpath
More locations can be added with the 'searchPath' parameter., db/changelog/baseline.yaml::3-load-bank-data::RHI
          The file bank_agency.sql was not found in the configured search path:
    - Spring classpath
More locations can be added with the 'searchPath' parameter., db/changelog/baseline.yaml::3-load-bank-data::RHI

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
Caused by: liquibase.exception.ValidationFailedException: Validation Failed:
     2 changes have validation failures
          The file bank_info.sql was not found in the configured search path:
    - Spring classpath
More locations can be added with the 'searchPath' parameter., db/changelog/baseline.yaml::3-load-bank-data::RHI
          The file bank_agency.sql was not found in the configured search path:
    - Spring classpath
More locations can be added with the 'searchPath' parameter., db/changelog/baseline.yaml::3-load-bank-data::RHI

	at liquibase.changelog.DatabaseChangeLog.validate(DatabaseChangeLog.java:407) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.core.helpers.DatabaseChangelogCommandStep.run(DatabaseChangelogCommandStep.java:92) ~[liquibase-core-4.30.0.jar:na]
	at liquibase.command.CommandScope.lambda$execute$6(CommandScope.java:231) ~[liquibase-core-4.30.0.jar:na]
	... 40 common frames omitted

Disconnected from the target VM, address: '127.0.0.1:56336', transport: 'socket'

Process finished with exit code 0
