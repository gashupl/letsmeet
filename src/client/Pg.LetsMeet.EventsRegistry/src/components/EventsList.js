import React from 'react';
import Event from './Event'
import Client from './Client';
import Config from '../config.json'

class EventsList extends React.Component {

  state = {
    partnerId: '',
    searchCompleted: false,
    errorMessage: '',
    events: []
  }

  handleSearch = (e) => {
    
    this.setState({ errorMessage: '' })

    var client = new Client(Config.API_URL, Config.KEY);
    client.getEventRegistrations(this.state.partnerId, (response) => {
      if (response.ok) {
        response.json().then(body => {
          if (body.length > 0) {
            this.setState({ events: body });
            this.setState({ searchCompleted: true });
          }
          else {
            this.setState({ errorMessage: "No events found :(" });
          }
        });
      }
      else {
        console.log(response);
        response.text().then(errorMessage => {
          this.setState({ errorMessage: errorMessage })
        });
      }

    },
      (error) => {
        console.log("Error handler 2");
        console.log(error);
        this.setState({ errorMessage: error.message })
      });

  }

  handleBackToSearch = (e) => {
    this.setState({ searchCompleted: false });
  }

  handlePartnerIdChange = (e) => {
    this.setState({ partnerId: e.target.value });
  }

  render() {
    if (this.state.searchCompleted == true) {
      const events = this.state.events.map((event) => (
        <Event
          key={event.eventId}
          name={event.name}
          details={event.details}
        />
      ));
      return (
        <div>
          <h1>Your events' requests history:</h1>
          <div id='events'>
            {events}
          </div>
          <div className='ui two bottom attached buttons'>
            <button
              className='ui basic blue button'
              onClick={this.handleBackToSearch}
            >
              Back to the search
            </button>
          </div>
        </div>);
    }
    else {
      return (
        <div className='ui centered card'>
          <div><label className='error'> {this.state.errorMessage}</label></div>
          <div className='content'>
            <div className='ui form'>
              <div className='field'>
                <label>Please enter your Partner ID</label>
                <input
                  type='text'
                  value={this.state.partnerId}
                  onChange={this.handlePartnerIdChange}
                />
              </div>
              <div className='ui two bottom attached buttons'>
                <button
                  className='ui basic blue button'
                  onClick={this.handleSearch}
                >
                  Search
                </button>
              </div>
            </div>
          </div>
        </div>
      );
    }
  }
}

export default EventsList