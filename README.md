# ApiBackendChallenge---Marco_Antonio_Ferreira
Api que faz webscraping do openfoodfacts.org feita para o desafio backend da Coodesh

# Como instalar: 
O projeto pode ser executado pelo IIS Express com a url já definida pela porta SSL no launch.settings. O banco de dados é SQL e local, com um projeto 
de banco SQL na solução, incluíndo um arquivo publish.xml para a publicação/criação do banco de dados na máquina local. Deve ser necessário apenas alterar a 
connection strings dentro do appsettings.json. O projeto possui um Dockerfile que pode ser usado para criar uma imagem, porém não possui como criar 
o banco de dados no docker para o projeto ser totalmente funcional pelo docker.

# Tecnologias utilizadas:
Esse projeto foi feito em C# no .NET 6.0, com um banco de dados em MS SQL. A api em desenvolvimento é vizualidade pelo swagger e o projeto utiliza 
o XUnit para testes unitários. O projeto utiliza o Serilog para informar exceções e usa o FakeItEasy para os testes unitários com o repositório.
é utilizado o ScrapySharp para fazer o web scraping da página openfoodfacts.org.O projeto possui arquivos Docker também, mas não foi feito o 
deploy do projeto no docker.

# Processo de Desenvolvimento da Aplicação 1:
Eu não conhecia bem a ideia de web scraping, achei genial quando pesquisei sobre. Então primeiramente fui entender sobre o que era e como fazer.
Fiz alguns projetos separados para entender como funciona e apliquei no projeto da api. O código referente ao scrapign se encontra na classe WebScraper.
Para executar o scraping com um limite de 100, a pagina inicial do openfoodfacts torna isso mais fácil, tendo em cada página 100 itens. Então,
fiz com que a página inical me entregasse todos os links dos 100 produtos que se encontram nela e a partir deles, buscar cada produto e 
transformar na classe requisitada. O scraping teve alguns problemas, como por exemplo certos items da página não possuírem todos os atributos, como nome e código.
Alguns itens simplesmente não passam as informações, mesmo tento elementos html com os mesmos atributos e ids.  

# Processo de Desenvolvimento da Aplicação 2:
Após o scraping estar funcional, foi feita a api e o banco de dados. O banco foi feito em um projeto SQL dentro do Visual Studio e conta com 1 tabela Product 
e 3 stored procedures. Uma para buscar todos os produtos, uma para buscar um produto por código e uma para inserir os produtos no banco. A stored procedure para inserir 
produtos no banco é usada automaticamente pelo sistema de atualização do banco de dados. Caso seja 2 horas da manhã, o sistema faz outro scraping na página e insere na
base de dados os produtos que não estão na base. As outras stored procedures são usadas pelos métodos Get da api. A api possúi metódos privados que não aparecem no swagger,
responsáveis pela atualização do banco. 

# Processo de Desenvolvimento da Aplicação 3:
Todas as funcionalidades descritas acima passaram por testes unitários para verificar sua funcionaliadde. Os métodos privados precisam ser convertidos em públicos 
para a execução dos testes, porém para rodar a api normalmente, eles devem ser privados. O código para web scraping também possú testes unitários. Após, foi questão de ajeitar 
alguns detalhes e fazer algumas refatorações. Também foi incorporado um log para acompanhamento de erros na api, utilizando o Serilog.

# Processo de Desenvolvimento da Aplicação 4 (O que não deu certo):
Após tudo descrito anteriormente, foi tentada a publicação da api no docker. A api em si é fácil de ser publicada, é apenas necessário mapear bem as portas. O problema foi 
fazer a publicação do banco de dados pelo docker. Não deu muito certo. Então preferi deixar sem mesmo. 



