CREATE OR ALTER  Procedure DB_DataBase_Save(@pId  int,@pName  varchar(255),@pAlias  varchar(255),@pConnectionString  varchar(255),@pStorage  varchar(255) ='',@pStatus  bit)
AS
BEGIN
     if not exists(Select * from DataBasesObject where Id=@pId) begin
         Insert into DataBasesObject(Name, Alias, ConnectionString, Storage, CreationDate, Status)
         Select @pName,@pAlias,@pConnectionString,@pStorage,getdate(),@pStatus;

         Select @pId =Id
         From DataBasesObject
         Where Name=@pName;
      End
      Else
      Begin
            UPDATE DataBasesObject
            Set Name =@pName,
                Alias =@pAlias,
                ConnectionString =@pConnectionString,
                Storage =@pStorage,
                Status =@pStatus,
                CreationDate=getdate()
            Where Id =@pId;
     end;

     Select @pId as id;
END;
