// Inject the portal user ID using Liquid
var portalUserId = '{{ user.id }}';
var portalUserName = '{{ user.fullname }}';

// Log the portal user ID and name for debugging
console.log("Portal User ID: " + portalUserId);
console.log("Portal User Name: " + portalUserName);

// Wait for the form to be ready
$(document).ready(function () {
    console.log("Document ready 3!");

    let idElement = $("#pg_createdbyportaluserid_id");
    let nameElement = $("#pg_createdbyportaluserid_name");
    let entityNameElement = $("#pg_createdbyportaluserid_entityname");
    
    if (idElement && portalUserId) {
        console.log("Field found");
        // idElement.attr("value", portalUserId);
        // nameElement.attr("value", portalUserName);
        // entityNameElement.attr("value", "contact");
        if (idElement.length && portalUserId) {
            idElement.val(portalUserId).trigger('change');
        }
        if (nameElement.length && portalUserName) {
            nameElement.val(portalUserName).trigger('change');
        }
        if (entityNameElement.length) {
            entityNameElement.val('contact').trigger('change');
        }
        console.log("Set portal user ID: " + portalUserId);
    } else {
        console.log("Field not found or portalUserId is empty");
    }
});

