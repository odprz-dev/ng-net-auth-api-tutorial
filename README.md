# Angular + .Net Core Web API + Auth0. 
Tutorial de como conectar angular con un backend desarrollado en net core web api y configurar una política de autorización y autenticación  con ayuda de Auth0
Este repositorio forma parte del tutorial  "**Angular + .Net Core Web Api + Auth0. Autorización basada en políticas. "**

El proyecto muestra una forma de conectar una aplicación cliente de angular con un servidor web api en .net Core. A demás gracias a auth0 establecemos una forma 
de autenticar usuarios y darles privilegios creando una política basada en autorización. 

[Para entender de que trata te invito a seguirlo dando click aquí](https://dev.to/odprz/series/12036)

# Instrucciones

Antes que nada es importante que tengamos una cuenta en auth0, si no es así crear una en htttps://auth0.com

Configurar un tenant, crear una app y una api.

En el proyecto de .Net `api-resource-server` modificar la seccion `auth:`  dentro del archivo `appssetings.json`  como se muestra:

```json
// ### appsetingss.json ###
{
  "auth": {
    "audience": "<my-audience>",
    "authority": "<my-authority>"
  
}
```

***audience:*** es el Identifier que le asignaste a tu API dentro de la configuración de Auht0 Applications ⇒ APIs 

![https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2Fa5c6963c-39d3-4ff1-a503-589f4ee03736%2FUntitled.png?table=block&id=32076da9-8b54-4048-aaff-c24941dc886b&width=2830&userId=&cache=v2](https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2Fa5c6963c-39d3-4ff1-a503-589f4ee03736%2FUntitled.png?table=block&id=32076da9-8b54-4048-aaff-c24941dc886b&width=2830&userId=&cache=v2)

***authority:*** regularmente es:  htttps://`<nombre-del-tenant>`.`<region>`.auth0.com/ una forma rapida de encontrarlo es yendo a settings ⇒ custom Domains

![https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F6224d7a5-6bb6-4a2b-9e16-e745549f1834%2FUntitled.png?table=block&id=e03c6ca3-7195-4b37-97b3-f10313f3fde6&width=2620&userId=&cache=v2](https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F6224d7a5-6bb6-4a2b-9e16-e745549f1834%2FUntitled.png?table=block&id=e03c6ca3-7195-4b37-97b3-f10313f3fde6&width=2620&userId=&cache=v2)

En el proyecto de angular `client-app-identity` modificar el archivo dentro de la carpeta `enviroments` llamado `auth0-config.ts`  e incluir tu configuración

```tsx
// ### auth0-config.ts ###

export const auth0 ={
  domain: '<my-domain>',
  clientId: '<my-clientId>',
  audience: '<my-audience>'
}
```

Como ya sabemos de donde sacar el domain y el audience no lo explicaré otra vez ( domain es lo mismo que authority).

el clientId lo obtendremos en Applications ⇒ Applications ⇒ <Nombre de nuestra aplicación>

![https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F71a2c2b0-9760-4269-a600-8ffa8aab508d%2FUntitled.png?table=block&id=6fa76a69-4aab-4dec-9679-ced348109c62&width=2810&userId=&cache=v2](https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F71a2c2b0-9760-4269-a600-8ffa8aab508d%2FUntitled.png?table=block&id=6fa76a69-4aab-4dec-9679-ced348109c62&width=2810&userId=&cache=v2)

listo ya teniendo configurado todo, basta con ejecutar los proyectos.

lanza los proyectoes en el siguiente orden

1. Servidor backend
2. Cliente Angular

Backend: abrimos visual studio y ejecutamos el proyecto  con el botón ▶ o F5 esperemos a que instale las dependencias y se ejecutará.

Si estamos usando el cli de **dotnet** en la raíz de la solución  `backend-app-idnetity`

```bash
#instalando paquetes nuguet
dotnet restore api-resource-server
#ejecutando el proyecto
dotnet run --project=api-resource-server --urls=https://localhost:44386
```

Frontend: para ejecutar nuestra aplicación de angular basta con instalas paquetes y ejecutar la aplicación desde el CLI.

  

```bash
### Con Yarn ###
yarn install
### Con NPM ##
npm install

# Una vez instaladas las dependencias 
ng serve -o

```

*Vamos a [http://localhost:4200/](http://localhost:4200/)  y listo!!*

![https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F205a9521-1e71-4092-be48-2679effc5436%2FUntitled.png?table=block&id=92aec842-92a2-4572-a0b7-17fb7d61d916&width=1850&userId=&cache=v2](https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F205a9521-1e71-4092-be48-2679effc5436%2FUntitled.png?table=block&id=92aec842-92a2-4572-a0b7-17fb7d61d916&width=1850&userId=&cache=v2)

Oscar Daniel Perez - odprz.dev@gmail.com - @odprz-dev - 14-02-2021

![https://i.imgur.com/zl8ljC3.png](https://i.imgur.com/zl8ljC3.png)
