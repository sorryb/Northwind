//Local Storage Class
function CartProducts(idProdus, quantity, category) {
    this.IdProdus = idProdus;
    this.Category = category;
    this.quantity = quantity;
}

//add product in cart
function AddToCart(productToAdd) {
    //need to check if this customer is loged in
    var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
    var add = true;
    for (var i = 0; i < productsInStorage.length; i++) {
        if (productsInStorage[i].IdProdus == productToAdd.IdProdus) {
            add = false;
            break;
        }
    }
    if (add)
        productsInStorage.push(productToAdd);
    else
        productsInStorage[i].quantity++;
    localStorage.setItem("cart", JSON.stringify(productsInStorage));
}



//count number of product in shopcart
function getCartCount() {
    //if is loged maybe we return a variable, or we return a json from server and when we update it send it back to server
    return (localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")).length : new Array().length);
}

//get array of products
function getCartProducts() {
    //also we need to see how we manage this
    return localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
}



/*
* numerele nu reprezinta pasii cronologici ce trebuie facuti
1. salvam product list intr-un array json
    -daca este logat il luam din database
    -daca nu este logat il luam din local
2. la fiecare incarcare de pagina trebuie sa verificam daca userul este logat si daca local sunt produse
    -daca local sunt produse atunci va trebui sa adaugam produsele pe server
    -produsele locale vor fi sterse
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
    -AddListProduct: dupa ce se logheaza ia json, trece prin fiecare produs, vede ca e disponibil si ca exista si il baga pe server.
        daca da return ProductShopResponse.error=0
        daca nu return ProductShopResponse.error=1 si detaliile necesare
        
        
    -DeleteProduct(productID): sterge un produs din baza de date (din json)
5. Ne vor trebui urmatoarele obiecte:
    -ProductShopCart={
        ID --Pentru cautare in baza de date
        Category --Pentru gasirea imaginii
        Quantity  --cantitatea ce o comanda
        UnitPrice
        TotalPrice
    }
    -ProductShopResponse{
        bool Error --in caz ca a fost vreo eroare
        MessageTitle messageTitle
        MessageText messageText
        data --neadaugate pe server
}



*/