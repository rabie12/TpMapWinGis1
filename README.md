2025-10-27T16:36:44.919+01:00 ERROR 21620 --- [business-registry] [           main] o.s.boot.SpringApplication               : Application run failed

org.springframework.beans.factory.BeanDefinitionStoreException: Failed to parse configuration class [eu.olkypay.business_registry.BusinessRegistryApplication]
	at org.springframework.context.annotation.ConfigurationClassParser.parse(ConfigurationClassParser.java:194) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassPostProcessor.processConfigBeanDefinitions(ConfigurationClassPostProcessor.java:418) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassPostProcessor.postProcessBeanDefinitionRegistry(ConfigurationClassPostProcessor.java:290) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.support.PostProcessorRegistrationDelegate.invokeBeanDefinitionRegistryPostProcessors(PostProcessorRegistrationDelegate.java:349) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.support.PostProcessorRegistrationDelegate.invokeBeanFactoryPostProcessors(PostProcessorRegistrationDelegate.java:118) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.support.AbstractApplicationContext.invokeBeanFactoryPostProcessors(AbstractApplicationContext.java:791) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.support.AbstractApplicationContext.refresh(AbstractApplicationContext.java:609) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.boot.web.servlet.context.ServletWebServerApplicationContext.refresh(ServletWebServerApplicationContext.java:146) ~[spring-boot-3.5.3.jar:3.5.3]
	at org.springframework.boot.SpringApplication.refresh(SpringApplication.java:752) ~[spring-boot-3.5.3.jar:3.5.3]
	at org.springframework.boot.SpringApplication.refreshContext(SpringApplication.java:439) ~[spring-boot-3.5.3.jar:3.5.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:318) ~[spring-boot-3.5.3.jar:3.5.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1361) ~[spring-boot-3.5.3.jar:3.5.3]
	at org.springframework.boot.SpringApplication.run(SpringApplication.java:1350) ~[spring-boot-3.5.3.jar:3.5.3]
	at eu.olkypay.business_registry.BusinessRegistryApplication.main(BusinessRegistryApplication.java:16) ~[classes/:na]
Caused by: org.springframework.context.annotation.ConflictingBeanDefinitionException: Annotation-specified bean name 'businessMonitorService' for bean class [eu.olkypay.business_registry.connector.ch.BusinessMonitorService] conflicts with existing, non-compatible bean definition of same name and class [eu.olkypay.business_registry.connector.BusinessMonitorService]
	at org.springframework.context.annotation.ClassPathBeanDefinitionScanner.checkCandidate(ClassPathBeanDefinitionScanner.java:361) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ClassPathBeanDefinitionScanner.doScan(ClassPathBeanDefinitionScanner.java:288) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ComponentScanAnnotationParser.parse(ComponentScanAnnotationParser.java:128) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassParser.doProcessConfigurationClass(ConfigurationClassParser.java:346) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassParser.processConfigurationClass(ConfigurationClassParser.java:281) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassParser.parse(ConfigurationClassParser.java:204) ~[spring-context-6.2.8.jar:6.2.8]
	at org.springframework.context.annotation.ConfigurationClassParser.parse(ConfigurationClassParser.java:172) ~[spring-context-6.2.8.jar:6.2.8]
	... 13 common frames omitted

I have this now
