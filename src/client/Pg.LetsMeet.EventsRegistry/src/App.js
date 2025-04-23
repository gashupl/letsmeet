import React, { Component } from 'react'
import './App.css'
import { MsalProvider } from "@azure/msal-react";
import EventsDashboard from './components/EventsDashboard';

class App extends Component {

  instance = this.props.instance;

  constructor(props) {
    super(props); 
  }
  
  render() {
    return (
      <MsalProvider instance={this.instance}>
        <div>
          <div><EventsDashboard/></div>
        </div>
      </MsalProvider>
    ); 
  }
}

/* export the component to be used in other components */
export default App
