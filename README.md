variables:
  MAVEN_CLI_OPTS: "-B -DskipTests -DupdateReleaseInfo=true -s settings.xml"
  MAVEN_OPTS: ""
  GIT_SNAPSHOT_REGEX: "/(feat(ure)?/.*)|develop/"
  GIT_UAT: "uat"
  GIT_MAIN: "main"
  GITLAB_TAG: "none"
  GITLAB_ENV: "none"

workflow:
  rules:
    - if : $CI_COMMIT_BRANCH =~ $GIT_SNAPSHOT_REGEX
      variables:
        GITLAB_TAG: "deploy_dev"
        GITLAB_ENV: "develop"
    - if : $CI_COMMIT_BRANCH == $GIT_UAT
      variables:
        GITLAB_TAG: "deploy_uat"
        GITLAB_ENV: "uat"
    - if : $CI_COMMIT_BRANCH == $GIT_MAIN
      variables:
        GITLAB_TAG: "deploy_prd"
        GITLAB_ENV: "prd"

default:
  image: mariadb:latest

stages:
  - liquibase
  - build
  - publish

updateLiquibase:
  stage: liquibase
  environment: $GITLAB_ENV
  script:
    - echo $JDBC_DB_URL
    - echo $DB_USERNAME
    - mariadb -h 192.168.89.1 -u $DB_USERNAME -p$DB_PASSWORD --connect-timeout=4 -e '\q' olky_reporting
  when: manual
  tags:
  - "$GITLAB_TAG"

variables:
  MAVEN_CLI_OPTS: "-B -DskipTests -DupdateReleaseInfo=true -s settings.xml"
  MAVEN_OPTS: ""

  GIT_MAIN: "main"
  GIT_UAT: "uat"
  GIT_SNAPSHOT_REGEX: "(feat(ure)?/.*)|develop"
  GITLAB_TAG: "deploy_dev"
  GITLAB_ENV: "none"

default:
  image: maven:3.9.10-amazoncorretto-21-alpine

stages:
  - build
  - publish
workflow:
  rules:
    - if : $CI_COMMIT_BRANCH =~ $GIT_SNAPSHOT_REGEX
      variables:
        GITLAB_TAG: "deploy_dev"
        GITLAB_ENV: "develop"
    - if : $CI_COMMIT_BRANCH == $GIT_UAT
      variables:
        GITLAB_TAG: "deploy_uat"
        GITLAB_ENV: "uat"
    - if : $CI_COMMIT_BRANCH == $GIT_MAIN
      variables:
        GITLAB_TAG: "deploy_prd"
        GITLAB_ENV: "prd"

before_script:
  - echo "$JAVA_HOME"
  - java -version
  - mvn -version
  - echo "$OLKY_CA_BUNDLE_BASE64" | base64 -d > olky.pem
  - echo "Decoded olky.pem:"
  - cat olky.pem

  - echo "Certificate details:"
  - echo "Importing cert into JVM truststore..."
  - keytool -import -trustcacerts -alias olky_bundle -file olky.pem -storepass $JAVA_KEYSTORE_PASSWORD -noprompt -keystore $JAVA_HOME/lib/security/cacerts

  - echo "Listing truststore certificates:"
  - keytool -list -keystore $JAVA_HOME/lib/security/cacerts -storepass $JAVA_KEYSTORE_PASSWORD | grep olky
  - ls

build:
  stage: build
  script:
    - mvn $MAVEN_CLI_OPTS clean install -X
  artifacts:
   paths:
      - target/bank-info.jar
publish:
  stage: publish
  script:
    - mvn $MAVEN_CLI_OPTS deploy

deploy:
  needs:
    - job: build
  stage: publish
  before_script: [] # don't run before script
  environment: $GITLAB_ENV
  script:
  - 'sudo /opt/script/gitlab/deploy_bankinfo.sh' 
  tags:
    - "$GITLAB_TAG"
je veux faire une adaptation du script bank-info à celui de olky reporting peux tu faire la comparaison et me donner les adaptations à faire avec un scirpt adpté et finaliser en fin ?
