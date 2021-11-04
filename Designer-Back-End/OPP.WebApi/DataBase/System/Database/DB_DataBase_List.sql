CREATE OR ALTER  Procedure  DB_DataBase_List(@pPageIndex  int, @pPageSize int)
AS
BEGIN
      Declare @totalRecords int
      Set @totalRecords = (Select count(*) from DataBasesObject);
      Select *, @totalRecords as totalRecords
      from DataBasesObject
       ORDER BY CURRENT_TIMESTAMP OFFSET (@pPageIndex-1)*@pPageSize ROWS FETCH FIRST @pPageSize ROWS ONLY
END;
