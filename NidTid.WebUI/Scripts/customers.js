$(function () {

    $('.datepicker').datepicker();

    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/CustomerDetails/" + ui.item.value;
            $('#customerTabs a:first').tab('show');
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
                showReports(selectedValue,null, null, null, 5);
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
        var userId = $("#UserId").val();
        $.ajax({
            type: 'POST',
            url: "/Report/SaveReport",
            data: $('#reportForm').serialize(),
            success: function (result) {
                clearForm();
                showReports(null, userId, null, null, 8);
            },
            error: function (result) {
                alert("Ett fel inträffade, försök igen.");
            }
        });
    });

    $("#filterReports").click(function () {
        var userId = $("#userDropDown").val();
        var projectId = $("#projectDropDown").val();
        var fromDate = $("#fromDate").val();
        var toDate = $("#toDate").val();
        showReports(projectId, userId, fromDate, toDate, 50);
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


function clearForm() {
    $(':input').not(':button, :submit, :reset, :hidden').val('');
}

function showReports(projectId, userId, fromDate, toDate, limit) {
    var url = "/Report/List";
    if (fromDate != null) {
        url = "/Report/SpreadsheetResult";
    }
    $.ajax({
        type: 'GET',
        url: url,
        data:
            {
                ProjectId: projectId,
                UserId: userId,
                fromDate: fromDate,
                toDate: toDate,
                Limit: limit
            },
        success: function (reports) {
            $('#reportList').html(reports);
            $("table.tablesorter").tablesorter({ widthFixed: true, sortList: [[0, 0]] })
        }
    });
    

}
/*
------------------------Menu Options---------------*/

