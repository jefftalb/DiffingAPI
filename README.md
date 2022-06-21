# DiffingAPI

## End Points
### Get /v1/diff/{id}

Example curl request:
```
curl -X 'GET' \
'https://localhost:7206/v1/diff/1' \
-H 'accept: application/json'
```

Example response:
```
{
  "DiffResultType": "ContentDoesNotMatch",
  "Diffs": [
    {
      "Offset": 0,
      "Length":1
    },
    {
      "Offset": 2,
      "Length": 2
    }
  ]
}
```
### Put /v1/diff/{id}/left

Parameters:

* data - base64 encoded binary data

Example curl request:
```
curl -X 'PUT' \
'https://localhost:7206/v1/Diff/1/left' \
-H 'accept: */*' \
-H 'Content-Type: application/json' \
-d '{
      "data": "AAAAAA=="
    }'
```

Example response:
```
{
  "id": 1,
  "leftData": "AAAAAA==",
  "rightData": null
}
```
### Put /v1/diff/{id}/right

Parameters:

* data - base64 encoded binary data

Example curl request:
```
curl -X 'PUT' \
'https://localhost:7206/v1/Diff/1/left' \
-H 'accept: */*' \
-H 'Content-Type: application/json' \
-d '{
      "data": "AAAAAA=="
    }'
```

Example response:
```
{
  "id": 1,
  "leftData": "AAAAAA==",
  "rightData": null
}
```
