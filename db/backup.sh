#!/bin/bash
PGPASSWORD=${POSTGRES_PASSWORD} pg_dump -h pg -U ${POSTGRES_USER} -Fc ${BACKUP_DB_NAME} > /backup/${BACKUP_DB_NAME}-$(date +%Y-%m-%d-%H-%M-%S).dump
echo "Backup done at $(date +%Y-%m-%d_%H:%M:%S)"
