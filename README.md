# Teste Sysplan

Tecnologias / Padrões de Arquitetura utilizadas

* C#
* DotNet.Core 3.1
* CQRS
* DDD
* SqlServer
* MongoDB
* FluentValidation
* Event Source
* Mediator

1 - Nesse projetos estamos usando 2 banco de dados, SQLServer para as Escritas(Create,Update,Delete), e o MongoDB para Leitura (Select)

2 - Comandos para rodar criar o migration

	1 - Add-Migration Initial -Context SysplanSqlServerContext
    2 - Update-database -Context SysplanSqlServerContext

3 - Criação do banco MongoDB pelo docker(docker-compose.yml)

	1 - Navegue no seu DOS ou o terminal de sua escolha até a pasta '@DIRETORIODOGIT\sysplan\docker\mongo' 
	2 - Executar o comando docker-compose up -d
	3 - Pronto, contanter do mongoDB criado!

Duvidas? Pode enviar um e-mail. 
Nome: Acaciano Neves
E-mail: acaciano.neves@gmail.com
