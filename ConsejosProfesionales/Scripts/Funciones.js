
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

function ValidarCuit(sender, e) {
    var parts = e.Value.split("-");

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
function mostrarPopup(id_popup, top) {

    $("#" + id_popup).modal("show");
    //if (top == 0 || top == undefined) {
    //    top = $(window).scrollTop() + 100;
    //}


    //$(this).removeAttr("href");

    ////transition effect
    //var docH = $(document).height() - 5;

    //var divmsk = document.createElement('div');
    //$(divmsk).attr("id", "div" + id_popup);
    //$(divmsk).css('background-color', 'Gray');
    //$(divmsk).css('position', 'absolute');

    //var zindex = max_ZIndex("form");
    //zindex++;

    //$(divmsk).css('z-index', zindex);
    //$(divmsk).css('top', '0px');
    //$(divmsk).css('left', '0px');
    //$(divmsk).css('width', '100%');
    //$(divmsk).css('height', docH.toString() + 'px');

    //document.body.appendChild(divmsk);


    ////$('#mask').css('height', docH.toString() + 'px');

    //var id = "#" + id_popup;
    ////$('#mask').fadeIn(1000);
    ////$('#mask').fadeTo("slow", 0.8);

    //$(divmsk).fadeIn(1000);
    //$(divmsk).fadeTo("slow", 0.8);

    ////Get the window height and width

    //var winW = $(window).width();

    ////Set the popup window to center         
    //$(id).css('top', top.toString() + 'px');
    //$(id).css('left', winW / 2 - $(id).width() / 2);


    //zindex++;
    //$(id).css('z-index', zindex);

    ////transition effect         
    //$(id).fadeIn(2000);

    return false;
}

function ocultarPopup(id_popup) {

    $("#" + id_popup).modal("hide");    
}

function max_ZIndex(obj) {
    var result = 0;
    var zindex = 0;


    $('.modalPopup').each(function () {

        if ($(this).css("display") != "none")
            result += 2;

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