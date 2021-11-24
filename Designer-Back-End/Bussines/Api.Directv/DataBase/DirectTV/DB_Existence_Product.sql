-- call DB_Existence_Product(Id);
DROP PROCEDURE IF EXISTS DB_Existence_Product;
CREATE PROCEDURE DB_Existence_Product(IN pId int )
SWL_return:
BEGIN
        select
            E.cod_bodega Id,
            nombre_bodega Winery ,
            sum(cantidad) Amount
        from parte P
        inner join existencia E on P.cod_parte = E.cod_parte
        inner join bodega B on E.cod_bodega = B.cod_bodega
        where pId = P.cod_parte
        group by nombre_bodega;
END;