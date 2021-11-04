CREATE OR ALTER  Procedure  DB_User_Save(@pId int, @pName varchar(255), @pLogin varchar(255),
                                                      @pPassword varchar(255), @pEmail varchar(255), @pStatus int,
                                                      @pDocument varchar(255), @pRole varchar(max))
AS
BEGIN

    if (@pId = 0) Begin
        insert into Users (Name, Login, Password, Email, CreationDate, Status, Document)
        values (@pName,@pLogin,@pPassword,@pEmail,Getdate(),@pStatus,@pDocument);

        Select Id
        into pId
        From Users
        Where Login = @pLogin;

    End;

    if (@pId > 0) Begin
            update Users
            set Name=@pName,
               Login=@pLogin,
               Status=@pStatus,
               Email=@pEmail,
               Document=@pDocument
            where Id=@pId;
    End;

     if(@pRole!='') begin
         Delete from UserByRoles Where  [user]= @pId;

        Insert into UserByRoles(Role,[User],CreationDate)
         Select Roles.Id, @pId,getdate()
         From Roles
		  Where CONCAT(',',@pRole,',') LIKE CONCAT('%,',Roles.Id,',%') 
     end;

     Select @pId as id;
END;
 
