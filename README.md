# ArquiteturaBasica

Este é um projeto de uma arquitetura básica para projetos com Web com .NET 4.
O básico do projeto até agora:

1. Asp.NET 4
2. NHibernate 4
3. Ninject para injeção de dependência, implementação do padrão de projeto ServiceLocator e manutenção do ciclo de vida do ISession do NHibernate.
4. Padrão de em Camadas:
  WEB
    > Negocio - Classes de negócio
      > Repositorio - Implementa padrão de projeto Repository
    Comuns
      >Classes utilitarias que são utilizados por todas as camadas
    Dominio
      >Interfaces de negócio
      >Interfaces de repositório
      >Entidades
      
  A camada de negócio e repositório contém classes básicas para serem herdadas. Essas classes fazem quase todo o trabalho e o programador só irá precisar implementar o que for fora do padrão.
  
  O projeto ainda está em desenvolvimento e serve para aqueles que queiram adicionar uma arquitetura em camadas para projetos mais antigos com .NET 3.5. 
Para projetos mais novos, sugiro algo mais atual com Entity Framework e Asp.NET MVC com bootstrap.
Irei colocar um exemplo com banco de dados SQLITE.
