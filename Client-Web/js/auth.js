console.log('auth.js initialized.')

function validateUsername(s) {
    if (s.length < 3 || s.length > 15) {
        return false
    }
    else if (s.includes('?')) {
        return false
    }
    return true
}

function validatePassword(s) {
    if (s.length < 6 || s.length > 25) {
        return false
    }
    return true
}

function tryAuthUserPwd(usernameId, pwdId) {
    // console.log('auth.js/tryAuthUserPwd called.')
    const username = document.getElementById(usernameId).value,
        pwd = document.getElementById(pwdId).value
    if (!validateUsername(username)) {
        alert('Please enter a valid username.')
        console.log('username is invalid.')
        return
    }
    else if (!validatePassword(pwd)) {
        alert('Please enter a valid password.')
        console.log('password is invalid.')
        return
    }
    trySocketAuthUserPwd(username, pwd)
}

/** 
 * Socket Options:
 * Protocol: ws = HTTP, wss = HTTPs
 * Address: Server IP, 127.0.0.1 = local
 * Port: 80/8080 = HTTP, 443 = HTTPs
 * Example: new Websocket(ws://192.168.0.1:80/)
 **/
function trySocketAuthUserPwd(username, pwd) {

    const socket = new WebSocket('ws://127.0.1:32000');
    console.log('created username/pwd auth socket.');

    socket.onopen = (event) => {
        console.log('trySocketAuthUserPwd: socket successfully opened');
        socket.send('{username:' + username + '\n,password:' + pwd + '}');
    }

    socket.onmessage = (event) => { console.log('Data received from server: ${event.data}'); }

    socket.onclose = (event) => {
        if (!event.wasClean) {
            console.log('Connection died unexpectedly, server may have closed. Possible ERROR 1006.');
            return;
        }
        console.log('Connection closed cleanly, code=${event.code} reason=${event.reason}');
    }

    socket.onerror = (error) => { console.log('socket error:', error); };
}