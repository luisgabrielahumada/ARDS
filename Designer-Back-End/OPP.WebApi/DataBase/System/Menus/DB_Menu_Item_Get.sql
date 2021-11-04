CREATE OR ALTER  Procedure DB_Menu_Item_Get(@pMenuId int,@pUserId int)
AS
BEGIN


    Select *,Menus.Status as autorized
    From Menus
        Inner Join MenuByPermissions On MenuByPermissions.Menu = Menus.Id and Menus.Id = @pMenuId
        Inner Join UserByRoles on MenuByPermissions.Role =UserByRoles.Role and UserByRoles.[User]=@pUserId
        Inner Join UserByDataBase  on UserByDataBase.[User] = UserByRoles.[User]
        Inner Join DataBasesObject  on DataBasesObject.Id = UserByDataBase.DataBaseId and Menus.DataBaseObject = DataBasesObject.Id;

END