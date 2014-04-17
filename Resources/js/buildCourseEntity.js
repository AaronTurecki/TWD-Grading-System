function addEntityParameter() {
    var displayType = '';
    displayType = document.getElementById("addEntityParameter").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addEntityParameter").style.display = "block";
        document.getElementById("addEntityParameter").value = "block";
        document.getElementById("removeEntityParameter").style.display = "none";

    } else {
        document.getElementById("addEntityParameter").style.display = "none";
        document.getElementById("addEntityParameter").value = "none";
    }
}

function removeEntityParameter() {
    var displayType = '';
    displayType = document.getElementById("removeEntityParameter").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeEntityParameter").style.display = "block";
        document.getElementById("removeEntityParameter").value = "block";
        document.getElementById("addEntityParameter").style.display = "none";
    } else {
        document.getElementById("removeEntityParameter").style.display = "none";
        document.getElementById("removeEntityParameter").value = "none";
    }
}