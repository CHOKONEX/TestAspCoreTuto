
/*
https://stackoverflow.com/questions/706664/generate-sql-create-scripts-for-existing-tables-with-query
*/

IF EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[spc_common_create_CreateTableFromExistingTable]') 
	AND type in (N'P', N'PC')
)
BEGIN 
	DROP PROCEDURE [dbo].[spc_common_create_CreateTableFromExistingTable]
END
GO

CREATE PROCEDURE [dbo].[spc_common_create_CreateTableFromExistingTable] 
	@SourceTableName varchar(Max),
	@TargetTableName varchar(Max),
	@AddDropIfItExists bit = 1
AS
BEGIN	
	
	DECLARE @SQL NVARCHAR(MAX) = N''

	SELECT @SQL = [dbo].[fn_common_generate_CreationTableScriptFromTable](@SourceTableName, @AddDropIfItExists)
	SELECT @SQL = REPLACE(@SQL, @SourceTableName, @TargetTableName)

	EXECUTE sp_executesql @SQL;
	PRINT @SQL
END

