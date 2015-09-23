/*
* Da formato a un número para su visualización
*
* numero (Number o String) - Número que se mostrará
* decimales (Number, opcional) - Nº de decimales (por defecto, auto)
* separador_decimal (String, opcional) - Separador decimal (por defecto, coma)
* separador_miles (String, opcional) - Separador de miles (por defecto, ninguno)
*/
function formato_numero(numero, decimales, separador_decimal, separador_miles) { // v2007-08-06
    numero = parseFloat(numero);
    if (isNaN(numero)) {
        return "";
    }

    if (decimales !== undefined) {
        // Redondeamos
        numero = numero.toFixed(decimales);
    }

    // Convertimos el punto en separador_decimal
    numero = numero.toString().replace(".", separador_decimal !== undefined ? separador_decimal : ",");

    if (separador_miles) {
        // Añadimos los separadores de miles
        var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
        while (miles.test(numero)) {
            numero = numero.replace(miles, "$1" + separador_miles + "$2");
        }
    }

    return numero;
}
/*
* solo numeros
*/
function solo_numeros() {
    var key = window.event.keyCode;
    if (key < 48 || key > 57) {
        window.event.keyCode = 0;
    } 
}
