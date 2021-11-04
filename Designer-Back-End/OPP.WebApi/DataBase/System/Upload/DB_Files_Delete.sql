CREATE OR ALTER  Procedure  DB_Files_Delete(@pId  int)
AS
BEGIN
        DELETE
        FROM Files
        Where id=@pId;
END;
