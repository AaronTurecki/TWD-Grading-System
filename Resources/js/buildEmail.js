function allStudents() {
    var emailName = document.getElementById('emailName').innerHTML;
    var emailUser = document.getElementById('emailUserName').innerHTML;
    document.getElementById('emailStudentLabel').value = "Email All Students";
    document.getElementById('sendEmailButton').value   = "Send to All Students";
    document.getElementById('emailStudentName').value  = "All";
}

function oneStudent(eventObj) {
    //var a = eventObj.target.id;
    //var emailName = document.getElementById('emailName').innerHTML;
    //var emailUser = document.getElementById('emailUserName').innerHTML;
    //document.getElementById('emailStudentLabel').value = "Email "   + emailName;
    //document.getElementById('sendEmailButton').value   = "Send to " + emailName;
    //document.getElementById('emailStudentName').value  = emailUser;
}