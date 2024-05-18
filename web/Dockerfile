FROM node:20.10-bookworm-slim AS builder
WORKDIR /app
COPY package*.json .
RUN npm ci
COPY . .

ARG VITE_PUBLIC_API_URL
ENV VITE_PUBLIC_API_URL=$VITE_PUBLIC_API_URL

RUN npm run build
RUN npm prune --production

FROM node:18-alpine
WORKDIR /app
COPY --from=builder /app/build build/
COPY --from=builder /app/node_modules node_modules/
COPY package.json .
ENV NODE_ENV=production
CMD [ "node", "build" ]
