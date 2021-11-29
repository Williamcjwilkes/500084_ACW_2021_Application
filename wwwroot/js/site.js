// form validation for the front end inputs

function ValidateInputs() {
    let x = document.forms["Login"]["UserName"].value;
    let y = document.forms["Login"]["Password"].value;

    if (x == "" || y == "")
    {
        document.getElementById("uErrorMesage").innerHTML = "Please enter a username!";
        document.getElementById("pErrorMesage").innerHTML = "Please enter a PassWord!";
        return false;

    }
}