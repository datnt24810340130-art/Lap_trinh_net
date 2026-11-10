-- Run this in SSMS as a database owner/sysadmin after KaraokeDB exists
-- on LAPTOP-UNQQA3HP. It grants the Windows identity shown in the login error
-- read/write access needed by the application, without granting db_owner.
USE [KaraokeDB];
GO

IF SUSER_ID(N'LAPTOP-UNQQA3HP\Dat') IS NULL
	THROW 50001, 'The SQL Server Windows login LAPTOP-UNQQA3HP\Dat does not exist on this SQL Server. Create/authorize that Windows login first.', 1;
GO

IF DATABASE_PRINCIPAL_ID(N'LAPTOP-UNQQA3HP\Dat') IS NULL
	CREATE USER [LAPTOP-UNQQA3HP\Dat] FOR LOGIN [LAPTOP-UNQQA3HP\Dat];
GO

IF NOT EXISTS
(
	SELECT 1
	FROM sys.database_role_members AS drm
	INNER JOIN sys.database_principals AS rolePrincipal ON rolePrincipal.principal_id = drm.role_principal_id
	INNER JOIN sys.database_principals AS userPrincipal ON userPrincipal.principal_id = drm.member_principal_id
	WHERE rolePrincipal.name = N'db_datareader'
	  AND userPrincipal.name = N'LAPTOP-UNQQA3HP\Dat'
)
	ALTER ROLE [db_datareader] ADD MEMBER [LAPTOP-UNQQA3HP\Dat];
GO

IF NOT EXISTS
(
	SELECT 1
	FROM sys.database_role_members AS drm
	INNER JOIN sys.database_principals AS rolePrincipal ON rolePrincipal.principal_id = drm.role_principal_id
	INNER JOIN sys.database_principals AS userPrincipal ON userPrincipal.principal_id = drm.member_principal_id
	WHERE rolePrincipal.name = N'db_datawriter'
	  AND userPrincipal.name = N'LAPTOP-UNQQA3HP\Dat'
)
	ALTER ROLE [db_datawriter] ADD MEMBER [LAPTOP-UNQQA3HP\Dat];
GO
