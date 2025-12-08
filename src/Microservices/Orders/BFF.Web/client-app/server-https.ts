import appConfigData from './app.config.json';

import { createServer } from 'https';
import { parse } from 'url';
import fs from 'fs';
import path from 'path';
import next from 'next';

const dev = true;
const hostname = 'localhost';
const port = appConfigData.config.ordersSpaNextJsPort;

const app = next({ dev, hostname, port });
const handle = app.getRequestHandler();

const certsDir = path.join(__dirname, 'certs');

const httpsOptions = {
  key: fs.readFileSync(path.join(certsDir, 'localhost.key')),
  cert: fs.readFileSync(path.join(certsDir, 'localhost.crt')),
};

app.prepare().then(() => {
  createServer(httpsOptions, (req, res) => {
    const parsedUrl = parse(req.url!, true);
    handle(req, res, parsedUrl);
  }).listen(port, () => {
    console.log(`✅ Orders Microservice NextJS SPA running at https://localhost:${port}`);
  });
});