var express = require('express');
const jsonServer = require('json-server');

var server = express();

server.use(jsonServer.defaults({}));
server.use(jsonServer.bodyParser);

server.use('/api', jsonServer.router('./json-server/db.json'));

server.listen(5173);
