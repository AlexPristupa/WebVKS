if not exists (select * from syslogins where name = 'lv')
if  ((select cast(right(left((SELECT @@VERSION),25),4) as int)) >= 2003)
	exec('create login lv with password = ''lv2020'', DEFAULT_DATABASE = mentolvks, CHECK_POLICY = OFF')
else
	EXEC sp_addlogin 'lv', 'lv2020', 'mentolvks', null
go

use mentolvks
go

if not exists (select * from sysusers where name = 'lv' and uid < 16382)
        EXEC sp_adduser 'lv'
else
      BEGIN
        EXEC sp_dropuser lv
        EXEC sp_adduser 'lv'
      END
go

EXEC sp_addrolemember 'db_owner', 'lv'  
go