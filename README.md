# 🚀 LarColabs API

Aplicação desenvolvida em **.NET 8 WebAPI** para gerenciamento de **colaboradores**, servindo como base para demonstração de boas práticas e padrões de projeto em APIs modernas.

---

## ✨ Funcionalidades

- ✅ CRUDs
- ✅ Relacionamentos **1:N** e **N:N**
- ✅ Autenticação com **JWT**
- ✅ Padronização com **DTOs e Profiles (AutoMapper)**
- ✅ Uso de **Enums** para maior consistência em valores fixos
- ✅ Documentação automática via **Swagger**
- ✅ Log de ações importantes (criação, atualização, relacionamentos, etc.)

---

## 📐 Padrões e decisões de projeto

A API foi construída adotando alguns padrões e boas práticas que ajudam a manter o código organizado, legível e fácil de manter:

- **DTOs (Data Transfer Objects)** → garantem separação entre as entidades do banco e os dados expostos/recebidos pela API.  
- **Profiles (AutoMapper)** → facilitam a conversão entre entidades e DTOs, reduzindo código repetitivo.  
- **Enums** → usados para representar valores fixos, mantendo consistência e evitando erros de digitação.  
- **Services** → centralizam a lógica de negócio e regras de validação, deixando os controllers mais enxutos.  
- **Autenticação JWT** → fornece segurança e controle de acesso às rotas.  
- **Validações de unicidade** → aplicadas em campos como **CPF**, **Email** e **Telefone**, garantindo integridade dos dados.  

---

## 📂 Estrutura de Pastas

```
LarColabs.WebApi/
├── Controllers/       # Endpoints de entrada da API
├── DTOs/              # Objetos de transferência de dados (Create, Update, Read)
├── Enums/             # Enumerações (tipo telefone, status, etc.)
├── Models/            # Entidades e classes de domínio
├── Profiles/          # Configurações do AutoMapper
├── Services/          # Lógica de negócio e integrações
├── Database/          # Contexto do EF Core e configurações de banco
├── Properties/        # Configurações do projeto
├── Program.cs         # Configuração principal da aplicação
├── appsettings.json   # Configurações de ambiente
```

---

## 🛠 Preparando o ambiente (Local, .NET 8.0)

### 1) Instalar o **.NET 8.0 SDK**
👉 [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

### 2) Instalar o **Entity Framework Core CLI**
👉 [Documentação EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)  
```bash
dotnet tool install --global dotnet-ef
```

### 3) Configurar banco de dados (PostgreSQL recomendado)
👉 [PostgreSQL Download](https://www.postgresql.org/download/)

### 4) Clonar o projeto
```bash
git clone https://github.com/seu-repositorio/larcolabs-api.git
cd larcolabs-api
```

### 5) Restaurar pacotes
```bash
dotnet restore
```

### 6) Rodar migrations e atualizar banco
```bash
dotnet ef database update
```

### 7) Executar aplicação local
```bash
dotnet run
```

A API estará disponível em:  
👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🐳 Executando com Docker

### 1) Build da imagem
```bash
docker build -t larcolabs-api .
```

### 2) Rodar container
```bash
docker run -d -p 5000:5000 larcolabs-api
```

### 3) Acessar documentação
👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 📋 Comandos úteis

### 🔹 Gerenciar migrations
```bash
dotnet ef migrations add NomeDaMigration
dotnet ef database update
```

### 🔹 Buildar aplicação
```bash
dotnet build
```

### 🔹 Executar em modo dev
```bash
dotnet run
```

### 🔹 Acessar logs em tempo real
```bash
dotnet watch run
```

---

## 📚 Tecnologias & Bibliotecas

- [ASP.NET Core WebAPI](https://learn.microsoft.com/aspnet/core) — framework principal para construção da API.  
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) — ORM para acesso e gerenciamento do banco de dados.  
- [AutoMapper](https://automapper.org/) — mapeamento automático entre entidades e DTOs.  
- [JWT Authentication](https://learn.microsoft.com/aspnet/core/security/authentication/jwt) — autenticação baseada em tokens.  
- [Swagger / Swashbuckle](https://swagger.io/tools/swagger-ui/) — documentação e testes da API.  
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) — hash seguro para senhas.  
- [Npgsql](https://www.npgsql.org/) — provider do PostgreSQL para .NET.  

---

## ✅ Conclusão

A **LarColabs API** já está preparada para ser executada localmente ou em containers Docker, conta com boas práticas de organização (DTOs, Profiles, Enums, Services) e autenticação via JWT.  
A documentação via Swagger facilita a validação dos endpoints e acelera o desenvolvimento do frontend.

## 🧑‍💻 Autor

Desenvolvido por **George Lucas** 🤖  
