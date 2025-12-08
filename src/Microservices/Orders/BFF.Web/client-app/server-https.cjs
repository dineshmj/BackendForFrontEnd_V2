const appConfig = require('./app.config.json');
const config = appConfig.config;

const { createServer } = require('https');
const { parse } = require('url');
const fs = require('fs');
const path = require('path');
const next = require('next');

const dev = true;
const hostname = 'localhost';
const port = appConfig.config.ordersSpaNextJsPort;

const app = next({ dev, hostname, port });
const handle = app.getRequestHandler();

const certsDir = path.join(__dirname, 'certs');

const httpsOptions = {
  key: fs.readFileSync(path.join(certsDir, 'localhost.key')),
  cert: fs.readFileSync(path.join(certsDir, 'localhost.crt')),
};

app.prepare().then(() => {
  createServer(httpsOptions, (req, res) => {
    const parsedUrl = parse(req.url, true);
    handle(req, res, parsedUrl);
  }).listen(port, () => {
    console.log(`✅ NextJS SPA running at https://localhost:${port}`);
  });
});