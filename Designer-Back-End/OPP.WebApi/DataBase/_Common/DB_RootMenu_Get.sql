CREATE OR ALTER  procedure DB_RootMenu_Get
    @pdatabaseId int
AS
BEGIN
    Select *
    FROM Menus
    Where DataBaseObject=@pdatabaseId
      and Root=0
      and Status=1;
END