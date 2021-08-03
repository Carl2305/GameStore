let EMPLEADO = {};
let EMPLEADO2 = {};
let modDirecc = false;
$(document).ready(function () {
    getDataEmployee();

    getDocumentosIdentidad();
    getDepartamento();
    getCargos();

    $("#btn_edit_Employee").click(function () {
        $("#formPutEmpleado")[0].reset();
        $("#modalUpdateEmpleado #imgPreview").removeAttr("src");

        $("#modalUpdateEmpleado #putImgProd").prop('checked', false);
        $("#modalUpdateEmpleado #divInputImg").addClass('d-none');
        $("#modalUpdateEmpleado #divInputImgText").addClass('d-none');

        $("#modalUpdateEmpleado").modal("show");


        $("#modalUpdateEmpleado #imgPreview").attr("src", "");
        $("#modalUpdateEmpleado #imgPreview").attr("src", `/${EMPLEADO.Foto_Empleado}`);
        $("#modalUpdateEmpleado #imgPreview").css({ 'width': '50%', 'height': '250px' });

    });
    $("#btn_edit_PassEmployee").click(function () {
        $("#formPutPassEmpleado")[0].reset();
        $("#modalUpdatePassEmpleado").modal("show");
    });

    $("#btnPutPassGuardarEmple").click(function () {
        if ($("#PassOld").val().trim() != "") {
            if ($("#PassNew").val().trim() === $("#PassConf").val().trim()) {
                swal({
                    title: "¿Deseas Cambiar tu Contraseña?",
                    icon: "warning",
                    buttons: ["Cancelar", "Continuar"],
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            url: '/Intranet/UpdatePassEmple',
                            dataType: 'json',
                            type: 'post',
                            data: {
                                passA: $("#PassOld").val().trim(),
                                passN: $("#PassNew").val().trim()
                            },
                            success: function (data) {
                                if (data == 1) {
                                    swal({
                                        title: "Contraseña Actualizada",
                                        text: "",
                                        icon: "success",
                                        button: "Continuar",
                                        dangerMode: true,
                                        closeOnClickOutside: false,
                                    }).then((willDelete) => {
                                        window.setTimeout(function () {
                                            window.location.href = "/Intranet/Login";
                                        }, 800);
                                    });
                                } else {
                                    swal({
                                        title: "Algo ocurrio, la contraseña antigua no es válida",
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
            else {
                swal({
                    title: "Las contraseñas no coinciden",
                    text: "La nueva contraseña y su confirmación no coinciden.",
                    icon: "warning",
                    button: "Continuar",
                    closeOnClickOutside: false
                });
                return;
            }
        } else {
            swal({
                title: "Ingresa tu contraseña Actual",
                text: "",
                icon: "warning",
                button: "Continuar",
                closeOnClickOutside: false
            });
            return;
        }
    });

    $("#putImgEmple").change(function () {
        if ($(this).prop('checked')) {
            $("#modalUpdateEmpleado #divInputImg").removeClass('d-none');
            $("#modalUpdateEmpleado #divInputImgText").removeClass('d-none');
            modImg = true;
        } else {
            $("#modalUpdateEmpleado #divInputImg").addClass('d-none');
            $("#modalUpdateEmpleado #divInputImgText").addClass('d-none');
            modImg = false;
            $("#modalUpdateEmpleado #imgPreview").attr("src", "");
            $("#modalUpdateEmpleado #imgPreview").attr("src", `/${EMPLEADO.Foto_Empleado}`);
        }
    });

    $("#modalUpdateEmpleado #uploadimg").change(function () {
        $("#modalUpdateEmpleado #imgPreview").attr("src", "");
        showImageSelected("modalUpdateEmpleado #uploadimg", "modalUpdateEmpleado #imgPreview");
        file = "modalUpdateEmpleado #uploadimg";
    });

    // editar un producto
    $("#modalUpdateEmpleado #btnPutGuardarEmple").click(function () {
        UpdateFotoEmple();
    });


/*****************************************************************/
/*************CRUD EMPLEADOS PARA EL ADMIN************************/
/*****************************************************************/

    getDataAllEmployees();
    $("#btn_SearchEmployee").click(function () {
        let cadena = $("#barSearchEmployee").val();
        getDataAllEmployees(cadena);
    });

    $("#btn_NewEmployee").click(function () {
        getDepartamento();
        $("#Provincia").empty();
        $("#Distrito").empty();
        $("#modalAddNewEmployee #imgP").css({ "display": "none" });
        $("#modalAddNewEmployee").modal("show");
        $("#formNewEmployee")[0].reset();
        $("#modalAddNewEmployee #imgPreview").removeAttr("src");
    });

    //previzualizar la imagen un producto en un modal editar producto
    $("#modalAddNewEmployee #uploadimg").change(function () {
        $("#modalAddNewEmployee #imgPreview").attr("src", "");
        showImageSelected("modalAddNewEmployee #uploadimg", "modalAddNewEmployee #imgPreview");
        file = "modalAddNewEmployee #uploadimg";
        $("#modalAddNewEmployee #imgPreview").css({ 'width': '40%', 'max-width': '220px', 'height': '100%', 'max-height': '250px' });
    });

    $("#Departamento").change(function () {
        getProvincia();
    });

    $("#Provincia").change(function () {
        getDistrito();
    });

    $("#PutDepartamento").change(function () {
        getProvinciaPut();
    })

    $("#PutProvincia").change(function () {
        getDistritoPut();
    })

    $("#btnAddGuardarEmployee").click(function () {
        addNewEmployee();
    });

   
})

const getDataEmployee = () => {
    $.ajax({
        url:'/PanelIntranet/EmployeeData',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            EMPLEADO = JSON.parse(data);
            setDataEmpleado(EMPLEADO);
        }, error: function (e) { console.log(e.getMessage);}
    });
}
const setDataEmpleado = data => {
    $("#Foto_Empleado").prop("src", `/${data.Foto_Empleado}`);
    $("#Nombre_Empleado").val(data.Nombre_Empleado);
    $("#Apellidos_Empleado").val(data.Apellidos_Empleado);
    $("#Telf_Empleado").val(data.Telf_Empleado);
    $("#Correo_Empleado").val(data.Correo_Empleado);
    $("#Direccion").val(data.Direccion);
    $("#Usuario_Empleado").val(data.Usuario_Empleado);
    $("#Cargo_Empleado").val(data.Nomb_Cargo);
    if (data.Estado == 0) {
        $("#btn_edit_Employee").prop("disabled", true);
        $("#Estado").val("Deshabilitado");
        $("#Estado").addClass("bg-danger font-weight-bold");
    } else {
        $("#Estado").val("Habilitado");
        $("#Estado").addClass("bg-success font-weight-bold");
    }
}
// falta validar el form del modal
const UpdateFotoEmple = () => {
    
    if (file != '') {
        let fileUpload = $("#" + file).get(0);
        let files = fileUpload.files;

        var fileDataUpdateE = new FormData();
        if (files.length > 0) {
            fileDataUpdateE.append(files[0].name, files[0]);
            file = "";
        }
    }
    else {
        sweetAlert("Faltan datos", "Debes seleccionar una imagen para mostrar en la web", "warning");
        return false;
    }

    $.ajax({
        url: "/PanelIntranet/MyProfileUpdateFotoEmpleado",
        type: "post",
        dataType: "json",
        contentType: false,
        processData: false,
        async: true,
        data: fileDataUpdateE,
        success: function (d) {
            if (d.data == 1) {
                swal({
                    title: "Datos actualizados Correctamente",
                    text: "",
                    icon: "success",
                    button: "Continuar",
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    modImg = false;
                    getDataEmployee();
                    $("#formPutEmpleado")[0].reset();
                    $("#modalUpdateEmpleado").modal("hide");
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

// funciones para el admin
const getDataAllEmployees = cod => {
    $.ajax({
        url: '/PanelIntranet/getAllEmployees',
        dataType: 'json',
        type: 'post',
        async:false,
        data: { docIden:cod},
        success: function (data) {
            setDataAllEmployees(data);
        }, error: function (e) { console.log(e.getMessage); }
    });
}
const setDataAllEmployees = data => {
    let body = "";
    if (JSON.parse(data).length != 0) {
        $("#messageError").addClass("d-none");
        $("#ListDataAllEmple").removeClass("d-none");
        $.each(JSON.parse(data), function (i, e) {
            if (e.Estado == 0) {
                body += `<tr style="pointer-events: none; opacity: 0.4;">`;
            } else {
                body += `<tr>`;
            }
            body += `<td>${e.Nombre_Empleado}</td>
                     <td>${e.Apellidos_Empleado}</td>
                     <td>${e.string_Fecha}</td>
                     <td>${e.Nomb_ubigeo}</td>
                     <td>${e.Direccion}</td>
                     <td>${e.Num_DocIdent_Empleado}</td>
                     <td>${e.Telf_Empleado}</td>
                     <td>${e.Correo_Empleado}</td>`;
                     if (e.Estado == 1) {
                         body += `<td class="text-success font-weight-bold">Activo</td>
                                  <td class="btn-group-sm btn-group">
                                     <button class="btn btn-warning btn-UpdateEmple" data-codemple="${e.Id_Empleado}">Editar</button>
                                     <button class="btn btn-danger btn-DeleteEmple" data-codemple="${e.Id_Empleado}">Eliminar</button>
                                  </td>`;
                     } else {
                         body += `<td class="text-danger font-weight-bold">Inactivo</td><td></td>`;
                     }
              body+=`</tr>`;
        });
        $("#body_tabEmployees").empty();
        $("#body_tabEmployees").append(body);
    } else {
        $("#ListDataAllEmple").addClass("d-none");
        $("#messageError").removeClass("d-none");
        $("#messageError").empty();
        body += `<div class="alert alert-warning mt-5 mr-auto ml-auto" role="alert">
					<div class="col-12 text-center mt-5 mb-5">
						<svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="orange" class="bi bi-exclamation-triangle" viewBox="0 0 16 16">
							<path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"></path>
							<path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"></path>
						</svg>
					</div>
					<div class="col-12 text-center">
						<h5 class="mt-5 mb-5">No existen Empleados</h5>
					</div>
				</div>`;
        $("#messageError").append(body);
    }


    // acciones de editar y eliminar

    $(".btn-UpdateEmple").click(function () {
        let codeEmployee = $(this).data("codemple");
        getDataEmployeeEdit(codeEmployee);
        $("#divModDireccEmployee").addClass('d-none');
        getDepartamentoPut();
        getCargosPut();
        $("#PutProvincia").empty();
        $("#PutDistrito").empty();
        $("#modalUpdateEmployee").modal("show");

        // cargando los datos a los inputs del modal 
        $("#PutDireccion").val(EMPLEADO2.Direccion);
        $("#PutDireccion").attr("data-ubigeo", EMPLEADO2.Ubigeo_Empleado);
        $("#PutDireccion").prop("readonly", true);
        $("#PutTelefono").val(EMPLEADO2.Telf_Empleado);
        $(`#PutidCargo option[value="${EMPLEADO2.Id_Cargo}"]`).attr('selected', true);
        $("#btnPutGuardarEmployee").attr("data-codemple", EMPLEADO2.Id_Empleado);
    });

    $("#chkDireccEmployee").change(function () {
        if ($(this).prop('checked')) {
            $("#divModDireccEmployee").removeClass('d-none');
            $("#PutDireccion").prop("readonly", false);
            modDirecc = true;
        } else {
            $("#divModDireccEmployee").addClass('d-none');
            $("#PutDireccion").prop("readonly", true);
            modDirecc = false;
        }
    });

    $("#btnPutGuardarEmployee").click(function () {
        let cod = $(this).data("codemple");
        putEmployee(cod);
    });

    $(".btn-DeleteEmple").click(function () {
        let code = $(this).data("codemple");
        swal({
            title: "¿Estas seguro de eliminar este empleado?",
            icon: "warning",
            buttons: ["Cancelar", "Continuar"],
            dangerMode: true,
            closeOnClickOutside: false,
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/PanelIntranet/DeleteEmployee',
                    dataType: 'json',
                    type: 'post',
                    data: { Idemple: code },
                    success: function (data) {
                        if (data == 1) {
                            swal({
                                title: "Empleado Eliminado Correctamente",
                                text: "",
                                icon: "success",
                                button: "Continuar",
                                dangerMode: true,
                                closeOnClickOutside: false,
                            }).then((willDelete) => {
                                getDataAllEmployees();
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
}

// falta validar el formulario
const addNewEmployee = () => {
    if (file != '') {
        let fileUpload = $("#" + file).get(0);
        let files = fileUpload.files;

        var fileDataNewEmployee = new FormData();
        if (files.length > 0) {
            fileDataNewEmployee.append(files[0].name, files[0]);
            file = "";
        }
    }
    else {
        sweetAlert("Faltan datos", "Debes seleccionar una imagen para mostrar en la web", "warning");
        return false;
    }
    let ubigeo = `${$("#Departamento").val().trim()}${$("#Provincia").val().trim()}${$("#Distrito").val().trim()}`;
    fileDataNewEmployee.append("Nombre_Empleado", $("#Nomb_Employee").val().trim());
    fileDataNewEmployee.append("Apellidos_Empleado", $("#Apel_Employee").val().trim());
    fileDataNewEmployee.append("string_Fecha", $("#F_Nacimiento").val());   //Fecha_Nacimiento
    fileDataNewEmployee.append("Telf_Empleado", $("#Telefono").val().trim());
    fileDataNewEmployee.append("Codigo_Documento", $("#T_Documento").val());
    fileDataNewEmployee.append("Num_DocIdent_Empleado", $("#Doc_Identidad").val().trim());
    fileDataNewEmployee.append("Ubigeo_Empleado", ubigeo);
    fileDataNewEmployee.append("Direccion", $("#Direccion").val().trim());
    fileDataNewEmployee.append("Correo_Empleado", $("#Email_Employee").val().trim());
    fileDataNewEmployee.append("Id_Cargo", $("#idCargo").val());

    $.ajax({
        url: "/PanelIntranet/addNewEmployee",
        type: "post",
        dataType: "json",
        contentType: false,
        processData: false,
        async: true,
        data: fileDataNewEmployee,
        success: function (data) {
            if (data == 1) {
                swal({
                    title: "Empleado creado satisfactoriamente",
                    text: "Las credenciales de acceso se enviaron al correo registrado en el formulario.",
                    icon: "success",
                    button: "Continuar",
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    getDataAllEmployees();
                    $("#modalAddNewEmployee").modal("hide");
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

const getDataEmployeeEdit = code => {
    $.ajax({
        url: '/PanelIntranet/EmployeeDataEdit',
        dataType: 'json',
        type: 'post',
        async: false,
        data: {cod:code},
        success: function (data) {
            EMPLEADO2 = JSON.parse(data);
        }, error: function (e) { console.log(e.getMessage); }
    });
}

// falta validar el formulario
const putEmployee = code => {
    var fileDataPutEmployee = new FormData();
    if (modDirecc) {
        let ubigeo = `${$("#PutDepartamento").val().trim()}${$("#PutProvincia").val().trim()}${$("#PutDistrito").val().trim()}`
        fileDataPutEmployee.append("Direccion", $("#PutDireccion").val().trim());
        fileDataPutEmployee.append("Ubigeo_Empleado", ubigeo);
    } else {
        fileDataPutEmployee.append("Direccion", $("#PutDireccion").val().trim());
        let ubg = $("#PutDireccion").data("ubigeo");
        fileDataPutEmployee.append("Ubigeo_Empleado", ubg);
    }
    fileDataPutEmployee.append("Id_Empleado", parseInt(code));
    fileDataPutEmployee.append("Telf_Empleado", $("#PutTelefono").val().trim());
    fileDataPutEmployee.append("Id_Cargo", $("#PutidCargo").val());

    $.ajax({
        url: "/PanelIntranet/PutEmployee",
        type: "post",
        dataType: "json",
        contentType: false,
        processData: false,
        async: true,
        data: fileDataPutEmployee,
        success: function (data) {
            if (data == 1) {
                swal({
                    title: "Datos Actualizados Correctamente",
                    icon: "success",
                    button: "Continuar",
                    dangerMode: true,
                    closeOnClickOutside: false,
                }).then((willDelete) => {
                    getDataAllEmployees();
                    $("#modalUpdateEmployee").modal("hide");
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