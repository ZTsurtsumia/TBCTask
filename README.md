# PersonDirectory CRUD

## Used Libraries

- EF
- Dapper
- Serilog
- MediatR
- Bogus
- FluentValidations
- FluentAssertions
- NetArchTestRules
- XUnit

## General Flow

API Handles requests and Pass it to Command/Query Handler in Application class, which does business logic and with help of Infra performs CRUD Operations.

## General Description

Domain layer is implemented with Rich Domain Model, each property represents Record.  
For Simplicity, we have just one Entity, ConnectedPersons and PhoneNumbers are saved as JSON Object (Something Like JSONB in Postgres). Also, Cities are coming from a Hardcoded List.  
Implemented Simple CQRS pattern with MediatR, Unit and Arch Tests

### Some features(Validation,Filter,Tests) are applied on single Models, Methods just for presentation.

## SetUp

After run, All Migrations will be applied on "(localdb)\\MSSQLLocalDB" automatically (so no need for separate SQL).  
Also, fake Seed data was implemented with help of Bogus Library, after initial run would be better to comment this code in Program.cs to avoid unnecessary data in DB:

![image](https://github.com/user-attachments/assets/5d8d8f4a-11c7-4919-8499-808c54e6a78c)
