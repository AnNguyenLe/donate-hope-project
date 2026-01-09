#!/usr/bin/env bash
docker network create donate-hope-network

docker container run \
    -d \
    --mount type=volume,source=donate-hope-db-vol,target=/var/lib/postgresql/data \
    -e POSTGRES_PASSWORD=admin123 \
    -e POSTGRES_DB=donate_hope \
    -e POSTGRES_USER=donate-hope-dba \
    --name donate-hope-db \
    --network donate-hope-network \
    postgres:17

docker image build -t donate-hope-backend ./backend

docker container run \
    --name donate-hope-backend-container \
    --network donate-hope-network \
    -p 8080:7066 \
    -e DB_HOST=donate-hope-db \
    -d \
    donate-hope-backend

docker image build \
    --build-arg API_BASE_URL=http://localhost:8080 \
    -t donate-hope-frontend ./frontend

docker container run \
    --name donate-hope-frontend-container \
    --network donate-hope-network \
    -p 8081:80 \
    -d \
    donate-hope-frontend