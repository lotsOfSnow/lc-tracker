#!/bin/bash

FILE=${BACKUP_DB_NAME}-$(date +%Y-%m-%d-%H-%M-%S).dump
PGPASSWORD=${POSTGRES_PASSWORD} pg_dump -h pg -U ${POSTGRES_USER} -Fc ${BACKUP_DB_NAME} > /backup/${FILE}
echo "Backup taken: ${FILE} at $(date +%Y-%m-%d_%H:%M:%S)"
