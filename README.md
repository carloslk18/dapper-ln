## Dapper Test

Este repositório é um pequeno projeto de testes com **Dapper**, uma micro-ORM para .NET, focado em consultas SQL performáticas com C#. Ideal para entender como o Dapper funciona na prática em operações CRUD.

## 🚀 Objetivo

Explorar e testar os principais recursos do Dapper:

- Conexão com banco de dados SQL Server
- Execução de comandos SQL com `Query`, `QueryFirst`, `Execute`
- Mapeamento de dados para objetos C#
- Inserção, leitura, atualização e deleção (CRUD)
- Boas práticas com `IDbConnection` e `using`

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [Dapper](https://github.com/DapperLib/Dapper)
- SQL Server (Docker)
- C#

## 📂 Estrutura do Projeto

```bash
📁 dapper-ln
 ┣ 📄 Program.cs         # Código principal de execução
 ┣ 📄 DbConnection.cs    # Classe de conexão com o banco
 ┣ 📄 User.cs            # Modelo de dados
 ┣ 📄 UserRepository.cs  # Métodos com Dapper
 ┗ 📄 README.md          # Este arquivo lindo aqui
```

## 🧪 O que está sendo testado

Como o Dapper se comporta em cenários simples

- Comparação de legibilidade com EF

- Facilidade de manutenção em projetos pequenos

- Velocidade de execução

## 📌 Notas Finais

Este projeto é experimental e educativo. Serve como base para aprender ou relembrar como o Dapper funciona em situações reais.

Feito com café e curiosidade por [Carlos Barbosa](https://github.com/carloslk18)
