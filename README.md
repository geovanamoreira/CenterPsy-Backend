# 🧠 CenterPsy - Backend

<p align="center">
  <strong>API responsável pelo gerenciamento de dados da plataforma CenterPsy.</strong>
</p>

---

## 📌 Sobre o projeto

O **CenterPsy Backend** é uma API desenvolvida em C# com ASP.NET Core, responsável pela comunicação entre o frontend Angular e o JSON Server.

Seu objetivo é gerenciar usuários, realizar autenticações e administrar os contatos recebidos pela plataforma.

## 🎯 Funcionalidades

| 👤 Usuários | 🔐 Login | 📩 Contatos |
|:---:|:---:|:---:|
| Cadastrar | Autenticar | Cadastrar |
| Consultar | Validar credenciais | Consultar |
| Editar | | Editar |
| Excluir | | Excluir |

---

## 🚀 Como executar

### 1. Clonar o repositório

```bash
git clone https://github.com/geovanamoreira/CenterPsy-Backend.git
```

### 2. Acessar a pasta

```bash
cd CenterPsy-Backend
```

### 3. Instalar as dependências

```bash
npm install
```

### 4. Iniciar o JSON Server

```bash
npm run start:db
```

### 5. Executar o backend

Em outro terminal, execute:

```bash
dotnet restore
dotnet run
```

<p align="center">
  💜 Desenvolvido como projeto de aprendizado em desenvolvimento web.
</p>
