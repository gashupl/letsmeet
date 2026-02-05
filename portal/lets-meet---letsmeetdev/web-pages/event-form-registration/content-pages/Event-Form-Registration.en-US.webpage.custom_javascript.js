document.addEventListener('DOMContentLoaded', function() {
    // Get the eventId from the URL query parameter
    function getEventIdFromUrl() {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get('eventId');
    }
    
    // Populate the pg_eventid lookup field
    function populateEventLookup(eventId, eventName) {
        if (eventId) {
            $("#pg_eventid_name").val(eventName);
            $("#pg_eventid").val(eventId);
            $("#pg_eventid_entityname").val("pg_event");
        }
    }
    
    // Execute the function
    var eventId = getEventIdFromUrl(); 
    populateEventLookup(eventId, 'Event name');
    
    // Also try after a short delay in case the form is still loading
    //setTimeout(populateEventLookup, 1000);
});
