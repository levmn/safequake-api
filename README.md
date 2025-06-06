# 🌀 SafeQuake API 

## Sobre o Projeto
Este é um projeto acadêmico desenvolvido para a FIAP com foco em eventos extremos. Nossa escolha foi desenvolver uma API para eventos sísmicos (terremotos) chamada SafeQuake.

## 🧑‍💻 Desenvolvedores - 2TDSPX
- [RM558948] [Allan Brito Moreira](https://github.com/Allanbm100)
- [RM558868] [Caio Liang](https://github.com/caioliang)
- [RM98276] [Levi Magni](https://github.com/levmn)

## Objetivo
O SafeQuake é uma API desenvolvida para monitorar, registrar e gerenciar informações sobre eventos sísmicos. O sistema permite o acompanhamento de terremotos em tempo real, fornecendo dados cruciais para tomada de decisões e alertas de segurança.

## Estrutura do Projeto
O projeto segue uma arquitetura em camadas e está organizado da seguinte forma:

```
safequake-api/
├── SafeQuake.API/
│   ├── Controllers/
│   └── Properties/
│
├── SafeQuake.Application/
│   ├── Interfaces/
│   └── UseCases/
│
├── SafeQuake.Domain/
│   ├── Entities/
│   ├── Requests/
│   └──Responses/
│
├── SafeQuake.Infrastructure/
│   ├── Migrations/
│   ├── Persistence/
│   └── .env.sample
│
├── SafeQuake.MVC/
│   ├── Controllers/
│   ├── Models/
│   ├── Properties/
│   ├── Views/
│   ├── wwwroot/
│   └── Program.cs
│
└── SafeQuake.Service/
    ├── Interfaces/
    ├── Models/
    └── Services/
```

## Fluxo do Projeto
O diagrama abaixo ilustra o fluxo de dados e a interação entre as diferentes camadas do projeto:

```mermaid
graph TD
    subgraph "Presentation Layer"
        MVC["SafeQuake.MVC"]
        API["SafeQuake.API"]
        Swagger["Swagger/OpenAPI"]
    end

    subgraph "Application Layer"
        UC["Use Cases"]
        IRepo["Repository Interfaces"]
        IServ["Service Interfaces"]
    end

    subgraph "Domain Layer"
        E["Entities"]
        Req["Requests"]
        Res["Responses"]
    end

    subgraph "Infrastructure Layer"
        DB["Database Context"]
        Mig["Migrations"]
    end

    subgraph "Service Layer"
        ExtServ["External Services"]
        EQServ["Earthquake Service"]
    end

    MVC --> API
    API --> Swagger
    API --> UC
    
    UC --> IRepo
    UC --> IServ
    UC --> E
    UC --> Req
    UC --> Res
    
    IRepo --> DB
    DB --> Mig
    
    IServ --> ExtServ
    ExtServ --> EQServ
```

### Explicação do Fluxo
1. **Camada de Apresentação**
   - A interface MVC e a API servem como pontos de entrada da aplicação
   - Swagger fornece documentação interativa e teste dos endpoints da API
   - Os Controllers delegam as operações para os Use Cases apropriados

2. **Camada de Aplicação**
   - Use Cases implementam a lógica de negócio
   - Define interfaces para repositórios (IRepo) e serviços externos (IServ)
   - Utiliza entidades do domínio e objetos de Request/Response

3. **Camada de Domínio**
   - Contém as entidades centrais do negócio
   - Define os objetos de Request e Response
   - Representa o núcleo da aplicação, independente de infraestrutura

4. **Camada de Infraestrutura**
   - Implementa as interfaces de repositório definidas na camada de aplicação
   - Gerencia o contexto do banco de dados Oracle
   - Controla as migrações do banco de dados

5. **Camada de Serviço**
   - Fornece serviços externos, como o serviço de terremotos
   - Implementa as interfaces de serviço definidas na camada de aplicação
   - Gerencia a comunicação com APIs externas

## Configuração do Ambiente

### Requisitos
- .NET Core SDK
- Oracle Database
- Visual Studio ou Visual Studio Code

### Configuração do Banco de Dados
1. Certifique-se de ter o Oracle Database instalado e em execução
2. Configure as strings de conexão no arquivo `.env`

### Arquivos de Configuração
1. Crie uma cópia do arquivo `.env.sample` e renomeie para `.env`
2. Configure as seguintes variáveis no arquivo `.env`:

```env
    DB_USER=
    DB_PASSWORD=
    DB_HOST=oracle.fiap.com.br
    DB_PORT=1521
    DB_SID=ORCL
```

## Como Executar o Projeto

1. Clone o repositório:
```bash
git clone https://github.com/levmn/safequake-api.git
cd safequake-api
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Execute as migrações do banco de dados:
```bash
dotnet ef database update
```

4. Execute o projeto MVC:
```bash
cd SafeQuake.MVC
dotnet run
```
