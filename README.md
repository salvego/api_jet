Api Jet

BackEnd feito em .NET C# Asp.Net 6 + SQLServer 2008 (em Docker)
Instalar o banco local ou via docker, no .NET precisa instalar as dependências 
executar o power shell como admin dar um cd na pasta raiz onde ficará a pasta da API
digite o seguintes comandos para instalar as depências

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

com o projeto aberto abra o terminal e digite
dotnet ef migrations add CreateDabaseJet
dotnet ef database update

pronto agora é só abrir o banco de dados e confirmar se a migrations foi, se der certo é só rodar a api 

para rodar a api é só digitar
dotnet run
