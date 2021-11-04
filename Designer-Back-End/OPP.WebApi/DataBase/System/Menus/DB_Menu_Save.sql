CREATE OR ALTER  Procedure DB_Menu_Save(@pId  int,@pName  varchar(255),@pPath  varchar(255),
                              @pIcon  varchar(255),@pRoot int,@pDataBaseId int,@pStatus  bit,@pSourcePrime  bit)
AS
BEGIN
     if not exists(Select * from Menus where Id=@pId and DataBaseObject = @pDataBaseId) Begin
         Insert into Menus(Name, Path, Icon, Root, DataBaseObject, CreationDate, Status, [Order],SourcePrime)
         Select @pName,@pPath,@pIcon,@pRoot,@pDataBaseId,getdate(),@pStatus, null,@pSourcePrime;

         Select @pId = Id
         From Menus
         Where Name=@pName and DataBaseObject = @pDataBaseId ;
      End
      else
      Begin
            UPDATE Menus
            Set Name =@pName,
                Path =@pPath,
                Icon =@pIcon,
                Root =@pRoot,
                Status =@pStatus,
                DataBaseObject =@pDataBaseId,
                CreationDate=getdate(),
                [Order] = null,
                SourcePrime = @pSourcePrime
            Where Id =@pId and DataBaseObject = @pDataBaseId;
     end;

     Select @pId as id;
END;
