CREATE OR ALTER  Procedure  DB_MenusByDatabase_Get(@pUserId  int)
AS
BEGIN
       SELECT 
					DataBasesObject.Name,
					DataBasesObject.Alias,
					DataBasesObject.Id,
					'Items' = (select Menus.Id,
							Menus.Name,
							Menus.Path,
							Menus.Root,
							Menus.Icon,
							(Case When ISNULL(Menus.SourcePrime,0)=0 Then '0' else '1' End)  as SourcePrime
							   from Menus
							   WHERE Menus.DataBaseObject = DataBasesObject.Id
									 And Status =1
							   Order by Menus.[Order]
							   FOR JSON PATH) 
				FROM DataBasesObject
					Inner Join UserByDataBase on DataBasesObject.Id = UserByDataBase.DatabaseId and UserByDataBase.[User]=@pUserId
				Where Status =1
				Order by DataBasesObject.Id asc
				FOR JSON PATH
END;