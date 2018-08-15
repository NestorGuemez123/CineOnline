function validarOpinion(datos) {
    alert(datos.opinion.value);
    if (datos.opinion.value != "") {
        registrarOpinion(datos);
    }
}

function registrarOpinion(datos) {

    var opinion = {
        MediaId: datos.serieId.value,
        Descripcion: datos.opinion.value,
        UsuarioId: null,
        FechaRegistro: Date.now(),
        Puntuacion: 4 //Aqui se debe de modificar la puntuacion
    };

    $.ajax({
        url: '@Url.Action("Registrar", "Opinion")',
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        data: JSON.stringify(opinion),
        success: function (data) {
            if (data.Success) {
                alert("Se rigistro la opinion");
            } else {
                alert("No se registro la opinion");
            }
        }
    });
}