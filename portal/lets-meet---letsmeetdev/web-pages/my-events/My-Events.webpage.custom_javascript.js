// Inject the portal user ID using Liquid
var portalUserId = '{{ user.id }}';
var portalUserName = '{{ user.name }}';

// Log the portal user ID and name for debugging
console.log("Portal User ID: " + portalUserId);
console.log("Portal User Name: " + portalUserName);

// Wait for the form to be ready
$(document).ready(function () {
var entityId = document.querySelector('input[name="EntityFormView.EntityId"]');
if (!entityId || !entityId.value) {
        // Set the value of the hidden field (replace with your field's logical name if needed)
        var idElement = $('input[name="pg_createdbyportaluserid_id"]');
        var nameElement = $('input[name="pg_createdbyportaluserid_name"]');
        var field = idElement.length ? idElement : nameElement;
        if (field.length && portalUserId) {
            idElement.val(portalUserId);
            nameElement.val(portalUserName);
            console.log("Set portal user ID: " + portalUserId);
        }
} else {
    console.log("Existing record. Exit function"); 
}
});

