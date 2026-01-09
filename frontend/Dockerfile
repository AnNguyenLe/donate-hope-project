FROM node:23 AS builder
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install --frozen-lockfile
ARG API_BASE_URL
ENV REACT_APP_API_BASE_URL=$API_BASE_URL
COPY . .
RUN npm run build

FROM nginx:alpine-slim
EXPOSE 8080
COPY --from=builder /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf