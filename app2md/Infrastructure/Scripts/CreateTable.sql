
CREATE TABLE ContactForm(
  ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  FirstName NVARCHAR(50),
  LastName NVARCHAR(50),
  EmailAddress NVARCHAR(50),    
  PhoneNumber NVARCHAR(15),
  AreaOfInterests NVARCHAR(50),
  Message1 NVARCHAR(2048)
)