document.addEventListener('DOMContentLoaded', function() {
    // Get the event ID from the URL query parameter
    function getEventIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('id');
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
    
    // Execute the function
    updateRegisterButtonHref();
});
