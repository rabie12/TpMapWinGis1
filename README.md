Excellent, tu es très proche du but 👌

Tu as déjà bien ajouté :

@NoArgsConstructor
@AllArgsConstructor

mais le message d’erreur MapStruct :

Ambiguous constructors found for creating CompanyDTO(String, String, String), CompanyDTO(String)
Either declare parameterless constructor or annotate the default constructor with @Default

te dit qu’il ne “voit” toujours pas ton constructeur vide généré par Lombok.
➡️ C’est un classique conflit MapStruct + Lombok lors du traitement d’annotations.

⸻

🧩 Pourquoi ça arrive

MapStruct et Lombok génèrent du code au moment de la compilation.
Mais MapStruct utilise son propre processeur d’annotations, et si Lombok n’est pas exécuté avant, alors MapStruct ne “voit” pas le constructeur vide que Lombok a ajouté via @NoArgsConstructor.

👉 En clair : ton code est correct, mais le maven-compiler-plugin n’est pas configuré pour que Lombok et MapStruct coopèrent.

⸻

✅ Solution 1 — corriger la configuration Maven (la vraie solution)

Voici le bloc maven-compiler-plugin correct, à mettre dans ton pom.xml :

<plugin>
    <groupId>org.apache.maven.plugins</groupId>
    <artifactId>maven-compiler-plugin</artifactId>
    <version>3.11.0</version>
    <configuration>
        <source>${java.version}</source>
        <target>${java.version}</target>
        <annotationProcessorPaths>
            <!-- Lombok doit être listé avant MapStruct -->
            <path>
                <groupId>org.projectlombok</groupId>
                <artifactId>lombok</artifactId>
                <version>1.18.34</version>
            </path>
            <path>
                <groupId>org.mapstruct</groupId>
                <artifactId>mapstruct-processor</artifactId>
                <version>${org.mapstruct.version}</version>
            </path>
        </annotationProcessorPaths>
    </configuration>
</plugin>

🧠 Important : l’ordre est Lombok avant MapStruct.
Cela permet à Lombok de générer les constructeurs avant que MapStruct ne fasse sa détection.

Ensuite :

mvn clean compile

et ton erreur disparaîtra 🎯

⸻

✅ Solution 2 — contournement rapide (si tu veux juste que ça compile)

Si tu veux forcer MapStruct à savoir comment instancier ton CompanyDTO, ajoute cette méthode dans ton CompanyMapper :

import org.mapstruct.ObjectFactory;

@Mapper(componentModel = "spring", nullValueCheckStrategy = NullValueCheckStrategy.ALWAYS)
public interface CompanyMapper {

    CompanyDTO legalEntityToCompanyDTO(LegalEntity legalEntity);

    LegalEntity companyDTOToLegalEntity(CompanyDTO companyDTO);

    @ObjectFactory
    default CompanyDTO createCompanyDTO() {
        return new CompanyDTO(); // 👈 MapStruct utilisera ce constructeur
    }

    // ... le reste de ton code
}

👉 Cela contourne complètement le problème : tu dis explicitement à MapStruct quel constructeur utiliser.

⸻

✅ Solution 3 — vérifie IntelliJ

Dans IntelliJ :

File → Settings → Build, Execution, Deployment → Compiler → Annotation Processors → ✔ Enable annotation processing

⚠️ Sans cette case cochée, IntelliJ ne lance pas le processeur Lombok pendant la génération MapStruct.

⸻

✅ En résumé

Problème	Cause	Solution
“Ambiguous constructors found…”	Lombok et MapStruct ne se synchronisent pas	Configurer maven-compiler-plugin (Lombok avant MapStruct)
Lombok non pris en compte	Annotation processing désactivé	Activer dans IntelliJ
Besoin d’un quick fix	Forcer la création avec @ObjectFactory	Ajouter createCompanyDTO() dans le mapper


⸻

💡 Recommandation

➡️ Mets à jour ton pom.xml comme dans Solution 1,
puis fais un mvn clean compile.
Tu pourras supprimer la méthode @ObjectFactory ensuite : tout sera propre et automatique.

⸻

Souhaites-tu que je te montre ton pom.xml complet corrigé (avec Lombok + MapStruct parfaitement configurés pour Java 21 et Spring Boot 3.5.3) ?