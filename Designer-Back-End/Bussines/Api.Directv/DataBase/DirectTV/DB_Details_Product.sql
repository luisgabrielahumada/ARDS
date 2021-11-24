-- call DB_Details_Product(Id);
DROP PROCEDURE IF EXISTS DB_Details_Product;
CREATE PROCEDURE DB_Details_Product(IN pId int )
SWL_return:
BEGIN


    Select P.nombre_parte        as 'Name',
           P.descripcion         as Description,
           CP.nombre_categoria   as Category,
           GI.nombre_grupo       as 'Group',
           P.cod_parteEan        as Cod_Ean,
           P.alto                as High,
           P.largo               as 'Long',
           P.ancho               as Width,
           P.peso                as Weight,
           UP.descripcion_unidad as Unit_of_weight,
           F.ContentType,
           F.Url
    from parte P
             inner join categoria_parte CP on CP.cod_categoria_parte = P.cod_categoria
             inner JOIN grupo_inventario GI on GI.cod_grupo_inventario = P.cod_grupo
             inner join unidad_peso UP on UP.cod_unidad_peso = P.unidad_peso
             left join Files F on P.cod_parte = F.RecordId
    where pId = P.cod_parte;


END;

