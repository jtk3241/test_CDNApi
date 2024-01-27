print 'create_s_developer'
DECLARE
	@l_exists Numeric;
BEGIN
	select @l_exists = count(*)
	from INFORMATION_SCHEMA.SEQUENCES
	where SEQUENCE_NAME = 'S_DEVELOPER';
	
	if @l_exists = 0
	begin
		CREATE SEQUENCE [S_DEVELOPER] 
		AS [numeric](18, 0)
		START WITH 1
		INCREMENT BY 1
		MINVALUE 1
		MAXVALUE 899999999999999
		CACHE  20 
	end
END
GO