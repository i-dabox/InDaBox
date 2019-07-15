//almacen????
//pasillo == numero del pasillo : le pasamos un pasillo especifico
//seccion == numero de la seccion : le pasamos un seccion especifico
//columna == numero de la columna : le pasamos una columna especifica     
//fila == numero de la fila : le pasamos una fila especifica

//seccion de elementos
$('.Borrar').click(eventoBorrarPasillo);
$('.Anadir').click(eventoMostrarCamposAnadirPasillo);
$('.BotonEditar').click(eventoMandarIdAModalEditar);
$('.BotonBorrar').click(eventoMandarIdAModalBorrar);

//Eventos
function eventoBorrarPasillo(event) {
    event.preventDefault();
    let id = $(this).attr('id');
    borrarPasillo(id);
}
function eventoMostrarCamposAnadirPasillo(event) {
    event.preventDefault();
    mostrarCamposAnadirPasillo();
}

function eventoMandarIdAModalEditar(event) {
    event.preventDefault();
    let id = $(this).attr('id');
    confirm(id);
}
function eventoMandarIdAModalBorrar(event) {
    event.preventDefault();
    let id = $(this).attr('id');
    borrarProducto(id);
}

$('form input').keydown(function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

$('#numeroDePasillo').change(mostrarPasillos);
$('#nombrePasillo').change(mostrarPasillos);

//seccion de metodos
function borrarProducto(id) {
    $('modalBorrarInput').attr('asp-for=', 'Id');
}
function borrarPasillo(id) {
    $('#' + id).remove();
}
/* Recibe dos selectores de elementos, comprueba que sus valores no esten vacios
 * y si no lo estan devuelve un objeto con sus valores
 */
function mostrarCamposAnadirPasillo() {
    $('#anadirForm').attr('class', ' ');
}
function comprobarCampos(elemento1, elemento2) {
    let valor1 = $(elemento1).val();
    let valor2 = $(elemento2).val();
    if (valor1 === "" || valor2 === "") {
        return null;
    }
    return { campo1: valor1, campo2: valor2 };
}
function vaciarElementos(selectoresElementos) {
    selectoresElementos.forEach(selectorElemento => {
        $(selectorElemento).empty();
    });
}
function mostrarPasillos() {
    let valores = comprobarCampos('#numeroDePasillo', '#nombrePasillo');
    if (valores === null) {
        return;
    }

    let numeroPasillo = valores.campo1;
    let nombrePasillo = valores.campo2;

    vaciarElementos(['#insertarPasillo', '.insertarNombrePasillo']);

    for (let pasillo = 0; pasillo < numeroPasillo; pasillo++) {
        anadirPasillo('#insertarPasillo', nombrePasillo, pasillo);
    }
}
function anadirPasillo(elemento, nombrePasillo, pasillo) {
    anadirNombrePasilloHidden(elemento, nombrePasillo, pasillo);
    anadirCamposSeccionesAPasillo(elemento, nombrePasillo, pasillo);

    function anadirNombrePasilloHidden(elemento, nombrePasillo, pasillo) {
        $(elemento)
            .append(`<input type="hidden" value="${nombrePasillo}_${pasillo + 1}" name="Pasillos[${pasillo}].Nombre" />`);
    }
    function anadirCamposSeccionesAPasillo(elemento, nombrePasillo, pasillo) {
        let cabeceraPasillo = `${nombrePasillo} ${pasillo + 1}`;
        let idElementoPasillo = `pasilloUnico${pasillo}`;

        $(elemento)
            .append(stringSecciones(idElementoPasillo, cabeceraPasillo, pasillo));


        $(`#NumeroDeSecciones${pasillo}`).change(function () {
            mostrarSecciones(pasillo);
        });

        $(`#Pasillos_${pasillo}`).change(function () {
            mostrarSecciones(pasillo);
        });
    }
}
function mostrarSecciones(pasillo) {
    let valores = comprobarCampos(`#NumeroDeSecciones${pasillo}`, `#Pasillos_${pasillo}`);
    if (valores === null) {
        return;
    }
    let numeroSeccion = valores.campo1;
    let nombreSeccion = valores.campo2;

    vaciarElementos([`#insertarSeccion${pasillo}`]);

    for (var seccion = 0; seccion < numeroSeccion; seccion++) {
        anadirSeccion('#insertarSeccion', pasillo, nombreSeccion, seccion);
    }
}
function anadirSeccion(elemento, pasillo, nombreSeccion, seccion) {
    anadirCamposColumnasASecciones(elemento, pasillo, nombreSeccion, seccion);

    function anadirCamposColumnasASecciones(elemento, pasillo, nombreSeccion, seccion) {
        $(elemento + pasillo).append(stringColumnasFilas(pasillo, seccion));

        $(`#Pasillos\\[${pasillo}\\]\\.Secciones\\[${pasillo}\\]\\.Nombre`).change(
            nombreSecciones(pasillo, seccion, nombreSeccion));
        $(`#numeroDeColumnas_${pasillo}${seccion}`).change(function () {
            insertarSeccionHidden(pasillo, seccion);
        });
        $(`#numeroDeFilas_${pasillo}${seccion}`).change(function () {
            insertarSeccionHidden(pasillo, seccion);
        });
    }
}
function nombreSecciones(pasillo, seccion, nombreSeccion) {
    $(`#seccionUnica_${pasillo}${seccion}`).empty();
    $(`#seccionUnica_${pasillo}${seccion}`).html(nombreSeccion + " " + (seccion + 1));
    $(`#inputSeccion_${pasillo}${seccion}`).attr('value', nombreSeccion + " " + (seccion + 1));
}
function insertarSeccionHidden(pasillo, seccion) {
    let numeroColumna = $(`#numeroDeColumnas_${pasillo}${seccion}`).val();
    let numeroFila = $(`#numeroDeFilas_${pasillo}${seccion}`).val();

    if (numeroColumna === "" || numeroFila === "") {
        return;
    }

    for (var columna = 0; columna < numeroColumna; columna++) {
        $(`#insertarHiddenColumna_${pasillo}${seccion}`).append(`
            <input type="hidden" value="columna_${pasillo}_${seccion}_${columna}" name="Pasillos[${pasillo}].Secciones[${seccion}].Columnas[${columna}].Nombre">`);

        for (var fila = 0; fila < numeroFila; fila++) {
            $(`#insertarHiddenFila_${pasillo}${seccion}`).append(`
                <input type="hidden" value="fila_${pasillo}_${seccion}_${columna}_${fila}" name="Pasillos[${pasillo}].Secciones[${seccion}].Columnas[${columna}].Filas[${fila}].Nombre">`);
        }
    }
}//Haccer llegar el numero de almacen o añadirlo aqui
function stringColumnasFilas(pasillo, seccion) {
    return `<div class="flecha">
                <h1 class="insertarNombreSeccion" id="seccionUnica_${pasillo}${seccion}"}></h1> 
                <input type="hidden" value="${pasillo}" id="inputSeccion_${pasillo}${seccion}"} name="Pasillos[${pasillo}].Secciones[${seccion}].Nombre">
                <hr>
            </div>
            <div class="form-group">
                <label class="control-label">Numero de columnas</label> 
                <input id="numeroDeColumnas_${pasillo}${seccion}" name="numeroDeColumnas_${seccion}" class="form-control"/>
                <span class="text-danger"></span>
                <div id="insertarHiddenColumna_${pasillo}${seccion}"></div>
            </div>
            <div class="form-group">
                <label class="control-label">Numero de filas</label>
                <input id="numeroDeFilas_${pasillo}${seccion}" name="numeroDeFilas_${seccion}" class="form-control"/>
                <div id="insertarHiddenFila_${pasillo}${seccion}"></div>
            </div>`;
}
function stringSecciones(idElementoPasillo, cabeceraPasillo, pasillo) {
    return `<div class="flecha">
                <h1 class="insertarNombrePasillo" id="${idElementoPasillo}">${cabeceraPasillo}</h1> 
                <hr>
            </div>
            <div class="form-group">
                <label class="control-label">Numero de secciones</label>
                <input id="NumeroDeSecciones${pasillo}" class="form-control"/>
            </div>
            <div>
                <label class="control-label">Nombre de sección</label>
                <input id="Pasillos_${pasillo}" class="form-control"/>
                <span class="text-danger"></span>
            </div>
            <div id="insertarSeccion${pasillo}"></div>`;
}

$('.tiempo-giro').click(function () {
    $(this).toggleClass('flecha-abierta');
});
