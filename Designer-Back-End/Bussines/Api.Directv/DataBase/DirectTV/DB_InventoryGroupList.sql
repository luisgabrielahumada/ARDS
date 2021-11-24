-- call DB_InventoryGroupList();
DROP PROCEDURE IF EXISTS DB_InventoryGroupList;
create procedure DB_InventoryGroupList()
SWL_return:
BEGIN
    select cod_grupo_inventario IdInventoryGroup,
           nombre_grupo  NameInventoryGroup
    from grupo_inventario
    group by nombre_grupo
    order by nombre_grupo asc;
END;
