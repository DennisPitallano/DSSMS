'use strict';
// Class definition

var PaymentReport = function() {
  // Private functions

  // demo initializer
  var initFilter = function() {
    $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();
    $('#kt_daterangepicker_1').daterangepicker({
      buttonClasses: ' btn',
      applyClass: 'btn-primary',
      cancelClass: 'btn-secondary'
    });
    $('#kt_daterangepicker_1').on('apply.daterangepicker', function(ev, picker) {
      console.log(picker.startDate._d);
      console.log(picker.startDate.format('YYYY-MM-DD'));
      console.log(picker.endDate);
      console.log(picker.endDate.format('YYYY-MM-DD'));
      $("#DateFrom").val(picker.startDate.format('MM-DD-YYYY'));
      $("#DateTo").val(picker.endDate.format('MM-DD-YYYY'));
      $("#spnDateFrom").text(picker.startDate.format('MMM DD, YYYY'));
      $("#spnDateTo").text(picker.endDate.format('MMM DD, YYYY'));
    });
  };

  return {
    // Public functions
    init: function() {
      initFilter();
    },
    OnBegin: function () {
      Swal.fire({
        title: "Processing in progress",
        html: "please wait...",
        allowOutsideClick: false,
        allowEscapeKey: false,
        showConfirmButton: false,
        onBeforeOpen: () => {
          Swal.showLoading();
        }
      }).then((result) => {
      });
    },
    OnSuccess: function (data, status) {
      Swal.close();
    },
    print: function() {
      $("#reportContainer").printThis();
    },
  };
}();

jQuery(document).ready(function() {
  PaymentReport.init();
});
