// -----------------------------------------------------------------------------
// Point d'entrée de l'application - bootstrap IoC (DI container)
//
// Ce fichier configure un conteneur d'injection de dépendances minimal en utilisant
// Microsoft.Extensions.DependencyInjection. L'objectif est de centraliser la
// création et la durée de vie des objets (repositories, services, stratégies,
// vues) afin de faciliter le test, l'extension et la maintenance.
//
// Rappels sur les durées de vie utilisées ici:
// - Singleton : une seule instance partagée pendant toute la durée de l'application.
//   Nous l'utilisons pour les repositories car ils gardent l'état en mémoire
//   (Listes / Dictionnaires) et doivent être accessibles depuis plusieurs services.
// - Transient : une nouvelle instance à chaque résolution. Utilisé pour `ConsoleView`
//   car la vue peut être recréée si nécessaire (ici ce n'est pas strictement
//   nécessaire, mais illustre une configuration différente).
//
// Structure des enregistrements (exemples):
// services.AddSingleton<IDepartmentRepository>(DepartmentRepository.Instance);
// services.AddSingleton<IEmployeeRepository>(EmployeeRepository.Instance);
// services.AddSingleton<DepartmentService>();
// services.AddSingleton<EmployeeService>();
// services.AddSingleton<IFilterStrategy, DepartmentFilterStrategy>();
// services.AddTransient<ConsoleView>();
//
// Pour exécuter :
// 1) dotnet build
// 2) dotnet run --project Gestion\ Employes.csproj
//
// Pour étendre :
// - Ajouter une nouvelle implémentation de repository et l'enregistrer
//   avec la durée de vie souhaitée (Singleton / Scoped / Transient).
// - Remplacer `IFilterStrategy` par une autre stratégie (ex: filtrage par rôle)
//   en changeant l'enregistrement du service ou en ajoutant une factory.
//
using System;
using Microsoft.Extensions.DependencyInjection;
using Gestion_Employes.Repositories;
using Gestion_Employes.Services;
using Gestion_Employes.Views;
using Gestion_Employes.Patterns;

// Création du conteneur de services
var services = new ServiceCollection();

// Enregistrement des repositories. Nous fournissons ici les singletons
// déjà implémentés (pattern Singleton dans les classes Repository).
services.AddSingleton<IDepartmentRepository>(DepartmentRepository.Instance);
services.AddSingleton<IEmployeeRepository>(EmployeeRepository.Instance);

// Enregistrement des services métiers qui vont consommer les repositories.
// On choisit Singleton car ces services n'ont pas d'état propre et peuvent
// partager une instance par application.
services.AddSingleton<DepartmentService>();
services.AddSingleton<EmployeeService>();

// Stratégies et vues
// - IFilterStrategy : pattern Strategy pour filtrer la liste des employés
// - ConsoleView : couche Vue, responsable de l'I/O utilisateur
services.AddSingleton<IFilterStrategy, DepartmentFilterStrategy>();
services.AddTransient<ConsoleView>();

// Construction du ServiceProvider (conteneur prêt à fournir des instances)
using var provider = services.BuildServiceProvider();

// Résolution et exécution de la vue principale
var view = provider.GetRequiredService<ConsoleView>();
view.Run();
