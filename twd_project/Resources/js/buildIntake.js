//function addModule() {
//    var displayType = '';
//    displayType = document.getElementById("addModule").value;

//    if (displayType == undefined || displayType == "none") {
//        document.getElementById("addModule").style.display = "block";
//        document.getElementById("addModule").value = "block";
//        document.getElementById("removeModule").style.display = "none";
//        document.getElementById("addStudent").style.display = "none";
//        document.getElementById("removeStudent").style.display = "none";
//    } else {
//        document.getElementById("addModule").style.display = "none";
//        document.getElementById("addModule").value = "none";
//    }
//}

//function removeModule() {
//    var displayType = document.getElementById('removeModule').value;

//    if (displayType == undefined || displayType == "none") {
//        document.getElementById("removeModule").style.display = "block";
//        document.getElementById("removeModule").value = "block";
//        document.getElementById("addModule").style.display = "none";
//        document.getElementById("addStudent").style.display = "none";
//        document.getElementById("removeStudent").style.display = "none";
//    } else {
//        document.getElementById("removeModule").style.display = "none";
//        document.getElementById("removeModule").value = "none";
//    }
//}

function addStudent() {
    var displayType = document.getElementById('addStudent').value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("addStudent").style.display = "block";
        document.getElementById("addStudent").value = "block";
        document.getElementById("addModule").style.display = "none";
        document.getElementById("removeModule").style.display = "none";
        document.getElementById("removeStudent").style.display = "none";
    } else {
        document.getElementById("addStudent").style.display = "none";
        document.getElementById("addStudent").value = "none";
    }
}

function removeStudent() {
    var displayType = document.getElementById('removeStudent').value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("removeStudent").style.display = "block";
        document.getElementById("removeStudent").value = "block";
        document.getElementById("addModule").style.display = "none";
        document.getElementById("removeModule").style.display = "none";
        document.getElementById("addStudent").style.display = "none";
    } else {
        document.getElementById("removeStudent").style.display = "none";
        document.getElementById("removeStudent").value = "none";
    }
} 