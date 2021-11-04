CREATE OR ALTER  Procedure DB_Menu_List(@pPageIndex  int, @pPageSize int, @DatabaseId int)
AS
BEGIN
       Declare @totalRecords int
      Set @totalRecords = (Select count(*) from Menus Where DataBaseObject = @DatabaseId);
      Select *, @totalRecords as totalRecords
      from Menus
      Where DataBaseObject = @DatabaseId
      ORDER BY CURRENT_TIMESTAMP OFFSET (@pPageIndex-1)*@pPageSize ROWS FETCH FIRST @pPageSize ROWS ONLY
END;