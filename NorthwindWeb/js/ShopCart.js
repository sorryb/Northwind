//Local Storage Class
function CartProducts(productId, quantity) {
    this.ID = productId;
    this.Quantity = quantity;
}

//work
function exportLocalShopCartToServer() {
    if (isLogedIn == 1 && localStorage.getItem("cart") != null && localStorage.getItem("cart") != "") {
        $.ajax({
            url: "http://" + window.location.host + "/ShopCart/ImportFromLocal",
            "data": {
                "json": localStorage.getItem("cart"),
            },
            dataType: "json",
            type: "POST"
        })
            .done(function () {
                localStorage.setItem("cart", "");
            })
            .fail(function () {
                alert("A aparut o eroare la trimiterea listei de produse locale catre server");
            })
    }
}

//when page was loaded
$("document").ready(function () {

    //shopcart count item
    if (isLogedIn == 1) {
        if (getLocalCartCount()) {
            var sendToServer = confirm("Aveti produse in shopcart, doriti sa le adaugam la cele din baza de date?");
            if (sendToServer) {
                exportLocalShopCartToServer();
                UpdateShop();
            }
            else {
                localStorage.setItem("cart", "");
            }
        }

        //discontinued product make it unavailable
        $(".discontinued").css("color", "black").prop("title", "Produs Indisponibil");
        $(".discontinued .shopcartcontainer-products").detach();
        $(".discontinued img").addClass("grayscale90");
    }
    UpdateShop();
})


//add product in cart
function AddToCart(productToAdd) {
    if (isLogedIn == 0) {
        //need to check if this customer is loged in
        var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
        var add = true;
        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == productToAdd.ID) {
                add = false;
                break;
            }
        }
        if (add) {
            productsInStorage.push(productToAdd);
        }
        else {
            productsInStorage[i].Quantity++;
        }
        localStorage.setItem("cart", JSON.stringify(productsInStorage));
        UpdateShop();
    }
    else {
        $.ajax({
            url: "http://" + window.location.host + "/ShopCart/Create",
            data: {
                id: productToAdd.ID,
                quantity: productToAdd.Quantity
            }
        })
            .done(function () {
                //ar trebui modificat
                UpdateShop();
                $("#ShopCartTable").DataTable().destroy();
                CreateShopCartDataTable("ShopCartTable");
            })
            .fail(function () {
                alert("Ceva nu a mers bine");
            });
    }
}

function ChangeQuantity(id, quantity) {
    if (quantity < 1 || quantity > 255) {
        $("#ShopCartTable").DataTable().destroy();
        CreateShopCartDataTable("ShopCartTable");
    }
    else {
        if (isLogedIn == 0) {
            var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
            var i = 0;
            for (; i < productsInStorage.length; i++) {
                if (productsInStorage[i].ID == id) {
                    break;
                }
            }
            productsInStorage[i].Quantity = quantity;
            localStorage.setItem("cart", JSON.stringify(productsInStorage));
            $("#ShopCartTable").DataTable().destroy();
            CreateShopCartDataTable("ShopCartTable");
        }
        else {
            $.ajax({
                url: searchControllerPath() + "/UpdateQuantity",
                data: {
                    id: id,
                    quantity: quantity
                }
            })
                .done(function () {
                    //ar trebui modificat
                    $("#ShopCartTable").DataTable().destroy();
                    CreateShopCartDataTable("ShopCartTable");
                })
                .fail(function () {
                    alert("Ceva nu a mers bine");
                });
        }
    }
}

function RemoveFromCart(id) {

    if (isLogedIn == 0) {
        var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();

        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == id) {
                break;
            }
        }
        productsInStorage.splice(i, 1);
        localStorage.setItem("cart", JSON.stringify(productsInStorage));
        $("#ShopCartTable").DataTable().destroy();
        CreateShopCartDataTable("ShopCartTable");
    }
    else {
        $.ajax({
            url: searchControllerPath() + "/Delete?id=" + id,
        })
            .done(function () {
                //ar trebui modificat
                $("#ShopCartTable").DataTable().destroy();
                CreateShopCartDataTable("ShopCartTable");
            })
            .fail(function () {
                alert("Ceva nu a mers bine");
            });
    }
    UpdateShop();
}

//count number of product in shopcart
function getLocalCartCount() {

    return (localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")).length : new Array().length);
}

//todo this
function getServerCartCount() {
    if (isLogedIn == 1) {
        $.ajax({
            url: "http://" + window.location.host + "/ShopCart/GetCartCount",
            dataType: "json",
            success: function (data) {
                $("#shopcart-productcount").text(data);
            }
        })
    }
}

//get array of products
function getLocalCartProducts() {
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

function UpdateShop() {
    if (isLogedIn == 1)
        getServerCartCount();
    else
        $("#shopcart-productcount").text(getLocalCartCount());

}
