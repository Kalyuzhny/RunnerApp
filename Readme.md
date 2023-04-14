## Solution description

**Please note it is not a fully functioning prototype. It was created just to demonstrate an ability to build modern microservices by using best practices, DDD  principles and design patterns**

#### Technologies used:
- .NET 7
- Entity Framework Core
- Docker
- MsSql
- Polly
- Mediatr
- Swagger

### Domain
I enjoy running a lot. That's the reason I choose this domain. An application consist of 2 sub-domains:
- Runner - incapsulates data for different people who likes running.
- Tournament - incapsulates data for different tournaments, including their participants (runners).

### Architecture definition
Solution consist of 2 microservices build with DDD practices with clearly separated  bounded context: Runner and Tournament.

Each of them have predefined architecture by using of 3 components:
1) Domain - has definition of domain model (in DDD it is called agregates)
2) Infrastructure - persistence layer. Is responsible for storing domain model in particular data store. It this example MsSql and Entity Framework is used. But since infrastructure is decoupled it may be easilly replaced to use any other data stores (sql storage with dapper, or NoSql storage or event broker, etc)
3) Api - entry point for application. Rest Api. 



- Api has a dependency to Domain and Infrastructure.
- Infrastructure has a dependency to Domain.
- Domain is core component. It doesn't have any other dependencies

### Implementation highlights:

- CQRS pattern. Since Runner microservice is fundamental for the solution it will be used frequently. To improve performance and scalability CQRS pattern is implemented. There are clear separation between Commands and Queries.
- Mediatr. Helped to decouple messages and not to introduce dependencies between them. I used it for CQRS as well as for making communication between Tournament and Runner apis. 
- Factory design pattern. Is used to build runner and tournament db contexts.
- Repository pattern. Is used to incapsulate basic CRUD operations
- Unit of Work pattern. Is used to prevent using of multiple DB contexts. 
- Retries. When making a call from Tournament.APi to Runner.Api - Polly retries is used. It is needed in case of not stable network to increase availability and fault tollerance. 
- Saga. Very simple Saga implementation. Please have a look at AddTurnamentsAsync action in Tournament controller. An application stores Tournament model to DB, then send request to AddTournamentRunners. In case if for some reason Runners was not added, The application execute compensational transaction to delete tournament from database.   
- Entity Framework code first approach. DB structure is defined in DB context and type configurations. 
- Dependency injections. DI are done by embedded in .net dependency injector. No external tools like Ninject, Autofac or Castle Windsor are used. 
- Swagger. Api is automatically documented by swagger. it is easier for clients to use it. 
- Docker and Docker compose support. It is much easier to deploy solution and can be easilly used to provision development environment on localhost. 

### What is not covered here (but has to be supported in real microservice application):
- security: authentication and authorization. Currently APIs are public. To secure it I would use bearer tokens and some OAuth 2 identity provider. 
- api gateway. It will give several advantages:
	- request routing. Single point of contact and less coupling between client and services.
	- api authentication. good practice is not implementing authentication in each of the service but to do it on getway level.
	- caching. it will boost performance
	- load balancing. It will increase resilency
	- logging and monitoring. It will improve maintanability so it would be much easier to understand what's happening with application.  
- initial DB creation and data feeding. To allow using localhost environment in debugging when integrating with database.
- testing(any kind: unit, integration, performance, etc).
- circuit breaker. To prevent a network or service failure from cascading to other services. It also protect application from DDOS attacks. 
- environment configuration. In real application different environments will be supported.
