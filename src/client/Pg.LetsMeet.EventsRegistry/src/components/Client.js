class Client {

    url = ''
    key = ''

    constructor(url, key) {
        this.url = url;
        this.key = key;
    }

    sendEventRegistration(requestForm, successHandler, errorHandler) {
        console.log("Sent " + requestForm.name + " to: " + this.url);

        var date = new Date(requestForm.plannedDate).toUTCString()

        var body = {
            'name': requestForm.name,
            'details': requestForm.details,
            'partnername': requestForm.organization,
            'partnerid': requestForm.partnerId,
            'partneremail': requestForm.partnerEmail,
            'plannedDate': date,
            'allowedParticipants': requestForm.allowedParticipants
        }
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'x-functions-key': this.key
            },
            body: JSON.stringify(body)
        };
        return fetch(this.url, requestOptions)
            .then(async response => {
                successHandler(response);
            })
            .catch(async response => {
                errorHandler(response);
            })
    }

    getEventRegistrations(partnerId, successHandler, errorHandler) {

        var urlWithParams = this.url + "?partnercode=" + partnerId; 

        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'x-functions-key': this.key
            }
        };
        return fetch(urlWithParams, requestOptions)
            .then(async response => {
                successHandler(response);
            })
            .catch(async response => {
                errorHandler(response);
            })
    }
}

export default Client;  