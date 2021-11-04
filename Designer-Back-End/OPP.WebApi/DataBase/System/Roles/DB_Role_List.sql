CREATE OR ALTER  Procedure  DB_Role_List(@pPageIndex  int, @pPageSize int, @DatabaseId int)
AS
BEGIN
      Declare @totalRecords int
      Set @totalRecords = (Select count(*)
                            from Roles);
      Select *, @totalRecords as totalRecords
      from Roles
      ORDER BY CURRENT_TIMESTAMP OFFSET (@pPageIndex-1)*@pPageSize ROWS FETCH FIRST @pPageSize ROWS ONLY
END