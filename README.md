# Run

All files needed to locally host the app are in `./docker` - `compose.yml` holds the runtime service definitions.

Some variables need to be configured depending on your environment, like the target backup directory. You can set them in `.env.secret`.

`start_prod.sh` runs `docker compose up` with env variables from `.env` and `.env.secret`.

`.env.secret` needs to be created manually. Place it next to `compose.yml`.

To override variables from default `.env`, just specify them again, with your own values, like so:
```
BACKUP_TARGET_DIR=/c/backup
```

# Database

## Backup

Every N seconds (specified in `.env` file for `compose.yml`), `pg_dump` is executed on the database
and saved on the host directory specified in `.env` file.

## Restore

`restore.sh` will try to drop the database, and then will restore it from backup using the specified container name.

Sample usage: `./restore.sh -c lctracker-pg-1 -f "C:/backup/2024-05-18-15-32-42.dump" -d lctracker -u prod`.