function addCourse() {
    var displayType = '';
    displayType = document.getElementById("addCourse").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addCourse").style.display = "block";
        document.getElementById("addCourse").value = "block";
        document.getElementById("removeCourse").style.display = "none";

    } else {
        document.getElementById("addCourse").style.display = "none";
        document.getElementById("addCourse").value = "none";
    }
}

function removeCourse() {
    var displayType = '';
    displayType = document.getElementById("removeCourse").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeCourse").style.display = "block";
        document.getElementById("removeCourse").value = "block";
        document.getElementById("addCourse").style.display = "none";
    } else {
        document.getElementById("removeCourse").style.display = "none";
        document.getElementById("removeCourse").value = "none";
    }
}