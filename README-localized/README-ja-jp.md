---
page_type: sample
products:
- office-excel
- office-onedrive
- ms-graph
languages:
- aspx
- csharp
description: "このサンプルでは、Microsoft Graph API を使用して ASP.NET Web アプリを職場または学校の Microsoft アカウントに接続する方法示します。"
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
# ASP.NET 4.6 用 Microsoft Graph Excel Starter のサンプル

## 目次

* [前提条件](#prerequisites)
* [アプリケーションの登録](#register-the-application)
* [サンプルのビルドと実行](#build-and-run-the-sample)
* [質問とコメント](#questions-and-comments)
* [投稿](#contributing)
* [その他のリソース](#additional-resources)

このサンプルでは、Microsoft Graph API を使用して ASP.NET 4.6 MVC Web アプリを職場または学校の (Azure Active Directory) Microsoft アカウントまたは個人用 (Microsoft) アカウントに接続し、ユーザーのプロファイル情報を取得してその情報を Excel ブックにアップロードする方法を示します。このサンプルでは、Microsoft Graph が返すデータを操作するために、[Microsoft Graph .NET クライアント ライブラリ](https://github.com/microsoftgraph/msgraph-sdk-dotnet)が使用されます。 

また、認証には、[Microsoft 認証ライブラリ (MSAL)](https://www.nuget.org/packages/Microsoft.Identity.Client/) が使用されます。MSAL SDK では [Azure AD v2 0 エンドポイント](https://azure.microsoft.com/en-us/documentation/articles/active-directory-appmodel-v2-overview)を操作するための機能が提供されており、開発者は職場または学校 (Azure Active Directory) アカウントおよび個人用 (Microsoft) アカウントの両方に対する認証を処理する単一のコード フローを記述することができます。

## MSAL プレビューに関する重要な注意事項

このライブラリは、運用環境での使用に適しています。このライブラリに対しては、現在の運用ライブラリと同じ運用レベルのサポートを提供します。プレビュー中にこのライブラリの API、内部キャッシュの形式、および他のメカニズムを変更する場合があります。これは、バグの修正や機能強化の際に実行する必要があります。これは、アプリケーションに影響を与える場合があります。例えば、キャッシュ形式を変更すると、再度サインインが要求されるなどの影響をユーザーに与えます。API を変更すると、コードの更新が要求される場合があります。一般提供リリースが実施されると、プレビュー バージョンを使って作成されたアプリケーションは動作しなくなるため、6 か月以内に一般提供バージョンに更新することが求められます。

## 前提条件

このサンプルを実行するには次のものが必要です。  

  * [Visual Studio](https://www.visualstudio.com/en-us/downloads) 
  * [Microsoft アカウント](https://www.outlook.com)または [Office 365 for Business アカウント](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)のいずれか。Office 365 アプリのビルドを開始するために必要なリソースを含む [Office 365 Developer サブスクリプション](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)にサインアップできます。
  * このレポジトリのルートにあるファイル **demo.xlsx** を、OneDrive アカウントのルート フォルダーにアップロードします。このファイルには空のテーブルが含まれており、テーブルには列が 2 つあります。
  
## アプリケーションを登録する

1. ASP.NET アプリの URL を特定します。Visual Studio のソリューション エクスプ ローラーで、[**Microsoft Graph Excel REST ASPNET**] プロジェクトを選択します。[**プロパティ**] ウィンドウで、**URL** の値を見つけます。この値をコピーします。

    ![Visual Studio の [プロパティ] ウィンドウのスクリーンショット](./images/vs-project-url.jpg)

1. ブラウザーを開き、[Azure Active Directory 管理センター](https://aad.portal.azure.com)へ移動します。**個人用アカウント** (別名:Microsoft アカウントか**職場または学校のアカウント**を使用してログインします。

1. 左側のナビゲーションで [**Azure Active Directory**] を選択し、次に [**管理**] で [**アプリの登録 (プレビュー)**] を選択します。

    ![アプリの登録のスクリーンショット ](./images/add-portal-app-registrations.jpg)

1. **[新規登録]** を選択します。[**アプリケーションの登録**] ページで、次のように値を設定します。

    - [**名称**] を、"`ASPNET Excel Starter Sample`" にします。
    - [**サポートされているアカウントの種類**] を [**任意の組織のディレクトリ内のアカウントと個人用の Microsoft アカウント**] に設定します。
    - [**リダイレクト URI**] で、1 つ目のドロップダウン リストを [`Web`] に設定し、手順 1 でコピーした ASP.NET アプリの URL に値を設定します。

    ![[アプリケーションの登録]
    > ページのスクリーンショット](./images/add-register-an-app.jpg)

1. [**登録**] を選択します。[**ASPNET Excel Starter Sample**] ページで、[**アプリケーション (クライアント) ID**] の値をコピーして保存します。この値は次の手順で必要です。

    ![新しいアプリ登録のアプリケーション ID のスクリーンショット](./images/add-application-id.jpg)

1. [**管理**] で [**認証**] を選択します。[**暗黙的な許可**] セクションを見つけ、[**ID トークン**] を有効にします。[**保存**] を選択します。

    ![[暗黙的な許可] セクションのスクリーンショット](./images/add-implicit-grant.jpg)

1. [**管理**] で [**証明書とシークレット**] を選択します。[**新しいクライアント シークレット**] ボタンを選択します。[**説明**] に値を入力し、[**有効期限**] のオプションのいずれかを選び、[**追加**] を選択します。

    ![[クライアントシークレットの追加] ダイアログのスクリーンショット](./images/add-new-client-secret.jpg)

1. このページを離れる前に、クライアント シークレットの値をコピーします。この値は次の手順で必要になります。

    > [重要!] このクライアント シークレットは今後表示されないため、この段階で必ずコピーするようにしてください。

    ![新規追加されたクライアント シークレットのスクリーンショット](./images/add-copy-client-secret.jpg)

## サンプルのビルドと実行

1. ASP.NET 4.6 用 Microsoft Graph Excel Starter Sample をダウンロードするか複製します。

2. Visual Studio でサンプル ソリューションを開きます。

3. ルート ディレクトリの Web.config ファイルで、**ida:AppId** と **ida:AppSecret** のプレースホルダ―の値をアプリの登録時にコピーしたアプリケーションの ID とパスワードと置き換えます。

4. F5 キーを押して、サンプルをビルドして実行します。これにより、NuGet パッケージの依存関係が復元され、アプリが開きます。

   >パッケージのインストール中にエラーが発生した場合は、ソリューションを保存したローカル パスが長すぎたり深すぎたりしていないかご確認ください。ドライブのルート近くにソリューションを移動すると問題が解決します。

5. 個人用アカウントか職場または学校のアカウントでサインインし、要求されたアクセス許可を付与します。

6. [**Get email address**] (メール アドレスを取得) ボタンを選択します。操作が完了すると、サインインしているユーザーの名前とメール アドレスがページに表示されます。

7. [**Upload to Excel**] (Excel にアップロードする) ボタンを選択します。アプリケーションにより demo.xlsx ブックに新しい行が作成され、ユーザーの名前とメール アドレスがその行に追加されます。ボタンの下に、成功メッセージが表示されます。

## 質問とコメント

このサンプルに関するフィードバックをお寄せください。質問や提案につきましては、このリポジトリの「[問題](https://github.com/microsoftgraph/aspnet-excelstarter-sample/issues)」セクションで送信できます。

お客様からのフィードバックを重視しています。[スタック オーバーフロー](http://stackoverflow.com/questions/tagged/microsoftgraph)でご連絡ください。ご質問には [MicrosoftGraph] のタグを付けてください。

## 投稿 ##

このサンプルに投稿する場合は、[CONTRIBUTING.md](CONTRIBUTING.md) を参照してください。

このプロジェクトでは、[Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/) が採用されています。詳細については、「[Code of Conduct の FAQ](https://opensource.microsoft.com/codeofconduct/faq/)」を参照してください。また、その他の質問やコメントがあれば、[opencode@microsoft.com](mailto:opencode@microsoft.com) までお問い合わせください。

## その他のリソース

- [その他の Microsoft Graph Connect サンプル](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph の概要](http://graph.microsoft.io)
- [Office 開発者向けコード サンプル](http://dev.office.com/code-samples)
- [Office デベロッパー センター](http://dev.office.com/)

## 著作権
Copyright (c) 2019 Microsoft.All rights reserved.


