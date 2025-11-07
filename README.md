Parfait 👍 tu veux adapter le pipeline de bank-info pour olky-reporting,
sans changer les noms des fichiers .jar (donc on garde bank-info.jar).

Voici ce qu’on va faire :
	•	✅ garder le bank-info.jar (pour compatibilité avec ton script de déploiement ou ton infra actuelle)
	•	✅ adapter tout le reste (nom du script, tags, environnement, logique CI/CD, etc.)
	•	✅ simplifier là où olky-reporting n’a pas besoin d’étapes supplémentaires

⸻

🧩 Explications des adaptations

Élément	Valeur bank-info	Adapté pour olky-reporting
Nom du JAR	bank-info.jar	inchangé
Script de déploiement	/opt/script/gitlab/deploy_bankinfo.sh	/opt/script/gitlab/deploy_olkyreporting.sh
Nom du projet	olky-reporting	modifié dans les noms de jobs et contextes
Image Docker	maven:3.9.10-amazoncorretto-21-alpine	identique
Stages	liquibase → build → publish	identique
DB de test (liquibase)	olky_reporting	identique
Tags GitLab (deploy_dev, etc.)	conservés	conservés


⸻

🧩 Script .gitlab-ci.yml final pour olky-reporting

(avec le même jar bank-info.jar)

variables:
  MAVEN_CLI_OPTS: "-B -DskipTests -DupdateReleaseInfo=true -s settings.xml"
  MAVEN_OPTS: ""

  GIT_MAIN: "main"
  GIT_UAT: "uat"
  GIT_SNAPSHOT_REGEX: "(feat(ure)?/.*)|develop"
  GITLAB_TAG: "deploy_dev"
  GITLAB_ENV: "none"

workflow:
  rules:
    - if: $CI_COMMIT_BRANCH =~ $GIT_SNAPSHOT_REGEX
      variables:
        GITLAB_TAG: "deploy_dev"
        GITLAB_ENV: "develop"
    - if: $CI_COMMIT_BRANCH == $GIT_UAT
      variables:
        GITLAB_TAG: "deploy_uat"
        GITLAB_ENV: "uat"
    - if: $CI_COMMIT_BRANCH == $GIT_MAIN
      variables:
        GITLAB_TAG: "deploy_prd"
        GITLAB_ENV: "prd"

default:
  image: maven:3.9.10-amazoncorretto-21-alpine

stages:
  - liquibase
  - build
  - publish

# --- Stage 1 : Liquibase (manuel) ---
updateLiquibase:
  stage: liquibase
  environment: $GITLAB_ENV
  script:
    - echo "🔍 Vérification de la connexion DB olky_reporting..."
    - echo $JDBC_DB_URL
    - echo $DB_USERNAME
    - mariadb -h 192.168.89.1 -u $DB_USERNAME -p$DB_PASSWORD --connect-timeout=4 -e '\q' olky_reporting
  when: manual
  tags:
    - "$GITLAB_TAG"

# --- Stage 2 : Build ---
before_script:
  - echo "$JAVA_HOME"
  - java -version
  - mvn -version
  - echo "$OLKY_CA_BUNDLE_BASE64" | base64 -d > olky.pem
  - echo "Import du certificat dans le truststore..."
  - keytool -import -trustcacerts -alias olky_bundle -file olky.pem -storepass $JAVA_KEYSTORE_PASSWORD -noprompt -keystore $JAVA_HOME/lib/security/cacerts
  - keytool -list -keystore $JAVA_HOME/lib/security/cacerts -storepass $JAVA_KEYSTORE_PASSWORD | grep olky

build:
  stage: build
  script:
    - mvn $MAVEN_CLI_OPTS clean install -X
  artifacts:
    paths:
      - target/bank-info.jar   # <-- on garde le même jar !

# --- Stage 3 : Publish ---
publish:
  stage: publish
  script:
    - mvn $MAVEN_CLI_OPTS deploy

# --- Stage 4 : Deploy ---
deploy:
  needs:
    - job: build
  stage: publish
  before_script: []  # pas besoin de réimporter le certificat
  environment: $GITLAB_ENV
  script:
    - 'sudo /opt/script/gitlab/deploy_olkyreporting.sh'
  tags:
    - "$GITLAB_TAG"


⸻

✅ En résumé

Élément	Statut
JAR (bank-info.jar)	✅ inchangé
Script de déploiement	✅ adapté (deploy_olkyreporting.sh)
Pipeline logique	✅ identique à bank-info
Tags GitLab	✅ conservés (deploy_dev, deploy_uat, deploy_prd)
Compatibilité Maven / CA certs	✅ conservée
Job Liquibase	✅ conservé (manuel)


⸻

Souhaites-tu que je te montre le script deploy_olkyreporting.sh minimal adapté à partir du deploy_bankinfo.sh ?
(je peux te générer la version complète compatible avec ce pipeline)