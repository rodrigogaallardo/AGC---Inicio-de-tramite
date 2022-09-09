
function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function separadorDecimal() {
    return parseFloat('1.1').toLocaleString().substring(1, 2);
}

function stringToFloat(value, vSeparadorDecimal) {

    var ret = 0.00;

    while (value.indexOf("_") >= 0) {
        value = value.replace("_", "");
    }

    if (vSeparadorDecimal == null)
        vSeparadorDecimal = separadorDecimal();


    if (vSeparadorDecimal == ',') {
        value = value.replace(".", "");
        value = value.replace(",", ".");
    }
    else {
        value = value.replace(",", "");
    }

    if (isNumber(value))
        ret = parseFloat(value);

    return ret;

}
function replaceAll(text, busca, reemplaza) {

    while (text.toString().indexOf(busca) != -1)
        text = text.toString().replace(busca, reemplaza);
    return text;
}


function ValidarCuit(e) {

    var ret = true;
    var str = replaceAll(e.value, "_", "");
    
    var parts = str.split("-");

    if (parts.length != 3) {
        ret = false;
        return ret;
    }

    if (parts[1].length == 7) {
        parts[1] = "0" + parts[1];
    }

    var cuitAuxiliar = parts.join("");

    if (cuitAuxiliar.length < 10) {
        ret = false;
        return ret;
    }

    if (cuitAuxiliar == "00000000000") {
        e.IsValid = false;
        return;
    }

    var sum = 0;
    var dv = 0;
    var i = 0;
    var factor = 2;
    var caracter = "";
    var Modulo11 = 0;
    var Verificador = 0;
    var CodVer = 0;

    for (i = 9; i >= 0; i--) {
        caracter = cuitAuxiliar.charAt(i);
        if (factor > 7) {
            factor = 2;
        }
        sum += parseInt(caracter) * factor;
        factor++;

    }

    dv = sum / 11;
    Modulo11 = sum - (11 * parseInt(dv));
    Verificador = 11 - Modulo11;
    if (Modulo11 == 0) {
        CodVer = 0;
    }
    else if (Verificador == 10) {
        CodVer = 9;
    }
    else {
        CodVer = Verificador;
    }

    if (CodVer != parseInt(cuitAuxiliar.charAt(10))) {
        ret = false;
        return ret;
    }

    return ret;
}

function ValidarCuitValidator(e) {

    var str = replaceAll(e.Value, "_", "");

    var parts = str.split("-");

    if (parts.length != 3) {
        e.IsValid = false;
        return;
    }

    if (parts[1].length == 7) {
        parts[1] = "0" + parts[1];
    }

    var cuitAuxiliar = parts.join("");

    if (cuitAuxiliar.length < 10) {
        e.IsValid = false;
        return;
    }

    if (cuitAuxiliar == "00000000000") {
        e.IsValid = false;
        return;
    }

    var sum = 0;
    var dv = 0;
    var i = 0;
    var factor = 2;
    var caracter = "";
    var Modulo11 = 0;
    var Verificador = 0;
    var CodVer = 0;

    for (i = 9; i >= 0; i--) {
        caracter = cuitAuxiliar.charAt(i);
        if (factor > 7) {
            factor = 2;
        }
        sum += parseInt(caracter) * factor;
        factor++;

    }

    dv = sum / 11;
    Modulo11 = sum - (11 * parseInt(dv));
    Verificador = 11 - Modulo11;
    if (Modulo11 == 0) {
        CodVer = 0;
    }
    else if (Verificador == 10) {
        CodVer = 9;
    }
    else {
        CodVer = Verificador;
    }

    if (CodVer != parseInt(cuitAuxiliar.charAt(10))) {
        e.IsValid = false;
        return;
    }

    e.IsValid = true;
}
function ValidarCuitSinGuiones(e) {

    var ret = true;

    if (e.value.length != 11) {
        ret = false;
        return ret;
    }

    var parts = new Array();
    parts[0] = e.value.substring(0, 2);
    parts[1] = e.value.substring(2, 10);
    parts[2] = e.value.substring(10);

    var cuitAuxiliar = parts.join("");

    if (cuitAuxiliar.length < 10) {
        ret = false;
    }

    if (cuitAuxiliar == "00000000000") {
        ret = false;
    }

    if (ret) {
        var sum = 0;
        var dv = 0;
        var i = 0;
        var factor = 2;
        var caracter = "";
        var Modulo11 = 0;
        var Verificador = 0;
        var CodVer = 0;

        for (i = 9; i >= 0; i--) {
            caracter = cuitAuxiliar.charAt(i);
            if (factor > 7) {
                factor = 2;
            }
            sum += parseInt(caracter) * factor;
            factor++;

        }

        dv = sum / 11;
        Modulo11 = sum - (11 * parseInt(dv));
        Verificador = 11 - Modulo11;
        if (Modulo11 == 0) {
            CodVer = 0;
        }
        else if (Verificador == 10) {
            CodVer = 9;
        }
        else {
            CodVer = Verificador;
        }

        if (CodVer != parseInt(cuitAuxiliar.charAt(10))) {
            ret = false;
        }
    }

    return ret;
}
function maxlength(control, tamanio) {
    
    var texto = $(control).val();
    if (texto.length > tamanio) {
        $(control).val(texto.substring(0, tamanio));
    }
    return false;
}
function soloNumeros(evt) {
    //asignamos el valor de la tecla a keynum
    if (window.event) {// IE
        keynum = evt.keyCode;
    } else {
        keynum = evt.which;
    }

    //comprobamos si se encuentra en el rango
    if ((keynum >= 48 && keynum <= 57) || (keynum >= 96 && keynum <= 105) || keynum == 44 || keynum == 46 || keynum == 8 || keynum == 7 || keynum == 9) {
        return true;
    } else {
        return false;
    }
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function separadorDecimal() {
    return parseFloat('1.1').toLocaleString().substring(1, 2);
}

function stringToFloat(value, vSeparadorDecimal) {

    var ret = 0.00;

    while (value.indexOf("_") >= 0) {
        value = value.replace("_", "");
    }

    if (vSeparadorDecimal == null)
        vSeparadorDecimal = separadorDecimal();


    if (vSeparadorDecimal == ',') {
        value = value.replace(".", "");
        value = value.replace(",", ".");
    }
    else {
        value = value.replace(",", "");
    }

    if (isNumber(value))
        ret = parseFloat(value);

    return ret;

}
function replaceAll(text, busca, reemplaza) {

    while (text.toString().indexOf(busca) != -1)
        text = text.toString().replace(busca, reemplaza);
    return text;
}

function ValidarCuit(sender, e) {
    var str = replaceAll(e.Value, "_", "");
    var parts = str.split("-");

    if (parts.length != 3) {
        e.IsValid = false;
        return;
    }

    if (parts[1].length == 7) {
        parts[1] = "0" + parts[1];
    }

    var cuitAuxiliar = parts.join("");

    if (cuitAuxiliar.length < 10) {
        e.IsValid = false;
        return;
    }

    if (cuitAuxiliar == "00000000000") {
        e.IsValid = false;
        return;
    }

    var sum = 0;
    var dv = 0;
    var i = 0;
    var factor = 2;
    var caracter = "";
    var Modulo11 = 0;
    var Verificador = 0;
    var CodVer = 0;

    for (i = 9; i >= 0; i--) {
        caracter = cuitAuxiliar.charAt(i);
        if (factor > 7) {
            factor = 2;
        }
        sum += parseInt(caracter) * factor;
        factor++;

    }

    dv = sum / 11;
    Modulo11 = sum - (11 * parseInt(dv));
    Verificador = 11 - Modulo11;
    if (Modulo11 == 0) {
        CodVer = 0;
    }
    else if (Verificador == 10) {
        CodVer = 9;
    }
    else {
        CodVer = Verificador;
    }

    if (CodVer != parseInt(cuitAuxiliar.charAt(10))) {
        e.IsValid = false;
        return;
    }

    e.IsValid = true;
}


function mostrarPopup(id_popup, top, sinEffect) {


    if (top == 0 || top == undefined) {
        top = $(window).scrollTop() + 100;
    }


    $(this).removeAttr("href");

    //transition effect
    var docH = $(document).height() - 5;
    var zindex;


    var divmsk = $("#divmask" + id_popup)[0];
    if (divmsk == undefined) {
        divmsk = document.createElement('div');
        $(divmsk).attr("id", "divmask" + id_popup);
        $(divmsk).css('background-color', 'Gray');
        $(divmsk).css('position', 'absolute');

        zindex = max_ZIndex("form");
        zindex++;

        $(divmsk).css('z-index', zindex);
        $(divmsk).css('top', '0px');
        $(divmsk).css('left', '0px');
        $(divmsk).css('width', '100%');
        $(divmsk).css('height', docH.toString() + 'px');


        document.body.appendChild(divmsk);
    }
    else {
        zindex = parseInt($(divmsk).css('z-index'));
    }

    //$('#mask').css('height', docH.toString() + 'px');

    var id = "#" + id_popup;
    //$('#mask').fadeIn(1000);
    //$('#mask').fadeTo("slow", 0.8);

    $(divmsk).fadeIn(1000);
    $(divmsk).fadeTo("slow", 0.8);

    //Get the window height and width

    var winW = $(window).width();

    //Set the popup window to center         
    $(id).css('top', top.toString() + 'px');
    $(id).css('left', winW / 2 - $(id).width() / 2);


    zindex++;
    $(id).css('z-index', zindex);

    //transition effect
    if (sinEffect) {
        $(id).show();
    }
    else {
        $(id).fadeIn(2000);
    }

    return false;
}

function ocultarPopup(id_popup) {


    $('#divmask' + id_popup).remove();
    $('#' + id_popup).hide();

    return false;
}

function max_ZIndex(obj) {
    var result = 0;
    var zindex = 0;


    $('.modalPopup, div[id|="divmask"]').each(function () {


        if ($(this).css("display") != "none") {
            zindex = parseInt($(this).css('z-index'));
            if (result <= zindex) {
                result = zindex;
            }
        }

    });

    return result;

}

function maxlength(id_control, tamanio) {

    var texto = $('#' + id_control).attr('value');
    if (texto.length > tamanio) {
        $('#' + id_control).attr('value', texto.substring(0, tamanio));
    }
    return false;
}