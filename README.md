Excellente remarque ✅ — tu as tout à fait raison !

Ton problème n’est pas seulement le constructeur ambigu, mais aussi la configuration MapStruct + Lombok dans ton pom.xml.
👉 Actuellement, ton pom compile, mais MapStruct et Lombok ne collaborent pas bien ensemble.

⸻

🧩 Le problème

MapStruct et Lombok génèrent tous deux du code au moment de la compilation (via annotation processing).
👉 Si Maven ne les configure pas correctement dans le bon ordre, MapStruct ne “voit” pas les constructeurs ou builders générés par Lombok — d’où des erreurs comme :

Ambiguous constructors found...
ou
Can't map property...


⸻

✅ La solution (pom.xml corrigé)

Voici le bloc correctement configuré pour que MapStruct et Lombok fonctionnent ensemble sous Java 21 et Spring Boot 3.5.3 :

<build>
    <finalName>${project.artifactId}</finalName>
    <plugins>
        <!-- ✅ Plugin de compilation Java avec support MapStruct + Lombok -->
        <plugin>
            <groupId>org.apache.maven.plugins</groupId>
            <artifactId>maven-compiler-plugin</artifactId>
            <version>3.11.0</version>
            <configuration>
                <source>${java.version}</source>
                <target>${java.version}</target>
                <annotationProcessorPaths>
                    <!-- Lombok -->
                    <path>
                        <groupId>org.projectlombok</groupId>
                        <artifactId>lombok</artifactId>
                        <version>1.18.34</version>
                    </path>
                    <!-- MapStruct -->
                    <path>
                        <groupId>org.mapstruct</groupId>
                        <artifactId>mapstruct-processor</artifactId>
                        <version>${org.mapstruct.version}</version>
                    </path>
                </annotationProcessorPaths>
            </configuration>
        </plugin>

        <!-- Spring Boot plugin -->
        <plugin>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-maven-plugin</artifactId>
        </plugin>
    </plugins>
</build>


⸻

✅ Et dans tes dépendances

Ajoute explicitement les deux (même si tu les avais déjà, vérifie les versions) :

<dependencies>
    <!-- Lombok -->
    <dependency>
        <groupId>org.projectlombok</groupId>
        <artifactId>lombok</artifactId>
        <version>1.18.34</version>
        <scope>provided</scope>
    </dependency>

    <!-- MapStruct -->
    <dependency>
        <groupId>org.mapstruct</groupId>
        <artifactId>mapstruct</artifactId>
        <version>${org.mapstruct.version}</version>
    </dependency>
</dependencies>


⸻

⚙️ Étape finale

Une fois ton pom.xml mis à jour :
	1.	Supprime le dossier généré :

mvn clean


	2.	Puis recompile :

mvn compile



💡 IntelliJ doit être configuré pour “Enable annotation processing” :

File → Settings → Build, Execution, Deployment → Compiler → Annotation Processors → ✔ Enable annotation processing

⸻

🧠 Bonus

Si ton CompanyDTO est annoté avec Lombok (@Data, @Builder, etc.), alors :
	•	tu n’as plus besoin de constructeur vide si tu ajoutes :

@NoArgsConstructor

(Lombok le génèrera pour MapStruct).

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CompanyDTO {
    private String identifier;
    private String legalName;
    private String rcs;
}


⸻

✅ En résumé

Problème	Solution
MapStruct + Lombok ne se voient pas	Configurer maven-compiler-plugin avec les 2 processors
Ambiguous constructor	Ajouter @NoArgsConstructor (ou constructeur vide)
MapStruct ne génère rien	Activer “annotation processing” dans IntelliJ


⸻

Souhaites-tu que je te montre le pom.xml complet prêt à copier-coller avec toutes ces corrections intégrées (MapStruct + Lombok + Spring Boot 3.5.3) ?