$(document).ready(function () {              //dopiero po zaladowaniu strony wykona caly skrypti funkcja omija kolizje przestrzeni nazw

    var x = 0;
    var s = "";

    console.log("Hello World");

    // jQuery można używać stosująć $costam zamiast jQuery.costam

    //var theForm = document.getElementById("theForm"); //Czysty JS
    var theForm = $("#theForm");  // Wersja tego wyżej w Jquery
    theForm.hide();

    //var button = document.getElementById("buyButton"); // pobranie buttona CZYSTY JS
    var button = $("#buyButton");

    //dodanie eventu dla click uzywajac funkcji anonimowej - CZYSTY JS
    //button.addEventListener("click", function () {
    //    console.log("buying Item");
    //});

    //To co wyżej używając JQuerry
    button.on("click", function () {
        console.log("Buying item")
    });


    //JQuery
    var productInfo = $(".product-props li"); //obiekty li w product-props - to samo co w CSS
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());         //this binduje wartość która klikneliśmy
    });


    function foo() {

    }

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");


    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(1000);
    });

});

/*Polecane kursy
 *Front end develpment QuickStart
 * JavaScript for C# developers*/