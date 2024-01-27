print 'create_t_developer'
DECLARE
	@l_exists numeric,
	@iCount numeric,
	@iCount2 numeric;
begin
	select @l_exists = count(*)
	from INFORMATION_SCHEMA.TABLES
	where table_name = 'T_DEVELOPER';
	
	if @l_exists = 0
	begin
		CREATE TABLE [T_DEVELOPER] (
		[ID] NUMERIC NOT NULL, 
		[IDENTGUID] uniqueidentifier NOT NULL, 
		[NAME] VARCHAR(255) NOT NULL, 
		[EMAIL] VARCHAR(255) NOT NULL, 
		[PHONENUMBER] VARCHAR(50), 
		[SKILLSET] VARCHAR(4000),
		[HOBBY] VARCHAR(4000),
		[UPDATEDON] DATETIME2,
		CONSTRAINT [PK_DEVELOPER] PRIMARY KEY CLUSTERED ([ID] ASC)
		);
	end
	
	select @iCount2 = count(*)
	FROM sys.indexes ind 
	INNER JOIN sys.tables t ON ind.object_id = t.object_id 
	where t.name = 'T_DEVELOPER'
	and ind.name = 'I_DEVELOPER_01';

	if (@iCount = 0 and @iCount2 > 0)
	begin
		drop index I_DEVELOPER_01 on T_DEVELOPER;
		set @iCount2 = 0
	end

	if (@iCount = 0 and @iCount2 = 0)
	begin
		CREATE NONCLUSTERED INDEX [I_DEVELOPER_01] ON [T_DEVELOPER] ([IDENTGUID] ASC)
	end
end
GO