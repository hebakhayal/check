function dateformate() {
    var input = document.getElementById("search")
    
    input.placeholder = "mm/dd/yy";
}

function Nameformate() {
    var input = document.getElementById("search")

    input.placeholder = "please enter the name of the survey ";

}

// When the user clicks on div, open the popup
function myFunction() {
    var popup = document.getElementById("myPopup");
    popup.classList.toggle("show");
}