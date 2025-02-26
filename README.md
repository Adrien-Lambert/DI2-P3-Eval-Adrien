# **Application de Gestion de Mots de Passe**

## **Table des Matières**
1. [Introduction](#introduction)
2. [Prérequis](#prérequis)
3. [Mise en place du Backend](#mise-en-place-du-backend)
4. [Mise en place du Frontend](#mise-en-place-du-frontend)
5. [Lancement du projet](#lancement-du-projet)
6. [Utilisation](#utilisation)

---

## **Introduction**
Cette application est une solution permettant de stocker et récupérer des mots de passe en fonction des applications définies. Le backend est développé en **.NET** et le frontend en **Angular**.

Chaque application peut être de deux types :
- **Grand public** : Les mots de passe sont chiffrés avec **AES**.
- **Professionnelle** : Les mots de passe sont chiffrés avec **RSA**.

L'architecture suit un modèle **N-Layer** avec un **Pattern Strategy** pour gérer le chiffrement.

---

## **Prérequis**

Avant de commencer, assurez-vous d'avoir installé :

- **Backend** :
  - .NET 8 ([Télécharger .NET](https://dotnet.microsoft.com/en-us/download))
  - SQL Server ([Télécharger SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))

- **Frontend** :
  - Node.js 18+ ([Télécharger Node.js](https://nodejs.org/en/))
  - Angular 18([Installation](https://angular.io/cli)) :
    ```sh
    npm install -g @angular/cli
    ```

---

## **Mise en place du Backend**

### **1. Création de la base de données**
L'application nécessite une base de données **SQL Server**. Créez-la avec :

```sql
CREATE DATABASE DatabaseName;
```
Remplacez `DatabaseName` par le nom de votre base de données.

### **2. Configuration de la connexion**

Dans **`appsettings.json`**, configurez la chaîne de connexion :

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=ServerAdress;Initial Catalog=DatabaseName;Integrated Security=True;TrustServerCertificate=True"
}
```
Remplacez `ServerAdress` par l'adresse de votre serveur SQL et `DatabaseName` par le nom de votre base.

### **3. Exécuter les migrations et mettre à jour la base**
Dans le terminal, naviguez vers le dossier **backend** et exécutez :

```sh
dotnet ef database update
```
Cela va créer les tables nécessaires.

### **4. Lancer le serveur**

Dans le dossier **backend**, exécutez :
```sh
dotnet run
```
Le serveur sera accessible sur **`https://localhost:7283/api`**.

---

## **Mise en place du Frontend**

### **1. Configuration des variables d'environnement**

Sur le front, les variables d'API sont définies dans **`front/src/environments/environment.ts`** :

```typescript
export const environment = {
    production: false,
    apiUrl: 'https://localhost:7283/api',
    apiKey :'66b5bb4b-73b5-405f-a7dd-09c5f821efe3'
};
```

Les clés API acceptées sont définies directement dans **`appsettings.json`**.

```json
"ApiKeys": [
  "66b5bb4b-73b5-405f-a7dd-09c5f821efe3"
]
```

### **2. Installation des dépendances**
Naviguez dans le dossier **front** et installez les dépendances :

```sh
npm install
```

### **3. Lancer l'application Angular**
Démarrez le serveur Angular :
```sh
ng serve
```
L'application sera accessible sur **`http://localhost:4200/`**.

---

## **Lancement du projet**

Pour exécuter l'application complète :

1. **Démarrer le backend** :
   ```sh
   cd backend
   dotnet run
   ```

2. **Démarrer le frontend** :
   ```sh
   cd front
   ng serve
   ```

Accédez à **`http://localhost:4200/`** pour tester l'application.

---

## **Utilisation**

### **1. Ajouter une application**
- Naviguez vers la section "Applications".
- Ajoutez une nouvelle application avec un nom et un type (Public ou Professionnel).

### **2. Ajouter un mot de passe**
- Dans la section "Mots de passe", ajoutez un compte associé à une application existante.
- Le mot de passe sera chiffré automatiquement selon le type de l'application.

### **3. Afficher et supprimer des mots de passe**
- La liste des mots de passe stockés est visible dans l'application.
- Un bouton permet de **supprimer** un mot de passe si nécessaire.

---

## **Sécurité**
- Toutes les requêtes vers l'API doivent inclure une clé d'API (`x-api-key`).
- Les mots de passe sont **chiffrés en base de données** avec AES ou RSA.
- L'affichage d'un mot de passe nécessite une requête sécurisée pour le déchiffrer côté back-end.

---

## **Conclusion**
Votre application Angular/.NET est maintenant configurée et prête à l'emploi. 🚀

**Bon développement !** 🎯

