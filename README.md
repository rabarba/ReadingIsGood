# Reading Is Good

**ReadingIsGood** is an online book web API that stores Customer, Order, Product and also you can create a new customer and place a new order.

It uses .Net Core 5.0, MongoDB, Mediator, FluentValidation, JWT.

It is dockerized so you can try it easily. Also, it has swagger documentation, and when you run the project you can reach it on **localhost:8080/swagger/index.html**


## Installation

If you have docker in your local machine everything will be soo easy for you :)

After cloning the project in your local all you have to do


```bash
docker-compose up
```

that's it.


![image](https://user-images.githubusercontent.com/25769522/104845589-50219580-58e7-11eb-8dec-fa557fad639d.png)

## Design

This app has three layers that are Domain, Infrastructure, and API(Application).

The Application Layer presents the restful endpoints and it has Commands, Query, and Notification Handlers. These Handlers validate the request and if it is valid send it to the infrastructure layer. Also after every command changes, it publishes a notification for logging.

The infrastructure Layer is between The Domain and The Application Layer like a bridge. It consumes interfaces and documents in Domain Layer and it runs CRUD +L operations.

The Domain Layer has documents and interfaces. It has no business logic.


