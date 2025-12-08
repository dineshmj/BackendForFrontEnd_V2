'use client';

import appConfigData from '../app.config.json';

export default function Home() {
  const pmsLoginUrl = appConfigData.config.pmsLoginUrl;
  return (
    <div style={{ padding: '2rem', fontFamily: 'system-ui' }}>
      <h1>Products Microservice</h1>
      <p>This is the <b>Products Microservice</b> home page. Please navigate to <a href={pmsLoginUrl}>Platform Maintenance System</a> to manage products.</p>
    </div>
  );
}