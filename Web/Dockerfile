FROM nginx:alpine

WORKDIR /usr/share/nginx/frontend/Web

COPY ./1-HTML /usr/share/nginx/frontend/Web/1-HTML
COPY ./2-CSS /usr/share/nginx/frontend/Web/2-CSS
COPY ./3-JavaScript /usr/share/nginx/frontend/Web/3-JavaScript
COPY ./4-Sources /usr/share/nginx/frontend/Web/4-Sources

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY ./default.conf /etc/nginx/conf.d/default.conf

EXPOSE 80