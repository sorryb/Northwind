//translate reports
var translate = {
    "Alphabeticallistofproducts": "Lista produselor in ordine alfabetica",
    "CategorySalesPerYear": "Vanzarile pe categorii pe an",
    "CurrentProductList": "Lista produselor",
    "Customer and Suppliers by City": "Clienti si furnizori ordonati dupa oras",
    "Employees": "Angajati",
    "Invoices": "Facturi",
    "OrderDetailsExtended": "Detalii comenzi extins",
    "OrdersQry": "Factura curier",
    "OrderSubtotals": "Subtotal comenzi",
    "ProductsAboveAveragePrice": "Produse peste pretul mediu",
    "ProductSalesPerYear": "Vanzarile produselor pe an",
    "ProductsByCategory": "Produse ordonate dupa categorie",
    "QuarterlyOrders": "Comenzi trimestriale",
    "SalesByAmount": "Vanzari ordonate dupa total",
    "SalesByCategory": "Vanzari ordonate dupa categorie",
    "SuMmaryOfSalesByQuarter": "Rezumatul vanzarilor pe trimestru",
    "SummaryOfSalesByYear": "Rezumatul vanzarilor pe an",
};
var a = "asd";
$(".reps").each(function (index, elem) {
    elem.innerHTML = translate[elem.innerHTML];
    console.log(elem.textContent);
})