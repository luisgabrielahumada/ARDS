CREATE OR ALTER  Procedure  DB_Files_Save(@pRecordId  int,@pTableName  varchar(255),@pName  varchar(255), @pContenType  varchar(255),@pPath  varchar(255),@pUrl  varchar(255))
AS
BEGIN
     Insert into Files(TableName,RecordId,Name,ContentType,Path,Url,CreationDate)
     values(@pTableName,@pRecordId,@pName,@pContenType,@pPath,@pUrl,getdate());
END;