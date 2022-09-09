DECLARE @ApplicationId		uniqueidentifier
		,@UserName			nvarchar(256)
		,@UserId			uniqueidentifier 
		,@fecha				DATETIME
		,@ApplicationName	nvarchar(256)

DECLARE 
	@Password nvarchar(128),
	@PasswordSalt nvarchar(128)

	
set @ApplicationName = 'PortalHabilitaciones'
set @fecha = getDate()

IF NOT EXISTS(SELECT 'X' FROM aspnet_users WHERE username = 'AGC-mesa' AND applicationid = 'A2EAEF96-F109-4B62-BC31-53E219C76362')
BEGIN

	set @UserName = 'AGC-Mesa'
	set @UserId = null

	--clave: agc-2558
	SET @Password = 'qpv2GqdYAfG1zmhbYEuCANYvJTyv729RrYWb1HBNsNaT5AFESQDFyuaaSo2Ce75i'
	SET @PasswordSalt = '84MzxGsPx2klQcMcHsUzDw=='

	EXEC dbo.aspnet_Membership_CreateUser 
		@ApplicationName,					--@ApplicationName
		@UserName,							--@UserName
		@Password,							--@Password
		@PasswordSalt,						--@PasswordSalt
		'ricardo.carolo@grupomost.com',		--@Email
		NULL,								--@PasswordQuestion
		NULL,								--@PasswordAnswer
		1,									--@IsApproved
		@fecha,								--@CurrentTimeUtc
		@fecha,								--@CreateDate
		0,									--@UniqueEmail
		2,									--@PasswordFormat
		@userid	OUTPUT						--@UserId OUTPUT
	
END
GO
DECLARE @userid uniqueidentifier

SELECT @userid = userid FROM aspnet_Users WHERE UserName = 'AGC-mesa'
IF NOT EXISTS(SELECT 'X' 
			  FROM 
				aspnet_UsersInRoles rol 
			  WHERE 
				rol.UserId = @userid
				AND rol.RoleId  = '98DB6308-50DC-4D76-A1B8-47DBBDB1D5D9'
			  )
BEGIN
	INSERT INTO aspnet_UsersInRoles(UserId,RoleId)
	VALUES(@userid,'98DB6308-50DC-4D76-A1B8-47DBBDB1D5D9')
END
GO
DECLARE @userid uniqueidentifier

SELECT @userid = userid FROM aspnet_Users WHERE UserName = 'AGC-mesa'
IF NOT EXISTS(SELECT 'X' 
			  FROM 
				Usuario
			  WHERE 
				UserId = @userid
			  )
BEGIN
	
	INSERT INTO Usuario(userid,username,email,idlocalidad,idprovincia)
	VALUES(@userid,'AGC-Mesa','ricardo.carolo@grupomost.com',999,2);

END
GO
