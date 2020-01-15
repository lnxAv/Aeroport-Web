function day() {
    var option = document.getElementById('selectpicker').value;
    var today = 'Aujourd\'hui';
    var tomorrow = "Demain";

    if (option == today) {

        alert("c now que case")

        var url = $("#RedirectTo").val();
        location.href = url.replace('__cible__', today );
    } else {
        var url = $("#RedirectTo").val();
        location.href = url.replace('__cible__', tomorrow);
    }
}