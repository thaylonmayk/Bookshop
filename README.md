# Bookshop

## Visão Geral

Bookshop é uma aplicação abrangente projetada para gerenciar uma livraria. Inclui funcionalidades para gerenciar livros, autores, assuntos, canais e preços. A aplicação é construída usando ASP.NET Core, Entity Framework Core, PostgreSQL e Angular para o frontend.


## Pré-requisitos

Before you begin, ensure you have the following installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio Code](https://code.visualstudio.com/) or any other IDE of your choice

## Configurando o Banco de Dados

1. **Criar o Banco de Dados:**

   Abra seu cliente PostgreSQL (por exemplo, pgAdmin, psql) e crie um novo banco de dados:

   ```sql
   CREATE DATABASE bookshop;

2. **Executar o Script SQL:**

Aplique o script initial_create.sql para configurar o esquema do banco de dados. Você pode usar a ferramenta de linha de comando psql ou o pgAdmin.

Using psql:

```sh
psql -U your_username -d bookshop -f /path/to/initial_create.sql
```
3. **Atualizar a String de Conexão:**

Substitua seu_usuario pelo seu nome de usuário do PostgreSQL e /path/to/initial_create.sql pelo caminho para o seu script SQL.


```sh
    {
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=bookshop;Username=your_username;Password=your_password"
    }

```

Substitua  your_username e your_password pelas suas credenciais do PostgreSQL.

## Instalação

1. **Backend:**

Navegue até o diretório do projeto e restaure as dependências:

```sh
dotnet restore
```

F2. **Frontend:**

Navegue até o diretório do projeto Angular (e.g., Bookshop.Client) e instale as dependências:
```sh
npm install
```
## Executando a aplicação
### Backend:

Navegue até o diretório do projeto e execute o projeto:
```sh
dotnet run
```
### Frontend:

Navegue até o diretório do projeto Angular (e.g., Bookshop.Client) e inicie o servidor de desenvolvimento Angular:
```sh
ng serve
```
### Testing
To run the unit tests, use the following command:
```sh
dotnet test
```

## Project Structure

- **Bookshop.Domain:** Contém as entidades de domínio e interfaces.
- **Bookshop.Infra.Data:** Contém o contexto de dados, repositórios e migrações.
- **Bookshop.Application:** Contém a lógica de negócios e serviços.
- **Bookshop.Server:** Contém os controladores da API e a configuração de inicialização.
- **Bookshop.Client:** Contém a aplicação frontend Angular.
