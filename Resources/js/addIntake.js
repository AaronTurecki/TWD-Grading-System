function addIntake() {
    var userType = document.getElementById('role').value;

    if (userType == "Student") {
        document.getElementById("intakeDropdown").style.display = "block";
    } else {
        document.getElementById("intakeDropdown").style.display = "none";
    };
}