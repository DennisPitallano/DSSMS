"use strict";

// Class definition
var StudentProfile = function () {
    // Base elements
    var avatar;
    var initAvatar = function () {
        avatar = new KTImageInput('kt_user_avatar');
    }

    return {
        // public functions
        loadForm: function (url, container) {
            $.ajaxPrefilter(function (options) {
                options.async = true;
            });
            $.ajax({
                url: `${url}`,
                dataType: "html",
                beforeSend: function () {
                },
                complete: function () {
                    $(".btn").tooltip();
                },
                success: function (html) {
                    $(container).html(html);
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    Swal.fire(
                        {
                            title: `Error Loading Data!${errorThrown}`,
                            type: "error",
                            confirmButtonClass: "btn btn-xlg btn-success btn-raised legitRipple",
                            confirmButtonText: "OK",
                            allowOutsideClick: false
                        }
                    );
                }
            });
        },
        init: function () {
            initAvatar();
        },
        getOrderPaymentForm: function (id, sid, balance, type) {
            console.log(id);
            KTApp.block('#orderPaymentFormModalBody', {
                overlayColor: 'red',
                opacity: 0.1,
                state: 'primary',
                message: 'Please wait...'
            });
            if (type === 'Update') {
                balance = $("#hdnTotalPayableAmount").val();
                console.log(balance);
            }
            const modal = $("#orderPaymentFormModal");
            modal.modal("show");
            const viewForm = `/students/GetOrderPaymentForm/${id}/${sid}/${balance}/${type}`;
            modal.find('.modal-body').load(viewForm, function () {

            });
            KTApp.unblock('#orderPaymentFormModalBody');
        },
        computeBalance: function (el, pamount) {
            const payment = $(el);
            var totalAmount = $("#hdnTotalPayableAmount").val();
            console.log(payment[0].value);
            if (payment[0].value) {
                var balance = (parseFloat(totalAmount) + parseFloat(pamount)) - (parseFloat(payment[0].value));
                if (balance < 0) {
                    $("#btnAddPayment").attr("disabled", "disabled");
                } else {
                    $("#btnAddPayment").removeAttr("disabled");
                }
                $("#txtBalance").val(balance);
            } else {
                $("#btnAddPayment").attr("disabled", "disabled");
                $("#txtBalance").val((parseFloat(totalAmount) + parseFloat(pamount)));
            }
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
        OnSuccessSavePayment: function (data, status) {
            console.log(data);
            if (data.status === 'Success') {
                Swal.close();
                Swal.fire({
                    title: "Payment has been saved!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                $("#orderPaymentFormModal").modal("hide");
                StudentProfile.loadForm(`/students/GetStudentCourseDetail/${data.studentId}`, '#kt_apps_contacts_view_tab_3');
                StudentProfile.loadForm(`/students/GetOrderPaymentHistoryDetail/${data.orderId}`, `#collapseOne6_${data.orderId}`);
            } else {
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
            }
        },
        OnSuccessUpdateCourseStatus: function (data, status) {
            console.log(data);
            if (data.status === 200) {
                Swal.close();
                Swal.fire({
                    title: data.responseJSON.description,
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                StudentProfile.loadForm(`/students/GetStudentCourseDetail/${data.responseJSON.studentId}`, '#kt_apps_contacts_view_tab_3');
            } else {
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
            }
        },
        confirmUpdateStatus: function (id, type) {
            Swal.fire({
                title: "Are you sure?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, update it!",
                cancelButtonText: "No, cancel!",
                reverseButtons: true,
                buttonsStyling: false,
                customClass: {
                    confirmButton: "btn font-weight-bold btn-primary",
                    cancelButton: "btn font-weight-bold btn-default"
                }
            }).then(function (result) {
                if (result.value) {
                    if (type === 1) {
                        $("#formStudentCourseUpdateInProgress-" + id).submit();
                    } else {
                        $("#formStudentCourseUpdateGraduated-" + id).submit();
                    }
                } else if (result.dismiss === "cancel") {
                    Swal.fire(
                        "Cancelled",
                        "Operation cancelled :)",
                        "error"
                    )
                }
            });
        },
        OnSuccessUpdateStudent: function (data, status) {
            console.log(data);
            if (data.status === 'Failed') {
                Swal.fire({
                    title: "Something went wrong!",
                    text: "Failed to update info.",
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
                    title: "Student information has been saved!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                const sid = $("#hdnSId").val();
                StudentProfile.loadForm(`/students/GetMiniProfile/${sid}`, '#dvMiniProfile');
            }
        },
        OnSuccessUpdateLicenseNote: function (data, status) {
            console.log(data);
            if (data.status === 'Failed') {
                Swal.fire({
                    title: "Something went wrong!",
                    text: "Failed to update info.",
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
            } else {
                Swal.close();
            }
        },
        submitForm: function (el, id) {
            console.log(el);
            var isChecked = $(el).is(":checked");
            // var origAction= $("#frmSN-" + id).attr('action');
            //  var updatedAction = `${origAction}&isAdded=${isChecked}`;
            // $("#frmSN-" + id).attr('action', updatedAction);
            console.log(isChecked);
            $("#frmSN-" + id).submit();
            // $("#frmSN-" + id).attr('action', origAction);
        },
        confirmUpdateStudentStatus: function () {
            Swal.fire({
                title: "Are you sure?",
                icon: "warning",
                text: "Update Student Status",
                showCancelButton: true,
                confirmButtonText: "Yes, update it!",
                cancelButtonText: "No, cancel!",
                reverseButtons: true,
                buttonsStyling: false,
                customClass: {
                    confirmButton: "btn font-weight-bold btn-primary",
                    cancelButton: "btn font-weight-bold btn-default"
                }
            }).then(function (result) {
                if (result.value) {
                    $("#formUStudentUpdateStatus").submit();
                } else if (result.dismiss === "cancel") {
                    Swal.fire(
                        "Cancelled",
                        "Operation cancelled :)",
                        "error"
                    )
                }
            });
        },
        getCancelCourseForm: function (id, sid, orid, amt) {
            console.log(id);
            KTApp.block('#cancelCourseFormModalBody .modal-body', {
                overlayColor: 'red',
                opacity: 0.1,
                state: 'primary',
                message: 'Please wait...'
            });

            const interBankModal = $("#cancelCourseFormModal");
            interBankModal.modal("show");
            const viewForm = `/students/GetCancelCourseForm/${id}/${sid}/${orid}/${amt}`;
            interBankModal.find('.modal-body').load(viewForm, function () {
                /*$('#CategoryId').selectpicker();*/
            });
            KTApp.unblock('#courseFormModalBody');
        },
        availRefund: function (el, amt) {
            const isCheck = $(el).is(":checked");
            console.log(isCheck);
            if (isCheck) {
                $("#RefundedAmount").val(amt);
                $("#RefundedAmount").removeAttr("disabled");
                //$("#AdjustmentAmount").val(amt);
                //$("#AdjustmentAmount").removeAttr("disabled");
            } else {
                $("#RefundedAmount").val(0);
                $("#RefundedAmount").attr("disabled", "disabled");
                //$("#AdjustmentAmount").val(0);
                //$("#AdjustmentAmount").attr("disabled", "disabled");
            }
            //}
        },
        OnSuccessCancelCourse: function (data, status) {
            console.log(data);
            if (data.status === 'Success') {
                Swal.close();
                Swal.fire({
                    title: "Cancel successful!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                $("#cancelCourseFormModal").modal("hide");
                StudentProfile.loadForm(`/students/GetStudentCourseDetail/${data.studentId}`, '#kt_apps_contacts_view_tab_3');
            } else {
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
            }
        },
        getChangeCourseForm: function (id, sid, orid, amt,catid,curcid) {
            console.log(id);
            KTApp.block('#changeCourseFormModalBody .modal-body', {
                overlayColor: 'red',
                opacity: 0.1,
                state: 'primary',
                message: 'Please wait...'
            });

            const interBankModal = $("#changeCourseFormModal");
            interBankModal.modal("show");
            const viewForm = `/students/GetChangeCourseForm/${id}/${sid}/${orid}/${amt}/${catid}/${curcid}`;
            interBankModal.find('.modal-body').load(viewForm, function () {
                $('#ToCourseId').selectpicker();
            });
            KTApp.unblock('#changeCourseFormModalBody');
        },
        OnSuccessChangeCourse: function (data, status) {
            console.log(data);
            if (data.status === 'Success') {
                Swal.close();
                Swal.fire({
                    title: "Change course successful!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                $("#changeCourseFormModal").modal("hide");
                StudentProfile.loadForm(`/students/GetStudentCourseDetail/${data.studentId}`, '#kt_apps_contacts_view_tab_3');
            } else {
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
            }
        },
    };
}();

jQuery(document).ready(function () {
    StudentProfile.init();
});