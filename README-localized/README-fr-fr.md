---
page_type: sample
products:
- office-excel
- office-onedrive
- ms-graph
languages:
- aspx
- csharp
description: "Cet exemple présente la connexion entre une application web ASP.NET et Microsoft professionnel ou scolaire (Azure Active Directory) utilisant l’API Microsoft Graph."
extensions:
  contentType: samples
  technologies:
  - Microsoft Graph
  services:
  - Excel 
  - OneDrive
  - Users
  createdDate: 12/4/2017 10:26:12 AM
---
# Exemple de démarrage Microsoft Graph Excel pour ASP.NET 4.6

## Table des matières

* [Conditions préalables](#prerequisites)
* [Inscription de l’application](#register-the-application)
* [Création et exécution de l’exemple](#build-and-run-the-sample)
* [Questions et commentaires](#questions-and-comments)
* [Contribution](#contributing)
* [Ressources supplémentaires](#additional-resources)

Cet exemple montre comment connecter une application web ASP.NET 4.6 MVC à un compte professionnel ou scolaire (Azure Active Directory) ou personnel (Microsoft) à l’aide de l’API Microsoft Graph pour récupérer les informations de profil de l'utilisateur et les charger dans un classeur Excel. Il utilise la bibliothèque cliente [Microsoft Graph .NET](https://github.com/microsoftgraph/msgraph-sdk-dotnet) pour exploiter les données renvoyées par Microsoft Graph. 

En outre, l’exemple utilise la [bibliothèque d’authentification Microsoft (MSAL)](https://www.nuget.org/packages/Microsoft.Identity.Client/) pour l’authentification. Le kit de développement logiciel (SDK) MSAL offre des fonctionnalités permettant d’utiliser le [point de terminaison Azure AD v2.0](https://azure.microsoft.com/en-us/documentation/articles/active-directory-appmodel-v2-overview), qui permet aux développeurs d’écrire un flux de code unique qui gère l’authentification des comptes professionnels ou scolaires (Azure Active Directory) et personnels (Microsoft).

## Remarque importante à propos de la préversion MSAL

La bibliothèque peut être utilisée dans un environnement de production. Nous fournissons la même prise en charge du niveau de production pour cette bibliothèque que pour nos bibliothèques de production actuelles. Lors de la version d’essai, nous pouvons apporter des modifications à l’API, au format de cache interne et à d’autres mécanismes de cette bibliothèque que vous devrez prendre en compte avec les correctifs de bogues ou les améliorations de fonctionnalités. Cela peut avoir un impact sur votre application. Par exemple, une modification du format de cache peut avoir un impact sur vos utilisateurs. Par exemple, il peut leur être demandé de se connecter à nouveau. Une modification de l’API peut vous obliger à mettre à jour votre code. Lorsque nous fournissons la version de disponibilité générale, vous devez effectuer une mise à jour vers la version de disponibilité générale dans un délai de six mois, car les applications écrites à l’aide de la version d’évaluation de la bibliothèque ne fonctionneront plus.

## Conditions préalables

Cet exemple nécessite les éléments suivants :  

  * [Visual Studio](https://www.visualstudio.com/en-us/downloads) 
  * Soit un [compte Microsoft](https://www.outlook.com), soit un [compte Office 365 pour entreprise](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account). Vous pouvez vous inscrire à [Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account) pour accéder aux ressources dont vous avez besoin pour commencer à créer des applications Office 365.
  * Chargez le fichier **demo.xlsx** situé dans la racine de ce référentiel vers le dossier racine de votre compte OneDrive. Ce fichier contient un tableau vide avec deux colonnes.
  
## Inscription de l’application

1. Déterminez l’URL de votre application ASP.NET. Dans l’Explorateur de solutions de Visual Studio, sélectionnez le projet **Microsoft Graph Excel REST ASPNET**. Dans la fenêtre des **Propriétés**, cherchez la valeur de l’**URL**. Copiez cette valeur.

    ![Capture d'écran de la fenêtre des propriétés de Visual Studio](./images/vs-project-url.jpg)

1. Ouvrez un navigateur et accédez au [Centre d’administration Azure Active Directory](https://aad.portal.azure.com). Connectez-vous à l’aide d’un **compte personnel** (alias : compte Microsoft) ou d’un **compte professionnel ou scolaire**.

1. Sélectionnez **Azure Active Directory** dans le volet de navigation gauche, puis sélectionnez **Inscriptions d’applications (préversion)** sous **Gérer**.

    ![Capture d’écran des inscriptions d’applications ](./images/add-portal-app-registrations.jpg)

1. Sélectionnez **Nouvelle inscription**. Sur la page **Inscrire une application**, définissez les valeurs comme suit.

    - Attribuez un **nom** à l'`exemple de démarrage ASPNET Excel`.
    - Définissez les **Types de comptes pris en charge** sur **Comptes figurant dans un annuaire organisationnel et comptes Microsoft personnels**.
    - Sous **URI de redirection**, définissez la première flèche déroulante sur `Web` et la valeur sur l’URL d’application ASP.NET que vous avez copiée à l’étape 1.

    ![Une capture d’écran de la page Inscrire une application](./images/add-register-an-app.jpg)

1. Sélectionnez **Inscrire**. Sur la page **Exemple de démarrage ASPNET Excel**, copiez la valeur de **ID d’application (client)** et enregistrez-la, car vous en aurez besoin à l’étape suivante.

    ![Une capture d’écran de l’ID d’application de la nouvelle inscription d'application](./images/add-application-id.jpg)

1. Sélectionnez **Authentification** sous **Gérer**. Recherchez la rubrique **Octroi implicite**, puis activez **Jetons de l’ID**. Choisissez **Enregistrer**.

    ![Une capture d’écran de la rubrique octroi implicite](./images/add-implicit-grant.jpg)

1. Sélectionnez **Certificats et secrets** sous **Gérer**. Sélectionnez le bouton **Nouvelle clé secrète client**. Entrez une valeur dans la **Description**, sélectionnez l'une des options pour **Expire le**, puis choisissez **Ajouter**.

    ![Une capture d’écran de la boîte de dialogue Ajouter une clé secrète client](./images/add-new-client-secret.jpg)

1. Copiez la valeur due la clé secrète client avant de quitter cette page. Vous en aurez besoin à l’étape suivante.

    > [!IMPORTANT]
    > Cette clé secrète client n’apparaîtra plus, aussi veillez à la copier maintenant.

    ![Une capture d’écran de la clé secrète client récemment ajoutée](./images/add-copy-client-secret.jpg)

## Création et exécution de l’exemple

1. Téléchargez ou clonez l’exemple de démarrage Microsoft Graph Excel pour ASP.NET 4.6.

2. Ouvrez l’exemple de solution dans Visual Studio.

3. Dans le fichier Web.config dans le répertoire racine, remplacez les valeurs d’espace réservé **ida:AppId** et **ida:AppSecret** par l’ID de l’application et le mot de passe que vous avez copiés lors de l’inscription de l’application.

4. Appuyez sur F5 pour créer et exécuter l’exemple. Cela entraîne la restauration des dépendances du package NuGet et l’ouverture de l’application.

   >Si vous constatez des erreurs pendant l’installation des packages, vérifiez que le chemin d’accès local où vous avez sauvegardé la solution n’est pas trop long/profond. Pour résoudre ce problème, il vous suffit de déplacer la solution dans un dossier plus près du répertoire racine de votre lecteur.

5. Connectez-vous à votre compte personnel, professionnel ou scolaire et accordez les autorisations demandées.

6. Choisissez le bouton **Obtenir l’adresse de messagerie**. Une fois l’opération terminée, le nom et l’adresse de messagerie de l’utilisateur connecté s’affichent dans la page.

7. Sélectionnez le bouton **Télécharger dans Excel**. L’application crée une nouvelle ligne dans le classeur demo.xlsx et ajoute le nom d’utilisateur et l’adresse de courrier sur cette ligne. Un message de réussite s’affiche sous le bouton.

## Questions et commentaires

Nous aimerions connaître votre opinion sur cet exemple. Vous pouvez nous faire part de vos questions et suggestions dans la rubrique [Problèmes](https://github.com/microsoftgraph/aspnet-excelstarter-sample/issues) de ce référentiel.

Votre avis compte beaucoup pour nous. Communiquez avec nous sur [Stack Overflow](http://stackoverflow.com/questions/tagged/microsoftgraph). Posez vos questions avec la balise [MicrosoftGraph].

## Contribution ##

Si vous souhaitez contribuer à cet exemple, voir [CONTRIBUTING.md](CONTRIBUTING.md).

Ce projet a adopté le [code de conduite Open Source de Microsoft](https://opensource.microsoft.com/codeofconduct/). Pour en savoir plus, reportez-vous à la [FAQ relative au code de conduite](https://opensource.microsoft.com/codeofconduct/faq/) ou contactez [opencode@microsoft.com](mailto:opencode@microsoft.com) pour toute question ou tout commentaire.

## Ressources supplémentaires

- [Autres exemples de connexion avec Microsoft Graph](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Présentation de Microsoft Graph](http://graph.microsoft.io)
- [Exemples de code du développeur Office](http://dev.office.com/code-samples)
- [Centre de développement Office](http://dev.office.com/)

## Copyright
Copyright (c) 2019 Microsoft. Tous droits réservés.


