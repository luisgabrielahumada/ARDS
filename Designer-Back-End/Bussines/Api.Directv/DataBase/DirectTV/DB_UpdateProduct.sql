-- call DB_UpdateProduct(pInt,pName,pDescription);
DROP PROCEDURE IF EXISTS DB_UpdateProduct;
CREATE PROCEDURE DB_UpdateProduct(IN pInt int,
                                  IN pName char(60),
                                  IN pDescription text)
SWL_return:
BEGIN
    Update parte
    Set parte.nombre_parte = pName,
        parte.descripcion = pDescription
    Where parte.cod_parte = pInt;
END;