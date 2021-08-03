function getDepartamento() {
    var _Body = "<option value='00' selected>-Departamento-</option>";
    $("#Departamento").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getDepartamentos',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodDep;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#Departamento").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getProvincia() {
    var _Body = "<option value='00' selected>-Provincia-</option>";
    $("#Provincia").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getProvincias',
        dataType: 'json',
        type: 'post',
        data: { codeDep: $("#Departamento").val() },
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodProv;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#Provincia").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getDistrito() {
    var _Body = "<option value='00' selected>-Distrito-</option>";
    $("#Distrito").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getDistritos',
        dataType: 'json',
        type: 'post',
        data: {
            codeDep: $("#Departamento").val(),
            codePro: $("#Provincia").val(),
        },
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodDist;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#Distrito").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getDocumentosIdentidad() {
    var _Body = "<option value='**' selected>-Doc. Identidad-</option>";
    $("#T_Documento").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getDocIdentidads',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                _Body += '<option value="' + v.Codigo_Documento + '">' + v.Descripcion_Corta + '</option>';
            })
            $("#T_Documento").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getCargos() {
    var _Body = "<option value='**' selected>-Seleccione Cargo-</option>";
    $("#idCargo").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getCargos',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                _Body += '<option value="' + v.Id_Cargo + '">' + v.Nombre_Cargo + '</option>';
            })
            $("#idCargo").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

/*******************************/

function getDepartamentoPut() {
    var _Body = "<option value='00' selected>-Departamento-</option>";
    $("#PutDepartamento").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getDepartamentos',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodDep;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#PutDepartamento").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getProvinciaPut() {
    var _Body = "<option value='00' selected>-Provincia-</option>";
    $("#PutProvincia").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getProvincias',
        dataType: 'json',
        type: 'post',
        data: { codeDep: $("#PutDepartamento").val() },
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodProv;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#PutProvincia").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getDistritoPut() {
    var _Body = "<option value='00' selected>-Distrito-</option>";
    $("#PutDistrito").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getDistritos',
        dataType: 'json',
        type: 'post',
        data: {
            codeDep: $("#PutDepartamento").val(),
            codePro: $("#PutProvincia").val(),
        },
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                let co = v.CodDist;
                _Body += '<option value="' + co.trim() + '">' + v.NombreUbigeo + '</option>';
            })
            $("#PutDistrito").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}

function getCargosPut() {
    var _Body = "<option value='**' selected>-Seleccione Cargo-</option>";
    $("#PutidCargo").empty();
    $.ajax({
        async: false,
        url: '/Intranet/getCargos',
        dataType: 'json',
        type: 'post',
        success: function (data) {
            $.each(JSON.parse(data), function (i, v) {
                _Body += '<option value="' + v.Id_Cargo + '">' + v.Nombre_Cargo + '</option>';
            })
            $("#PutidCargo").append(_Body);
        },
        error: function (x) {
            console.log(x);
        },
        complete: function () {
        }
    })
}
