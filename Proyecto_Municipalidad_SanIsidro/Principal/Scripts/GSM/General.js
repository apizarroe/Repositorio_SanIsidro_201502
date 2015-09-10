

$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#jqDialogCargando').modal('show');
    })

    $(document).ajaxStop(function () {
        $('#jqDialogCargando').modal('hide');
    });
    /*
    $(document).ajaxComplete(function (event, xhr, settings) { 
        try {
            var obj = JSON.parse(xhr.responseText); 
            if (obj.Codigo == 'ERROR') { 
                OpenDialogConfirmacion('Su sesion ha expirado, vuelva a ingresar al sistema..', headerDialog, 1000, function () {
                    CloseDialogModal('#jqConfirmacion');
                    window.location = path + "Home/Login";
                }); 
            } 
        }
        catch (err) {
            // Do something about the exception here
        }                   
    });*/
});



function LlamarConfirmar(titulo, mensaje, eventoConfirmar) {
    /*
    ModalConfirm            --> popup
    headerConfirmTitle      --> titulo del mensaje
    hidModalId              --> valor a guardar
    tdMensajeConfirm        --> texto del mensaje
    btnAceptarModalConfirm  --> boton que se le debe agregar el evento de confirmacion
    */


    if (titulo != '') {
        $('#headerConfirmTitle').html(titulo);
    }
    if (mensaje != '') {
        $('#tdMensajeConfirm').html(mensaje);
    }

    //$("#btnAceptarModalConfirm").off('click').click(btnAceptarModalConfirm);
    //  $("#btnConfirm").off('click').click(btnConfirm);
    $('#ModalConfirm').modal('show');

}

function LlamarAlert(titulo, mensaje, evento) {
    /*
    ModalAlert
    ModalAlertTitle
    hidModalAlert
    tdModalAlert
    */
    if (titulo != '') {
        $('#ModalAlertTitle').html(titulo);
    }
    if (mensaje != '') {
        $('#tdModalAlert').html(mensaje);
    }
    $("#btnAlertCancel").off('click').click(evento);
    $('#ModalAlert').modal('show');
}




