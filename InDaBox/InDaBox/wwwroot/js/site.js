//almacen????
//pasillo == numero del pasillo : le pasamos un pasillo especifico
//seccion == numero de la seccion : le pasamos un seccion especifico
//columna == numero de la columna : le pasamos una columna especifica     
//fila == numero de la fila : le pasamos una fila especifica

//seccion de elementos
$('.Borrar').click(eventoBorrarPasillo);
$('.Anadir').click(eventoMostrarCamposAnadirPasillo);

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


$('form input').keydown(function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

$('#numeroDePasillo').change(mostrarPasillos);
$('#nombrePasillo').change(mostrarPasillos);

//seccion de metodos
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

    $('.activar-flecha').off();
    $('.activar-flecha').click(abrirCerrarFlecha);
}
function anadirPasillo(elemento, nombrePasillo, pasillo) {
    anadirNombrePasilloHidden(elemento, nombrePasillo, pasillo);
    anadirCamposSeccionesAPasillo(elemento, nombrePasillo, pasillo);

    function anadirNombrePasilloHidden(elemento, nombrePasillo, pasillo) {
        $(elemento)
            .append(`<input type="hidden" value="${nombrePasillo}_${pasillo + 1}" name="Pasillos[${pasillo}].Nombre"/>`);
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
    $('.activar-flecha').off();
    $('.activar-flecha').click(abrirCerrarFlecha);
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
    return `<div class="row">
                <div class="flecha form-group col-12 mx-auto textoTitulo">
                    <h4 class="insertarNombreSeccion activar-flecha" data-toggle="collapse" href="#insertarHiddenFila_${pasillo}${seccion}-ocultar"><i class="icono plus fa fa-angle-right tiempo-giro flecha-abierta"></i>Sección: <span id="seccionUnica_${pasillo}${seccion}" class="color-nombre"></span></h4> 
                    <input type="hidden" value="${pasillo}" id="inputSeccion_${pasillo}${seccion}"} name="Pasillos[${pasillo}].Secciones[${seccion}].Nombre">
                    <hr class="linea mx-auto"/>
                 </div>
                <div id="insertarHiddenFila_${pasillo}${seccion}-ocultar" class="row collapse show">
                    <div class="form-group ajustar-padding-columnajs col-6">
                        <label class="control-label">Numero de columnas</label> 
                        <input id="numeroDeColumnas_${pasillo}${seccion}" name="numeroDeColumnas_${seccion}" class="form-control"/>
                        <span class="text-danger"></span>
                        <div id="insertarHiddenColumna_${pasillo}${seccion}"></div>
                    </div>
                    <div class="form-group ajustar-padding-columnajs col-6">
                        <label class="control-label">Numero de filas</label>
                        <input id="numeroDeFilas_${pasillo}${seccion}" name="numeroDeFilas_${seccion}" class="form-control"/>
                        <div id="insertarHiddenFila_${pasillo}${seccion}"></div>
                    </div>
                </div>
            </div>`;
}

function stringSecciones(idElementoPasillo, cabeceraPasillo, pasillo) {    
    return `<div class="row">
            <div class="flecha form-group col-12 mx-auto textoTitulo">
                <h4 class="insertarNombrePasillo activar-flecha" id="${idElementoPasillo}" data-toggle="collapse" href="#insertarSeccion${pasillo}-ocultar"><i class="icono plus fa fa-angle-right tiempo-giro flecha-abierta"></i>Pasillo: <span class="color-nombre">${cabeceraPasillo}</span></h4> 
                <hr class="linea mx-auto"/>
            </div>
            <div id="insertarSeccion${pasillo}-ocultar" class="row collapse show">
                <div class="form-group ajustar-padding col-6">
                    <label class="control-label">Numero de secciones</label>
                    <input id="NumeroDeSecciones${pasillo}" class="form-control"/>
                </div>
                <div class="form-group ajustar-padding col-6">
                    <label class="control-label">Nombre de sección</label>
                    <input id="Pasillos_${pasillo}" class="form-control"/>
                    <span class="text-danger"></span>
                </div>
                <div id="insertarSeccion${pasillo}"></div>
            </div>
            </div>`;
}

    $('.activar-flecha').click(function () {
        $(this).children('i').toggleClass('flecha-abierta');
    });

$(document).ready(function () {
    $(".flecha-abajo").click(function () {
        $(".flecha-derecha").slideToggle(300);
    });
});

function abrirCerrarFlecha() {
    $(this).children('i').toggleClass('flecha-abierta');
}

//-------------Animacion Index-----------------

document - addEventListener('DOMContentLoaded', function () {
    let wrapper = document.getElementById('wrapper');
    let topLayer = wrapper.querySelector('.top');
    let handle = wrapper.querySelector('.handle');
    let skew = 0;
    let delta = 0;

    if (wrapper.className.indexOf('skewed') != -1) {
        skew = 990;
    }

    wrapper.addEventListener('mousemove', function (e) {
        delta = (e.clientX - window.innerWidth / 2) * 0.5;

        handle.style.left = e.clientX + delta + 'px';

        topLayer.style.width = e.clientX + skew + delta + 'px';

    })
});
