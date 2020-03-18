---
page_type: sample
products:
- office-excel
- office-onedrive
- ms-graph
languages:
- aspx
- csharp
description: "En este ejemplo se muestra cómo conectar una aplicación web ASP.NET a una cuenta profesional o educativa (Azure Active Directory) de Microsoft con la API de Microsoft Graph"
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
# Ejemplo de Microsoft Graph Excel Starter para ASP.NET 4.6

## Tabla de contenido

* [Requisitos previos](#prerequisites)
* [Registrar la aplicación](#register-the-application)
* [Compilar y ejecutar el ejemplo](#build-and-run-the-sample)
* [Preguntas y comentarios](#questions-and-comments)
* [Colaboradores](#contributing)
* [Recursos adicionales](#additional-resources)

Este ejemplo muestra cómo conectar una aplicación web ASP.NET 4.6 MVC a una cuenta profesional o educativa de Microsoft (Azure Active Directory) o a una cuenta personal (Microsoft) utilizando la API de Microsoft Graph para recuperar la información del perfil de un usuario y cargar esa información en un libro de Excel. Usa la [biblioteca cliente .NET de Microsoft Graph](https://github.com/microsoftgraph/msgraph-sdk-dotnet) para trabajar con los datos devueltos por Microsoft Graph. 

Además, en el ejemplo se usa la [Biblioteca de autenticación de Microsoft (MSAL)](https://www.nuget.org/packages/Microsoft.Identity.Client/) para la autenticación. El SDK de MSAL ofrece características para trabajar con el [punto de conexión v2.0 de Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-appmodel-v2-overview), lo que permite a los desarrolladores escribir un flujo de código único que controla la autenticación para las cuentas profesionales, educativas (Azure Active Directory) o las cuentas personales (Microsoft).

## Nota importante acerca de la vista previa de MSAL

Esta biblioteca es apta para utilizarla en un entorno de producción. Ofrecemos la misma compatibilidad de nivel de producción de esta biblioteca que la de las bibliotecas de producción actual. Durante la vista previa podemos realizar cambios en la API, el formato de caché interna y otros mecanismos de esta biblioteca, que deberá tomar junto con correcciones o mejoras. Esto puede afectar a la aplicación. Por ejemplo, un cambio en el formato de caché puede afectar a los usuarios, como que se les pida que vuelvan a iniciar sesión. Un cambio de API puede requerir que actualice su código. Cuando ofrecemos la versión de disponibilidad General, deberá actualizar a la versión de disponibilidad General dentro de seis meses, ya que las aplicaciones escritas mediante una versión de vista previa de biblioteca puede que ya no funcionen.

## Requisitos previos

Este ejemplo necesita lo siguiente:  

  * [Visual Studio](https://www.visualstudio.com/en-us/downloads) 
  * Ya sea una [cuenta de Microsoft](https://www.outlook.com) u [Office 365 para una cuenta empresarial](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account). Puede registrarse para [una suscripción a Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account), que incluye los recursos que necesita para comenzar a crear aplicaciones de Office 365.
  * Cargue el archivo **demo.xlsx** en la raíz de este repositorio en la carpeta raíz de su cuenta de OneDrive. Este archivo contiene una tabla vacía con dos columnas.
  
## Registrar la aplicación

1. Determine la dirección URL de la aplicación ASP.NET. En el explorador de soluciones de Visual Studio, seleccione el proyecto **Microsoft-Graph-ASPNET-Excel-Donations**. En la ventana **Propiedades**, busque el valor de la **URL**. Copie este valor.

    ![Captura de pantalla de la ventana de propiedades de Visual Studio](./images/vs-project-url.jpg)

1. Abra un explorador y diríjase al [Centro de administración de Azure Active Directory](https://aad.portal.azure.com). Inicie sesión con una **cuenta personal** (por ejemplo: una cuenta de Microsoft) o una **cuenta profesional o educativa**.

1. Seleccione **Azure Active Directory** en el panel de navegación izquierdo y, después, seleccione **Registros de aplicaciones (versión preliminar)** en **Administrar**.

    ![Una captura de pantalla de los registros de la aplicación ](./images/add-portal-app-registrations.jpg)

1. Seleccione **Nuevo registro**. En la página **Registrar una aplicación**, establezca los siguientes valores.

    - Establezca el **nombre** en el `ejemplo de ASPNET para Starter`.
    - Establezca los**Tipos de cuentas admitidas** en **Cuentas en cualquier directorio de organización y cuentas personales de Microsoft**.
    - En la **URI de redirección`, establezca la primera lista desplegable en la `Web` y establezca el valor en la dirección URL de la aplicación ASP.NET que copió en el paso 1.

    ![Captura de pantalla de la página Registrar una aplicación](./images/add-register-an-app.jpg)

1. Elija **Registrar**. En la página **Ejemplo de ASPNET Excel Starter**, copie el valor del **id. de aplicación (cliente)** y guárdelo. Lo necesitará en el siguiente paso.

    ![Captura de pantalla del id. de aplicación del nuevo registro de la aplicación](./images/add-application-id.jpg)

1. Seleccione **Autenticación** en **Administrar**. Localice la sección **concesión implícita** y habilite los **tokens de id.** Elija **Guardar**.

    ![Captura de pantalla de la sección de Concesión implícita](./images/add-implicit-grant.jpg)

1. Seleccione **certificados y secretos** en **administrar**. Seleccione el botón **Nuevo secreto de cliente**. Escriba un valor en **Descripción** y seleccione una de las opciones de **Expirar** y luego seleccione **Agregar**.

    ![Captura de pantalla del diálogo Agregar un cliente secreto](./images/add-new-client-secret.jpg)

1. Copie el valor del secreto del cliente antes de salir de esta página. Lo necesitará en el siguiente paso.

    > [¡IMPORTANTE!]
    > El secreto del cliente no volverá a ser mostrado, asegúrese de copiarlo en este momento.

    ![Captura de pantalla del nuevo secreto del cliente agregado](./images/add-copy-client-secret.jpg)

## Compilar y ejecutar el ejemplo

1. Descargue o clone el ejemplo de Microsoft Graph Excel Starter para ASP.NET 4.6

2. Abra la solución del ejemplo en Visual Studio.

3. En el archivo Web.config en el directorio raíz, reemplace los valores de los marcadores de posición en **ida:AppId** e **ida:AppSecret** por los valores que ha copiado durante el registro de la aplicación.

4. Pulse F5 para compilar y ejecutar el ejemplo. Esto restaurará las dependencias de paquetes de NuGet y abrirá la aplicación.

   >Si observa algún error durante la instalación de los paquetes, asegúrese de que la ruta de acceso local donde colocó la solución no es demasiado larga o profunda. Para resolver este problema, mueva la solución más cerca de la raíz de la unidad.

5. Inicie sesión con su cuenta personal, profesional o educativa y conceda los permisos solicitados.

6. Seleccione el botón **Obtener la dirección de correo electrónico**. Cuando finaliza la operación, el nombre y la dirección de correo electrónico del usuario con sesión iniciada se muestra en la página.

7. Elija el botón **Cargar a Excel**. La aplicación crea una nueva fila en el libro demo.xlsx y agrega el nombre de usuario y la dirección de correo electrónico a la misma. Se mostrará un mensaje de Éxito debajo del botón.

## Preguntas y comentarios

Nos encantaría recibir sus comentarios sobre este ejemplo. Puede enviarnos sus preguntas y sugerencias a través de la sección [Problemas](https://github.com/microsoftgraph/aspnet-excelstarter-sample/issues) de este repositorio.

Su opinión es importante para nosotros. Conecte con nosotros en [Stack Overflow](http://stackoverflow.com/questions/tagged/microsoftgraph). Etiquete sus preguntas con [MicrosoftGraph].

## Colaboradores ##

Si le gustaría contribuir a este ejemplo, vea [CONTRIBUTING.md](CONTRIBUTING.md).

Este proyecto ha adoptado el [Código de conducta de código abierto de Microsoft](https://opensource.microsoft.com/codeofconduct/). Para obtener más información, vea [Preguntas frecuentes sobre el código de conducta](https://opensource.microsoft.com/codeofconduct/faq/) o póngase en contacto con [opencode@microsoft.com](mailto:opencode@microsoft.com) si tiene otras preguntas o comentarios.

## Recursos adicionales

- [Otros ejemplos de Microsoft Graph Connect](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Información general de Microsoft Graph](http://graph.microsoft.io)
- [Ejemplos de código de Office Developer](http://dev.office.com/code-samples)
- [Centro para desarrolladores de Office](http://dev.office.com/)

## Copyright
Copyright (c) 2019 Microsoft. Todos los derechos reservados.


