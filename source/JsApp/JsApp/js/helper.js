// helper function to show data to the user
function display(selector, data) {
    if (data && typeof data === 'string') {
        data = JSON.parse(data);
    }
    if (data) {
        data = JSON.stringify(data, null, 2);
    }

    $(selector).text(data);
}

function checkSessionState() {
    manager.oidcClient.loadMetadataAsync().then(function (meta) {
        if (meta.check_session_iframe && manager.session_state) {
            document.getElementById('rp').src = 'check-session.html#' +
                'session_state=' + manager.session_state +
                '&check_session_iframe=' + meta.check_session_iframe +
                '&client_id=' + settings.client_id;
        }
        else {
            document.getElementById('rp').src = 'about:blank';
        }
    });

    window.onmessage = function (e) {
        if (e.origin === 'https://localhost:44301' && e.data === 'changed') {
            manager.removeToken();
            manager.renewTokenSilentAsync().then(function () {
                // Session state changed but we managed to silently get a new identity token, everything's fine
                console.log('renewTokenSilentAsync success');
            }, function () {
                // Here we couldn't get a new identity token, we have to ask the user to log in again
                console.log('renewTokenSilentAsync failed');
            });
        }
    }
}

var settings = {
    authority: 'https://localhost:44300',
    client_id: 'js',
    popup_redirect_uri: 'https://localhost:44301/popup.html',

    // Specify we want to renew tokens silently and the URL of the page that has to be used for that
    silent_renew: true,
    silent_redirect_uri: 'https://localhost:44301/silent-renew.html',

    // post-logout URL
    post_logout_redirect_uri: 'https://localhost:44301/index.html',

    response_type: 'id_token token',
    scope: 'openid profile email api',

    filter_protocol_claims: true
};

var manager = new OidcTokenManager(settings);

manager.addOnTokenObtained(function () {
    display('.js-access-token', { access_token: manager.access_token, expires_in: manager.expires_in });
});

$('.js-login').click(function () {
    manager.openPopupForTokenAsync()
        .then(function () {
            display('.js-id-token', manager.profile);

            // Load the iframe and start listening to messages
            checkSessionState();

            // Removed
            // display('.js-access-token', { access_token: manager.access_token, expires_in: manager.expires_in });
        }, function (error) {
            console.error(error);
        });
});

$('.js-call-api').click(function () {
    var headers = {};
    if (manager.access_token) {
        headers['Authorization'] = 'Bearer ' + manager.access_token;
    }

    $.ajax({
        url: 'https://localhost:44302/values',
        method: 'GET',
        dataType: 'json',
        headers: headers
    }).then(function (data) {
        display('.js-api-result', data);
    }).fail(function (error) {
        display('.js-api-result', {
            status: error.status,
            statusText: error.statusText,
            response: error.responseJSON
        });
    });
});

$('.js-logout').click(function () {
    manager.redirectForLogout();
});