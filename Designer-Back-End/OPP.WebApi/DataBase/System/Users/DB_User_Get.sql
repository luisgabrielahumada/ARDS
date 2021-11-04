CREATE OR ALTER  Procedure DB_User_Get(@pId  int)
AS
BEGIN
        SELECT *
        FROM Users
        Where id=@pId;

        Select Roles.id, name
        From Roles
            Inner Join UserByRoles On UserByRoles.Role = Roles.Id
        Where UserByRoles.User=@pId;
END;
 