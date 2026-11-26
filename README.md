# Sistema Financeiro

![.NET Core 2.1](https://img.shields.io/badge/.NET_Core-2.1-512BD4?logo=dotnet)
![ADO.NET](https://img.shields.io/badge/acesso_a_dados-ADO.NET-512BD4)

Aplicação de console para controle financeiro pessoal, com contas e categorias, acessando o banco com **ADO.NET** e camadas separadas.

## Estrutura

```
Model/                 entidades (Account, Category)
Persist/               camada de acesso a dados (DAL) com ADO.NET
Db/                    conexão com o banco
SistemFinanceiroSoN/   aplicação de console
```

## Como rodar
Ajuste a connection string em `Db/Connection.cs` e execute o projeto `SistemFinanceiroSoN`.
