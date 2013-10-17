$(function () {
    var activeProject;

    $('#customerTabs a:first').tab('show');
    $('#userTabs a:first').tab('show');

    $('.datepicker').datepicker();

    $("#userAC").autocomplete({
        source: '/User/FilteredUsers',
        select: function (e, ui) {
            document.location.href = "/User/Index/" + ui.item.value;
        },
        delay: 5
    });


    /********************************************
    ----------->> Customers <<-------------
    */

    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/CustomerDetails/" + ui.item.value;
        },
        delay: 5
    });

    $(document.body).on("click", "#saveCustomer",function () {
        $.ajax({
            type: 'POST',
            url: "/Customer/CustomerDetails",
            data: $('#customerForm').serialize(),
            success: function (result) {
                $('#customerWrapper').html(result);
            },
            error: function (result) {
                alert("Ett fel inträffade och kunduppgifterna kunde inte sparas. Försök igen.");
            }
        });
    });

    $(document.body).on("click", "#newCustomer", function () {
        $.ajax({
            type: 'GET',
            url: "/Customer/Create",
            success: function (result) {
                $('#customerTabs a:first').tab('show');
                $('#customerTab').html(result);
            },
            error: function (result) {
                alert("Ett fel inträffade. Försök igen.");
            }
        });
    });

    $(document.body).on("click", "#btnDeleteCustomer", function () {
        var customerId = $(this).attr("data-id");
        var customerName = $(this).attr("data-name");
        bootbox.confirm("Är du säker på att du vill ta bort " + customerName +"? Detta kan inte ångras!", function (result) {
            if (result == true) {
                deleteFromDB(customerId, "/Customer/DeleteCustomer", "/Customer/CustomerDetails")
            }
        });
    });

    /********************************************
        ----------->> Users <<-------------
    */

    $(document.body).on("click", "#saveUser", function () {
        $.ajax({
            type: 'POST',
            url: "/User/SaveUser",
            data: $('#userForm').serialize(),
            success: function (result) {
                $('#userWrapper').html(result);
            },
            error: function (result) {
                alert("Ett fel inträffade och uppgifterna kunde inte sparas. Försök igen.");
            }
        });
    });

    $(document.body).on("click", "#newUser", function () {
        $.ajax({
            type: 'GET',
            url: "/User/Create",
            success: function (result) {
                $('#userTabs a:first').tab('show');
                $('#userTab').html(result);
            },
            error: function (result) {
                alert("Ett fel inträffade. Försök igen.");
            }
        });
    });

    $(document.body).on("click", "#btnDeleteUser", function () {
        var userId = $(this).attr("data-id");
        var userName = $(this).attr("data-name");
        bootbox.confirm("Är du säker på att du vill ta bort " + userName + "? Detta kan inte ångras!", function (result) {
            if (result == true) {
                deleteFromDB(userId, "/User/DeleteUser", "/User/index")
            }
        });
    });


    /********************************************
        ----------->> Vehicles <<-------------
    */
    $(document.body).on("click", "#saveMeterPost", function () {
        $.ajax({
            type: 'POST',
            url: "/MeterPost/SaveMeterPost",
            data: $('#meterpostForm').serialize(),
            success: function (result) {
                updateVehicles();
                clearForm();
            },
            error: function (result) {
                alert("Ett fel inträffade och uppgifterna kunde inte sparas. Försök igen.");
            }
        });
    });

    $(document.body).on("click", "#deleteMeterPost", function () {
        var meterPostId = $(this).attr("data-id");
        bootbox.confirm("Vill du permanent ta bort denna post?", function (result) {
            if (result == true) {
                deleteFromDB(meterPostId, "/MeterPost/DeletePost", "/Vehicle/Index")
            }
        });
    });
    
    $(document.body).on("click", "#createVehicle", function () {
        $.ajax({
            type: 'POST',
            url: "/Vehicle/Create",
            data: $('#vehicleForm').serialize(),
            success: function (result) {
                alert(result);
                $('#newVehicleModal').modal('hide');
                updateVehicles();
            },
            error: function (result) {
                alert("Ett fel inträffade och uppgifterna kunde inte sparas. Försök igen.");
            }
        });
    });

    $(document.body).on("click", ".meterPostTable", function () {
        var vehicleId = $(this).attr("data-vehicleId");
        $.ajax({
            type: 'GET',
            url: "/MeterPost/TableByVehicle",
            data:{
                vehicleId: vehicleId
            },
            success: function (result) {
                $('#meterPostTable').html(result);
                $('#meterPostModal').modal('show');
            }
        });
    });
    /********************************************
        ----------->> Projects <<-------------
    */

    $(document.body).on("click", "#saveProject", function () {
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

    $(document.body).on("click", "#newProject", function () {
        var customerId = $(this).attr("data-customerId");
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

    $(".project-link").click(function () {
        var selectedValue = $(this).attr("data-id");
        $.ajax({
            type: 'GET',
            url: "/Project/ProjectDetails",
            data: {
                projectId: selectedValue
            },
            success: function (prj) {
                $('#projectWrapper').html(prj);
                activeProject = selectedValue;
                showReports(selectedValue, null, null, null, 10, "List");
            }
        });
    });

    $(document.body).on("change", "#projectActiveDiv .check-box", function () {
        var active = $(this).prop('checked');
        $.ajax({
            type: 'POST',
            url: "/Project/ToggleActive",
            data: {
                active: active,
                projectId: activeProject
            },
            success: function (msg) {

            }
        });
    });


    /********************************************
    ----------->> Reports <<-------------
    */

    $(document.body).on("change", ".customerDropDown", function () {
        var customerId = $(this).val();
        $.ajax({
            type: 'GET',
            url: "/Customer/FilteredProjects",
            data: {
                customerId: customerId
            },
            success: function (json) {
                var sel = $(".projectDropDown");
                sel.empty();
                sel.append('<option value="null">-- Välj Projekt --</option>');
                for (var i = 0; i < json.length; i++) {
                    sel.append('<option value="' + json[i].id + '">' + json[i].name + '</option>');
                }
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
                showReports(null, userId, null, null, 8, "List");
            },
            error: function (result) {
                alert(result);
            }
        });
    });

    $(document.body).on("change", ".spreadCustomerDropDown", function () {
        var customerId = $(this).val();
        $.ajax({
            type: 'GET',
            url: "/Customer/FilteredProjectsAll",
            data: {
                customerId: customerId
            },
            success: function (json) {
                var sel = $(".spreadProjectDropDown");
                sel.empty();
                sel.append('<option value="null">-- Välj Projekt --</option>');
                for (var i = 0; i < json.length; i++) {
                    sel.append('<option value="' + json[i].id + '">' + json[i].name + '</option>');
                }
            }
        });
    });

    $(document.body).on("click", "#deleteReport", function () {
        var reportId = $(this).attr("data-id");
        bootbox.confirm("Vill du permanent ta bort denna rapport?", function (result) {
            if (result == true) {
                deleteReport(reportId);
            }
        });
    });

    $(document.body).on("click", "#submitEditedReport", function () {
        var userId = $("#UserId").val();
        $.ajax({
            type: 'POST',
            url: "/Report/SaveReport",
            data: $('#editReportForm').serialize(),
            success: function (result) {
                $('#editReportModal').modal('hide');
                showReports(null, userId, null, null, 8, "List");
            },
            error: function (result) {
                alert("Ett fel inträffade, försök igen.");
            }
        });
    });

    $(document.body).on("click", "#editReport", function (e) {
        e.preventDefault();
        var reportId = $(this).attr("data-id");
        $.ajax({
            type: 'GET',
            url: "/Report/EditReportModal",
            data: {
                reportId: reportId
            },
            success: function (result) {
                $('#editReportModal').html(result);
                $('#editReportModal').modal('show');
                $('.datepicker').datepicker();
            }
        });
    });

    $(document.body).on("change", "#nrOfReportsDropDown", function () {
        var value = $(this).val();
        showReports(activeProject, null, null, null, value, "List");
    });

    $("#filterReports").click(function () {
        var userId = $("#userDropDown").val();
        var projectId = $(".spreadProjectDropDown").val();
        var fromDate = $("#fromDate").val();
        var toDate = $("#toDate").val();
        showReports(projectId, userId, fromDate, toDate, null, "SpreadsheetResult");
    });

});



//------------>> Functions <<--------------

function clearForm() {
    $(':input').not(':button, :submit, :reset, :hidden').val('');
}

function updateVehicles() {
    $.ajax({
        type: 'GET',
        url: "/Vehicle/List",
        data:
            {
            },
        success: function (vehicles) {
            $('#vehicleDiv').html(vehicles);
        }
    });
}

function deleteReport(id) {
    var userId = $("#UserId").val();
    $.ajax({
        type: 'POST',
        url: "/Report/DeleteReport",
        data:
            {
                id: id
            },
        success: function () {
            showReports(null, userId, null, null, 8, "List");
        }
    });
}

function deleteFromDB(id, action, returnURL) {
    $.ajax({
        type: 'POST',
        url: action,
        data:
            {
                id: id
            },
        success: function (message) {
            alert(message);
            document.location.href = returnURL;
        }
    });
}

function showReports(projectId, userId, fromDate, toDate, limit, url) {
    $.ajax({
        type: 'GET',
        url: "/Report/" + url,
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

