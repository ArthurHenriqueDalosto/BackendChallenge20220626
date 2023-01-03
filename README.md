# Backend Challenge 20220626
Challenge by Coodesh

Sdk: Microsoft.NET.Sdk                                                                                                                                                 
Framework: .NET 6.0

Este projeto tem como função realizar o Scraping do site https://world.openfoodfacts.org/, armazenando os dados em um banco de dados relacional.


| BackendChallenge | 
Descrição: Realiza o Scraping no horário designado e responde as requisições do usuário;
Pacote Referenciados: HtmlAgilityPack(V1.11.46), Swashbuckle.AspNetCore(V6.2.3);
Projetos Referenciados: ChallangeData.csproj;

| ChallangeData |
Descricao: Uma Library responsável por gerenciar os modelos e as mirações;
Pacotes Referenciados: Microsoft.EntityFrameworkCore.Relational.Design (V1.1.6), Npgsql.EntityFrameworkCore.PostgreSQL=(V7.0.1),
Microsoft.EntityFrameworkCore.Tools(V7.0.1);

| BackendChallengeTest |
Descrição: Realiza os testes unitários nos endpoints da Api BackendChallenge;
Pacotes Referenciados: Microsoft.AspNetCore.Mvc.Testing(V6.0.3), Microsoft.NET.Test.Sdk(V17.3.2), NUnit(V3.13.3), NUnit3TestAdapter(V4.2.1);
Pacotes Referenciados: BackendChallenge.csproj, ChallangeData.csproj.

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Para rodar o projeto, você deve seguir as seguintes instruções.

1 - Crie um Postgres com Docker:
> docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=123 -d postgres

2 - Crie uma Docker Newtwork (Para comunicação entre os containers):
> docker network create docker-network

3 - Adicione o container do Postgres na rede:
> docker network connect docker-network postgres

4 - Inspecione a rede
> docker network inspect docker-network

5 - Procure pelo Ipv4 do docker postgres                                                                                                                               
                                                                                                                                                          
![image](https://user-images.githubusercontent.com/90391201/210358491-2fed7192-ec10-4323-a545-c75c38871b30.png)

6 - Vá até o arquivo appsettings.json dentro do projeto BackendChallenge e troque o IP da connection string pelo IPV4:                                                                                                                                                                                                               
![image](https://user-images.githubusercontent.com/90391201/210363201-a8d35ffa-d178-4abe-b267-0b14fe832156.png)

7 - Informe a hora e o minuto em que deseja realizar o Scraping diário:   

!Atenção:o horário é UTC então sempre adicione 3 horas a mais, caso queira rodar o scrap ao meio dia, deverá escrever 09:00:00 (segundos não influenciam);           
                                                                                                                                                              
![image](https://user-images.githubusercontent.com/90391201/210363293-2d68adb1-1ee7-4936-b958-ba60c9b656c3.png)

8 - Adicione uma Migration utilizando o Console do Gerenciador de Pacotes do Projeto:

Atenção: selecione ChallangeData com projeto padrão                                                                                                                   
                                                                                                                                                                 ![image](https://user-images.githubusercontent.com/90391201/210361264-e4158f92-dcdc-46a1-a108-f0f287c7e6a1.png)
> add-migration v.0.1

9 - Update no banco de dados:

Atenção: selecione BackendChallenge como projeto padrão                                                                                                                                                                                                                                                    
![image](https://user-images.githubusercontent.com/90391201/210361747-21c6453c-2b29-49d1-b6b5-7f42a9b79862.png)
> Update-Database

10 - Build o projeto BackendChallenge:
> docker build -t productive-dev/backendchallenge .

11 - Rode o projeto:
> docker run --name backendchallenge -p 8080:80  productive-dev/backendchallenge

12 - Adicione a o container na Newtork criada anteriormente:
> docker network connect docker-network backendchallenge

Após concluir todas as etapas, acesse : localost:8080/swagger/index.html;







