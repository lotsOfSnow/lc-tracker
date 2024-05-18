# Database
## Backup

Every N seconds (specified in `.env` file for `compose.yml`), `pg_dump` is executed on the database
and saved on the host directory specified in `.env` file.

In order to drop database if it exists, and restore from backup, use `restore.sh`, 
e.g. `./restore.sh -c lctracker-pg-1 -f "C:/backup/2024-05-18-15-32-42.dump" -d lctracker -u prod`.