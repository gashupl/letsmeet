// Inject the portal user ID using Liquid
var portalUserId = '{{ user.id }}';
var portalUserName = '{{ user.fullname }}';

// Log the portal user ID and name for debugging
console.log("Portal User ID: " + portalUserId);
console.log("Portal User Name: " + portalUserName);

// Wait for the form to be ready
$(document).ready(function () {

    let  removeTextMuted = (attributeName) => {
        let field = $(`#${attributeName}_name`).parent();
        let textmuted = field.find('.text-muted');
        textmuted.remove(); 
    };

    console.log("Document ready 9!");
    let attribute = "#pg_createdbyportaluserid";
    let idElement = $(attribute);
    let nameElement = $(attribute + "_name");
    let entityNameElement = $(attribute + "_entityname");

    if (idElement && portalUserId) {
        console.log("Field found");
        idElement.attr("value", portalUserId);
       // nameElement.val(portalUserName); 
        nameElement.attr("value", portalUserName);
        entityNameElement.attr("value", "contact");
        //removeTextMuted (attribute); 

        console.log("Set portal user ID: " + portalUserId);
    } else {
        console.log("Field not found or portalUserId is empty");
    }
});

