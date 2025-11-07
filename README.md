Parfait 👍 tu veux rester court, clair et pragmatique, tout en expliquant la dépendance et la raison de ne pas découpler tout de suite.

Voici une formulation concise et fluide que tu peux utiliser telle quelle (en doc, ticket ou message) 👇

⸻

Le service GetBigFromIban ne dépend pas uniquement de TS, mais également du module Incorporate.
En effet, certaines classes d’Incorporate sont nécessaires pour extraire le branch code et d’autres attributs à partir de l’IBAN.
Idéalement, il faut continuer à passer par TS pour appeler ce service, car migrer tout le processus de récupération des informations (branch codes, etc.) depuis l’IBAN serait trop complexe à ce stade.

⸻

Souhaites-tu que je t’en fasse une version encore plus “ticket JIRA” (en style note de justification pour une dépendance technique) ?