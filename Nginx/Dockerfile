FROM nginx:1.12-alpine

RUN apk update && apk add openssl
RUN rm /etc/nginx/conf.d/default.conf

RUN openssl dhparam -out /etc/ssl/certs/dhparam.pem 2048
RUN openssl req -x509 -nodes -days 3650 -newkey rsa:2048 -keyout /etc/ssl/certs/self.key -out /etc/ssl/certs/self.crt \
    -subj "/C=RU/ST=Moscow/L=Kremlin/O=FITR2023/OU=Org/CN=Testpoint"
 
COPY nginx.conf /etc/nginx/conf.d
COPY ssl-params.conf /etc/nginx/ssl-params.conf