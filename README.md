# Projet Restaurant - Test Bdd Mock

## Description

Ce projet est une API RESTful développée en C# avec .NET 8, conçue pour gérer les opérations d'un restaurant. Elle permet de communiquer avec une base de données SQLite pour effectuer des opérations CRUD (Create, Read, Update, Delete) sur diverses entités, telles que les clients, les employés, les tables, les commandes et les réservations.

### Fonctionnalités principales

- Gestion des clients, employés, tables, menus, réservations et commandes.
- Simulation de la base de données avec une base en mémoire pour les tests, permettant de vérifier les fonctionnalités sans interagir avec une base de données réelle.
- Tests unitaires pour chaque route en utilisant `xUnit`.

## Prérequis

Assurez-vous d'avoir **.NET 8 SDK** installé sur votre machine.

## Installation

Clonez le dépôt, puis restaurez les dépendances nécessaires en utilisant la commande suivante :

```bash
dotnet restore
```

## Lancer le projet

Pour lancer l'application en mode développement, exécutez la commande suivante :

```bash
dotnet run
```

L'API sera accessible par défaut à l'adresse http://localhost:3000.

## Tests

Les tests unitaires sont situés dans le dossier `Tests`. Ces tests permettent de valider le bon fonctionnement des routes et des opérations CRUD.

### Exécution des tests

Voici quelques éléments pour comprendre l'implémentation des tests dans ce projet :

- **Assertions** : Utilisez `Assert` pour effectuer des vérifications sur les résultats de chaque méthode.
- **Mocking** : Les données sont mockées en utilisant la base de données en mémoire fournie par `InMemoryDatabase`.
- **Transactions** ignorées : Notez que la base en mémoire ne supporte pas les transactions. Cela est géré dans les tests en ignorant les avertissements.

Pour lancer les tests, utilisez la commande suivante :

```bash
dotnet test
```
