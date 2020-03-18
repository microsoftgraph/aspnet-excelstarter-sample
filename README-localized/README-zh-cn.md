---
page_type: sample
products:
- office-excel
- office-onedrive
- ms-graph
languages:
- aspx
- csharp
description: "此示例演示如何使用 Microsoft Graph API 将 ASP.NET Web 应用连接到 Microsoft 工作或学校帐户 (Azure Active Directory)。"
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
# 针对 ASP.NET 4.6 的 Microsoft Graph Excel Starter 示例

## 目录

* [先决条件](#prerequisites)
* [注册应用程序](#register-the-application)
* [生成并运行示例](#build-and-run-the-sample)
* [问题和意见](#questions-and-comments)
* [参与](#contributing)
* [其他资源](#additional-resources)

此示例演示如何使用 Microsoft Graph API 将 ASP.NET 4.6 MVC Web 应用连接到 Microsoft 工作或学校帐户 (Azure Active Directory) 或个人帐户 (Microsoft)，以检索用户的配置文件信息并将该信息上传到 Excel 工作簿。该应用使用 [Microsoft Graph .NET 客户端库](https://github.com/microsoftgraph/msgraph-sdk-dotnet)来处理 Microsoft Graph 返回的数据。 

此外，此示例使用 [Microsoft 身份验证库 (MSAL)](https://www.nuget.org/packages/Microsoft.Identity.Client/) 进行身份验证。MSAL SDK 提供可使用 [Azure AD v2.0 终结点](https://azure.microsoft.com/en-us/documentation/articles/active-directory-appmodel-v2-overview)的功能，借助该终结点，开发人员可以编写单个代码流来处理对工作或学校帐户 (Azure Active Directory) 或个人帐户 (Microsoft) 的身份验证。

## 有关 MSAL 预览版的重要说明

此库适用于生产环境。我们为此库提供的生产级支持与为当前生产库提供的支持相同。在预览期间，我们可能会更改 API、内部缓存格式和此库的其他机制，必须接受这些更改以及 bug 修复或功能改进。这可能会影响应用。例如，缓存格式更改可能会对用户造成影响，如要求用户重新登录。API 更改可能会要求更新代码。在我们提供通用版后，必须在 6 个月内更新到通用版，因为使用预览版库编写的应用可能不再可用。

## 先决条件

此示例要求如下：  

  * [Visual Studio](https://www.visualstudio.com/en-us/downloads) 
  * [Microsoft 帐户](https://www.outlook.com)或 [Office 365 商业版帐户](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)。你可以注册 [Office 365 开发人员订阅](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)，其中包含开始构建 Office 365 应用所需的资源。
  * 将此存储库根目录中的 **demo.xlsx** 文件上传到 OneDrive 帐户的根文件夹。此文件包含一个包含两列的空表格。
  
## 注册应用程序

1. 确定 ASP.NET 应用的 URL。在 Visual Studio 的“解决方案资源管理器”中，选择 **Microsoft Graph Excel REST ASPNET** 项目。在“**属性**”窗口中，查找“**URL**”值。复制此值。

    ![Visual Studio 属性窗口的屏幕截图](./images/vs-project-url.jpg)

1. 打开浏览器，并转到 [Azure Active Directory 管理中心](https://aad.portal.azure.com)。使用**个人帐户**（也称为：“Microsoft 帐户”）或**工作/学校帐户**登录。

1. 选择左侧导航栏中的“**Azure Active Directory**”，再选择“**管理**”下的“**应用注册(预览版)**”。

    ![“应用注册”的屏幕截图 ](./images/add-portal-app-registrations.jpg)

1. 选择**“新注册”**。在“**注册应用**”页面上，按如下方式设置值。

    - 将“**名称**”设置为 `ASPNET Excel Starter 示例`。
    - 将“**受支持的帐户类型**”设置为“**任何组织目录中的帐户和个人 Microsoft 帐户**”。
    - 在“**重定向 URI**”下，将第一个下拉列表设置为“`Web`”，并将值设置为在第 1 步中复制的 ASP.NET 应用 URL。

    ![“注册应用程序”页面的屏幕截图](./images/add-register-an-app.jpg)

1. 选择“**注册**”。在“**ASPNET Excel Starter 示例**”页面上，复制并保存“**应用程序(客户端) ID**”的值，以便在下一步中使用。

    ![新应用注册的应用程序 ID 的屏幕截图](./images/add-application-id.jpg)

1. 选择**管理**下的**身份验证**。找到**隐式授予**部分，并启用 **ID 令牌**。选择**保存**。

    ![“隐式授予”部分的屏幕截图](./images/add-implicit-grant.jpg)

1. 选择**管理**下的**证书和密码**。选择**新客户端密码**按钮。在**说明**中输入值，并选择一个**过期**选项，再选择**添加**。

    ![“添加客户端密码”对话框的屏幕截图](./images/add-new-client-secret.jpg)

1. 离开此页前，先复制客户端密码值。将在下一步中用到它。

    > [重要提示！]
    > 此客户端密码不会再次显示，所以请务必现在就复制它。

    ![新添加的客户端密码的屏幕截图](./images/add-copy-client-secret.jpg)

## 生成并运行示例

1. 下载或克隆针对 ASP.NET 4.6 的 Microsoft Graph Excel Starter 示例。

2. 在 Visual Studio 中打开示例解决方案。

3. 在根目录的 Web.config 文件中，使用你在应用注册过程中复制的应用程序 ID 和密码来替换 **ida:AppId** 和 **ida:AppSecret** 占位符值。

4. 按 F5 生成并运行此示例。这将还原 NuGet 包依赖项并打开该应用。

   >如果在安装包时出现任何错误，请确保你放置该解决方案的本地路径并未太长/太深。将解决方案移动到更接近驱动器根目录的位置可以解决此问题。

5. 使用个人帐户或者工作或学校帐户登录，并授予所请求的权限。

6. 选择“**获取电子邮件地址**”按钮。完成此操作后，页面上将显示登录用户的用户名和电子邮件地址。

7. 选择“**上传到 Excel**”按钮。应用程序将在 .xlsx 工作簿中创建一个新行，并将用户名和电子邮件地址添加到该行。按钮下方将显示一条成功消息。

## 问题和意见

我们乐意倾听你对此示例的反馈。你可以在该存储库中的[问题](https://github.com/microsoftgraph/aspnet-excelstarter-sample/issues)部分将问题和建议发送给我们。

我们非常重视你的反馈意见。请在[堆栈溢出](http://stackoverflow.com/questions/tagged/microsoftgraph)上与我们联系。使用 [MicrosoftGraph] 标记出你的问题。

## 参与 ##

如果想要参与本示例，请参阅 [CONTRIBUTING.md](CONTRIBUTING.md)。

此项目已采用 [Microsoft 开放源代码行为准则](https://opensource.microsoft.com/codeofconduct/)。有关详细信息，请参阅[行为准则常见问题解答](https://opensource.microsoft.com/codeofconduct/faq/)。如有其他任何问题或意见，也可联系 [opencode@microsoft.com](mailto:opencode@microsoft.com)。

## 其他资源

- [其他 Microsoft Graph Connect 示例](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph 概述](http://graph.microsoft.io)
- [Office 开发人员代码示例](http://dev.office.com/code-samples)
- [Office 开发人员中心](http://dev.office.com/)

## 版权信息
版权所有 (c) 2019 Microsoft。保留所有权利。


