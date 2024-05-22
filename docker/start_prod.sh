#!/bin/bash

DEFAULT_ENV_FILE=".env"
SECRET_ENV_FILE=".env.secret"

DOCKER_COMPOSE_CMD="docker-compose --env-file $DEFAULT_ENV_FILE --env-file $SECRET_ENV_FILE"

$DOCKER_COMPOSE_CMD up -d
