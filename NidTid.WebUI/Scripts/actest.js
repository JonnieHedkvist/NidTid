$(function () {

    $("#customerAC").autocomplete({
        source: '/Customer/FilteredCustomers',
        select: function (e, ui) {
            document.location.href = "/Customer/View/" + ui.item.value;    
        },
        delay: 5
    });

});