CREATE OR ALTER  Procedure DB_MenuByPermissions_Save(@pId  int,@pMenu  int,
                                           @pRole  int,@pL bit,@pW bit,@pD bit,@pV bit,@pR bit,
                                           @pStatus  bit)
AS
BEGIN
     if not exists(Select * from MenuByPermissions where Menu=@pMenu and Role=@pRole) Begin
         Insert into MenuByPermissions(Role,Menu,L,V,R,w,D,Status,CreationDate)
         Select @pRole,@pMenu,@pL,@pV,@pR,@pW,@pD,@pStatus,getdate();
    end
      else
      begin
          if(@pId = 0) begin
              Select @pId=id
              from MenuByPermissions
              where Menu=@pMenu and Role=@pRole;
          end;


          Update MenuByPermissions
              Set Role=@pRole,
                  L=@pL,
                  V=@pV,
                  W=@pW,
                  R=@pR,
                  D=@pD,
                  CreationDate=getdate(),
                  Status=@pStatus
          Where id=@pId;
     end;

     Select @pId as id;
END;