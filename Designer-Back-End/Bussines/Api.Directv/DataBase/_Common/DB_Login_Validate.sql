DROP PROCEDURE IF EXISTS DB_Login_Validate;
create procedure DB_Login_Validate(
    IN v_login VARCHAR(255),
    IN v_password  VARCHAR(255))
SWL_return:
BEGIN
        Declare v_Id int;
        Declare v_name varchar(255);
        Declare v_sessionID varchar(255);
        Declare v_email varchar(255);

         SELECT Users.Id,
                Users.name,
                UUID()  as sessionID,
                Users.Email
         Into v_Id,v_name,v_sessionID,v_email
         FROM Users
         WHERE Login = v_login AND Password = v_password;

         Set @rowCount  = FOUND_ROWS();

        if (@rowCount >0) then
             SELECT v_Id as id,v_name as name,v_sessionID as sessionID,v_login as login,v_email as email,'' as message,0 as error;
        end if;

        if (@rowCount = 0) then
            select 'Usuairo y/o Clave invalida...' as message, -1 as error;
            LEAVE SWL_return;
        end if;

END;