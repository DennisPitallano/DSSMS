"use strict";
var SystemSetupView = function() {
    return {
        init: function() {
        },
        OnBeginSave: function () {
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
        OnSuccessSave: function (data, status) {
            console.log(data);
            if (data.status === 'Failed') {
                Swal.fire({
                    title: "Something went wrong!",
                    text: data.description,
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
            } else {
                Swal.close();
                Swal.fire({
                    title: "Company profile has been saved!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
            }
        },
    };

}();

jQuery(document).ready(function() {
    CourseView.init();
});