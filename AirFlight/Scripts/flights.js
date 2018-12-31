(function () {
    'use strict'
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation')

        // Loop over them and prevent submission
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
    }, false);
}())

$(document).ready(function () {
    $('#departureairport').select2(
        {
            ajax: {
                url: '/Home/FindAirport',
                datatype: 'json',
                delay: 800,
                data: function (params) {
                    var query = {
                        search: params.term + ' airport'
                    }
                    return query;
                },
                processResults: function (data) {
                    try {
                        var serverData = $.map(JSON.parse(data), function (obj) {
                            obj.id = obj.id || obj.Value;
                            obj.text = obj.text || obj.Text;

                            return obj;
                        })
                        return {
                            results: serverData
                        }
                    }
                    catch (e) {
                        $("html").html(data);
                    }
                }
            },
            minimumInputLength: 3
        });
    $('#destinationairport').select2(
        {
            ajax: {
                url: '/Home/FindAirport',
                datatype: 'json',
                delay: 800,
                data: function (params) {
                    var query = {
                        search: params.term + ' airport'
                    }
                    return query;
                },
                processResults: function (data) {
                    try {
                        var serverData = $.map(JSON.parse(data), function (obj) {
                            obj.id = obj.id || obj.Value;
                            obj.text = obj.text || obj.Text;

                            return obj;
                        });
                        return {
                            results: serverData
                        }
                    }
                    catch (e) {
                        $("html").html(data);
                    }
                }
            },
            minimumInputLength: 3
        });

    $('#destinationairport').on('select2:select', function (e) {
        $("#DestinationAirportName").val(e.params.data.text);
    });
    $('#departureairport').on('select2:select', function (e) {
        $("#DepartureAirportName").val(e.params.data.text);
    });

    if ($("#DepAirportGeoPoint").val() && $("#DepartureAirportName").val()) {
        var data = {
            id: $("#DepAirportGeoPoint").val(),
            text: $("#DepartureAirportName").val()
        };

        var newOption = new Option(data.text, data.id, false, false);
        $('#departureairport').append(newOption).trigger('change');
    }

    if ($("#DestAirportGeoPoint").val() && $("#DestinationAirportName").val()) {
        var data = {
            id: $("#DestAirportGeoPoint").val(),
            text: $("#DestinationAirportName").val()
        };

        var newOption = new Option(data.text, data.id, false, false);
        $('#destinationairport').append(newOption).trigger('change');
    }
});
