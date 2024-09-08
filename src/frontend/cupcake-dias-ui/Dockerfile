# Step 1: Build the Angular app
FROM node:20-alpine AS build-stage

# Set the working directory inside the container
WORKDIR /app

COPY . .

# Install dependencies
RUN npm install

# Copy the source files and build the Angular app
RUN npm run build --prod

# Step 2: Serve the Angular app using Nginx
FROM nginx:alpine

# Copy built files to Nginx's default directory
COPY --from=build-stage /app/dist/cupcake-dias-ui/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80 to allow Nginx to serve the content
EXPOSE 80
