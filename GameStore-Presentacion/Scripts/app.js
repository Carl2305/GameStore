var CART_GAME_STORE = [];
var CART_DATA_GAME_STORE = [];
$(document).ready(function () {
	loadDataProdCart();
	loadDataCart();

	getDocumentosIdentidad();
	getDepartamento();

	$("#Departamento").change(function () {
		getProvincia();
	});

	$("#Provincia").change(function () {
		getDistrito();
	});

	$('#objCarrito').html('');
	$('#objCarrito').html(JSON.parse(localStorage.getItem('CART_DATA_GAME_STORE'))[0].items);

	// accion cuando se agrega un producto al carrito
	$("#btnAddCar").click(function () {
		let quantity = $("#selectNum").val();
		let prod = $(this).data('codprod');
		$.ajax({
			url: '/Sales/AddElementToCart',
			type: 'POST',
			async: false,
			data: { code: prod, cant: quantity },
			success: function (data) {
				try {
					if (data != 0) {
						// almacena el json de todos los productos en el controlador
						// en el localstorage CART_MIME_STORE
						loadDataProdCart();

						// carga la tabla del modal con la data del localstorage CART_MIME_STORE
						LoadTableCartModal(CART_GAME_STORE);

						// almacena la data del carrito (total, num_items, subtotal, igv) en
						// el localstorage CART_DATA_MIME_STORE
						loadDataCart();

						// carga el footer del modal del carrito con la data del localstorage CART_DATA_MIME_STORE
						loadFooterModalCart(CART_DATA_GAME_STORE);

						// muestra el modal carrito
						window.setTimeout(function () {
							$('#modalAddCar').modal('show');
							RemoveItemCart();
						}, 900);
					}
				} catch (e) { console.log(e.getMessage) }
			},
			error: function (e) { console.log("error ajax " + e.getMessage); }
		});
	});

	// funciones que se necesarias par la pagina fila de compra

	// carga los items de los productos en la pagina final de compra con la data del 
	// localstorage CART_MIME_STORE
	LoadItemsCartSale(JSON.parse(localStorage.getItem('CART_GAME_STORE')));

	// carag la data del carrito (total, num_items, subtotal, igv) con la data 
	// del localstorage CART_DATA_MIME_STORE
	LoadDataResSale(JSON.parse(localStorage.getItem('CART_DATA_GAME_STORE')));

	// esta funcion permite eleiminar un  elemento del carrito
	RemoveItemCart();
		
	$('#btnFCompra').click(function () {
		$.ajax({
			url: 'FinallyNewSale',
			type: 'POST',
			dataType: 'json',
			async: false,
			success: function (data) {
				x = parseInt(data[0]);
				switch (x) {
					case 1: swal({
						title: data[1],
						text: null,
						icon: data[3],
						buttons: ["Cancelar", "Iniciar Sesión"],
						closeOnClickOutside: false,
					}).then((willDelete) => {
						if (willDelete) {
							$('#modal_Login').modal('show');
						}
					}); break;
					case 4: swal({
						title: data[1],
						text: data[2],
						icon: data[3],
						buttons: data[4],
						closeOnClickOutside: false,
					}).then((willDelete) => {
						if (willDelete) {
							$('#btnFCompra').prop('disabled', true);
							window.setTimeout(function () {
								// almacena el json de todos los productos en el controlador
								// en el localstorage CART_MIME_STORE
								loadDataProdCart();

								// almacena la data del carrito (total, num_items, subtotal, igv) en
								// el localstorage CART_DATA_MIME_STORE
								loadDataCart();

								// carga los items de los productos en la pagina final de compra con la data del 
								// localstorage CART_MIME_STORE
								LoadItemsCartSale(JSON.parse(localStorage.getItem('CART_GAME_STORE')));

								// carag la data del carrito (total, num_items, subtotal, igv) con la data 
								// del localstorage CART_DATA_MIME_STORE
								LoadDataResSale(JSON.parse(localStorage.getItem('CART_DATA_GAME_STORE')));
							}, 4000);
							window.setTimeout(function () {
								window.location.href = '/Home';
							}, 10000);
						}
					}); break;
					default: swal({
						title: data[1],
						text: data[2],
						icon: data[3],
						buttons: data[4],
						timer: data[5],
					}); break;
				}
			},
			error: function (e) { console.log("error ajax " + e.getMessage); }
		});
	});

	$('#btn-login_menup').click(function () {
		$("#frm-Login")[0].reset();
		$('#modal_Login').modal('show');
	});

	$("#btn-Login").click(function () {
		if ($("#email").val().trim() != "") {
			if ($("#password").val().trim() != "") {
				$.ajax({
					url: '/Home/Login',
					dataType: 'json',
					type: 'post',
					data: {
						mail: $("#email").val().trim(),
						pass: $("#password").val().trim()
					},
					success: function (data) {
						if (data == 1) {
							window.setTimeout(function () {
								window.location.href = document.URL;
							}, 500);
						} else {
							swal({
								title: "Credenciales Incorrectas",
								text: "Verifique las credenciales de acceso.",
								icon: "warning",
								button: "Continuar",
							});
						}
					}, error: function (e) { console.log(e.getMessage); }
				});
			} else {
				swal({
					title: "Faltan Credenciales",
					text: "Ingrese la contraseña que registró anteriormente.",
					icon: "warning",
					button: "Continuar",
					closeOnClickOutside: false
				});
			}
			return
		} else {
			swal({
				title: "Faltan Credenciales",
				text: "Ingrese el Correo con el cual se registró anteriormente.",
				icon: "warning",
				button: "Continuar",
				closeOnClickOutside: false
			});
			return;
        }
	});

	$('#regist_client').click(function () {
		AddNewClient();
	});

})


// esta funcion sirve para cargar los productos del json (Tools.cart)
// en el localstorage
const loadDataProdCart = () => {
	$.ajax({
		url: '/Sales/DataCart',
		type: 'POST',
		dataType: 'json',
		async: false,
		success: function (data) {
			CART_GAME_STORE = [];
			try {
				if (data != "") {
					$.each(JSON.parse(data), function (item, e) {
						const prodcuto = {
							codigo: e.Id_Producto,
							nombre: e.Nomb_Product,
							imagen: e.Imagen_det,
							cantidad: e.Cantidad_Venta,
							precio: e.Precio_Detalleventa,
							total: e.SubTotal_sale,
							categoria: e.Nomb_Categoria
						};
						CART_GAME_STORE.push(prodcuto);
					});
					LoadTableCartModal(CART_GAME_STORE);
					localStorage.setItem('CART_GAME_STORE', JSON.stringify(CART_GAME_STORE));
				}
			} catch (e) {
				console.log(e.getMessage)
			}
		},
		error: function (e) {
			console.log(e.getMessage)
		}
	});
}

// esta funcion permite cargar la tabla del modal
// recibe el la data del localstorage CART_MIME_STORE
const LoadTableCartModal = data => {
	let body = "";
	$.each(data, function (item, e) {
		body += `<tr>
					<td>
						<img src="/${e.imagen}" style="width: 10vh; height:100px;">
					</td>
					<td>${e.nombre} (${e.categoria})</td>
					<td>${e.cantidad}</td>
					<td>S/ ${e.precio}</td>
					<td>S/ ${e.total}</td>
					<td>
						<button class="btn btn-default bg-danger btnDeleteProduct" data-codprod="${e.codigo}">
							<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="white" class="bi bi-trash" viewBox="0 0 16 16">
								<path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
								<path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
							</svg>
						</button>
					</td>
				</tr>`;
	});
	$("#tab_body_cart_modal").empty();
	$("#tab_body_cart_modal").append(body);
}

// esta funcion sirve para cargar los datos del carrito (total, num_items, subtotal, igv)
// actualiza el icono del carrito y lo almacena en el localstorage
const loadDataCart = () => {
	$.ajax({
		url: '/Sales/DataForSales',
		type: 'POST',
		dataType: 'json',
		async: false,
		success: function (data) {
			CART_DATA_GAME_STORE = [];
			CART_DATA_GAME_STORE.push(data);
			$('#objCarrito').html('');
			$('#objCarrito').html(data.items);
			// aqui se guarda la Cookie de la data del carrito
			localStorage.setItem('CART_DATA_GAME_STORE', JSON.stringify(CART_DATA_GAME_STORE));
		},
		error: function (e) { console.log(e.getMessage) }
	});
}

// esta funcion permite cargar el footer el modal del carrito
// recibe la data del carrito del localstorage CART_DATA_MIME_STORE
const loadFooterModalCart = data => {
	let body = "";
			$.each(data, function (item, e) {
				body += ` <h6 class="d-flex justify-content-end" style="color:blue;">
								SubTotal (${e.items}) : S/ ${e.Stotal}
							</h6>
							<h6 class="d-flex justify-content-end" style="color:blue;">
								IGV (18%) : S/ <span>${e.IGV}</span>
							</h6>
							<h4 class="d-flex justify-content-end" style="color:red;">
								Total : S/ ${e.total}
							</h4>`;
			});
			$('#footer_modal_cart').empty();
			$('#footer_modal_cart').append(body);
}

// esta funcion permite eliminar un elemento del carrito y sea del modal o de la pagina final
const RemoveItemCart = () => {
	$('.btnDeleteProduct').click(function () {
		let prod = $(this).data('codprod');
		swal({
			title: "¡Adevertencia!",
			text: "¿Estas seguro de quitar ese producto del carrito?",
			icon: "warning",
			buttons: ["Cancelar", true],
			dangerMode: true,
		})
		.then((willDelete) => {
			if (willDelete) {
				$.ajax({
					url: '/Sales/RemoveElementToCart',
					type: 'POST',
					async: false,
					data: { code: prod },
					success: function (data) {
						if (data != 0) {
							swal({
								title: "Producto Eliminado del Carrito",
								text: "\n",
								icon: "success",
								buttons: false,
								timer: 1200
							});
							window.setTimeout(function () {
								// almacena el json de todos los productos en el controlador
								// en el localstorage CART_MIME_STORE
								loadDataProdCart();

								// carga la tabla del modal con la data del localstorage CART_MIME_STORE
								LoadTableCartModal(CART_GAME_STORE);

								// almacena la data del carrito (total, num_items, subtotal, igv) en
								// el localstorage CART_DATA_MIME_STORE
								loadDataCart();

								// carga el footer del modal del carrito con la data del localstorage CART_DATA_MIME_STORE
								loadFooterModalCart(CART_DATA_GAME_STORE);

								// carga los items de los productos en la pagina final de compra con la data del 
								// localstorage CART_MIME_STORE
								LoadItemsCartSale(CART_GAME_STORE);

								// carag la data del carrito (total, num_items, subtotal, igv) con la data 
								// del localstorage CART_DATA_MIME_STORE
								LoadDataResSale(CART_DATA_GAME_STORE);

								// carga de nuevo la funcion para eliminar un producto
								RemoveItemCart();

							}, 1200);
                        }
					},
					error: function (e) { console.log("error ajax " + e.getMessage); }
				});
			}
		});
	});
}

// esta funcion sirve para cargar los productos en la pagina final de compra
// recibe la data del carrito del localstorage
const LoadItemsCartSale = data => {
	let body = "";
	if (data.length != 0) {
		$.each(data, function (item, e) {
			body += `<div class="card mb-3" style="max-width: 100%;">
					<div class="row no-gutters">
						<div class="col-md-5 col-sm-5">
							<img class="img-fluid" src="/${e.imagen}" alt="" title="${e.nombre}" style="width: 100%; height: 200px; padding: 15px">
						</div>
						<div class="col-md-7 col-sm-7">
							<div class="card-body">
								<h4 class="card-title">${e.nombre}</h4>
								<h6>${e.categoria}</h6>
								<h5 class="d-flex justify-content-center" style="color:red;">
									S/ ${e.total}
								</h5>
								<p class="card-text">Cantidad: <span style="margin-left: 3px; font-weight: bold;">${e.cantidad}</span></p>
							</div>
							<div class="d-flex justify-content-end mr-2 mb-2">
								<button class="btn btn-danger btnDeleteProduct" data-codprod="${e.codigo}">Eliminar</button>
							</div>
						</div>
					</div>
				</div>`;
		});
	} else {
		body = `<div class="alert alert-warning" role="alert">
					<div class="col-12 text-center mt-5 mb-5">
						<svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="orange" class="bi bi-exclamation-triangle" viewBox="0 0 16 16">
							<path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
							<path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
						</svg>
					</div>
					<div class="col-12">
						<h5 class="mt-5 mb-5">No existen productos en el carrito de compras. Para Procesar tu Compra, debes seleccionar un producto como mínimo <a href="/Home">aquí</a></h5>
					</div>
				</div>`;
	}
	$('#ListProductsCar').empty();
	$('#ListProductsCar').append(body);
}

// esta funcion sirve para cargar los datos del carrito (total, num_items, subtotal, igv)
// en la pagina final de compra recibe la data del carrito del localstorage
const LoadDataResSale = data => {
	let body = "";
	if (data.length != 0) {
		$.each(data, function (item, e) {
			body += `<div class="col-12">
					<h3 class="text-center">RESUMEN DE TU PEDIDO</h3>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><p>SubTotal (<span id="CantProd">${e.items}</span>)</p></div>
						<div class="col-7">
							<h5 class="d-flex justify-content-end" style="color:blue;">S/ ${e.Stotal}</h5>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><p>IGV (18%) </p></div>
						<div class="col-7">
							<h5 class="d-flex justify-content-end" style="color:blue;">S/ ${e.IGV}</h5>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><h4>TOTAL</h4></div>
						<div class="col-7">
							<h4 class="d-flex justify-content-end" style="color:red;">S/ ${e.total}</h4>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<button class="btn btn-success" style="width: 100%;" id="btnFCompra">Procesar Compra</button>
				</div>`;
		});
	} else {
		body += `<div class="col-12">
					<h3 class="text-center">RESUMEN DE TU PEDIDO</h3>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><p>SubTotal (<span id="CantProd">0</span>)</p></div>
						<div class="col-7">
							<h5 class="d-flex justify-content-end" style="color:blue;">S/ 0.00</h5>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><p>IGV (18%) </p></div>
						<div class="col-7">
							<h5 class="d-flex justify-content-end" style="color:blue;">S/ 0.00</h5>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<div class="row">
						<div class="col-5"><h4>TOTAL</h4></div>
						<div class="col-7">
							<h4 class="d-flex justify-content-end" style="color:red;">S/ 0.00</h4>
						</div>
					</div>
					<hr>
				</div>
				<div class="col-12">
					<button class="btn btn-success" style="width: 100%;" id="btnFCompra">Procesar Compra</button>
				</div>`;
	}
	
	$('#ResumPedido').empty();
	$('#ResumPedido').append(body);
}

// esta funcion sirve para registrar un nuevo cliente
// falta validar el formulario
const AddNewClient = () => {
	let ubigeo = `${$("#Departamento").val().trim()}${$("#Provincia").val().trim()}${$("#Distrito").val().trim()}`;
	var fileDataNewClient = new FormData();
	fileDataNewClient.append("Nombre_Cliente", $("#nomb_client").val().trim());
	fileDataNewClient.append("Apellidos_Cliente", $("#ape_client").val().trim());
	fileDataNewClient.append("Num_Telefono", $("#telf_client").val().trim());
	fileDataNewClient.append("Codigo_Documento", $("#T_Documento").val().trim());
	fileDataNewClient.append("Num_DocIdent_Cliente", $("#Doc_Identidad").val().trim());
	fileDataNewClient.append("Ubigeo_Empleado", ubigeo);
	fileDataNewClient.append("Direccion", $("#direcc_client").val().trim());
	fileDataNewClient.append("Correo_Cliente", $("#email_client").val().trim());

	$.ajax({
		url: "/Clients/RegisterNewClient",
		type: "post",
		dataType: "json",
		contentType: false,
		processData: false,
		async: false,
		data: fileDataNewClient,
		success: function (data) {
			if (data == 1) {
				swal({
					title: "Registro exitoso",
					text: "Las credenciales de acceso se enviaron al correo registrado en el formulario.",
					icon: "success",
					button: "Continuar",
					dangerMode: true,
					closeOnClickOutside: false
				}).then((willDelete) => {
					window.setTimeout(function () {
						window.location.href = "/Home";
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
		},
		error: function (e) { console.log(e.getMessage); }
	});
}