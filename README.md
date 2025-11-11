 Parfait — je te propose ci-dessous deux versions prêtes à coller dans un ticket : une courte (pour un ticket rapide) et une détaillée (avec toutes les infos techniques, étapes de test et rollback).
J’ai utilisé les infos que tu as fournies : OLK-UAT-Bitbang-01 → UAT-Back-01 (192.168.91.10:9005).
Je suppose que le flux est TCP (port d’écoute applicatif) et que la communication est initiée depuis OLK-UAT-Bitbang-01 vers UAT-Back-01, si ce n’est pas le cas indique-le dans le ticket. Le seul détail manquant que je n’ai pas demandé (parce que tu as déjà donné ce que tu avais) est l’adresse IP de OLK-UAT-Bitbang-01 — je l’ai marqué comme « à confirmer » dans la version détaillée.

⸻

Version courte (copier-coller)

Titre : Autorisation communication OLK-UAT-Bitbang-01 → UAT-Back-01 (192.168.91.10:9005)

Description :
Ouverture d’un accès firewall pour permettre à la machine OLK-UAT-Bitbang-01 de communiquer avec UAT-Back-01 (192.168.91.10) sur le port 9005.
	•	Source (hostname) : OLK-UAT-Bitbang-01
	•	IP source : à confirmer (inscrire l’IP de OLK-UAT-Bitbang-01)
	•	Destination (hostname / IP) : UAT-Back-01 / 192.168.91.10
	•	Port destination : 9005
	•	Protocole : TCP (supposition)
	•	Sens : Source → Destination (OLK-UAT-Bitbang-01 initie vers 192.168.91.10:9005)
	•	Motif / justification : Communication applicative UAT entre bitbang et back-end pour tests UAT.
	•	Impact : Aucun impact attendu sur la production ; changement limité à la zone UAT.
	•	Test post-changement : Vérifier que OLK-UAT-Bitbang-01 se connecte correctement et que l’application côté UAT-Back-01 accepte la connexion sur le port 9005.
	•	Rollback : Retirer la règle ajoutée si problème.

⸻

Version détaillée (ticket formel avec commande exemple)

Titre : Demande ouverture port 9005 TCP — OLK-UAT-Bitbang-01 → UAT-Back-01 (192.168.91.10)

Résumé/objectif :
Permettre la communication réseau nécessaire pour les tests UAT : OLK-UAT-Bitbang-01 doit pouvoir atteindre le service backend hébergé sur UAT-Back-01 (192.168.91.10) sur le port TCP 9005.

Détails techniques :
	•	Source (nom) : OLK-UAT-Bitbang-01
	•	IP Source : [A REMPLIR] (adresse IP de OLK-UAT-Bitbang-01 — à confirmer)
	•	Destination (nom / IP) : UAT-Back-01 / 192.168.91.10
	•	Protocole : TCP (si le service utilise UDP, remplacer par UDP)
	•	Port destination : 9005 (dst)
	•	Sens / direction : Source → Destination (connexion initiée depuis OLK-UAT-Bitbang-01 vers UAT-Back-01:9005)
	•	Règle demandée (formulation concise) : Allow TCP from OLK-UAT-Bitbang-01 (IP = à renseigner) to 192.168.91.10 port 9005.

Exemples de règles / commandes (à adapter selon firewall) :
	•	iptables (sur le serveur de destination / firewall linux)

# Autoriser les nouvelles connexions TCP depuis la source vers le port 9005
iptables -I INPUT -p tcp -s <IP_OLK_UAT_BITBANG> --dport 9005 -m conntrack --ctstate NEW,ESTABLISHED -j ACCEPT
iptables -I OUTPUT -p tcp -d <IP_OLK_UAT_BITBANG> --sport 9005 -m conntrack --ctstate ESTABLISHED -j ACCEPT

	•	Cisco ACL (ex. routeur / ASA)

access-list UAT-ALLOW extended permit tcp host <IP_OLK_UAT_BITBANG> host 192.168.91.10 eq 9005
interface <inside-or-u at-zone>
  access-group UAT-ALLOW in

	•	Palo Alto / Firewall next-gen (policy fields)

From Zone: UAT-OLK
To Zone: UAT-BACK
Source: OLK-UAT-Bitbang-01 (or its IP)
Destination: 192.168.91.10
Application/Service: service-tcp-9005 (or service set TCP/9005)
Action: allow
Log: yes

Fenêtre de changement proposée : à planifier hors heures critiques UAT (indiquer date/heure souhaitée) — à remplir par le demandeur.

Tests post-changement :
	1.	Depuis OLK-UAT-Bitbang-01 : telnet 192.168.91.10 9005 ou nc -vz 192.168.91.10 9005 — doit établir la connexion.
	2.	Vérifier logs applicatifs sur UAT-Back-01 pour connexion acceptée.
	3.	Valider scénarios fonctionnels UAT dépendants du service.

Critères de succès :
	•	Connexion TCP établie et échanges applicatifs fonctionnels entre les 2 machines ; tests UAT validés par l’équipe applicative.

Rollback / Plan de retour arrière :
	•	Supprimer/retirer la règle ajoutée (ou réappliquer la règle précédente) et confirmer que le service redevient inaccessible depuis la source si nécessaire. Exemple iptables rollback :

iptables -D INPUT -p tcp -s <IP_OLK_UAT_BITBANG> --dport 9005 -j ACCEPT

Risques / Impact :
	•	Faible risque si restriction sur IP source. Si la règle est ouverte à tout le sous-réseau, risque d’exposition plus grand → recommander restreindre à l’IP précise de OLK-UAT-Bitbang-01.

Informations manquantes (à compléter avant exécution) :
	•	IP précise de OLK-UAT-Bitbang-01.
	•	Confirmation que le protocole est bien TCP (et non UDP).
	•	Date / créneau souhaité pour l’application du changement.
	•	Contact de l’équipe applicative pour validation des tests (nom/email/tel).

Demandeur / Approvals :
	•	Demandeur : ton nom / équipe
	•	Approvers requis : réseau / sécurité / responsable UAT

⸻

Si tu veux, je peux :
	•	produire la version anglaise si le ticket doit être créé dans un tool anglophone ;
	•	fournir la ligne exacte à coller dans votre firewall (j’ai besoin seulement de l’IP de OLK-UAT-Bitbang-01 et de la fenêtre horaire).

Dis-moi si je dois générer la version finale prête à coller dans Jira/ServiceNow (et si tu veux que j’inclus des champs spécifiques comme numéro de CI, priorité, catégorie, etc.).