# Dockerfile for Frontend (Angular)
FROM node:20 AS build-stage

# Set working directory
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application files and build
COPY . .
RUN npm run build --prod

# Use lightweight HTTP server to serve the app
FROM node:20-alpine
WORKDIR /app
COPY --from=build-stage /app/dist /app
EXPOSE 4200
CMD ["npx", "http-server", "/app", "-p", "4200"]
