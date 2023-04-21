FROM node

WORKDIR /app

COPY package.json /app

RUN npm install yarn

RUN yarn

COPY . .

EXPOSE 3000

CMD ["yarn", "start"]

