# dotnet-api-identity

## Overview
The Identity Web API is a development utility that generates JWT tokens for local development environments. This tool is intended for development use only and should never be deployed to production.

## Security Requirements
### Key Management

Keys must never be hardcoded in the application
Store all cryptographic keys and secrets in a secure key management service:

- AWS Secrets Manager
- Azure Key Vault
- HashiCorp Vault
- Other enterprise secret management solutions

Import keys securely through your deployment pipeline

## Usage
This tool is intended for developers who need to:

Test JWT token validation locally
Develop applications that consume JWT tokens
Mock authentication flows during development

## API Endpoints

The table below lists the available Web API endpoints. Complete endpoint documentation is also available in the OpenAPI specification file (Identity.json) and through the interactive Scalar UI that launches automatically when running the Web API.

| Method | Endpoint | Description |
|--------|----------------|----------------|
| POST    | `/identity/token` | Returns JWT token for a userId |

### Generate Token Example:

```
POST {{Identity_HostAddress}}/identity/token
Content-Type: application/json
{
  "userid": "1f4820b1-21a6-4fe4-8dad-b886fab90f8b",
  "email": "admin@bytesizebrews.com",
  "CustomClaims": {
    "admin" : false
  }
}
```
#### References
1. [Adding JWT Authentication & Authorization in ASP.NET Core](https://www.youtube.com/watch?v=mgeuh8k3I4g&t=465s)
