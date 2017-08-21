function AddToCart(i)
{
    var x = localStorage.getItem("cart") ? localStorage.getItem("cart") : "";
    x = x + i+" ";
    localStorage.setItem("cart",x)
}