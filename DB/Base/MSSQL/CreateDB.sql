use master
go

if not exists (select 1 from sysdatabases where name = 'mentolvks')
create database mentolvks COLLATE Cyrillic_General_CI_AS
go

alter database mentolvks SET RECOVERY SIMPLE WITH NO_WAIT
go

alter database mentolvks MODIFY FILE ( NAME = 'mentolvks', FILEGROWTH = 10%)
go

alter database mentolvks SET NEW_BROKER  with rollback immediate
go