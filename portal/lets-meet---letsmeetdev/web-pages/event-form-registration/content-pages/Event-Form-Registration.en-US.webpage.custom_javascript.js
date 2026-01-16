document.addEventListener('DOMContentLoaded', function() {
    // Get the eventId from the URL query parameter
    function getEventIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('eventId');
    }
    
    // Populate the pg_eventid lookup field
    function populateEventLookup() {
      //TODO: Update event Id lookup
    }
    
    // Execute the function
    populateEventLookup();
    
    // Also try after a short delay in case the form is still loading
    //setTimeout(populateEventLookup, 1000);
});
