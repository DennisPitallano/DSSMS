"use strict";
var KTDatatablesExtensionButtons = function() {

    var initTable1 = function() {

        // begin first table
        var table = $('#kt_datatable1').DataTable({
            responsive: true,
            // Pagination settings
            dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,

            buttons: [
                'print',
                'copyHtml5',
                'excelHtml5',
                'csvHtml5',
                'pdfHtml5',
            ],
            columnDefs: [
                {
                    // hide columns by index number
                    targets: [7],
                    orderable: false
                }
            ],
        });

    };
    return {

        //main function to initiate the module
        init: function() {
            initTable1();
        },

    };

}();

jQuery(document).ready(function() {
    KTDatatablesExtensionButtons.init();
});