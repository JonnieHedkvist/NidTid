$(function () {

    toggleInputs(true);
    
    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/CustomerDetails/" + ui.item.value;    
        },
        delay: 5
    });

    $("#projectList").change(function () {
        var selectedValue = $("#projectList option:selected").val();
        $.ajax({
            type: 'GET',
            url: "/Project/ProjectDetails",
            data: {
                projectId: selectedValue
            },
            success: function (prj) {
                $('#projectWrapper').html(prj);
            }
        });
    });


    $("#customerDropDown").change(function () {
        var customerId = $("#customerDropDown option:selected").val();
        alert(customerId);
        $.ajax({
            type: 'GET',
            url: "/Customer/FilteredProjects",
            data: {
                customerId: customerId
            },
            success: function (json) {
                $.each(json, function (i, value) {
                    $('#projectDropDown').append($('<option>').text(value).attr('value', value));
                });
            }
        });
    });


    $("#newProjectBtn").click(function () {
        var customerId = $("#Id").val();
        alert(customerId);
        $.ajax({
            type: 'GET',
            url: "/Project/NewProject",
            data: {
                customerId: customerId
            },
            success: function (prj) {
                $('#projectWrapper').html(prj);
            }
        });
    });

    $(document.body).on("click", "#saveProject",function () {
        
        $.ajax({
            type: 'POST',
            url: "/Project/ProjectDetails",
            data: $('#projectForm').serialize(),
            success: function (result) {
                alert(result);
            },
            error: function (result) {
                alert(result);
            }
        });
    });


    var editable = false;
    $("#btnUnlock").click(function () {
        if (editable == false) {
            toggleInputs(false);
            this.value = "Spara";
            editable = true;
        }
        else {
            this.setAttribute('type', 'submit');
            editable = false;
        }
        
    });


});

function toggleInputs(onOff) {
    $("#Name").prop('disabled', onOff);
    $("#Referens").prop('disabled', onOff);
    $("#FastBtn").prop('disabled', onOff);
    $("#KontoStr").prop('disabled', onOff);
    $("#Desription").prop('disabled', onOff);
    $("#newProjectBtn").prop('disabled', onOff);
    $("#PostNr").prop('disabled', onOff);
    $("#PostOrt").prop('disabled', onOff);
    $("#Moms").prop('disabled', onOff);
}


/*
------------------------Menu Options---------------*/

$("#menuCustomer").click(function () {
    alert("TJO!");
    $.ajax({
        type: 'GET',
        url: "/Customer/CustomerDetails",
        success: function (cstm) {
            $('#contentBody').html(cstm);
        }
    });
});