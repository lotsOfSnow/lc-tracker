#!/bin/bash

usage() {
  echo "-c to recreate, -r to rebuild, -d to detach, --env to specify environment"
  exit 0
}

recreate_flag=false
rebuild_flag=false
detach_flag=false

for arg in "$@"; do
    case $arg in
        -h|--help)
            usage
        ;;
        -c)
            recreate_flag=true
        ;;
        -r)
            rebuild_flag=true
        ;;
        -d)
            detach_flag=true
        ;;
        --env)
            env="$2"
            shift
        ;;

    esac
done

DEFAULT_ENV_FILE=".env"

if [ -n "$env" ]; then
    SECRET_ENV_FILE=".env.${env}.secret"
else
    SECRET_ENV_FILE=".env.secret"
fi

DOCKER_COMPOSE_CMD="docker-compose --env-file $DEFAULT_ENV_FILE --env-file $SECRET_ENV_FILE up"

if [ "$recreate_flag" = true ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --force-recreate --no-deps"
fi

if [ "$rebuild_flag" = true ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --build"
fi

if [ "$detach_flag" = true ]; then
    DOCKER_COMPOSE_CMD="$DOCKER_COMPOSE_CMD --detach"
fi


echo $DOCKER_COMPOSE_CMD

$DOCKER_COMPOSE_CMD -d
