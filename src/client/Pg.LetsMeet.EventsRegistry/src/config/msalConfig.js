export const msalConfig = {
    auth: {
      clientId: '53a44d37-66df-4b2a-99a9-64fe206e77db', 
      authority: 'https://letsmeet.b2clogin.com/letsmeet.onmicrosoft.com/B2C_1_letsmeet_signin', // Choose SUSI as your default authority.
      knownAuthorities: ['letsmeet.b2clogin.com'], // Mark your B2C tenant's domain as trusted.
      //redirectUri: 'http://localhost:3000/', // You must register this URI on Azure Portal/App Registration. Defaults to window.location.origin
      //postLogoutRedirectUri: 'http://localhost:3000/', // Indicates the page to navigate after logout.
      navigateToLoginRequestUrl: false, // If 'true', will navigate back to the original request location before processing the auth code response.
    },
    cache: {
      cacheLocation: 'sessionStorage', // Configures cache location. 'sessionStorage' is more secure, but 'localStorage' gives you SSO between tabs.
      storeAuthStateInCookie: false, // Set this to 'true' if you are having issues on IE11 or Edge
    }
  }