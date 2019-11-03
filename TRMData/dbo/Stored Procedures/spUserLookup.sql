CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128) 
AS
Begin
	set nocount on;

	select FirstName, LastName, EmailAddress, CreatedDate
	from [dbo].[User]
	where Id = @Id;
End
