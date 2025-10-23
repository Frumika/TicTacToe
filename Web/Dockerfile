FROM nginx:alpine

WORKDIR /usr/share/nginx/html

COPY ./1-HTML /usr/share/nginx/html/1-HTML
COPY ./2-CSS /usr/share/nginx/html/2-CSS
COPY ./3-JavaScript /usr/share/nginx/html/3-JavaScript
COPY ./4-Sources /usr/share/nginx/html/4-Sources

COPY ./nginx.conf /etc/nginx/nginx.conf

EXPOSE 80