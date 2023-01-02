# Backend Challenge 20220626
Challenge by Coodesh

Sdk: Microsoft.NET.Sdk
Framework: .NET 6.0

Este projeto tem como função realizar o Scraping do site https://world.openfoodfacts.org/, armazenando os dados em um banco de dados relacional.


| BackendChallenge | 
Descrição: Realiza o Scraping no horário designado e responde as requisições do usuário;
Pacote Referenciados: HtmlAgilityPack(V1.11.46), 
                      Swashbuckle.AspNetCore(V6.2.3);
Projetos Referenciados: ChallangeData.csproj;


| ChallangeData |
Descricao: Uma Library responsável por gerenciar os modelos e as mirações;
Pacotes Referenciados: Microsoft.EntityFrameworkCore.Relational.Design (V1.1.6),
                       Npgsql.EntityFrameworkCore.PostgreSQL=(V7.0.1),
                       Microsoft.EntityFrameworkCore.Tools(V7.0.1);

| BackendChallengeTest |
Descrição: Realiza os testes unitários nos endpoints da Api BackendChallenge;
Pacotes Referenciados: Microsoft.AspNetCore.Mvc.Testing(V6.0.3),
                       Microsoft.NET.Test.Sdk(V17.3.2),
                       NUnit(V3.13.3),
                       NUnit3TestAdapter(V4.2.1);
Pacotes Referenciados: BackendChallenge.csproj, ChallangeData.csproj.
                      




