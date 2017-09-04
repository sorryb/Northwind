//Local Storage Class
function CartProducts(idProdus, quantity) {
    this.idProdus = idProdus;
    this.quantity = quantity;
}

//add product in cart
function AddToCart(product)
{
    //need to check if this customer is loged in
    var x = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
    x.push(product);
    localStorage.setItem("cart", JSON.stringify(x));
}

//count number of product in shopcart
function getCartCount() {
    //if is loged maybe we return a variable, or we return a json from server and when we update it send it back to server
    return (localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array()).Lenght;
}

//get array of products
function getCartProducts() {
    //also we need to see how we manage this
    return localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
}



/*
* numerele nu reprezinta pasii cronologici ce trebuie facuti
1. salvam product chart intr-un array json
    -daca este logat il luam din database
    -daca nu este logat il luam din local
2. la fiecare incarcare de pagina trebuie sa verificam daca userul este logat si daca local sunt produse
    -daca local sunt produse atunci va trebui sa il intrebam daca isi adauga produsele pe server
    -produsele locale vor fi sterse indiferent de ce selecteaza (trebuie sa ii spunem asta)
3. la plasarea comenzii:
    -verificam daca este logat
        -daca este atunci trimitem apelam o actiune ce ia json-ul din server
        -daca nu este conectat atunci il trimitem la pagina de autentificate (si ar trebui sa facem a.i. de aici sa il redirectioneze inapoi spre a trimite comanda)
                    jsonul datorita pct 2 va fi luat tot de pe server
4. trebuie sa avem un controller shop
    -Index: va afisa intreg cos
    -JsonShopCart: va trimite catre client un json cu obiectele din shop (vezi 5.)
    -AddOrder: va adauga order folosind json-ul din baza de date
    -AddProduct(productID, Quantity): va adauga/modifica(daca exista) un produs
    -DeleteProduct(productID): sterge un produs din baza de date (din json)
5. Ne vor trebui urmatoarele obiecte:
    -ProductShopCart={
        ID --Pentru cautare in baza de date
        Category --Pentru gasirea imaginii
        Quantity  --cantitatea ce o comanda
    }

*/







$(document).ready(function () {
    //carousel interval
    $("#myCarousel").carousel({ interval: 5000 });
})

