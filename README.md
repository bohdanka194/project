# Book store
 
| Architerchure | solid & grasp | Project type | Storage     | 
| ------------- | --------------| ------------ | ----------- |
| REST Api + SPA| +/-           | Books store  | SQL Server  |
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
..* 200 Ok
..* 400 Bad request
..* 403 Forbidden

## DELETE api/books/{id}
Responses: 
..* 200 Ok
..* 400 Bad request
..* 404 Not found
..* 403 Forbidden

## GET api/cart/ 
Gets the user's cart contents.
Responses
..* 200 Ok
..* 401 Not authorized

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
..* 200 Ok
..* 400 Bad request
..* 401 Not authorized

## DELETE api/cart/{id} 

Responses
..* 200 Ok
..* 400 Bad request
..* 404 Not found
..* 401 Not authorized

## POST api/cart/order
Commits payment
Responses:
..* 200 Ok `Your order is being processed.`
..* 405 Method not allowed

# Telemetry
We use Azure Application Insights

![screen](https://github.com/bohdanka194/project/blob/master/misc/Screenshot_16.png)

