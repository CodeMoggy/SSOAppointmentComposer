//Copyright (c) CodeMoggy. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.

/// <reference path="App.js" />

var _mailbox;
var _customProps;

(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            _mailbox = Office.context.mailbox;
            _mailbox.getUserIdentityTokenAsync(getUserIdentityTokenCallback);
        });
    };

    function getUserIdentityTokenCallback(asyncResult) {

        var token = asyncResult.value;

        var request = new Object();
        request.token = token;

        // get the email address of the organizer
        request.organizerEmailAddress = Office.context.mailbox.userProfile.emailAddress;

        // set a custom property on the appointment
        Office.context.mailbox.item.loadCustomPropertiesAsync(function (asyncResult) {

            _customProps = asyncResult.value;

            var thirdPartyEventId = _customProps.get("com.kf.event");
            request.thirdPartyEventId = thirdPartyEventId;

            $.ajax({
                url: 'api/ServiceProxy',
                type: 'POST',
                data: JSON.stringify(request),
                contentType: 'application/json;charset=utf-8'
            }).done(function (data) {
                $("#tenant").val(data.organizerTenant);
                $("#msexchuid").val(data.token.msexchuid);
                $("#amurl").val(data.token.amurl);
                $("#uniqueID").val(data.token.uniqueID);
                $("#aud").val(data.token.aud);
                $("#iss").val(data.token.iss);
                $("#x5t").val(data.token.x5t);
                $("#nbf").val(data.token.nbf);
                $("#exp").val(data.token.exp);
                $("#thirdPartySecretKey").val(data.thirdPartySecretKey);
                $("#error").val("none");

                // if this is a new request then save the value of the thirdPartyEventId as a custom property
                if (thirdPartyEventId == null) {
                    _customProps.set("com.kf.event", data.thirdPartyEventId);
                    _customProps.saveAsync(function (asyncResult) {

                        // on save set the body of the appointment just to show something has happened
                        if (asyncResult.status == Office.AsyncResultStatus.Succeeded)
                            var appointment = Office.context.mailbox.item.body.setAsync("This new appointment has a custom property equal to " + data.thirdPartyEventId);
                    });
                };

            }).fail(function (data) {
                if (data.errorMessage != null)
                    $('#error').val(data.errorMessage);
            }).always(function () {
                // placeholder
            });
        });
    }
})();