# Step 1: Build the Angular app
FROM node:20 AS build-stage

# Set the working directory inside the container
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application files and build the Angular app
COPY . .
RUN npm run build --prod

# Step 2: Serve the Angular app with Nginx
FROM nginx:alpine

# Copy the built Angular files to Nginx's default serving directory
COPY --from=build-stage /app/dist /usr/share/nginx/html

# Copy a custom Nginx configuration file, if necessary
COPY nginx.conf /etc/nginx/nginx.conf

# Expose the default Nginx HTTP port
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
