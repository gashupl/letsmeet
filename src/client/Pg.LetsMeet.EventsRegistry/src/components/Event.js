import React from 'react';

class Event extends React.Component {
    render() {
        return (
            <div className='ui centered card'>
                <div className='content'>
                    <div className='header'>
                        {this.props.name}
                    </div>
                    <div className='center aligned description'>
                        <h2>
                            {this.props.details}
                        </h2>
                    </div>
                </div>
            </div>
        );
    }
}

export default Event

