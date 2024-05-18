#!/bin/bash

# e.g: ./restore.sh -c lctracker-pg-1 -f "C:/backup/2024-05-18-15-32-42.dump" -d lctracker -u prod
usage() {
    echo "Copy backup to container, drop DB with --force, restore from backup"
    echo "If DB doesn't exist, just copy and restore"
    echo "Usage: $0 -c <container_name> -f <file_path>  -d <db_name> -u <db_user>"
    echo "  -c <container_name>  Name of the Docker container"
    echo "  -f <file_path>       Full path to the input file to be copied"
    echo "  -d <db_name>         Name of the PostgreSQL database to drop and restore"
    echo "  -u <db_user>         Username to use for dropping and restoring the database"
    exit 1
}

for arg in "$@"; do
    case $arg in
        -h|--help)
            usage
        ;;
    esac
done

while getopts ":c:f:o:d:u:" opt; do
    case $opt in
        c) container_name="$OPTARG"
        ;;
        f) file_path="$OPTARG"
        ;;
        d) db_name="$OPTARG"
        ;;
        u) db_user="$OPTARG"
    esac
done

output_file_name=$(basename "$file_path")

docker cp "$file_path" "$container_name":/"$output_file_name"

if [ $? -eq 0 ]; then
    echo "File $file_path successfully copied to container $container_name as $output_file_name."
else
    echo "Failed to copy the file to the container."
    exit 1
fi

# Terminate all connections & drop
docker exec -i "$container_name" dropdb -U "$db_user" "$db_name" -f

if [ $? -eq 0 ]; then
    echo "Database $db_name successfully dropped by user $db_user in container $container_name."
else
    echo "Failed to drop the database $db_name in container $container_name."
fi

docker exec -i "$container_name" pg_restore -U "$db_user" -C -d postgres "$output_file_name"

if [ $? -eq 0 ]; then
    echo "Database $db_name successfully restored using $output_file_name by user $db_user in container $container_name."
else
    echo "Failed to restore the database $db_name using $output_file_name in container $container_name."
    exit 1
fi
