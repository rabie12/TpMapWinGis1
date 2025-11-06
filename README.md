Ah, merci pour votre patience — je vois qu’on a un problème récurrent et que les solutions classiques (classpath: + relativeToChangelogFile: false) ne fonctionnent pas dans votre contexte. Ça arrive souvent avec Spring Boot + Liquibase si les fichiers ne sont pas réellement présents dans le classpath au runtime, ou si SpringLiquibase ne voit pas le bon chemin.

On va régler ça de manière sûre et définitive.

⸻

1️⃣ Vérifier absolument que les fichiers sont dans le classpath

SpringBoot ne peut voir que ce qui est dans le classpath (le dossier resources compilé dans le JAR).
	•	Vos fichiers SQL/CSV doivent être dans src/main/resources, par exemple :

src/main/resources/db/changelog/baseline.yaml
src/main/resources/db/changelog/sql/bank_info.sql
src/main/resources/db/changelog/sql/bank_agency.sql

	•	Vérifiez qu’ils sont réellement dans le JAR après compilation :

mvn clean package
jar tf target/*.jar | grep bank_info.sql

Si la commande ne retourne rien, vos fichiers ne sont pas copiés dans le JAR, donc SpringLiquibase ne peut pas les trouver.

⸻

2️⃣ Utiliser loadFile directement depuis le SpringLiquibase bean

Parfois, même avec classpath:, SpringLiquibase échoue à résoudre des fichiers via YAML. La solution la plus fiable est de définir explicitement un bean SpringLiquibase et charger les fichiers via Resource :

@Bean
public SpringLiquibase liquibase(DataSource dataSource, ResourceLoader resourceLoader) {
    SpringLiquibase liquibase = new SpringLiquibase();
    liquibase.setDataSource(dataSource);
    liquibase.setChangeLog(resourceLoader.getResource("classpath:db/changelog/baseline.yaml").getURI().toString());
    return liquibase;
}

Ici, on s’assure que SpringLiquibase reçoit un URI exact vers le fichier au lieu de tenter de résoudre un chemin relatif.

⸻

3️⃣ Alternative ultra-fiable : utiliser <sql> inline dans YAML

Si vos fichiers sont petits ou moyens, vous pouvez mettre directement vos inserts SQL dans le YAML au lieu de dépendre d’un fichier externe :

- changeSet:
    id: 3-load-bank-data
    author: RHI
    changes:
      - sql:
          sql: |
            INSERT INTO bank_info (id, bic, name, bank_info_id)
            VALUES
            (177845, NULL, 'SRLG', 37105),
            (177846, NULL, 'SRLG', 37105);

✅ Cela supprime totalement les problèmes de path, car tout est inline dans le YAML.

⸻

4️⃣ Résumé des causes possibles

Cause	Effet
Fichiers CSV/SQL hors resources	SpringLiquibase ne les trouve pas → relativePath null
Chemins relatifs avec relativeToChangelogFile: true	Fonctionne souvent avec CLI mais pas Spring Boot
IDE (IntelliJ/Eclipse) qui n’inclut pas les fichiers dans le runtime classpath	SpringLiquibase ne trouve rien
Mauvais préfixe (file: ou classpath: mal utilisé)	Erreur de résolution Spring


⸻

💡 Mon conseil final :
	1.	Assurez-vous que les fichiers sont dans src/main/resources/db/changelog/sql/.
	2.	Faites un mvn clean package et vérifiez le JAR.
	3.	Utilisez soit classpath: avec relativeToChangelogFile: false, soit insérez vos SQL inline dans YAML si les fichiers restent introuvables.
	4.	Si vous voulez, je peux vous générer un baseline.yaml prêt à l’emploi avec vos tables et données bancaires, 100% compatible Spring Boot, en SQL inline pour éviter tout problème de path.

Voulez-vous que je fasse ça ?