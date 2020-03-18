---
page_type: sample
products:
- office-excel
- office-onedrive
- ms-graph
languages:
- aspx
- csharp
description: "Este exemplo mostra como conectar um aplicativo Web ASP.NET a uma conta corporativa ou de estudante Microsoft (Azure Active Directory) usando a API do Microsoft Graph."
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
# Exemplo do Excel Starter no Microsoft Graph para ASP.NET 4.6

## Sumário

* [Pré-requisitos](#prerequisites)
* [Registrar o aplicativo](#register-the-application)
* [Criar e executar o exemplo](#build-and-run-the-sample)
* [Perguntas e comentários](#questions-and-comments)
* [Colaboração](#contributing)
* [Recursos adicionais](#additional-resources)

Este exemplo mostra como conectar um aplicativo Web do ASP.NET 4.6 MVC a uma conta corporativa ou de estudante da Microsoft (Azure Active Directory) da Microsoft ou uma conta pessoal (Microsoft) usando a API do Microsoft Graph API para recuperar informações de perfil de um usuário e carregá-las em uma pasta de trabalho do Excel. O exemplo usa a [Biblioteca de Clientes .NET do Microsoft Graph](https://github.com/microsoftgraph/msgraph-sdk-dotnet) para trabalhar com dados retornados pelo Microsoft Graph. 

Além disso, o exemplo usa a [Biblioteca de Autenticação da Microsoft (MSAL)](https://www.nuget.org/packages/Microsoft.Identity.Client/) para autenticação. O SDK da MSAL fornece recursos para trabalhar com o [ponto de extremidade do Microsoft Azure AD versão 2.0](https://azure.microsoft.com/en-us/documentation/articles/active-directory-appmodel-v2-overview), que permite aos desenvolvedores gravar um único fluxo de código para tratar da autenticação de contas pessoais (Microsoft), corporativas ou de estudantes (Azure Active Directory).

## Observação importante sobre a Visualização da MSAL

Esta biblioteca é adequada para uso em um ambiente de produção. Ela recebe o mesmo suporte de nível de produção que fornecemos às nossas bibliotecas de produção atuais. Durante a visualização, podemos fazer alterações na API, no formato de cache interno e em outros mecanismos desta biblioteca, que você será solicitado a implementar juntamente com correções de bugs ou melhorias de recursos. Isso pode impactar seu aplicativo. Por exemplo, uma alteração no formato de cache pode impactar seus usuários, exigindo que eles entrem novamente. Uma alteração na API pode requerer que você atualize seu código. Quando fornecermos a versão de Disponibilidade Geral, você será solicitado a atualizar a versão de Disponibilidade Geral no prazo de seis meses, pois os aplicativos escritos usando uma versão de visualização da biblioteca podem não funcionar mais.

## Pré-requisitos

Este exemplo requer o seguinte:  

  * [Visual Studio](https://www.visualstudio.com/en-us/downloads) 
  * Uma [conta Microsoft](https://www.outlook.com) ou um [conta do Office 365 para empresas](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account). Inscreva-se para [uma Assinatura de Desenvolvedor do Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account), que inclui os recursos necessários para começar a criação de aplicativos do Office 365.
  * Carregue o arquivo **demo.xlsx** na raiz deste repositório para a pasta raiz da sua conta do OneDrive. Este arquivo contém uma tabela vazia com duas colunas.
  
## Registrar o aplicativo

1. Determine a URL do aplicativo ASP.NET. No Gerenciador de Soluções do Visual Studio, marque o projeto **ASPNET REST do Excel no Microsoft Graph**. Na janela **Propriedades**, encontre o valor da **URL**. Copie esse valor.

    ![Captura de tela da janela de propriedades do Visual Studio](./images/vs-project-url.jpg)

1. Abra um navegador e navegue até o [centro de administração do Azure Active Directory](https://aad.portal.azure.com). Faça logon usando uma **conta pessoal** (também conhecida como: Conta Microsoft) **Conta Corporativa ou de Estudante**.

1. Selecione **Azure Active Directory** na navegação à esquerda e, em seguida, selecione **Registros de aplicativo (Visualizar)** em **Gerenciar**.

    ![Captura de tela dos Registros de aplicativo](./images/add-portal-app-registrations.jpg)

1. Selecione **Novo registro**. Na página **Registrar um aplicativo**, defina os valores da seguinte forma.

    - Defina **Nome** como `Exemplo de ASPNET Excel`.
    - Defina **Tipos de contas com suporte** para **Contas em qualquer diretório organizacional e contas pessoais da Microsoft**.
    - Em **URI de Redirecionamento**, defina o primeiro menu suspenso como `Web` e defina o valor como a URL do aplicativo ASP.NET que você copiou na etapa 1.

    ![Captura de tela da página Registrar um aplicativo](./images/add-register-an-app.jpg)

1. Escolha **Registrar**. Na página **Exemplo de ASPNET Excel Starter**, copie o valor da **ID do aplicativo (cliente)** e salve-o, você precisará dele na próxima etapa.

    ![Captura de tela da ID do aplicativo do novo registro do aplicativo](./images/add-application-id.jpg)

1. Selecione **Autenticação** em **Gerenciar**. Localize a seção **Concessão Implícita** e habilite **Tokens de ID**. Escolha **Salvar**.

    ![Uma captura de tela da seção Concessão Implícita](./images/add-implicit-grant.jpg)

1. Selecione **Certificados e segredos** em **Gerenciar**. Selecione o botão **Novo segredo do cliente**. Insira um valor em **Descrição**, selecione uma das opções para **Expira** e escolha **Adicionar**.

    ![Uma captura de tela da caixa de diálogo Adicionar um segredo do cliente](./images/add-new-client-secret.jpg)

1. Copie o valor secreto do cliente antes de sair desta página. Você precisará dele na próxima etapa.

    > [!IMPORTANTE]
    > Este segredo do cliente nunca é mostrado novamente, portanto, copie-o agora.

    ![Uma captura de tela do segredo do cliente recém adicionado](./images/add-copy-client-secret.jpg)

## Criar e executar o exemplo

1. Baixe ou clone o Exemplo do Excel Starter no Microsoft Graph para ASP.NET 4.6.

2. Abra a solução de exemplo no Visual Studio.

3. No arquivo Web.config no diretório raiz, substitua os valores dos espaços reservados **ida:AppId** e **ida:AppSecret** pela ID de aplicativo e senha copiadas durante o registro do aplicativo.

4. Pressione F5 para criar e executar o exemplo. Isso restaurará dependências do pacote NuGet e abrirá o aplicativo.

   >Caso receba mensagens de erro durante a instalação de pacotes, verifique se o caminho para o local onde você colocou a solução não é muito longo ou extenso. Para resolver esse problema, coloque a solução junto à raiz da unidade.

5. Entre com sua conta pessoal, corporativa ou de estudante, e conceda as permissões solicitadas.

6. Escolha o botão **Obter endereço de email**. Quando a operação for concluída, o nome e o endereço de email do usuário conectado serão exibidos na página.

7. Escolha o botão **Carregar no Excel**. O aplicativo cria uma nova linha na pasta de trabalho demo.xlsx e adiciona o nome de usuário e o endereço de email a essa linha. Será exibida uma mensagem de sucesso abaixo do botão.

## Perguntas e comentários

Gostaríamos de saber sua opinião sobre este exemplo. Você pode enviar perguntas e sugestões na seção [Problemas](https://github.com/microsoftgraph/aspnet-excelstarter-sample/issues) deste repositório.

Seus comentários são importantes para nós. Junte-se a nós na página do [Stack Overflow](http://stackoverflow.com/questions/tagged/microsoftgraph). Marque suas perguntas com [MicrosoftGraph].

## Colaboração ##

Se quiser contribuir para esse exemplo, confira [CONTRIBUTING.md](CONTRIBUTING.md).

Este projeto adotou o [Código de Conduta do Código Aberto da Microsoft](https://opensource.microsoft.com/codeofconduct/).  Para saber mais, confira as [Perguntas frequentes sobre o Código de Conduta](https://opensource.microsoft.com/codeofconduct/faq/) ou entre em contato pelo [opencode@microsoft.com](mailto:opencode@microsoft.com) se tiver outras dúvidas ou comentários.

## Recursos adicionais

- [Outros exemplos de conexão usando o Microsoft Graph](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Visão geral do Microsoft Graph](http://graph.microsoft.io)
- [Exemplos de código para desenvolvedores do Office](http://dev.office.com/code-samples)
- [Centro de Desenvolvimento do Office](http://dev.office.com/)

## Direitos autorais
Copyright (c) 2019 Microsoft. Todos os direitos reservados.


