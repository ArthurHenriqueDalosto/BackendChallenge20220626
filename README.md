# Backend Challenge 20220626
Challenge by Coodesh

Sdk: Microsoft.NET.Sdk                                                                                                                                                 
Framework: .NET 6.0

Este projeto tem como função realizar o Scraping do site https://world.openfoodfacts.org/, armazenando os dados em um banco de dados relacional.

| BackendChallenge | 
Descrição: Realiza o Scraping no horário designado e responde as requisições do usuário;

| ChallangeData |
Descrição: Uma Library responsável por gerenciar os modelos e as migrações;

| BackendChallengeTest |
Descrição: Realiza os testes unitários nos endpoints da Api BackendChallenge;

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
Para rodar o projeto, você deve seguir as seguintes instruções.

1 - Clone o repositório;

2 - Crie um Postgres com Docker:
> docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=123 -d postgres

3 - Crie uma Docker Network (Para comunicação entre os containers):
> docker network create docker-network

4 - Adicione o container do Postgres na rede:
> docker network connect docker-network postgres


5 - Adicione uma Migration utilizando o Console do Gerenciador de Pacotes do Projeto:

Atenção: selecione ChallangeData com projeto padrão e certifique-se que seu startup project esteja assim:                                                               
![image](https://user-images.githubusercontent.com/90391201/210361264-e4158f92-dcdc-46a1-a108-f0f287c7e6a1.png)                                                         
![image](https://user-images.githubusercontent.com/90391201/210547843-acb4766b-2914-4ac3-acaa-c8ffe66f079d.png)
                                                                                                                                                                 
> add-migration v.0.1

6 - Update no banco de dados:

Atenção: selecione BackendChallenge como projeto padrão                                                                                                                                                                                                                                                    
![image](https://user-images.githubusercontent.com/90391201/210361747-21c6453c-2b29-49d1-b6b5-7f42a9b79862.png)
> Update-Database

7 - Usando o Powershell, Inspecione a rede
> docker network inspect docker-network

8 - Procure pelo Ipv4 do docker postgres                                                                                                                             
                                                                                                                                                      
![image](https://user-images.githubusercontent.com/90391201/210358491-2fed7192-ec10-4323-a545-c75c38871b30.png)

9 - No arquivo appsettings.json dentro do projeto BackendChallenge e troque o IP da connection string pelo IPV4:                                                                                                                                                                                                               
![image](https://user-images.githubusercontent.com/90391201/210363201-a8d35ffa-d178-4abe-b267-0b14fe832156.png)

10 - Ainda no arquivo appsettings.json dentro do projeto BackendChallenge e informe a hora e o minuto em que deseja realizar o Scraping diário:                             
!Atenção:o horário é UTC então sempre adicione 3 horas a mais, caso queira rodar o scrap ao meio dia, deverá escrever 09:00:00 (segundos não influenciam);           
                                                                                                                                                              
![image](https://user-images.githubusercontent.com/90391201/210363293-2d68adb1-1ee7-4936-b958-ba60c9b656c3.png)

11 - Navegue até a pasta  /BackendChallenge20220626

12 - Build o projeto BackendChallenge:
> docker build -t productive-dev/backendchallenge .

13 - Rode o projeto:
> docker run --name backendchallenge -p 8080:80  productive-dev/backendchallenge

14  - Adicione a o container na Newtork criada anteriormente:
> docker network connect docker-network backendchallenge

Após concluir todas as etapas, acesse : localhost:8080/swagger/index.html



OBS: CASO TENHA ALGUM PROBLEMA/DIFICULDADE SIGA ESTE PASSO A PASSO: https://youtu.be/LGKMwQcRC_c



