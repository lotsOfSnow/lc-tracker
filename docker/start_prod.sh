#!/bin/bash

usage() {
  echo "-c to recreate, -r to rebuild"
  exit 0
}

for arg in "$@"; do
    case $arg in
        -h|--help)
            usage
        ;;
    esac
done

while getopts 'cr' flag; do
  case "${flag}" in
    c) recreate_flag=true ;;
    r) rebuild_flag=true ;;
    d) detach_flag=true ;;
  esac
done

DEFAULT_ENV_FILE=".env"
SECRET_ENV_FILE=".env.secret"

DOCKER_COMPOSE_CMD="docker-compose --env-file $DEFAULT_ENV_FILE --env-file $SECRET_ENV_FILE up"

if [ recreate_flag ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --force-recreate --no-deps"
fi

if [ rebuild_flag ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --build"
fi

if [ detach_flag ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --detach"
fi


echo $DOCKER_COMPOSE_CMD

$DOCKER_COMPOSE_CMD -d
