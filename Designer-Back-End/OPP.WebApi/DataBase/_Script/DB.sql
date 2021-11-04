--create table DataBasesObject
--(
--    Id               int identity(1,1),
--    Name             varchar(255)     not null,
--    Alias            varchar(255)     not null,
--    ConnectionString varchar(2000)    not null,
--    Storage          varchar(255)     null,
--    CreationDate     datetime         not null,
--    Status           bit default  1  not null,
--    constraint DataBasesObject_Name_uindex
--        unique (Name),
--    constraint DataBases_Id_uindex
--        unique (Id)
--);

--alter table DataBasesObject
--    add primary key (Id);

--create table Files
--(
--    Id           int identity(1,1),
--    TableName    varchar(255) null,
--    RecordId     int          null,
--    CreationDate datetime     null,
--    UpdatedId    int          null,
--    Name         varchar(255) null,
--    ContentType  varchar(255) null,
--    Path         varchar(255) null,
--    Url          varchar(255) null,
--    constraint Files_Id_uindex
--        unique (Id)
--);

--alter table Files
--    add primary key (Id);

--create table MenuByPermissions
--(
--    Id           int identity(1,1),
--    Role         int      not null,
--    Menu         int      not null,
--    L            bit      not null,
--    W            bit      null,
--    D            bit      not null,
--    CreationDate datetime not null,
--    V            bit      null,
--    R            bit      null,
--    Status       bit      not null,
--    constraint MenuByPermissions_Id_uindex
--        unique (Id)
--);

--alter table MenuByPermissions
--    add primary key (Id);

--create table Menus
--(
--    Id             int identity(1,1),
--    Name           varchar(255)     not null,
--    Path           varchar(255)     null,
--    Icon           varchar(255)     null,
--    Root           int              null,
--    DataBaseObject int              not null,
--    CreationDate   datetime         not null,
--    Status         bit default 1  not null,
--    [Order]        int              null,
--    SourcePrime    bit              null,
--    constraint Menus_Id_uindex
--        unique (Id)
--);

--alter table Menus
--    add primary key (Id);

--create table Roles
--(
--    Id           int identity(1,1),
--    Name         varchar(255)     not null,
--    CreationDate datetime         not null,
--    Status       bit default 1  not null,
--    constraint Roles_Id_uindex
--        unique (Id)
--);

--alter table Roles
--    add primary key (Id);


--create table UserByDataBase
--(
--    Id           int identity(1,1),
--    [User]         int      not null,
--    DatabaseId   int      not null,
--    CreationDate datetime not null,
--    constraint UserByDataBase_Id_uindex
--        unique (Id)
--);

--alter table UserByDataBase
--    add primary key (Id);

--create table UserByRoles
--(
--    Id           int identity(1,1),
--    [Role]         int      not null,
--    [User]         int      not null,
--    CreationDate datetime null,
--    constraint UserByRoles_Id_uindex
--        unique (Id)
--);

--alter table UserByRoles
--    add primary key (Id);

--create table Users
--(
--    Id           int identity(1,1),
--    Name         varchar(255)     not null,
--    Login        varchar(255)     not null,
--    Password     varchar(255)     not null,
--    Email        varchar(255)     not null,
--    CreationDate datetime         not null,
--    Status       bit default 1    not null,
--    Document     varchar(255)     not null,
--    constraint Users_Id_uindex
--        unique (Id)
--);

--alter table Users
--    add primary key (Id);
