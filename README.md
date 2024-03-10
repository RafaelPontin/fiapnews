# FiapNews

### Instruções para Executar o Projeto: 

* Rodar pelo IIS Express:
* 
    ![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/477e76cb-66fd-4b85-94be-226b57b96a92)

* Se rodar o swagger, esta funcionando:
     ![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/08455acf-926b-48d8-9af9-34ea82b26630)

## Credenciais Padrão
  Ao executar o projeto, algumas tabelas já estarão preenchidas com credenciais padrão para diferentes tipos de usuários. Use essas credenciais para acessar o sistema:
 
### Administrador:
**Login:**  admin 

**Senha:**  123456 



### Autor:

**Login:** autor 

**Senha:** 123456 


### Assinante:

**Login:** assinante 

**Senha:** 123456 

[Documentação da regra de negocio e dos termos de aceite](https://github.com/RafaelPontin/fiapnews/wiki/Dom%C3%ADnio)  


|Alunos| E-mail|
|------|-------|
|Antonio Andderson de Freitas Soares|andderson.freitas@gmail.com|
|Elielson do Nascimento Rodrigues|elielsonrj@hotmail.com|
|RAFAEL FAUSTINO MAGALHAES PONTIN|rfmpontin@gmail.com|
|Alexssander Ferreira do Nascimento|alexssanderferreira@hotmail.com|
|Autran Francisco Martine Castão|autran.martine@gmail.com|


# Tech Challenge Fase 3

* Instalar o Docker na maquina
* Rodar o comando no prompt :
  ~~~docker
    docker run -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
  ~~~

* Atualizar a main pelo git
* Inicializar os seguintes projetos (CategoriaConsumer, ConfigSite e FiapNews)

   1. CategoriaConsume: Worker responsável pelo gerenciamento da fila
  
   2. ConfigSite: Micro Serviço responsável pela criação das configurações iniciais de uma nova pagina para o front
  
   3. FiapNews: API responsável pelo gerenciamento do sistema criada na fase 1
   ![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/e63fc40e-08a1-47b2-b5ca-2b9046a531e0)

  
### Como realizar os testes

* Chamar o end point /api/Categoria/Adicionar
 ![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/57d0c9e7-5c82-48dd-b664-79c9f8cb4fd8)

* A mensagem sera criada no RabbitMQ
![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/0cc96628-cafd-4ca1-a34c-831969e039f4)

* No final do processo existirar um novo registro no banco
![image](https://github.com/RafaelPontin/fiapnews/assets/16031920/bd98b74f-7ac8-4dd6-8dae-2c86a9984726)
  
