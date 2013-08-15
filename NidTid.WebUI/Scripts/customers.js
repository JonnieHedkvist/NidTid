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
    $("#OrgNr").prop('disabled', onOff);
    $("#Adress").prop('disabled', onOff);
    $("#Phone1").prop('disabled', onOff);
    $("#Phone2").prop('disabled', onOff);
    $("#Email").prop('disabled', onOff);
    $("#PostNr").prop('disabled', onOff);
    $("#PostOrt").prop('disabled', onOff);
    $("#Moms").prop('disabled', onOff);
}