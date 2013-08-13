$(function () {

    toggleInputs(true);
    
    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/CustomerDetails/" + ui.item.value;    
        },
        delay: 5
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

});