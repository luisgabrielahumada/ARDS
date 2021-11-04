CREATE OR ALTER  procedure DB_Login_Validate
    @v_login VARCHAR(255),
    @v_password  VARCHAR(255)
AS
BEGIN
        Declare @v_Id int, @rowCount int
        Declare @v_name varchar(255)
        Declare @v_sessionID varchar(255)
        Declare @v_email varchar(255)

         SELECT @v_Id=Users.Id,
                @v_name=Users.name,
                @v_sessionID=NewID(),
                @v_email=Users.Email
         FROM Users
         WHERE [Login] = @v_login AND [Password] = @v_password;

         Set @rowCount  = @@rowcount

        if (@rowCount >0)
		Begin
             SELECT @v_Id as id,@v_name as name,@v_sessionID as sessionID,@v_login as login,@v_email as email,'' as message,0 as error
        end

        if (@rowCount = 0)
		Begin
            RaisError('Usuairo y/o Clave invalida...',16,1);
			return
        end
END