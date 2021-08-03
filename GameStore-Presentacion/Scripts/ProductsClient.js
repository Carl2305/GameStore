$(document).ready(function () {
    /*getProducts();

    $(".btn-showDetail").click(function () {
        let cod=$(this).data("prod");
        alert(cod)
    });*/
});
// trae al data de todos los productos desde el controller
const getProducts = () => {
    $.ajax({
        url: "/Products/getProductos",
        type: "POST",
        dataType:"json",
        async: false,
        success: function (data) {
            setDataProducts(JSON.parse(data));
        },
        error: function (e) { console.log(e.getMessage); }
    });
}
// pinta las card de los productos recibiendo la data de getProducts
const setDataProducts = (data) => {
    let body = "";
    $.each(data, function (i, e) {
        if (e.Stock_Producto < 3) {
            body += `<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3" style="pointer-events: none; opacity: 0.4;">`;
        } else {
            body += `<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">`;
        }
        body += `<div class="card mb-3">
                    <img src="${e.Imagen_Producto}" class="card-img-top" alt="" title="${e.Nombre_Producto}" height="250">
                    <div class="card-body">
                        <div style="height: 8rem;">
                            <h4 class="card-title nombProduct">${e.Nombre_Producto} hola mundo hola mundo </h4>
                            <h5 class="card-text text-center" id="precio">S/ 
                                <span>
                                   ${e.Precio_Producto}
                                </span>
                            </h5>
                        </div>
                        <div class="d-flex justify-content-center">
                            <button type="button" class="btn btn-primary w-100 btn-showDetail" data-prod="${e.Id_Producto}">Ver</button>
                        </div>
                    </div>
                </div>
            </div>`;
    });
    $("#GalleryProducts").empty();
    $("#GalleryProducts").append(body);
}
// trae el detalle de un producto especifico
const getDetailProduct = cod => {
    $.ajax({
        url: "/Products/getDetailProduct",
        type: "POST",
        dataType: "json",
        async: false,
        data: {code:cod},
        success: function (data) {
            console.log(JSON.parse(data));
        },
        error: function (e) { console.log(e.getMessage); }
    });
}
// pinta la data del detalle de un producto
const setDataDetailProduct = data => {
    let body = "";
}