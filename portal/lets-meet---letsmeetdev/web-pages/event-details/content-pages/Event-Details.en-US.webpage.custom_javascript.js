document.addEventListener('DOMContentLoaded', function() {
    // Get the event ID from the URL query parameter
    function getEventIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('id');
    }

    function getEventApiUrl(eventId) {
        const portalUrl = window.location.origin;
        var apiUrl = `${portalUrl}/_api/pg_events(${encodeURIComponent(eventId)})`
            + '?$select=pg_allowedparticipantsquantity,pg_registeredparticipantsquantity';
        return apiUrl;
    }
    
    // Update the Register button href with the event ID
    function updateRegisterButtonHref() {
        const eventId = getEventIdFromUrl();
        const registerButton = document.getElementById('registerButton');
        
        if (eventId && registerButton) {
            // Check if the href already has query parameters
            const currentHref = registerButton.getAttribute('href');
            const separator = currentHref.includes('?') ? '&' : '?';
            
            // Update the href with the event ID
            registerButton.setAttribute('href', currentHref + separator + 'eventId=' + encodeURIComponent(eventId));
        }
    }

    function enableRegisterButton() {
        const eventId = getEventIdFromUrl();
        if (!eventId) return;
        const eventApiUrl = getEventApiUrl(eventId);
        const registerButton = document.getElementById('registerButton');
        // Fetch event data
        fetch(eventApiUrl)
            .then(response => response.json())
            .then(data => {
                const allowed = data.pg_allowedparticipantsquantity;
                const registered = data.pg_registeredparticipantsquantity || 0;
                // Enable only if registration is allowed
                if (registered < allowed) {
                    console.log('Enabling Register button');
                    registerButton.removeAttribute('disabled');
                    registerButton.style.pointerEvents = 'auto';
                    registerButton.style.opacity = '1';
                    registerButton.style.cursor = 'pointer';
                }
                else{
                    console.log('Registration full, keeping Register button disabled');
                }
            })
            .catch(error => {
                console.error('Error fetching event data:', error);
            });
    }
    
    // Execute the function
    updateRegisterButtonHref();
    enableRegisterButton(); 
});
