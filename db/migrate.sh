#!/bin/bash

if [ "$#" -ne 3 ]; then
  echo "Usage: $0 <docker_container> <db_name> <db_user>"
  exit 1
fi

DOCKER_CONTAINER=$1
DB_NAME=$2
DB_USER=$3

TIMESTAMP=$(date +"%Y%m%d%H%M%S")

SQL_FILE="migration_script_$TIMESTAMP.sql"
echo "Generating idempotent SQL from EF Core migrations..."
if ! dotnet ef migrations script --idempotent --output "$SQL_FILE" --project ../server/src/LcTracker.Api
then
  echo "Failed to generate migration script."
  exit 1
fi

echo "Generated SQL script:"
cat "$SQL_FILE"

read -r -p "Do you want to execute this script on the database? (y/n): " user_input

if [[ "$user_input" == "y" ]]; then
  echo "Executing the script on the database..."
  if ! docker exec -i "$DOCKER_CONTAINER" psql -U "$DB_USER" -d "$DB_NAME" -f - < "$SQL_FILE"
  then
    echo "Failed to execute the migration script on the database."
  else
    echo "Migration script executed successfully."
  fi
else
  echo "Script execution canceled by user."
fi

read -r -p "Do you want to delete the generated SQL file? (y/n): " delete_input

if [[ "$delete_input" == "y" ]]; then
  if rm -f "$SQL_FILE"
  then
    echo "SQL file deleted successfully."
  else
    echo "Failed to delete the SQL file."
  fi
fi
