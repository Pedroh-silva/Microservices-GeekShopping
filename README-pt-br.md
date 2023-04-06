![geek_shopping](https://user-images.githubusercontent.com/102628363/230409432-93091453-63a2-42e3-b45f-bfeb98e048c5.png)
# Microservices-GeekShopping

Este projeto consiste em uma arquitetura de microsserviços baseada em APIs REST para comunicação entre os serviços. Foi projetado para ser escalável e fácil de manter. O projeto é desenvolvido em C#.

![image](	https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)
![jQuery](https://img.shields.io/badge/jquery-%230769AD.svg?style=for-the-badge&logo=jquery&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)

## 📄 Sobre
> To read this file in English, please access [README.md](./README.md).

Neste projeto, meu principal objetivo foi aprender sobre comunicação REST em microsserviços, melhores práticas como padrões de projeto, criar uma interface amigável ao usuário, integrar o projeto com o RabbitMQ executado em um contêiner Docker para mensagens e entender a autenticação e autorização de usuários. Acredito que este projeto me deu acesso e aprimoramento de várias habilidades importantes para um desenvolvedor e pude aprimorar meu conhecimento em tecnologias já conhecidas.

No geral, este projeto foi uma ótima oportunidade para aprender e crescer como desenvolvedor, e espero continuar aplicando o conhecimento e as habilidades que adquiri em projetos futuros. Se você tiver alguma dúvida ou feedback, não hesite em entrar em contato comigo.

### ✅Boas práticas usadas neste projeto:

* Conceitos de REST e RESTful
* Separação de Conceitos ou SOC
* Data Transfer Objects (DTO)
* Padrão Repository
* Comunicação síncrona e assíncrona entre microsserviços
* API Gateway
* Servidor de Autenticação

### 💻 Tecnologias utilizadas:

* Docker para rodar um container do RabbitMQ localmente
* OAuth2, OpenID, JWT, Duende, e Identity Server para autenticação
* ASP.NET Core MVC para o front-end
* Ocelot para a API Gateway
* Entre outras tecnologias
---

# Para instalar e rodar o programa:
   1. Para executar, você precisa ter o SDK .NET 6.0 instalado no seu computador. Você pode baixá-lo em [.NET](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0).
   2. Você também precisa ter o RabbitMQ instalado. Para facilitar o uso, recomendamos utilizar o Docker. Você pode baixar o Docker em: [Docker](https://www.docker.com/get-started/)
   3. Uma vez que você tenha o Docker instalado, você pode iniciar o contêiner RabbitMQ abrindo o terminal e executando o seguinte comando:

   `docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management`
   
   4. Clone o repositório do projeto
   5. Abra o arquivo de solução no Visual Studio
   6. Compile a solução
   7. Configure múltiplos projetos para inicialização, selecione "Start" na coluna Action. Certifique-se de selecionar "None" para os projetos `GeekShopping.MessageBus` e `GeekShopping.PaymentProcessor`
   8. Use um navegador da web para acessar a aplicação em: https://localhost:7109
      
---

## 📁Estrutura do Projeto:

* ### Front-end:
  - GeeksShopping.Web é uma aplicação web ASP.NET Core MVC
* ### Gateway:
  - GeekShopping.APIGateway é uma API Gateway com Ocelot
* ### Payments: 
  - IProcessPayment e ProcessPayment, onde há um método chamado "PaymentProcessor", que é um mock que sempre retorna true. Nenhuma lógica foi aplicada, mas poderia ser outro microsserviço, outra linguagem de programação, etc.
* ### Services:
  - ### GeeksShopping.CartAPI
    - É uma API de carrinho de compras que permite que os clientes gerenciem seus carrinhos, apliquem e removam cupons de desconto e processem pagamentos enviando-os para o RabbitMQ.
    - Endpoints:
     ![cartAPI](https://user-images.githubusercontent.com/102628363/230440440-071ac200-47c5-4363-b3e9-4bf439ce2acc.png)
  - ### GeeksShopping.CouponAPI
    - É uma API para gerenciamento de cupons de desconto, que possui um único endpoint para recuperar informações sobre um cupom de desconto específico através do código do cupom.
    - Endpoints: 
    ![CouponAPI](https://user-images.githubusercontent.com/102628363/230441485-8e81bbf4-f922-4992-b742-3ff121f8e6c4.png)
  - ### GeeksShopping.Email
    - Um serviço que é chamado e salva um "mock" do conteúdo do email no banco de dados SQL, uma vez que não possui a lógica para enviar e-mails. Ele é chamado após uma atualização ocorrer na fila "DirectPaymentUpdateExchange"
  - ### GeeksShopping.OrderAPI
    - É um serviço que lida com o processamento de pedidos usando o padrão de mensageria, consumindo a fila de checkout, criando todo o pedido, adicionando-o ao banco de dados e, finalmente, enviando uma mensagem de pagamento que será consumida posteriormente.
  - ### GeeksShopping.PaymentAPI
      - É uma API que lida com o processamento de pagamentos, usando o padrão de mensagens com RabbitMQ para processar pagamentos e atualizar o status de pagamento dos pedidos, usando o método "paymentProcessor" descrito anteriormente.
  - ### GeeksShopping.ProductAPI
     - Uma API que recupera informações sobre produtos existentes, cria novos produtos, atualiza informações sobre produtos existentes e trata da exclusão de produtos. Como demonstração de aprendizagem sobre boas práticas de segurança, ela limita a ação de exclusão a usuários com privilégios de administrador, para que ações críticas como exclusão de dados sejam realizadas apenas por usuários autorizados.
     - Endpoints:
      ![ProductAPI](https://user-images.githubusercontent.com/102628363/230442820-05500c9e-cc35-4f09-a941-ab241e9c81d8.png)
  - ### Authentication:
    - O projeto utiliza o Duende Identity Server, uma solução de código aberto para gerenciamento de identidade e acesso que fornece recursos de autenticação e autorização, como OAuth2, OpenID e JWT. Ele permite que aplicativos autentiquem usuários com segurança e gerenciem o acesso a recursos protegidos.
    > Você pode acessar aqui o site da Duende Software e a documentação: [Duende Software](https://duendesoftware.com/products/identityserver).
