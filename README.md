# Projeto de Login e Gerenciamento de Usuários

Este projeto é uma aplicação web desenvolvida com arquitetura MVC (Model/View/Controller) que permite realizar o CRUD (Create, Read, Update, Delete) de login de usuários e gerenciamento de administradores.

### Funcionalidades

* Cadastro de usuários
* Login de usuários
* Gerenciamento de administradores
* Consulta de clientes por CPF/CNPJ e email em outro banco de dados
* Envio de email com senha criptografada provisória para clientes que não têm acesso

### Tecnologias Utilizadas

* .NET Core
* Entity Framework Core
* Microsoft SQL Server
* HTML, CSS e JavaScript

### Arquitetura do Projeto

O projeto é dividido em várias camadas:

* **Models**: Contém as classes que representam as entidades do banco de dados.
* **Repositorio**: Contém as classes que encapsulam a lógica de acesso ao banco de dados.
* **Helper**: Contém as classes que fornecem funcionalidades auxiliares.
* **Views**: Contém as páginas web que são renderizadas para o usuário.
* **Controller**: Contém as classes que lidam com as requisições e respostas do usuário.

### Instalação e Execução

Para executar o projeto, é necessário ter o .NET Core instalado no seu computador. Em seguida, execute os seguintes comandos no terminal:

```
dotnet restore
dotnet build
dotnet run
```

A aplicação será executada em `http://localhost:5000`. Acesse essa URL no seu navegador para utilizar a aplicação.

### Contribuição

Se você deseja contribuir com o projeto, por favor, faça um fork do repositório e envie um pull request com suas alterações. Certifique-se de que as alterações estejam de acordo com as diretrizes de contribuição do projeto.