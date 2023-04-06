![geek_shopping](https://user-images.githubusercontent.com/102628363/230409432-93091453-63a2-42e3-b45f-bfeb98e048c5.png)
# Microservices-GeekShopping

This project consists of a microservices architecture based on REST APIs for communication between the services. It is designed to be scalable and easy to maintain. The project is developed in C#.

![image](	https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)
![jQuery](https://img.shields.io/badge/jquery-%230769AD.svg?style=for-the-badge&logo=jquery&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)

## 📄 About

> Para ler este arquivo em português, acesse [README-pt-br.md](./README-pt-br.md).

In this project, my main goal was to learn about REST communication in microservices, best practices such as design patterns, creating a user-friendly interface, integrating the project with RabbitMQ running in a Docker container for messaging, and understanding user authentication and authorization. I believe that this project has given me access to and improvement of various important skills for a developer, and I was able to improve my knowledge in already known technologies.

Overall, this project has been a great opportunity for me to learn and grow as a developer, and I hope to continue to apply the knowledge and skills I've gained to future projects. If you have any questions or feedback, please don't hesitate to reach out to me.

### ✅Good practices used in this project include:

* REST and RESTful concepts
* Separation of Concerns (SoC)
* Data Transfer Objects (DTO)
* Repository pattern
* Synchronous and asynchronous communication between microservices
* API Gateway
* Authentication Server

### 💻 Technologies used:

* Docker to run a RabbitMQ container locally
* OAuth2, OpenID, JWT, Duende, and Identity Server for authentication
* ASP.NET Core MVC for the front-end
* Ocelot for the API Gateway
* Among other technologies
---

# To install and run the project:

   1. To run you need to have the .NET 6.0 SDK installed on your computer. You can download it from [.NET](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0).
   2. You also need RabbitMQ installed. For ease of use, we recommend using Docker. You can download Docker from: [Docker](https://www.docker.com/get-started/) 
   3. Once you have Docker installed, you can start the RabbitMQ container by opening terminal and running the following command:
   
   `docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management`
   
   4. Clone the project repository 
   5. Open the solution file in Visual Studio
   6. Build the solution
   7. Configure multiple startup projects for each project select "Start" in the Action column. Make sure to select "None" for the `GeekShopping.MessageBus` and `GeekShopping.PaymentProcessor` projects
   8. Use a web browser to access the application at: https://localhost:7109
      
---

## 📁Project Structure:

* ### Front-end:
  - GeeksShopping.Web is an ASP.NET Core MVC web application
* ### Gateway:
  - GeekShopping.APIGateway is an API Gateway with Ocelot
* ### Payments: 
  - IProcessPayment and ProcessPayment, where it has a method called "PaymentProcessor" which is a mock that always returns true. No logic has been applied, but it could be another microservice, another programming language, etc.
* ### Services:
  - ### GeeksShopping.CartAPI
    - It is a shopping cart API that allows customers to manage their carts, apply and remove discount coupons, and process payments by sending them to RabbitMQ.
    - Endpoints: 
     ![cartAPI](https://user-images.githubusercontent.com/102628363/230440440-071ac200-47c5-4363-b3e9-4bf439ce2acc.png)
  - ### GeeksShopping.CouponAPI
    - It is an API for managing discount coupons, which has a single endpoint to retrieve information about a specific discount coupon through the coupon code.
    - Endpoints: 
    ![CouponAPI](https://user-images.githubusercontent.com/102628363/230441485-8e81bbf4-f922-4992-b742-3ff121f8e6c4.png)
  - ### GeeksShopping.Email
    - A service that is called and saves a "mock" of the email content in the SQL database, as it does not have the logic to send emails. It is called after an update occurs in the "DirectPaymentUpdateExchange" queue.
  - ### GeeksShopping.OrderAPI
     - It is a service that deals with the processing of payment orders using the messaging pattern, consuming the checkout queue, creating the entire order, adding it to the database, and finally sending a payment message that will be consumed later.
  - ### GeeksShopping.PaymentAPI
       - It is an API that deals with payment processing, using the messaging pattern with RabbitMQ to process payments and update the payment status of orders, using the "paymentProcessor" method described above.
  - ### GeeksShopping.ProductAPI
     - An API that retrieves information about existing products, creates new products, updates information about existing products, and handles product deletion. As a demonstration of learning about good security practices, it limits the deletion action to users with administrator privileges, so that critical actions such as data deletion are only performed by authorized users
     - Endpoints:
      ![ProductAPI](https://user-images.githubusercontent.com/102628363/230442820-05500c9e-cc35-4f09-a941-ab241e9c81d8.png)
  - ### Authentication:
    - The project uses Duende Identity Server, an open-source solution for identity and access management that provides authentication and authorization features such as OAuth2, OpenID, and JWT. It allows applications to securely authenticate users and manage access to protected resources.
    > You can acess here Duende Software and documentation: [Duende Software](https://duendesoftware.com/products/identityserver).


  
  
  
  
  
