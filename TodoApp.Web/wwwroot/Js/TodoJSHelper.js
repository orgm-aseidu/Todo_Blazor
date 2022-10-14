function makeFieldEditable(inputElementId) {
    document.getElementById(inputElementId).readOnly = false;
    document.getElementById(inputElementId).focus();
}
function makeFieldReadOnly(inputElementId) {
    document.getElementById(inputElementId).readOnly = true;
}