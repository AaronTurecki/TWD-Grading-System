function UpdateParameterGrade(fname, lname, grade, latePenalty, comment, userId) {
    var displayType = '';
    displayType = document.getElementById("updateGrade").value;

    if (displayType == undefined || displayType == "none") {
        document.getElementById("updateGrade").style.display = "block";
        document.getElementById("updateGrade").value = "block";

        var a = document.getElementById("update-heading");
        a.innerHTML="Update " + fname + " " + lname + "'s grade";
        document.getElementById("grade").value = grade;
        document.getElementById("latePenalty").value = latePenalty;
        document.getElementById("comment").value = comment;
        document.getElementById("userId").value = userId;

    } else {
        document.getElementById("updateGrade").style.display = "none";
        document.getElementById("updateGrade").value = "none";
    }
}




