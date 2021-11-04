CREATE OR ALTER  Procedure DB_User_List(@pPageIndex  int, @pPageSize int, @pDatabaseId int, @pAll int)
AS
BEGIN
     Declare @totalRecords int
     if(@pAll = 1) Begin
         Select *, 0  as totalRecords
         from Users
         Where Id not in (Select [user] From UserByDataBase where UserByDataBase.DatabaseId = @pDatabaseId);

         Select *, 0 as totalRecords
         from Users
         Where Id in (Select [user] From UserByDataBase where UserByDataBase.DatabaseId = @pDatabaseId)
     end
     else
     Begin
          Set @totalRecords = (Select count(*)
                           From Users
                                Inner Join UserByDataBase on Users.Id = UserByDataBase.[User]
                            Where UserByDataBase.DatabaseId = @pDatabaseId);

		  Select Users.*, @totalRecords as totalRecords
		  From Users
			   Inner Join UserByDataBase on Users.Id = UserByDataBase.[User]
		  Where UserByDataBase.DatabaseId = @pDatabaseId
		  ORDER BY CURRENT_TIMESTAMP OFFSET (@pPageIndex-1)*@pPageSize ROWS FETCH FIRST @pPageSize ROWS ONLY
     end
END;