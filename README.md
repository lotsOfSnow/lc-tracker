# Database

## Backup

Every N seconds (specified in `.env` file for `compose.yml`), `pg_dump` is executed on the database
and saved on the host directory specified in `.env` file.

## Restore

`restore.sh` will try to drop the database, and then will restore it from backup using the specified container name.

Sample usage: `./restore.sh -c lctracker-pg-1 -f "C:/backup/2024-05-18-15-32-42.dump" -d lctracker -u prod`.