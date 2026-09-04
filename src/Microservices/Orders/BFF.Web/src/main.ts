import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import session from 'express-session';
import passport from 'passport';
import fs from 'fs';
import * as path from 'path';
//import path from 'path';

console.log('MAIN NODE_EXTRA_CA_CERTS:', process.env.NODE_EXTRA_CA_CERTS);
console.log('MAIN EXTRA CA COUNT:', require('tls').getCACertificates('extra').length);


import { Agent, setGlobalDispatcher } from 'undici';
// (reuse your existing `import fs from 'fs';` — no need for a second one)

const caBundle = fs.readFileSync(
  path.join(__dirname, '..', 'certs', 'extra-ca-bundle.pem'),
);

setGlobalDispatcher(
  new Agent({
    connect: {
      ca: caBundle,
    },
  }),
);

async function bootstrap() {
  const httpsOptions = {
    key: fs.readFileSync(path.join(__dirname, '..', 'certs', 'localhost.key')),
    cert: fs.readFileSync(path.join(__dirname, '..', 'certs', 'localhost.crt')),
  };
  
  const app = await NestFactory.create(AppModule, {
    httpsOptions,
  });

  // Enable CORS for the NextJS frontend
  app.enableCors({
    origin: process.env.NEXTJS_URL,
    credentials: true,
  });

  // Session configuration – for cross-site BFF usage
  // app.use(csurf({ cookie: { sameSite: 'none', secure: true } }));

  app.use(
    session({
      secret: process.env.SESSION_SECRET,
      resave: false,
      saveUninitialized: false,
      proxy: true, // Allow secure cookies behind proxy

      cookie: {
        httpOnly: true,
        secure: true,      // Required for SameSite=None
        sameSite: 'none',  // Required for rendering within iFrame of the shell application.
        maxAge: 24 * 60 * 60 * 1000, // 24 hours
      },
    }),
  );

  // Initialize Passport
  app.use(passport.initialize());
  app.use(passport.session());

  const port = Number (process.env.PORT);
  await app.listen(port);
  console.log(`🚀 Orders BFF is running on: https://localhost:${port}`);
}

bootstrap();