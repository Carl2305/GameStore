let PRODUCTO = {};
let file = "";
let modImg = false;
var codeProduct;
$(document).ready(function () {
    getAllProducts();
    // buscar un producto
    $("#btn_SearchProduct").click(function () {
        let cad = $("#barSearchProduct").val();
        getAllProducts(cad);
    });

    //modal agregar un producto
    $("#addProduct").click(function () {
        $("#imgP").css({ "display": "none" });
        $('#modalNewPruduct').modal("show");
        $("#formNewProyect")[0].reset();
        $("#imgPreview").removeAttr("src");
    });

    //previzualizar la imagen un producto en un modal nuevo producto
    $("#uploadimg").change(function () {
        $("#imgP").css({ "display": "block" });
        showImageSelected("uploadimg", "imgPreview");
        file = "uploadimg";
    });

    //previzualizar la imagen un producto en un modal editar producto
    $("#modalUpdatePruduct #uploadimg").change(function () {
        $("#modalUpdatePruduct #imgPreview").attr("src", "");
        showImageSelected("modalUpdatePruduct #uploadimg", "modalUpdatePruduct #imgPreview");
        file = "modalUpdatePruduct #uploadimg";
    });



    // insertar un nuevo producto
    $("#btnAddGuardar").click(function () {
        addProducto();
    });

    // editar un producto
    $("#btnPutGuardar").click(function () {
        putProducto(codeProduct);
    });

    $("#putImgProd").change(function () {
        if ($(this).prop('checked')) {
            $("#divInputImg").removeClass('d-none');
            modImg = true;

        } else {
            $("#divInputImg").addClass('d-none');
            modImg = false;
            $("#modalUpdatePruduct #imgPreview").attr("src", "");
            $("#modalUpdatePruduct #imgPreview").attr("src", `/${PRODUCTO.Imagen_Producto}`);
        }
    });



    

});
const getAllProducts = cad => {
    $.ajax({
        url: '/PanelIntranet/Products',
        dataType: 'json',
        type: 'post',
        async:false,
        data: {cadena:cad},
        success: function (data) {
            setDataProducts(data);
        }, error: function (e) { console.log(e.getMessage); }
    });
}

// a la funcion setDataProducts al atributo imagen se debe 
// anteponer un / para que se cargué correctamente la imagen
const setDataProducts = data => {
    let body = "";
    $.each(JSON.parse(data), function (item, e) {
        if (e.Estado === 0) {
            body += `<div class="col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3" id="itemsProducts" style="pointer-events: none; opacity: 0.4;">`;
        } else {
            body += `<div class="col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3" id="itemsProducts">`;
        }
        body += `<div class="card mb-3" style="max-width: 100%;">
                    <div class="row no-gutters">
                        <div class="col-sm-12 col-md-3 col-lg-4">
                            <img class="img-fluid" src="/${e.Imagen_Producto}" alt="" title="${e.Nombre_Producto}" style="width: 100%; height: 200px; padding: 5px">
                        </div>
                        <div class="col-sm-12 col-md-9 col-lg-8">
                            <div class="card-body row">
                                <div class="row">
                                    <div class="col-12">
                                        <h4 class="card-title descip_prod">${e.Nombre_Producto}</h4>
                                    </div>
                                    <div class="col-7">
                                        <h5>Precio S/ <span id="SubTotal" style="margin-left: 3px; color: red;">${e.Precio_Producto}</span></h5>
                                    </div>
                                    <div class="col-5">
                                        <p class="card-text">Stock: <span style="margin-left: 3px; font-weight: bold;">${e.Stock_Producto}</span></p>
                                    </div>
                                    <div class="col-12 mt-2">
                                        <h5 class="card-text text-left" id="categoria">Categoria: ${e.Nombre_Categoria}</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end m-0 modal-footer">`;
        if (e.Estado === 0) {
            body += `<div class="btn"><div class="btn"></div></div></div></div></div>`;
        } else {
            body += `<button class="btn btn-warning mr-3 btnUpdateProduct" id="btnUpdateProduct" data-codprod="${e.Id_Producto}">Editar</button>
                        <button class="btn btn-danger btnDeleteProduct" id="btnDeleteProduct" data-codprod="${e.Id_Producto}">Eliminar</button>
                    </div>
                </div>
            </div>`;
        }         
    });
    $("#ListProducts").empty();
    $("#ListProducts").append(body);

    // modal modificar produto
    $(".btnUpdateProduct").click(function () {
        codeProduct = $(this).data("codprod");
        getDataProduct(codeProduct);
        $("#putImgProd").prop('checked', false);
        $("#divInputImg").addClass('d-none');


        $("#modalUpdatePruduct").modal("show");


        $("#Nomb_ProductPut").val(PRODUCTO.Nombre_Producto);
        $("#PrecioPut").val(PRODUCTO.Precio_Producto.toLocaleString('es-ES'));
        $("#StockPut").val(PRODUCTO.Stock_Producto);
        $(`#cbo_categoriaPut option[value="${PRODUCTO.Id_Categoria}"]`).attr('selected', true);
        $("#txtA_descripcionPut").val(PRODUCTO.Descripcion_Producto);

        $("#btnPutGuardar").attr("data-codp", PRODUCTO.Id_Producto);

        $("#modalUpdatePruduct #imgP").css({ "display": "block" });
        $("#modalUpdatePruduct #imgPreview").attr("src", `/${PRODUCTO.Imagen_Producto}`);
        $("#modalUpdatePruduct #imgPreview").css({ 'width': '50%', 'height': '250px' });

    });

    // swall para eliminar un producto
    $(".btnDeleteProduct").click(function () {
        codeProduct = $(this).data("codprod");
        deleteProducto(codeProduct);
    });
}

// funcion para previsualizar la imagen del modal 
// forma de hacerlo como en ChAPATUChamBA
const showImageSelected =(input = "logo", imagen = "imagen") => {
    let seleccionArchivos = document.querySelector("#" + input);
    let imagenPrevisualizacion = document.querySelector("#" + imagen);

    const archivos = seleccionArchivos.files;
    if (!archivos || !archivos.length) {
        imagenPrevisualizacion.src = "";
        //$("#imgP").css({ "display": "none" });
        //$("#modalUpdatePruduct #imgPreview").attr("src", `/${PRODUCTO.Imagen_Producto}`);
        return;
    }
    const primerArchivo = archivos[0];
    const objectURL = URL.createObjectURL(primerArchivo);
    imagenPrevisualizacion.src = objectURL;
    $(imagenPrevisualizacion).css({ 'width': '50%', 'height': '250px' });
}

const getDataProduct = cod => {
    $.ajax({
        url: '/PanelIntranet/getDataProduct',
        dataType: 'json',
        type: 'post',
        async:false,
        data: {code:cod},
        success: function (data) {
            PRODUCTO = JSON.parse(data);
        }, error: function (e) { console.log(e.getMessage); }
    });
}

// falta validar formulario
const addProducto = () => {
    // modificacion para realizar la subida de la imagen a la BD
    // valiadar si se da click en el #fileUpload o en #imageUpload
    // respectivamente para asifgnarlo a una vairable y realizar la valiadación

    if (file != '') {
        let fileUpload = $("#" + file).get(0);
        let files = fileUpload.files;

        var fileData = new FormData();
        if (files.length > 0) {
            fileData.append(files[0].name, files[0]);
            file = "";
        }
    }
    else {
        sweetAlert("Faltan datos", "Debes seleccionar una imagen para mostrar en la web", "warning");
        return false;
    }
    fileData.append("Nombre_Producto", $("#Nomb_Product").val());
    fileData.append("Precio_Producto", $("#Precio").val());
    fileData.append("Stock_Producto", $("#Stock").val());
    fileData.append("Id_Categoria", $("#cbo_categoria").val());

    fileData.append("Descripcion_Producto", $("#txtA_descripcion").val());

    // aquí va el ajax que envia la informacion al controller
    $.ajax({
        url: "/PanelIntranet/addNewProduct",
        type: "post",
        dataType: "json",
        contentType: false,
        processData: false,
        async: true,
        data: fileData,
        success: function (data) {
            if (data == 1) {
                swal({
                    title: "Excelente",
                    text: "Producto creado satisfactoriamente",
                    icon: "success",
                    button: "Continuar",
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    getAllProducts();
                    $("#modalNewPruduct").modal("hide");
                });
            } else {
                swal({
                    title: "Algo ocurrio",
                    text: "",
                    icon: "warning",
                    button: "Continuar",
                });
            }
        },
        error: function (e) { console.log(e.getMessage); }
    });
}

// falta validaar el formulario
const putProducto = id => {
    var fileDataUpdate = new FormData();
    if (modImg) {
        if (file != '') {
            let fileUpload = $("#" + file).get(0);
            let files = fileUpload.files;
            if (files.length > 0) {
                fileDataUpdate.append(files[0].name, files[0]);
                file = "";

                fileDataUpdate.append("modImagen", 1);
            }
        }
        else {
            sweetAlert("Faltan datos", "Debes seleccionar una imagen para mostrar en la web", "warning");
            return false;
        }
    } else {
        fileDataUpdate.append("modImagen", 0);
    }
    fileDataUpdate.append("Id_Producto", id)
    fileDataUpdate.append("Nombre_Producto", $("#Nomb_ProductPut").val());
    fileDataUpdate.append("Precio_Producto", $("#PrecioPut").val());
    fileDataUpdate.append("Stock_Producto", $("#StockPut").val());
    fileDataUpdate.append("Id_Categoria", $("#cbo_categoriaPut").val());

    fileDataUpdate.append("Descripcion_Producto", $("#txtA_descripcionPut").val());

    // aquí va el ajax que envia la informacion al controller
    $.ajax({
        url: "/PanelIntranet/putProducto",
        type: "post",
        dataType: "json",
        contentType: false,
        processData: false,
        async: true,
        data: fileDataUpdate,
        success: function (data) {
            if (data == 1) {
                swal({
                    title: "Excelente",
                    text: "Producto Actualizado satisfactoriamente",
                    icon: "success",
                    button: "Continuar",
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    modImg = false;
                    getAllProducts();
                    $("#formPutProyect")[0].reset();
                    $("#modalUpdatePruduct").modal("hide");
                });
            } else {
                swal({
                    title: "Algo ocurrio",
                    text: "",
                    icon: "warning",
                    button: "Continuar",
                });
            }
        },
        error: function (e) { console.log(e.getMessage); }
    });
}

const deleteProducto = id => {
    swal({
        title: "¿Deseas eliminar este producto?",
        text: "",
        icon: "warning",
        buttons: ["Cancelar", true],
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: '/PanelIntranet/deleteProducto',
                dataType: 'json',
                type: 'post',
                async: false,
                data: { code: id },
                success: function (data) {
                    if (data == 1) {
                        swal({
                            title: "Producto Eliminado Correctamente",
                            text: "",
                            icon: "success",
                            button: "Continuar",
                            dangerMode: true,
                            closeOnClickOutside: false,
                        }).then((willDelete) => {
                            getAllProducts();
                        });
                    } else {
                        swal({
                            title: "Algo ocurrio",
                            text: "",
                            icon: "warning",
                            button: "Continuar",
                        });
                    }
                }, error: function (e) { console.log(e.getMessage); }
            });
        }
    });
}
