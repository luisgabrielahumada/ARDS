CREATE OR ALTER  Procedure  DB_Files_Get(@pRecordId  int, @pTableName varchar(255))
AS
BEGIN
        SELECT *
        FROM Files
        Where RecordId=@pRecordId and TableName =@pTableName;
END
