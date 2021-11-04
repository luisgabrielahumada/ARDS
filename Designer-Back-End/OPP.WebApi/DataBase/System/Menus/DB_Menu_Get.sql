CREATE OR ALTER  Procedure DB_Menu_Get(@pId  int, @pdatabaseId int)
As
BEGIN
        SELECT *
        FROM Menus
        Where id=@pId and DataBaseObject = @pdatabaseId;


        SELECT MenuByPermissions.*,Roles.Name
        FROM MenuByPermissions
                Inner Join Roles On Roles.Id = MenuByPermissions.Role
        Where Menu=@pId;
END;
