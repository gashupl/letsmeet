import React from 'react'
import ReactDOM from 'react-dom'
import App from './App'
import { PublicClientApplication } from '@azure/msal-browser';
import { msalConfig } from './config/msalConfig';

const rootElement = document.getElementById('root');
const msalInstance = new PublicClientApplication(msalConfig);

ReactDOM.render(<App instance={ msalInstance }/>, rootElement)
