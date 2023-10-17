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
 * Address: Server IP (192.168.0.1 = local)
 * Port: 80/8080 = HTTP, 443 = HTTPs
 * Example: new Websocket(ws://192.168.0.1:80/)
 **/
function trySocketAuthUserPwd(username, pwd) {

    const socket = new WebSocket('ws://192.168.0.1:17327');
    console.log('created socket with username/password.');

    socket.onopen = function (e) {
        console.log("trySocketAuthUserPwd: socket successfully opened")
        //alert("Sending to server")
        socket.send('{username:' + username + '\n,password:' + pwd + '}')
    }

    socket.onmessage = function (event) {
        console.log(`[message] Data received from server: ${event.data}`)
    }

    socket.onclose = function (event) {
        if (!event.wasClean) {
            console.log('[close] Connection died unexpectedly! Probably ERROR 1006.')
            return
        }
        console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`)
    }

    socket.onerror = function (error) {
        console.log(`socket error:`, error)
    }
}