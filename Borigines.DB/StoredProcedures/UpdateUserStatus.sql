CREATE PROCEDURE [dbo].[UpdateUserStatus]
	@Id int ,
	@status BIT
AS
BEGIN 
	update Users SET Is_Active = @status WHERE Id = @Id ; 
END