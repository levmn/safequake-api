# SafeQuake API

## Sobre o Projeto
Este é um projeto acadêmico desenvolvido para a FIAP com foco em eventos extremos. Nossa escolha foi desenvolver uma API para eventos sísmicos (terremotos) chamada SafeQuake.

## Desenvolvedores - 2TDSPX
- Allan Britto Moreira [RM558948]
- Caio Liang [RM558868]
- Levi Magni [RM98276]

## Objetivo
O SafeQuake é uma API desenvolvida para monitorar, registrar e gerenciar informações sobre eventos sísmicos. O sistema permite o acompanhamento de terremotos em tempo real, fornecendo dados cruciais para tomada de decisões e alertas de segurança.

## Estrutura do Projeto
O projeto segue uma arquitetura em camadas e está organizado da seguinte forma:

```
safequake-api/
├── SafeQuake.API/
│   ├── Controllers/
│   ├── Properties/
│   └── Tests/
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

## Testes dos Endpoints

O projeto inclui uma pasta `Tests` com arquivos REST Client para testar os endpoints da API. Para utilizar:

1. Instale a extensão "REST Client" no Visual Studio Code
2. Navegue até a pasta `SafeQuake.API/Tests`
3. Abra os arquivos `.http`
4. Clique em "Send Request" acima de cada requisição para testá-la

### Exemplos de Testes Disponíveis
- GET /api/earthquakes
- POST /api/earthquakes
- GET /api/earthquakes/{id}
- PUT /api/earthquakes/{id}
- DELETE /api/earthquakes/{id}
