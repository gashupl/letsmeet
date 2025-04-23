import React from 'react';
import Client from './Client';
import Config from '../config.json'

class NewEventRequestForm extends React.Component {
    state = {
        name: '',
        details: '',
        organization: '',
        partnerId: '',
        email: '',
        plannedDate: '',
        allowedParticipants: '',
        formSent: false,
        errorMessage: ''
    }


    handleNameChange = (e) => {
        this.setState({ name: e.target.value });
    }
    handleDetailsChange = (e) => {
        this.setState({ details: e.target.value });
    }
    handleOrganizationChange = (e) => {
        this.setState({ organization: e.target.value });
    }
    handlePartnerIdChange = (e) => {
        this.setState({ partnerId: e.target.value });
    }
    handleEmailChange = (e) => {
        this.setState({ email: e.target.value });
    }
    handlePlannedDateChange = (e) => {
        this.setState({ plannedDate: e.target.value });
    }
    handleAllowedParticipantsChange = (e) => {
        this.setState({ allowedParticipants: e.target.value });
    }
    handleSend = () => {
        this.setState({ errorMessage: '' })
        var client = new Client(Config.API_URL, Config.KEY);
        client.sendEventRegistration(this.state, (response) => {
            if (response.ok) {
                console.log("Success handler!");
                this.setState({ formSent: true })
            }
            else {
                console.log("Error handler 1");
                console.log(response);
                response.text().then(errorMessage => {
                    this.setState({ errorMessage: errorMessage })
                })
            }

        },
            (error) => {
                console.log("Error handler 2");
                console.log(error);
                this.setState({ errorMessage: error.message })
            });
    }
    handleClear = () => {
        this.setState({
            name: '',
            details: '',
            organization: '',
            partnerId: '',
            email: '',
            plannedDate: '',
            allowedParticipants: ''
        });
    }
    handleBack = () => {
        this.setState({ formSent: false });
        this.handleClear();
    }
    render() {
        if (this.state.formSent == true) {
            return (
                <div className='ui two bottom attached buttons'>
                    <button
                        className='ui basic blue button'
                        onClick={this.handleBack}
                    >
                        Back to form
                    </button>
                </div>
            );
        }
        else {
            return (
                <div>
                    <div class="ui container center aligned"> 
                     <h1>Welcome to LET'S MEET events' registation service</h1> 
                     <h4> Please enter your event data below to register it in our system.</h4>
                     </div>
                    <div className='ui centered card'>
       
                        <div><label className='error'> {this.state.errorMessage}</label></div>
                        <div className='content'>
                            <div className='ui form'>
                                <div className='required field'>
                                    <label>Name</label>
                                    <input
                                        type='text'
                                        value={this.state.name}
                                        onChange={this.handleNameChange}
                                    />
                                </div>
                                <div className='required field'>
                                    <label>Details</label>
                                    <input id='name'
                                        type='text'
                                        value={this.state.details}
                                        onChange={this.handleDetailsChange}
                                    />
                                </div>
                                <div className='required field'>
                                    <label>Organization</label>
                                    <input
                                        type='text'
                                        value={this.state.organization}
                                        onChange={this.handleOrganizationChange}
                                    />
                                </div>
                                <div className='field'>
                                    <label>Partner ID</label>
                                    <input
                                        type='text'
                                        value={this.state.partnerId}
                                        onChange={this.handlePartnerIdChange}
                                    />
                                </div>
                                <div className='required field'>
                                    <label>E-mail</label>
                                    <input
                                        type='text'
                                        value={this.state.email}
                                        onChange={this.handleEmailChange}
                                    />
                                </div>
                                <div className='required field'>
                                    <label>Planned date</label>
                                    <input
                                        type='datetime-local'
                                        value={this.state.plannedDate}
                                        onChange={this.handlePlannedDateChange}
                                    />
                                </div>
                                <div className='field'>
                                    <label>Allowed participants</label>
                                    <input
                                        type='number'
                                        value={this.state.allowedParticipants}
                                        onChange={this.handleAllowedParticipantsChange}
                                    />
                                </div>
                                <div className='ui two bottom attached buttons'>
                                    <button
                                        className='ui basic blue button'
                                        onClick={this.handleSend}
                                    >
                                        Send
                                    </button>
                                    <button
                                        className='ui basic red button'
                                        onClick={this.handleClear}
                                    >
                                        Clear
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            );
        }

    }
}

export default NewEventRequestForm

