# DesafioFlex
Nota: flex.sql é o banco de dados em MySQL
Há chamadas de script/styles em CDN no sistema.

O projeto faz requisições para consumir uma lista de clientes de uma apiRest ( https://jsonplaceholder.typicode.com/users/ ). Além de possuir uma API de controle de Dividas RESTFull.

O projeto usa modelo de camadas MVC em C# com Razor, jQuery, Bootstrap, e o banco de dados em MySQL (estou tentando colocar em uma hospedagem no momento para visualização do projeto).

Como descrito acima, o arquivo flex.sql é o arquivo de criação do banco de dados. Possui apenas uma tabela para realização desse desafio.

------

# Getting started

Para iniciar o projeto, é necessário alterar os dados de conexão com o banco de dados no arquivo /Models/DBConnection.cs as variáveis que necessitam ser alteradas são:

 * server 
 * user
 * pass
 
Além disso, é necessário a criação do banco MySQL. Pode se utilizar o arquivo flex.sql para isso.
