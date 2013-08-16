$(function () {

    toggleInputs(true);
    
    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/CustomerDetails/" + ui.item.value;    
        },
        delay: 5
    });

    $('.datepicker').datepicker();

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
                showReports(selectedValue,null,5);
            }
        });
    });


    $("#customerDropDown").change(function () {
        var customerId = $("#customerDropDown option:selected").val();
        $.ajax({
            type: 'GET',
            url: "/Customer/FilteredProjects",
            data: {
                customerId: customerId
            },
            success: function (json) {
                var sel = $("#projectDropDown");
                sel.empty();
                sel.append('<option value="null">-- Välj Projekt --</option>');
                for (var i = 0; i < json.length; i++) {
                    sel.append('<option value="' + json[i].id + '">' +json[i].name + '</option>');
                }
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

    $(document.body).on("click", "#saveReport", function () {

        $.ajax({
            type: 'POST',
            url: "/Report/SaveReport",
            data: $('#reportForm').serialize(),
            success: function (result) {
                clearForm();
                showReports(null, 1, 8);
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

function clearForm() {
    $(':input').not(':button, :submit, :reset, :hidden').val('');
}

function showReports(projectId, userId, limit) {
    $.ajax({
        type: 'GET',
        url: "/Report/List",
        data:
            {
                ProjectId: projectId,
                UserId: userId,
                Limit: limit
            },
        success: function (reports) {
            $('#reportList').html(reports);
        }
    });

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