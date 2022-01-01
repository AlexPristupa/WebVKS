DO $do$ BEGIN
if not exists(select 1 from pg_shadow where usename = 'lv') then 
begin
create user lv
    encrypted password 'lv2017';
end ;
END IF;END $do$;

DO $do$ BEGIN
if not exists(select 1 from pg_shadow where usename = 'dbo') then 
begin
CREATE ROLE "dbo" LOGIN
  ENCRYPTED PASSWORD 'dbo';
end ;
END IF;END $do$;

DO $do$ BEGIN
if not exists(select 1 from pg_shadow where usename = 'lvrls') then 
begin
create user lvrls
    encrypted password 'lvrls2017';
end ;
END IF;END $do$;
