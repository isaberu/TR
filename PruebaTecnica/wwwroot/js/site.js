// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    setTimeout(function () {
        $(".alert").fadeOut("slow");
    }, 3000); // Desaparece en 3 segundos
});

$(document).ready(function () {

    $("#fecha").datepicker({
        maxDate: 0,
        dateFormat: "dd/mm/yy"
    });
});

function cambiarComaPorPunto() {
    var input = document.getElementById("ImportePedidoRealizado");
    input.value = input.value.replace(/,/g, ".");
}

$('#searchComercial, #searchCliente').on('keyup', function () {
    var filterComercial = $('#searchComercial').val().toLowerCase();
    var filterCliente = $('#searchCliente').val().toLowerCase();

    $('#tablaVisitas tbody tr').each(function () {
        var tdComercial = $(this).find('td').eq(0).text().toLowerCase();
        var tdCliente = $(this).find('td').eq(1).text().toLowerCase();

        if (tdComercial.indexOf(filterComercial) > -1 && tdCliente.indexOf(filterCliente) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});
