function activeMenu() {

    //On recuper le parent de notre div
    var monheader = document.getElementById("menu");
    //On recupere une liste d'element ayant la btn se trouvant ds le parent header
    var btns = monheader.getElementsByTagName("a");

    //on ajouter un écouteur de type click sur chaque btn en utilisant la boucle for et ainsi que son indice
    for (var i = 0; i < btns.length; i++) {
        btns[i].addEventListener("click", function () {
            //on recupere l'element ayant ayant la classe Active
            var current = document.getElementsByClassName("active");
            console()
            // on remplace cette classe en enlevant active
            current[0].className = current[0].className.replace("active", " ");
            //et l'objet courrant sur lequel on a cliqué, on ajoute active
            this.className = "active";
        });
    }

}