$(document).ready(function () {
    $("#btnGestInvoice").click(function () {
        let codef = $(this).data('codf');
        swal({
            title: "Gestionar Pago de un Pedido",
            icon: "warning",
            buttons: ["Cancelar", "Continuar"],
            dangerMode: true,
            closeOnClickOutside: false,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/PanelIntranet/GestionarPagoDePedido',
                    dataType: 'json',
                    type: 'post',
                    data: { code: codef },
                    success: function (data) {
                        if (data == 1) {
                            swal({
                                title: "Pago gestionado correctamente",
                                text: "",
                                icon: "success",
                                button: "Continuar",
                                dangerMode: true,
                                closeOnClickOutside: false,
                            }).then((willDelete) => {
                                window.setTimeout(function () {
                                    window.location.href = "/PanelIntranet/Sales";
                                }, 800);
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
    });

    $("#FilterSales").prop('checked',false);
    getDataSales(0);
    $("#FilterSales").change(function () {
        if ($(this).prop('checked')) {
            getDataSales(1);
        } else {
            getDataSales(0);
        }
    });
    
})

const getDataSales = (s) => {
    $.ajax({
        url: '/PanelIntranet/Sales',
        dataType: 'json',
        type: 'post',
        data: {fil:s},
        success: function (data) {
            setDataSales(data);
        }, error: function (e) { console.log(e.getMessage); }
    });
}
const setDataSales = data => {
    let body = "";
    if (JSON.parse(data).length != 0) {
        $.each(JSON.parse(data), function (i, e) {

            body += `<div class="col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3" id="itemsOrdenes">`;
            if (e.Estado != 0) {
                body += `<div class="card mb-3" style="max-width: 100%; background:rgb(88, 239, 107, 0.54);">`;
            } else {
                body += `<div class="card mb-3" style="max-width: 100%; background: rgb(227, 76,76, 0.25);">`;
            }
            body += `<div class="row no-gutters">
                    <div class="col-md-12 col-sm-12">
                        <div class="col-12 d-flex justify-content-end">
                            <p id="CodOrden" style="font-weight: bold;">N° Pedido: ${e.Id_Factura}</p>
                        </div>
                        <div class="col-12 d-flex justify-content-end">
                            <p id="idFecha">Fecha: ${e.string_Fecha}</p>
                        </div>
                        <div class="card-body">
                            <h6 class="card-title" style="font-weight: bold;">Cliente: ${e.Nomb_Client}</h6>
                            <p class="card-text d-flex justify-content-end">${e.tipo_doc}: <span id="idDniCliente"> ${e.Num_DocIdentC}</span></p>`;

            if (e.Estado != 0) {
                body += `<p class="card-text">S/ <span id="Importe" style="margin-left: 3px; font-weight: bold; color: green;">${e.Importe_Total}</span></p>
                    <p class="card-text">Estado: <span id="idEstado" style="font-weight: bold; color: green;">PAGADO</span></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
            } else {
                body += `<p class="card-text">S/ <span id="Importe" style="margin-left: 3px; font-weight: bold; color: red;">${e.Importe_Total}</span></p>
                     <p class="card-text"> Estado: <span id="idEstado" style="font-weight: bold; color: red;">NO PAGADO</span></p>
                     </div>
                        <form class="d-flex justify-content-end mr-2 mb-2" action="/PanelIntranet/DetailSale" method="post">
                            <button type="submit" class="btn btn-primary btnVer" name="code" value="${e.Id_Factura}">Gestionar Pago</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>`;
            }
        });
    } else {
        body += `<div class="alert alert-warning mt-5 mr-auto ml-auto" role="alert">
					<div class="col-12 text-center mt-5 mb-5">
						<svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="orange" class="bi bi-exclamation-triangle" viewBox="0 0 16 16">
							<path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"></path>
							<path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"></path>
						</svg>
					</div>
					<div class="col-12">
						<h5 class="mt-5 mb-5">No existen pedidos según el filtro</h5>
					</div>
				</div>`;
    }
    $("#ListOrdenes").empty();
    $("#ListOrdenes").append(body);
}