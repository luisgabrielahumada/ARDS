CREATE OR ALTER  Procedure DB_User_Exists_Save(@pDataBaseId int,  @pUsers varchar(max))
AS
BEGIN
        Delete from UserByDataBase Where  DatabaseId= @pDataBaseId;

        Insert into UserByDataBase(DatabaseId,[User],CreationDate)
        Select @pDataBaseId, Users.Id,Getdate()
        From Users
        Where CONCAT(',',@pUsers,',') LIKE CONCAT('%,',Users.Id,',%') 
END;