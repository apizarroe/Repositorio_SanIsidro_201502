//creamos una variable de tipo array para pasar y recuperar los datos
var datos = new Array();
var datos1 = new Array();
var datos2 = new Array();
var datos3 = new Array();

function abrir(url, largo, altura) {
    var left = (screen.width - largo) / 2
    var top = ((screen.height - altura) / 2) - 25
    if (top < 0)
        top = 0
    open(url, 'ventana', 'scrollbars=yes,top=' + top + ',left=' + left + ',width=' + largo + ',height=' + altura);
}

function abrir_modal(url, largo, altura, obj1) {
    //    var datos = new Array();
    datos = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;
    campo1 = eval(obj1);
    if (isArray(datos)) {
        campo1.value = datos[0];
        document.forms[0].submit();
    }

    //var btn = document.getElementById("TuButtonClientID");

    //    else {
    //        campo1.value = '';
    //    }
}

function abrir_modal2(url, largo, altura, obj1, obj2) {
    url.valueOf
    datos2 = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;

    campo1 = eval(obj1);
    campo2 = eval(obj2);
    if (isArray(datos2)) {
        campo1.value = datos2[0];
        campo2.value = datos2[1];
        document.forms[0].submit();
    }
}

function abrir_modal3(url, largo, altura, obj1, obj2, obj3) {
    url.valueOf
    datos3 = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;

    campo1 = eval(obj1);
    campo2 = eval(obj2);
    campo3 = eval(obj3);
    if (isArray(datos3)) {
        campo1.value = datos3[0];
        campo2.value = datos3[1];
        campo3.value = datos3[2];
        document.forms[0].submit();
    }
}

function abrir_modal_submit(url, largo, altura, obj1, obj2) {

    datos1 = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;

    campo1 = eval(obj1);
    boton1 = eval(obj2);
    if (isArray(datos2)) {
        campo1.value = datos1[0];
        //campo2.value = datos2[1];
        boton1.click();
    }
}

function abrir_moda2(url, largo, altura, obj1, obj2) {
    url.valueOf
    datos2 = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;

    campo1 = eval(obj1);
    campo2 = eval(obj2);
    if (isArray(datos2)) {
        campo1.value = datos2[0];
        campo2.value = datos2[1];

    }
}


function AbreVentanaModal() {
    datos[0] = DatoPadre1.value;
    datos[1] = DatoPadre2.value;
    datos[2] = DatoPadre3.value;

    //aqui paso los datos a la ventana hija
    datos = showModalDialog('hija.htm', datos, 'status:no;resizable:yes');

    //aqui recuepero datos de la ventana hija
    DatoPadre1.value = datos[0];
    DatoPadre2.value = datos[1];
    DatoPadre3.value = datos[2];
}
//aqui recuperamos los datos de la ventana padre 
function RecuperaDatos() {
    datos = dialogArguments;
    DatoHijo1.value = datos[0];
    DatoHijo2.value = datos[1];
    DatoHijo3.value = datos[2];
}

//aqui le devolvemos los datos a la ventana padre 
function Devuelve(valor) {
    Array.add(datos, valor);
    datos.constructor = 'Array'
    //datos.a
    //    datos[1] = DatoHijo2.value;
    //    datos[2] = DatoHijo3.value;

    returnValue = datos;
    window.close();
}



function Devuelve2(valor1, valor2) {
    Array.add(datos2, valor1);
    Array.add(datos2, valor2);
    datos2.constructor = 'Array'
    //datos2[0] = valor1;
    //datos2[1] = valor2;
    //    datos[2] = DatoHijo3.value;

    returnValue = datos2;
    window.close();
}

function Devuelve3(valor1, valor2, valor3) {
    Array.add(datos3, valor1);
    Array.add(datos3, valor2);
    Array.add(datos3, valor3);
    datos3.constructor = 'Array'
    returnValue = datos3;
    window.close();
}

function abrir_moda(url, largo, altura, obj1) {
    //    var datos = new Array();
    datos = window.showModalDialog(url, datos, "dialogHeight: " + altura + "px; dialogWidth: " + largo + "px; status:no;") + 0;
    campo1 = eval(obj1);
    if (isArray(datos)) {

        document.forms[0].submit();
    }


}

function isArray(obj) {
    if (obj.constructor.toString().indexOf("Array") == -1)
        return false;
    else
        return true;
}

function OcultarNivel(condition) {
    if (condition == '1')
        document.getElementById('ImgBuscarSue').style.display = 'none';

    else if (condition == '2')
        document.getElementById('ImgBuscarSue').style.display = 'none';

}

function AbrirAnexoCorreccion(Anno, CodDependenciaOrigen, CodTipoTramite, CodTipoAnexo, NumeroDocumento) {
    window.showModalDialog('FrmDetalleAnexo.aspx?Anno=' + Anno + '&CodDependenciaOrigen=' + CodDependenciaOrigen + '&CodTipoTramite=' + CodTipoTramite + '&CodTipoAnexo=' + CodTipoAnexo + ' &NumeroDocumento=' + NumeroDocumento, datos, "dialogHeight: 300px; dialogWidth: 710px; status:no;") + 0;

}

function AbrirDocumentoTramite(Ibp1, ibp2, forma1, forma2, numerosecuencia1, numerosecuencia2) {
    window.showModalDialog('FrmDocumentosTramite.aspx?CodigoIBP1=' + Ibp1 + '&CodigoIBP2=' + ibp2 + '&NumeroSecuencia1=' + numerosecuencia1, datos, "dialogHeight: 300px; dialogWidth: 710px; status:no;") + 0;

}

function hideModalPopupViaClient(ev) {
    ev.preventDefault();
    var modalPopupBehavior = $find('programmaticModalPopupBehavior');
    modalPopupBehavior.hide();
}


function AbrirModalIBPDetalle(ibp, forma) {
    //muebles
    if (forma == 'F')
        window.showModalDialog('FrmDetalleMueblesIBP.aspx?ibp=' + ibp, datos, "dialogHeight: 480px; dialogWidth: 700px; status:no;");
    //vehiculos
    else if (forma == 'G')
        window.showModalDialog('FrmDetalleVehiculosIBP.aspx?ibp=' + ibp, datos, "dialogHeight: 480px; dialogWidth: 700px; status:no;");

    // naves
    else if (forma == 'H')
        window.showModalDialog('FrmDetalleNavesIBP.aspx?ibp=' + ibp, datos, "dialogHeight: 480px; dialogWidth: 700px; status:no;");

    //inmueble
    else if (forma == 'I')
        window.showModalDialog('FrmDetalleInmueblesIBP.aspx?ibp=' + ibp, datos, "dialogHeight: 480px; dialogWidth: 700px; status:no;");


}