# app2md

## prerequisitiies
1. installed visual studio 2019 (because of .NET 3.0 it wont work on vs17)
2. installed Sql Server Managemnt Studio v17.9.1

## steps
1. download repository from https://github.com/rafalkaliszdev/app2md
2. launch visual studio 2019 3. in file “appsettings.json” you need to provide all credentials for MailClient
4. compile and run solution on debug mode (best for code review)
5. complete form (see how validation works on different field types: plain string, email, phone +extra for )
5a. try to type letters 'x' or 'v' for first/last name (example of remote specific validation)
6. on submit, form should send mail, create new record in database and return view 'thank you' with record id

## remarks
1. i've tried to keep this app minimalistic (no need for n-tier layered architecture, only 1 .csproj)

## honest criticism
1. "database" implementation is not best (minimalistic)
2. no services real services
3. no dependency injection in services 
4. no logging (especially events or errors)
5. remote validation could be done in real (not dummy) use case
6. no stored procedure
7. 