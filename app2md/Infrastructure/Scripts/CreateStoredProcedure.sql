CREATE OR ALTER PROCEDURE	[dbo].[InsertContactForm] 
                            @FirstName NVARCHAR(50),
                            @LastName NVARCHAR(50),
                            @EmailAddress NVARCHAR(50),
                            @PhoneNumber NVARCHAR(50),
                            @AreaOfInterests INT,
                            @Message1 NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON; 
	INSERT INTO ContactForm (FirstName, LastName, EmailAddress, PhoneNumber, AreaOfInterests, Message1) 
	values (@FirstName, @LastName, @EmailAddress, @PhoneNumber, @AreaOfInterests, @Message1) 
END

