function addIntake() {
    var displayType = '';
    displayType = document.getElementById("addIntake").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addIntake").style.display = "block";
        document.getElementById("addIntake").value = "block";
        document.getElementById("removeIntake").style.display = "none";

    } else {
        document.getElementById("addIntake").style.display = "none";
        document.getElementById("addIntake").value = "none";
    }
}

function removeIntake() {
    var displayType = '';
    displayType = document.getElementById("removeIntake").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeIntake").style.display = "block";
        document.getElementById("removeIntake").value = "block";
        document.getElementById("addIntake").style.display = "none";
    } else {
        document.getElementById("removeIntake").style.display = "none";
        document.getElementById("removeIntake").value = "none";
    }
}