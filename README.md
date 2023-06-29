# AvaTrading

# Solutions

The two solutions I propose are a monolith and a microservice architecture.
Started with a monolith (AvaTrading folder) and after writing the rough logic, split it up by domain into microservices.

Monolithic Architecture:
Pros:
Simplicity: Monolithic architecture is straightforward to develop, deploy, and test as the entire application is contained within a single codebase.
Performance: Monolithic applications can be more efficient in terms of inter-component communication since function calls are in-process and do not involve network overhead.
Cons:
Scalability: Monolithic applications can be challenging to scale horizontally as the entire application needs to be replicated.
Limited technology flexibility: Adopting new technologies or frameworks becomes difficult as the entire application needs to be modified or rewritten.

Microservice Architecture:
Pros:
Scalability: Microservices allow independent scaling of different components, enabling efficient resource allocation and better performance.
Technology diversity: Each microservice can use the most appropriate technology stack, enabling flexibility and adaptability to different requirements.
Independent development and deployment: Each microservice can be developed, tested, and deployed independently, allowing for faster iteration and continuous deployment.
Fault isolation: Failures in one microservice do not affect the entire application, as other services can continue to operate normally.
Cons:
Distributed system complexity: Microservices introduce the complexity of distributed systems, including network communication, service discovery, and data consistency.
Operational overhead: Managing and monitoring multiple microservices can be challenging, requiring additional infrastructure, deployment tools, and monitoring systems.
Service coordination: Coordinating transactions or maintaining consistency across multiple microservices can be complex due to the distributed nature of the architecture.
Increased latency: Inter-service communication can introduce network latency, impacting overall application performance compared to monolithic architectures.

# Components and services
NewsBackgroudService - A service used for fetching data hourly from the Polygon API and storing it in a NoSql(MongoDB) Database
AuthenticationApi - An Web API used for registration and authorization, used a JWT Token for the authorization, stored the user data in a Sql(Sql Server) Database
NewsApi - An web API used for fetching information saved by the NewsBackgroundService which requires a bearer token for authentication in most endpoints
SubscriptionApi - An web API used for subscribing and unsubscribing for news using a Sql(Sql Server) Database

# Task estimations

Research and reading the polygon documentation - 1 hour
Setting up the environment and databases - 3 hours
Coding up the monolith (With testing and debugging) - 2 days
Separating sevice's:
NewsBackgroudService - 2 hours
AuthenticationApi - 2 hours
NewsApi - 2 hours
SubscriptionApi - 1 hour
Refactoring - 2 hours
Testing - 2 hours
