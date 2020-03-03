# product-api
A multi-tenanted cloud-native API for ordering products.

This is a test project written as a coding challenge for a job opportunity.

# Design Notes
I completed the task by writing an API using ASP.NET Core Web Api project type (dotnet new webapi).

The solution is multi-tenanted by seperating out Products and Orders for different Accounts. In reality, a lot more care would be required to ensure data segregation between Accounts, as well as authentication and permissions (User should require Key's/Secret's in order to query/modify data within their own dataset).

The ProductApi project is the entry point in the application, and contains just the ASP.NET Controllers exposed by the API, all logic that is concerned with these Controllers and what they return is emcompassed within the ProductApi project.

The ProductApi.BusinessLogic project contains the Business Logic interfaces and classes that perform all the operations required in the system. The Orchestrator pattern is used to encapsulate Business Logic operations into interfaces in this project, and as such, the code within this project is technology agnostic, so the webapi project may be swapped out with any other technology yet still use the same Orchestrator interfaces.

The ProductApi.DataAccess project contains a very simple in memory data store using the Repository pattern, there is absolutely no persistence, once the application shuts off, all data is lost, as per the requirement "Please only invest minimal effort in data persistence and security.". With that said, implementing data access using the existing interfaces may be done with minimal changes to the existing interfaces (Will probably require extra care with exposing IQueryable or Linq operations).

The ProductApi.Model project contains all the entities for the system, as well as custom Exceptions defined for usage across the solution.

The ProductApi.Tests project contains mostly tests performed on the Orchestrators within the Business Logic layer, all the requirements defined in the spec pertaining to when an order is rejected have a corresponding test to ensure compliance, the tests may be run within Visual Studio, or using the following command from the solution directory:
```
dotnet test ProductApi.Tests
```

# Coding Style
I have used coding style guidelines I have mostly been adhering to over my career, which should be fairly familiar to most C# developers. I have tried to comment most publicly exposed classes/interfaces using /// summaries, using <inheritdoc/> in place of implemented methods.

# Deployment Notes
The project may be run on premise as a regular .NET core application, but is also ready to be deployed to the Cloud, for example to AWS Elastic Beanstalk, to do so, follow the steps below:

1. Open up Visual Studio
2. Select Tools->Extensions and Updates
3. Select the Online link on the left toolbar
4. Search for "AWS Toolkit" appropriate for your Visual Studio installation, and install it.
5. Once you have followed all steps (including restarting Visual Studio), Open up the ProductApi solution in Visual Studio.
6. Right Click on ProductApi in the Solution Explorer, and click "Publish to AWS Elastic Beanstalk..."
7. Fill in "Account Profile to use" and Region to your choices.
8. Select "Create a new application environment" and click Next.
9. Choose a name for the environment, Url, then click Next.
10. Complete the rest of the Wizard with your own choices.
11. Once finished, a new tab will appear showing live updates while creating the new Environment, wait for this to complete (will take a number of minutes).

# Usage
The API is fairly bare-bones, with the operations listed below.

### GET /api/accounts
Gets all the Accounts in the system.

Example Response:
```
[
  {
    "id":"c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "name":"Acme Ltd"
  },
  {
    "id":"71328bc4-6fd0-4b8f-9a03-5cb076107236",
    "name":"Matchstick Company"
  }
]
```
### GET /api/accounts/{id}
Gets a single Account from the system with the given AccountId={id}

```
{
  "id":"c6c8c4a8-7924-4836-8a79-e25ab1466bad",
  "name":"Acme Ltd"
}
```

### POST /api/accounts
Creates a new Account in the system, if successful, returns the Account object, including the AccountId that was created.

Example Request:
```
{
  "Name": "Account Name"
}
```

Example Response:
```
{
    "id": "3ba886ab-0885-429e-98a9-0298235a0728",
    "name": "Account Name"
}
```

### DELETE /api/accounts/{id}
Deletes an Account with the given AccountId={id}, returns Http OK on success.

### GET /api/accounts/{accountId}/orders
Gets all the Orders in the system for a particular Account.

Example Response:
```
[
  {
    "id": "09a7c3ae-8e46-4967-9742-1231453e110d",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "productId": "6ee608f0-7323-477d-bed5-94ee31df2736",
    "quantity": 2,
    "unitPrice": 20,
    "deliveryAddress": "Test Address 2",
    "paid": false,
    "complete": false
  },
  {
    "id": "8e511378-e6ec-4c7b-be21-8cac4592b5c1",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "productId": "6ee608f0-7323-477d-bed5-94ee31df2736",
    "quantity": 1,
    "unitPrice": 15,
    "deliveryAddress": "Test Address 1",
    "paid": false,
    "complete": false
  }
]
```

### GET /api/accounts/{accountId}/orders/{id}
Gets a single Order from the system for the specified Account with AccountId={accountId} and OrderId={id}

Example Response:
```
{
  "id": "09a7c3ae-8e46-4967-9742-1231453e110d",
  "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
  "productId": "6ee608f0-7323-477d-bed5-94ee31df2736",
  "quantity": 2,
  "unitPrice": 20,
  "deliveryAddress": "Test Address 2",
  "paid": false,
  "complete": false
}
```

### POST /api/accounts/{accountId}/orders
Creates an Order in the system for the specified AccountId={accountId}

Example Request:
```
{
	AccountId: "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
	ProductId: "6ee608f0-7323-477d-bed5-94ee31df2736",
	Quantity: 1,
	UnitPrice: 15.0,
	DeliveryAddress: "Test Address 1"
}
```

Example Response:
```
{
    "id": "c6444662-fc25-49f0-8207-4b593a30215e",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "productId": "6ee608f0-7323-477d-bed5-94ee31df2736",
    "quantity": 1,
    "unitPrice": 15.0,
    "deliveryAddress": "Test Address 1",
    "paid": false,
    "complete": false
}
```

### GET /api/accounts/{accountId}/products
Gets all the Products in the system for the specified AccountId={accountId}

Example Response:
```
[
  {
    "id": "6ee608f0-7323-477d-bed5-94ee31df2736",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "name": "Pen",
    "costPrice": 2
  },
  {
    "id": "dbea726e-4b3f-4abc-8fed-c94d43710c68",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "name": "Battery",
    "costPrice": 3
  },
  {
    "id": "0c543a2e-0b2b-4e2b-b180-6eeaec476801",
    "accountId": "c6c8c4a8-7924-4836-8a79-e25ab1466bad",
    "name": "Paper",
    "costPrice": 15
  }
]
```
