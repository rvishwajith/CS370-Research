console.log('form-util.js initialized.')

function disableFormReload(form) {
    function helper(event) {
        event.preventDefault()
    }
    form.addEventListener('submit', helper)
}