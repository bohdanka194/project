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

# Telemetry
We use Azure Application Insights

![screen](https://github.com/bohdanka194/project/blob/master/misc/Screenshot_16.png)

