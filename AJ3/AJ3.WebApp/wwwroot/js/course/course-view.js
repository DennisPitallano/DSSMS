"use strict";
var CourseView = function() {
    var _pageLength = 10;
    var initFilter = function() {
        $('#courseCategories').selectpicker();
    };

    var initTable = function(plength) {
        // begin first table
        var table = $('#kt_datatable1').DataTable({
            responsive: true,
            pageLength: plength,
            order: [[ 0, 'desc' ]],
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
        $('#kt_datatable1').on( 'length.dt', function ( e, settings, len ) {
            console.log( 'New page length: '+len );
            _pageLength = len;
        } );

    };
    return {
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
                    $('#CategoryId').selectpicker();
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
        loadTable: function (url, container) {
            $.ajaxPrefilter(function (options) {
                options.async = true;
            });
            $.ajax({
                url: `${url}`,
                dataType: "html",
                beforeSend: function () {
                },
                complete: function () {
                    initTable(_pageLength);
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
        //main function to initiate the module
        init: function() {
            initFilter();
            initTable(_pageLength);
        },
        getAvailableCourses: function (el) {
            var selectedValue = $(el).find('option:selected').val();
            console.log(selectedValue);
            KTApp.block('#dvCourseMasterList', {
                overlayColor: '#000000',
                state: 'danger',
                message: 'Please wait...'
            });
            CourseView.loadTable(`/course/GetCoursesByCategoryId/${selectedValue}`, '#dvCourseMasterList');
            KTApp.unblock('#dvCourseMasterList');
        },
        getCourseForm: function (id) {
            console.log(id);
            KTApp.block('#courseFormModalBody .modal-body', {
                overlayColor: 'red',
                opacity: 0.1,
                state: 'primary',
                message: 'Please wait...'
            });
           
            const interBankModal = $("#courseFormModal");
            interBankModal.modal("show");
            const viewForm = `/course/GetCourseForm/${id}`;
            interBankModal.find('.modal-body').load(viewForm, function () {
                $('#CategoryId').selectpicker();
            });
            KTApp.unblock('#courseFormModalBody');
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
        OnSuccessSaveCourse: function (data, status) {
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
                    title: "Course has been saved!",
                    text: "",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary btn-lg"
                    }
                });
                if (data.status==='Success') {
                    KTApp.block('#dvCourseMasterList', {
                        overlayColor: '#000000',
                        state: 'danger',
                        message: 'Please wait...'
                    });
                    CourseView.loadTable(`/course/GetCoursesByCategoryId/${data.categoryId}`, '#dvCourseMasterList');
                    KTApp.unblock('#dvCourseMasterList');
                    $('#courseCategories').selectpicker('val', `${data.categoryId}`);
                    $('#courseCategories').selectpicker('refresh');
                }
                $("#courseFormModal").modal("hide");
            }
        },
        confirmUpdateStatus: function (id) {
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
            }).then(function(result) {
                if (result.value) {
                    $("#formCourseDelete-"+id).submit();
                } else if (result.dismiss === "cancel") {
                    Swal.fire(
                        "Cancelled",
                        "Operation cancelled :)",
                        "error"
                    )
                }
            });
        },
        OnSuccessUpdateStatus: function (data, status) {
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
                    title: "Set as inactive successful!",
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