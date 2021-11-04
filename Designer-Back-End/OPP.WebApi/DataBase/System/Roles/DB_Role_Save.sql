CREATE OR ALTER  Procedure DB_Role_Save(@pId int, @pName varchar(255), @pStatus bit)
AS
BEGIN
     if not exists(Select * from Roles where Id=@pId)
     Begin
         Insert into Roles(Name, CreationDate, Status)
         Select @pName,Getdate(),@pStatus;

         Select @pId=Id
         From Roles
         Where Name=@pName;
      End
      else
      Begin
            UPDATE Roles
            Set Name =@pName,
                Status =@pStatus,
                CreationDate=Getdate()
            Where Id =@pId;
      end

     Select @pId as id;
END;

