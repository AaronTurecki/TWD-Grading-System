function addCourseEntity() {
    var displayType = '';
    displayType = document.getElementById("addCourseEntity").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addCourseEntity").style.display = "block";
        document.getElementById("addCourseEntity").value = "block";
        document.getElementById("removeCourseEntity").style.display = "none";
        document.getElementById("addMarker").style.display = "none";
        document.getElementById("removeMarker").style.display = "none";
    } else {
        document.getElementById("addCourseEntity").style.display = "none";
        document.getElementById("addCourseEntity").value = "none";
    }
}

function removeCourseEntity() {
    var displayType = '';
    displayType = document.getElementById("removeCourseEntity").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeCourseEntity").style.display = "block";
        document.getElementById("removeCourseEntity").value = "block";
        document.getElementById("addCourseEntity").style.display = "none";
        document.getElementById("addMarker").style.display = "none";
        document.getElementById("removeMarker").style.display = "none";
    } else {
        document.getElementById("removeCourseEntity").style.display = "none";
        document.getElementById("removeCourseEntity").value = "none";
    }
}

function addMarker() {
    var displayType = '';
    displayType = document.getElementById("addMarker").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addMarker").style.display = "block";
        document.getElementById("addMarker").value = "block";
        document.getElementById("removeMarker").style.display = "none";
        document.getElementById("addCourseEntity").style.display = "none";
        document.getElementById("removeCourseEntity").style.display = "none";
    } else {
        document.getElementById("addMarker").style.display = "none";
        document.getElementById("addMarker").value = "none";
    }
}

function removeMarker() {
    var displayType = '';
    displayType = document.getElementById("removeMarker").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeMarker").style.display = "block";
        document.getElementById("removeMarker").value = "block";
        document.getElementById("addMarker").style.display = "none";
        document.getElementById("addCourseEntity").style.display = "none";
        document.getElementById("removeCourseEntity").style.display = "none";
    } else {
        document.getElementById("removeMarker").style.display = "none";
        document.getElementById("removeMarker").value = "none";
    }
}