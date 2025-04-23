import React from 'react';
import EventsList from './EventsList'
import NewEventRequestForm from './NewEventRequestForm'
import { AuthenticatedTemplate, UnauthenticatedTemplate, MsalConsumer } from '@azure/msal-react';

class EventsDashboard extends React.Component {

    state = {
        showLoginForm: true,
        showNewForm: false,
        showListForm: false,    
        idToken: ''   
    }

    constructor(props) {
        super(props);
        this.showEventsList  = this.showEventsList.bind(this); 
        this.showMainForm  = this.showMainForm.bind(this); 
    }

    showMainForm = () => {  
        if(this.state.idToken){
            this.setState({
                showNewForm: true, 
                showListForm:false
            });
        }
    }

    showEventsList = () => {

        if(this.state.idToken){
            this.setState({
                showNewForm: false, 
                showListForm: true
            });
        }
    }



    showLoginPopup = async (instance) => {

        console.log(this.state.idToken);
        try{
            //ID tokens contain claims that carry information about the user. It should not be used for authorization purposes.
            if(!this.state.idToken){
                let idToken = await instance.loginPopup(); 
                    this.setState({
                        showLoginForm: false, 
                        showNewForm: true,
                        showListForm: false, 
                        idToken: idToken
                    });
            }
        }
        catch(error) {
            console.log(error);
        }
    }

    logout = async (instance) => {
        try {
            await instance.logoutPopup();
            this.setState({
                showLoginPopup: true,
                idToken: '',
                showNewForm: false,
                showListForm: false
            });
        } catch (error) {
            console.error(error);
        }
    }; 


    render() {


        return (
            <MsalConsumer>
                 { msalInstance => {
                    return (
                        <div>
                            <div className="ui fixed inverted menu">
                                <div className="ui container">
                                    <a className="header item" onClick={this.showMainForm}>
                                        <img className="logo" src="assets/images/logo.png" />
                                            LET'S MEET!
                                    </a>
                                    <a className="item" onClick={this.showEventsList}>Your events</a>
                                    <div className="right menu">
                                    {this.state.idToken === '' ? (
                                        <a className="item" onClick={() => this.showLoginPopup(msalInstance.instance)}>Login</a>
                                        ) : (
                                            <a className="item" onClick={() => this.logout(msalInstance.instance)}>Logout</a>
                                        )}
                                    </div>
                                </div>
                            </div>
                            <div>
                            {this.state.showLoginForm && (
                                    <div>
                                        <div className='ui one column centered grid'>
                                            <div className='content'>
                                                <div>      
                                                    <label>You are not authenticated 🥺. Please login into application </label>                          
                                                </div>      
                                                <div style={{ marginTop: '20px' }}></div>
                                                <div>
                                                    <button className='ui basic blue button' onClick={() => this.showLoginPopup(msalInstance.instance)} >Login</button> 
                                                </div>
                                            </div>                                    
                                        </div>
                                        
                                    </div>
                                )}
                                {this.state.idToken !== '' && this.state.showNewForm && (
                                    <div>
                                        <div className='ui two column centered grid'>
                                            <div className='column'><NewEventRequestForm /></div>
                                        </div>
                                        
                                    </div>
                                )}
    
                                {this.state.idToken !== '' && this.state.showListForm && (
                                    <div>
                                        <div className='ui two column centered grid'>
                                            <div className='column'>
                                                <EventsList />
                                            </div>
                                        </div>
                                    
                                    </div>
                                )}
                            </div>
                        </div>
                     )
                 }
            }
            </MsalConsumer>
        );
    }
}

export default EventsDashboard