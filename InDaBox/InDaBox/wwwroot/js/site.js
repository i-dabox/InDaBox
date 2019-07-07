$('form input').keydown(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

$('#numeroDePasillo').change(mostrarPasillos);
$('#nombrePasillo').change(mostrarPasillos);

function mostrarPasillos() {
    let numeroPasillo = $('#numeroDePasillo').val();
    let nombrePasillo = $('#nombrePasillo').val();

    if (numeroPasillo === "" || nombrePasillo === "") {
        return;
    }

    $('#insertarPasillo').empty();
    for (var i = 0; i < numeroPasillo; i++) {
        $('#insertarPasillo')
            .append(`<input type="hidden" value="${nombrePasillo}" name="Pasillos[${i}].Nombre" />`);
    }

    for (var i = 0; i < numeroPasillo; i++) {
        $('#insertarPasillo')
            .append(
                `<div class="flecha">
        <h1 class="insertarNombrePasillo" id="pasilloUnico${i}"></h1> 
        <hr>
    </div>
    <div class="form-group">
        <label  class="control-label">Numero de secciones</label>
        <input id="NumeroDeSecciones${i}" name="NumeroDeSecciones${i}" class="form-control"/>
    </div>
    <div>
        <label  class="control-label">Nombre de sección</label>
        <input id="Pasillos[${i}].Secciones[${i}].Nombre" name="Pasillos[${i}].Secciones[${i}].Nombre" class="form-control"/>
        <span class="text-danger"></span>
    </div>
    <div id="insertarSeccion${i}"></div>`
        );
        let seccion = i; 
        $(`#NumeroDeSecciones${seccion}`).change(function() {
            mostrarSecciones(seccion);
        });
        $(`#Pasillos\\[${seccion}\\]\\.Secciones\\[${seccion}\\]\\.Nombre`).change(function() {
            mostrarSecciones(seccion);
        });
    };

    $('.insertarNombrePasillo').empty();
    let numero = 1;
    for (let i = 0; i < numeroPasillo; i++) {
        $('#pasilloUnico' + i).append(nombrePasillo + " " + numero);
        numero++;
    };
};

function mostrarSecciones(seccion) {
    let numeroSeccion = $(`#NumeroDeSecciones${seccion}`).val();
    let nombreSeccion = $(`#Pasillos\\[${seccion}\\]\\.Secciones\\[${seccion}\\]\\.Nombre`).val();
    let pasillo = seccion;
    if (numeroSeccion === "" || nombreSeccion === "") {
        return;
    }
    $('#insertarSeccion' + pasillo).empty();
    for (var i = 0; i < numeroSeccion; i++) {
        $('#insertarSeccion' + pasillo).append(
            `<div class="flecha">
     <h1 class="insertarNombreSeccion" id="seccionUnica_${pasillo}${i}"}></h1> 
     <input type="hidden" value="nombrepasillo" name="Pasillos[${pasillo}].Secciones[${i}].Nombre">
     <hr>
     </div>
     <div class="form-group">
        <label class="control-label">Numero de columnas</label> 
        <input id="numeroDeColumnas_${pasillo}${i}" name="numeroDeColumnas_${i}" class="form-control"/>
        <span class="text-danger"></span>
        <div id="insertarHiddenColumna_${pasillo}${i}"></div>
        </div>
     <div class="form-group">
        <label class="control-label">Numero de filas</label>
        <input id="numeroDeFilas_${pasillo}${i}" name="numeroDeFilas_${i}" class="form-control"/>
        <div id="insertarHiddenFila_${pasillo}${i}"></div>
    </div>`
        );
        let seccion = i;
        $(`#Pasillos\\[${pasillo}\\]\\.Secciones\\[${pasillo}\\]\\.Nombre`).change(
            nombreSecciones(seccion, pasillo, nombreSeccion)); // a ver rellena el formulario y mandame pantallazo antes de enviar
       
        $(`#numeroDeColumnas_${pasillo}${i}`).change(function () {
            insertarHidden(pasillo,seccion);
        });
        $(`#numeroDeFilas_${pasillo}${i}`).change(function () {
            insertarHidden(pasillo, seccion);
        });

    }
}

function insertarHidden(pasillo, seccion) {
    let numeroColumna = $(`#numeroDeColumnas_${pasillo}${seccion}`).val();
    let numeroFila = $(`#numeroDeFilas_${pasillo}${seccion}`).val(); 
    if (numeroColumna === "" || numeroFila === "") {
        return;
    }

    for (var i = 0; i < numeroColumna; i++) {
        $(`#insertarHiddenColumna_${pasillo}${seccion}`).append(`
            <input type="hidden" value="columna_${pasillo}_${seccion}_${i}" name="Pasillos[${pasillo}].Secciones[${seccion}].Columnas[${i}].Nombre">`);
       
        for (var j = 0; j < numeroFila; j++) {
            $(`#insertarHiddenFila_${pasillo}${seccion}`).append(`
                <input type="hidden" value="fila_${pasillo}_${seccion}_${i}_${j}" name="Pasillos[${pasillo}].Secciones[${seccion}].Columnas[${i}].Filas[${j}].Nombre">`);
        }
    }
}

function nombreSecciones(seccion, pasillo, nombreSeccion) {
    let numero = 1;
    $(`#seccionUnica_${pasillo}${seccion}`).empty();
 
    for (let i = 0; i <= seccion; i++) {
        $(`#seccionUnica_${pasillo}${seccion}`).html(nombreSeccion + " " + numero);
        numero++;
    };
}
