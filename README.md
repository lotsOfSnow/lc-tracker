# Run

Easiest way to build and locally host the app is to use Docker. `./docker/compose.yml` holds the runtime service definitions.

Some variables need to be configured depending on your environment, like the target backup directory. You can set them in `.env.secret`.

`run.sh` executes `docker compose up` with env variables from `.env` and `.env.secret` by default. 
It can be used for different environments via the `--env` flag: `--env stage` will use `.env.stage.secret` instead of `.env.secret`.

`.env.secret` needs to be created manually. Place it next to `compose.yml`.

To override variables from default `.env`, just specify them again, with your own values, like so:
```
BACKUP_TARGET_DIR=/c/backup
```

# Database

Scripts related to database are in `./db`.

## Backup

Every N seconds (specified in `.env` file for `compose.yml`), `pg_dump` is executed on the database
and saved on the host directory specified in `.env` file.

### Backup on demand

Execute `backup-now.sh` with the backup container's name as argument.

## Restore

`restore.sh` will try to drop the database, and then will restore it from backup using the specified container name.

Sample usage: `./restore.sh -c lctracker-pg-1 -f "C:/backup/2024-05-18-15-32-42.dump" -d lctracker -u prod`.

## Migrations

`migrate.sh` will generate SQL script out of EF Core migrations, present them to the user, and apply them in the container upon confirmation.

