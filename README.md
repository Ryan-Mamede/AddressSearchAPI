# AddressSearch API

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-8.0-purple)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019-CC2927?logo=microsoftsqlserver)
![JWT](https://img.shields.io/badge/Security-JWT-yellow)

Uma API REST robusta para consulta, normalização e armazenamento de endereços brasileiros através da integração com ViaCEP.

## 📋 Visão Geral

Esta API permite:
- Consultar endereços via CEP usando a API [ViaCEP](https://viacep.com.br/)
- Normalizar dados de endereços
- Armazenar endereços em SQL Server
- Realizar operações CRUD em endereços
- Proteção de endpoints com autenticação JWT

## 🚀 Stack Tecnológica

- **Backend**: .NET 8, ASP.NET Core
- **ORM**: Entity Framework Core 8
- **Banco de Dados**: SQL Server (LocalDB)
- **Documentação**: Swagger/OpenAPI
- **Autenticação**: JWT (JSON Web Tokens)
- **API Externa**: [ViaCEP](https://viacep.com.br/)

## 🔧 Instalação e Configuração

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB ou instância padrão)
- Git

### 1. Clonar o Repositório

```bash
git clone <URL-DO-SEU-REPO>.git
cd AddressSearch.Services
dotnet restore
```

> **Dica**: No Visual Studio, abra a solução (.sln) e defina `AddressSearch.Api` como Startup Project.

### 2. Configuração do Banco de Dados

A string de conexão padrão está configurada para LocalDB em `src/AddressSearch.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ViaCep;Integrated Security=True;Connect Timeout=30;Encrypt=False"
  },
  "Jwt": {
    "Issuer": "addresssearch",
    "Audience": "addresssearch",
    "Key": "CHAVE-SECRETA-BEM-GRANDINHA-AQUI"
  }
}
```

> ⚠️ **Segurança**: Em ambientes de produção, nunca comite segredos. Utilize User Secrets, Azure Key Vault ou variáveis de ambiente.

### 3. Aplicar Migrações

#### Opção A — CLI do .NET

```bash
# Instalar a ferramenta EF Core (caso não tenha)
dotnet tool install --global dotnet-ef

# Aplicar migrações
dotnet ef database update \
  --project src/AddressSearch.Infra.Data \
  --startup-project src/AddressSearch.Api
```

#### Opção B — Package Manager Console (Visual Studio, recomendo.)

```powershell
# Definir o projeto padrão como AddressSearch.Infra.Data
Update-Database
```

### 4. Executar a API

```bash
dotnet run --project src/AddressSearch.Api
```

Acesse a documentação Swagger: http://localhost:5227/swagger

> **Nota**: A porta pode variar de acordo com seu arquivo `launchSettings.json`.
>
> Se aparecer "Failed to determine the https port for redirect", você pode:
> - Desabilitar `UseHttpsRedirection()` no `Program.cs` (apenas em DEV)
> - Ou criar um perfil HTTPS no `launchSettings.json`

## 🔐 Autenticação

A API utiliza autenticação JWT (Bearer Token). Todas as rotas, exceto login e registro, exigem um token válido.

### Registrar Novo Usuário

```bash
curl -X POST "http://localhost:5227/auth/register" \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"user@dev.test\",\"password\":\"P@ssw0rd!\"}"
```

### Login (Obter Token)

```bash
curl -X POST "http://localhost:5227/auth/login" \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"user@dev.test\",\"password\":\"P@ssw0rd!\"}"
```

Resposta:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6...",
  "expiresIn": 3600
}
```

### Uso do Token

Para qualquer rota protegida, inclua o header:

```
Authorization: Bearer <seu-token-jwt>
Use o token(após registrar e fazer LOGIN) no authorize para poder testar as endpoints!!
```

No Swagger, clique no botão **Authorize** e informe: `Bearer <seu-token-jwt>`

## 📍 Endpoints (Localizações)

Todas as rotas abaixo exigem token de autenticação.

### Criar por CEP (POST)

Consulta o ViaCEP, normaliza e armazena o endereço.

```bash
curl -X POST "http://localhost:5227/localizacoes/cep/26580-080" \
  -H "Authorization: Bearer <token>"
```

### Obter por Id (GET)

```bash
curl -X GET "http://localhost:5227/localizacoes/<ID-GUID>" \
  -H "Authorization: Bearer <token>"
```

### Atualizar CEP (PUT)

Reconsulta o ViaCEP para um novo CEP (não edita manualmente campos).

```bash
curl -X PUT "http://localhost:5227/localizacoes/<ID-GUID>/cep/01001000" \
  -H "Authorization: Bearer <token>"
```

### Remover (DELETE)

```bash
curl -X DELETE "http://localhost:5227/localizacoes/<ID-GUID>" \
  -H "Authorization: Bearer <token>"
```

### Listar com Filtros e Paginação (GET)

```bash
curl -X GET "http://localhost:5227/localizacoes?uf=RJ&cepPrefix=26580&page=1&pageSize=10&sortDesc=true" \
  -H "Authorization: Bearer <token>"
```

#### Parâmetros de Filtro Suportados

| Parâmetro | Descrição | Exemplo |
|-----------|-----------|---------|
| uf | Filtrar por estado | `uf=RJ` |
| cepPrefix | Filtrar por prefixo de CEP | `cepPrefix=26580` |
| dataCriacaoInicio | Data de criação inicial | `dataCriacaoInicio=2025-08-20` |
| dataCriacaoFim | Data de criação final | `dataCriacaoFim=2025-08-25` |
| page | Número da página | `page=1` |
| pageSize | Tamanho da página | `pageSize=10` |
| sortDesc | Ordenar por data de criação decrescente | `sortDesc=true` |

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas:

- **Api**: Controllers, middleware, configuração
- **Services**: Casos de uso, DTOs, mapeamentos
- **Domain**: Entidades, interfaces, contratos
- **Infra.Data**: Implementações EF Core, DbContext, repositórios, migrations, HttpClient

## 🔗 Integração Externa

A API integra-se com o serviço ViaCEP para obter dados de endereços a partir de CEPs.

- **ViaCEP**: https://viacep.com.br/
- **Exemplo de consulta**: https://viacep.com.br/ws/01001000/json/

## 📦 Pacotes Principais

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore.SqlServer
- Swashbuckle.AspNetCore
- Microsoft.EntityFrameworkCore.Design


Desenvolvido por [Ryan Mamede] - © 2025
