
function changeFunc() {
    var selectBox = document.getElementById("Type");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    if (selectedValue == "multiselect") {
        document.getElementById("btn_multi").style.display = "initial";
    } else if (selectedValue == "singleselect") {
        document.getElementById("btn_multi").style.display = "initial";
    } else if (selectedValue == "dropdown") {
        document.getElementById("btn_multi").style.display = "initial";
    }
    else {
        document.getElementById("btn_multi").style.display = "none";
    }
}

$(document).ready(function () {
    var max_fields = 10;
    var wrapper = $(".container1");
    var add_button = $("#add_form_field");

    var x = 1;
    $(add_button).click(function (e) {
        e.preventDefault();
        if (x < max_fields) {
            x++;
            $(wrapper).append('<div><input type="text" name="Body"/><a href="#" class="delete">Delete</a></div>'); //add input box
        }
        else {
            alert('You Reached the limits')
        }
    });

    $(wrapper).on("click", ".delete", function (e) {
        e.preventDefault(); $(this).parent('div').remove(); x--;
    })
});
