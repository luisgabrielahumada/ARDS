-- call DB_Product_List(302,'','',1,20);
DROP PROCEDURE IF EXISTS DB_Product_List;
CREATE PROCEDURE DB_Product_List(IN pInventoryGroup INT,
                                 IN pSku varchar(255),
                                 IN pPart varchar(255),
                                 IN pPageIndex INT,
                                 IN pPageSize INT)
SWL_return:
BEGIN
    set pPageIndex = (pPageSize * (pPageIndex-1));
    DROP TABLE IF EXISTS  products;
    CREATE TEMPORARY TABLE products (Id int, Sku varchar(255), Name varchar(255), Description varchar(255), Image varchar(255),cod_grupo int);
    Insert into products(Id, Sku, Name, Description, Image,cod_grupo)
    Select  cod_parte as Id,
            cod_parte_manual as Sku,
            nombre_parte as Name ,
            descripcion as Description,
            imagen as Image,
            cod_grupo
    from parte
    where cod_parte_manual=pSku
    Union all
    Select  cod_parte as Id,
            cod_parte_manual as Sku,
            nombre_parte as Name ,
            descripcion as Description,
            imagen as Image,
            cod_grupo
    from parte
    Where UPPER(TRIM(nombre_parte)) like CONCAT('%',UPPER(TRIM(pPart)), '%')
      And pPart <> ''
      And pSku =''
      And (cod_grupo=pInventoryGroup or pInventoryGroup=0)
    Union all
    Select  cod_parte as Id,
            cod_parte_manual as Sku,
            nombre_parte as Name ,
            descripcion as Description,
            imagen as Image,
            cod_grupo
    from parte
    where (cod_grupo=pInventoryGroup or pInventoryGroup=0)
      And pSku=''
      And pPart='';

    Set @totalRecords=(select count(*) from products);

    PREPARE STMT FROM
        " SELECT *,@totalRecords as totalRecords FROM products limit ? offset ? ";
    SET @LIMMT = pPageSize;
    SET @OFFSET = pPageIndex;
    EXECUTE STMT USING @LIMMT,@OFFSET;
    DEALLOCATE PREPARE STMT;
END;
