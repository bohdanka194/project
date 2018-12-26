# Book store
 
| Architerchure | solid & grasp | Project type | Storage     | 
| ------------- | --------------| ------------ | ----------- |
| REST Api + SPA| +/-           | Books store  | SQL Server  |

* [Functional specs](https://github.com/bohdanka194/project/blob/master/misc/Screenshot_9.png)

# Api endpoints

## GET api/books 
Responses: 200 Ok

## POST api/books 
Request body:
```javascript
{
 Author: String,
 Price: number,
 ISBN10: String,
 Description: String,
 Image: String,
 Rating: integer,
 Title: String,
 Votes: integer,
 Pages: integer
}
```
Responses:
* 200 Ok
* 400 Bad request
* 403 Forbidden

## DELETE api/books/{id}
Responses: 
* 200 Ok
* 400 Bad request
* 404 Not found
* 403 Forbidden

## GET api/cart/ 
Gets the user's cart contents.
Responses
* 200 Ok
* 401 Not authorized

## POST api/cart/ 
Gets the user's cart contents.

Request body:
```javascript
{
 item: String,
 quantity: integer
}
```
Responses
* 200 Ok
* 400 Bad request
* 401 Not authorized

## DELETE api/cart/{id} 

Responses
* 200 Ok
* 400 Bad request
* 404 Not found
* 401 Not authorized

## POST api/cart/order
Commits payment

Responses:
* 200 Ok `Your order is being processed.`
* 405 Method not allowed

## GET api/cart/history
Gets the payment history
Responses:
* 200 Ok
* 401 Not autorized
* 405 Method not allowed

# Security

## POST api/auth/token
Gets the JWT token, it will be valid 20 minutes. sha256 algorithm is used

Responses: 
* 200 Ok
```
{
    access_token : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93
                    cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5IiwiaHR0cDovL3NjaGVtYXMub
                    Wljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZi
                    I6MTQ4MTYzOTMxMSwiZXhwIjoxNDgxNjM5MzcxLCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJ
                    odHRwOi8vbG9jYWxob3N0OjUxODg0LyJ9.dQJF6pALUZW3wGBANy_tCwk5_NR0TVBwgnxRbblp5Ho",
    username: "qwerty"
}
```
* 400 `Invalid user name or password.`

# Telemetry
We use Azure Application Insights

![screen](https://github.com/bohdanka194/project/blob/master/misc/Screenshot_16.png)

