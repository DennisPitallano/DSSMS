'use strict';
// Class definition

var OrderDetail = function() {
    // Private functions
    return {
        // Public functions
        init: function() {
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
        print: function(id) {
            $(`#${id}`).printThis();
        },
    };
}();

jQuery(document).ready(function() {
    OrderDetail.init();
});