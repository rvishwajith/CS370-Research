# About
This is a log of update notes for the web client of the research project, which is meant to test logging in on a website using either WebSockets or HTTPs requests.

## v0.3.0
## In Progress as of October 16, 2023
- Started working on WebSocket code for username/password authentication in auth.js:
  - Creates a local websocket using 'WS' (insecure/HTTP) protocol and sends arbitrary data on connection.
    - Handles the connection failing or any errors with sending messages.
    - Will use 'WSS' later (secure/HTTPs encryption), but is not a priority.
      - Also requires HTTPs certificate setup.

## v0.2.1
## Completed as of October 12, 2023
- Added validatePassword() and validateUsername() to auth.js:
  - Only does some basic checks like length for now, will switch to regex later.
    - Works for simple  length checks.
    - Sends an alert to the user if username or password is too short.

## v0.2.0
## Completed as of October 10, 2023
- Added sitemap.html to serve as a navigation page for all pages.
  - May be removed when styling is added.
- login.html and index.html now link to navigation.
  - All other pages will link the same way.
- Added js/auth.js:
  - tryAuthUserPwd(): A function to handle websocket authentication using form contents.
- login.html sign-in form now links to auth.js (tryAuthUserPwd) on submission.
- Added favicon.ico website icon (placeholder).
  - Added references to all page headers.
- Added js/form-util.js:
  - disableFormReload(): A function that prevents the page from reloading when a form is submitted. Used so that login page doesn't reload with a typical GET/POST request, because it will instead use a WebSocket.

## v0.1.2
## Completed as of October 8th, 2023
- Changed form layout on login.html and added links to homepage / index.html.
- index.html now links to login page.

## v0.1.1
### Completed as of October 7th, 2023
- Added login.html:
  - Moved any login content from index.html to login.html
  - Form for logging in now has:
    - Username
    - Password
    - "Remember Me" Checkbox
    - "Sign In" Button (to submit form)


## v0.1.0 (Project Creation)
### Completed as of October 4th, 2023
- Added index.html:
  - Homepage / defualt website page.
  - Form for logging in.
- Added folders for JavaScript (~/js/), CSS (~/css/) and SASS (~/sass).
  - SASS is not being used yet but may be used later.