CenterPsy - Backend

Sobre o projeto

O CenterPsy é um projeto desenvolvido para gerenciar informações de uma plataforma voltada à área de psicologia. Este repositório contém o backend responsável por disponibilizar uma API REST que realiza a comunicação entre o frontend e o armazenamento de dados.

A aplicação foi desenvolvida em C# utilizando ASP.NET Core, com integração ao JSON Server para simular uma base de dados durante o desenvolvimento.

Objetivo

O objetivo do backend é centralizar o processamento das solicitações realizadas pelo frontend, permitindo o gerenciamento de usuários, autenticação de login e administração das mensagens enviadas pelo formulário de contato.

Dessa forma, o sistema facilita a comunicação entre as diferentes partes da aplicação e organiza o acesso às informações.

Tecnologias utilizadas

C# – Linguagem de programação utilizada no desenvolvimento.

.NET 9 – Plataforma utilizada para executar a aplicação.

ASP.NET Core Web API – Criação e disponibilização dos endpoints da API REST.

JSON Server – Simulação de uma API para armazenamento e gerenciamento dos dados.

Angular – Frontend integrado ao backend.

HTTP Client – Comunicação entre a aplicação C# e o JSON Server.

Funcionalidades

Usuários

Listar usuários cadastrados.

Buscar usuário por ID.

Cadastrar novos usuários.

Editar informações de usuários.

Excluir usuários.

Login

Autenticar usuários por e-mail e senha.

Verificar as credenciais informadas.

Retornar as informações do usuário autenticado.

Contatos

Listar mensagens recebidas.

Buscar contato por ID.

Cadastrar novas mensagens.

Editar informações dos contatos.

Excluir contatos.

Definir automaticamente o status inicial como "Pendente".

Registrar a data de criação da mensagem.

Estrutura do projeto

CenterPsy-Backend/
│
├── Controllers/
│   ├── UsuarioController.cs
│   ├── LoginController.cs
│   └── ContatoController.cs
│
├── Models/
│   ├── Usuario.cs
│   └── Contato.cs
│
├── Services/
│   ├── UsuarioService.cs
│   ├── LoginService.cs
│   └── ContatoService.cs
│
├── Program.cs
├── CenterPsy-Backend.csproj
├── appsettings.json
├── db.json
├── package.json
└── README.md

Como executar o projeto

1. Pré-requisitos

Antes de iniciar, certifique-se de possuir os seguintes programas instalados:

Visual Studio 2022 ou Visual Studio Code.

.NET 9 SDK.

Node.js e npm.

Git.

2. Clonar o repositório

Abra o terminal e execute:

git clone https://github.com/geovanamoreira/CenterPsy-Backend.git

Entre na pasta do projeto:

cd CenterPsy-Backend

3. Instalar as dependências do JSON Server

Dentro da pasta do projeto, execute:

npm install

Esse comando instala as dependências necessárias para executar o servidor JSON.

4. Iniciar o JSON Server

Execute:

npm run start:db

O JSON Server será iniciado na porta 3000, utilizando o arquivo db.json como base de dados.

A API ficará disponível em:

http://localhost:3000

Importante: mantenha esse terminal aberto enquanto utilizar a aplicação.

5. Iniciar o backend C#

Abra um segundo terminal na pasta do projeto e execute:

dotnet restore

Esse comando restaura as dependências do projeto.

Em seguida, inicie a aplicação:

dotnet run

O ASP.NET Core iniciará a API backend.
