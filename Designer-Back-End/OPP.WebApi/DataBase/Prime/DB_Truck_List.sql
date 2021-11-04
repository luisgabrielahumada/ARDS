CREATE OR ALTER  Procedure  DB_Truck_List @pPageIndex  int, @pPageSize int
AS
BEGIN
      Declare @totalRecords int
      Set @totalRecords = (Select count(*) from truck);
      Select *, @totalRecords as totalRecords
      from truck
      ORDER BY CURRENT_TIMESTAMP OFFSET (@pPageIndex-1)*@pPageSize ROWS FETCH FIRST @pPageIndex ROWS ONLY
END;