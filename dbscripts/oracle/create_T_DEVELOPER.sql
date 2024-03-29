PROMPT create_t_developer
DECLARE
	l_EXIST NUMBER;
begin
	SELECT COUNT(*) INTO l_EXIST
	FROM USER_TABLES
	WHERE TABLE_NAME = 'T_DEVELOPER';
	
	if l_EXIST = 0 then
		EXECUTE IMMEDIATE '
		CREATE TABLE "T_DEVELOPER" (
		"ID" NUMBER NOT NULL ENABLE, 
		"IDENTGUID" RAW(16) NOT NULL ENABLE, 
		"NAME" VARCHAR2(255) NOT NULL ENABLE, 
		"EMAIL" VARCHAR2(255) NOT NULL ENABLE, 
		"PHONENUMBER" VARCHAR2(50), 
		"SKILLSET" VARCHAR2(4000),
		"HOBBY" VARCHAR2(4000),
		"UPDATEDON" DATE
		)
		SEGMENT CREATION IMMEDIATE 
		PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
		NOCOMPRESS LOGGING
		TABLESPACE "USERS"
		';
		EXECUTE IMMEDIATE 'CREATE UNIQUE INDEX IPK_DEVELOPER ON T_DEVELOPER (ID) TABLESPACE USERS';
		EXECUTE IMMEDIATE 'CREATE UNIQUE INDEX I_DEVELOPER_01 ON T_DEVELOPER (IDENTGUID) TABLESPACE USERS';
		EXECUTE IMMEDIATE 'ALTER TABLE T_DEVELOPER ADD CONSTRAINT PK_DEVELOPER PRIMARY KEY (ID) USING INDEX IPK_DEVELOPER ENABLE';
	end if;
end;
/