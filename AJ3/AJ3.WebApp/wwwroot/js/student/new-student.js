"use strict";

// Class Definition
var KTAddStudent = function () {
	// Private Variables
	var _wizardEl;
	var _formEl;
	var _wizardObj;
	var _avatar;
	var _validations = [];
	var arrows;


	if (KTUtil.isRTL()) {
		arrows = {
			leftArrow: '<i class="la la-angle-right"></i>',
			rightArrow: '<i class="la la-angle-left"></i>'
		}
	} else {
		arrows = {
			leftArrow: '<i class="la la-angle-left"></i>',
			rightArrow: '<i class="la la-angle-right"></i>'
		}
	}
	// Private Functions
	var _initWizard = function () {
		// Initialize form wizard
		_wizardObj = new KTWizard(_wizardEl, {
			startStep: 1, // initial active step number
			clickableSteps: false  // allow step clicking
		});

		// Validation before going to next page
		_wizardObj.on('change', function (wizard) {
			if (wizard.getStep() > wizard.getNewStep()) {
				return; // Skip if stepped back
			}

			// Validate form before change wizard step
			var validator = _validations[wizard.getStep() - 1]; // get validator for currnt step

			if (validator) {
				validator.validate().then(function (status) {
					if (status == 'Valid') {
						wizard.goTo(wizard.getNewStep());

						KTUtil.scrollTop();
					} else {
						Swal.fire({
							text: "Sorry, looks like there are some errors detected, please try again.",
							icon: "error",
							buttonsStyling: false,
							confirmButtonText: "Ok, got it!",
							customClass: {
								confirmButton: "btn font-weight-bold btn-light"
							}
						}).then(function () {
							KTUtil.scrollTop();
						});
					}
				});
			}

			return false;  // Do not change wizard step, further action will be handled by he validator
		});

		// Change event
		_wizardObj.on('changed', function (wizard) {
			KTUtil.scrollTop();
		});

		// Submit event
		_wizardObj.on('submit', function (wizard) {
			Swal.fire({
				text: "All is good! Please confirm the form submission.",
				icon: "success",
				showCancelButton: true,
				buttonsStyling: false,
				confirmButtonText: "Yes, submit!",
				cancelButtonText: "No, cancel",
				customClass: {
					confirmButton: "btn font-weight-bold btn-primary",
					cancelButton: "btn font-weight-bold btn-default"
				}
			}).then(function (result) {
				if (result.value) {
					_formEl.submit(); // Submit form
				} else if (result.dismiss === 'cancel') {
					Swal.fire({
						text: "Your form has not been submitted!.",
						icon: "error",
						buttonsStyling: false,
						confirmButtonText: "Ok, got it!",
						customClass: {
							confirmButton: "btn font-weight-bold btn-primary",
						}
					});
				}
			});
		});
	}

	var _initValidations = function () {
		// Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
		
		const minDate = new Date();
		const minYear = minDate.getFullYear()-100;
		minDate.setFullYear(minYear);

		const maxDate = new Date();
		const maxYear = maxDate.getFullYear()-16;
		maxDate.setFullYear(maxYear);
		// Validation Rules For Step 1
		_validations.push(FormValidation.formValidation(
			_formEl,
			{
				fields: {
					FirstName: {
						validators: {
							notEmpty: {
								message: 'First Name is required'
							}
						}
					},
					LastName: {
						validators: {
							notEmpty: {
								message: 'Last Name is required'
							}
						}
					},
					Address: {
						validators: {
							notEmpty: {
								message: 'Address is required'
							}
						}
					},
					PhoneNumber: {
						validators: {
							notEmpty: {
								message: 'Phone is required'
							},
							Phone: {
								country: 'PH',
								message: 'The value is not a valid phone number. (e.g 09171234567)'
							}
						}
					},
					Email: {
						validators: {
							emailAddress: {
								message: 'The value is not a valid email address'
							}
						}
					},
					HeightCm: {
						validators: {
							notEmpty: {
								message: 'Height is required'
							},
							numeric: {
								message: 'The value is not a valid',
								decimalSeparator: '.'
							}
						}
					},
					Weight: {
						validators: {
							notEmpty: {
								message: 'Weight is required'
							},
							numeric: {
								message: 'The value is not a valid',
								decimalSeparator: '.'
							}
						}
					},
					DateOfBirth: {
						validators: {
							notEmpty: {
								message: 'Date of birth is required'
							},
							date: {
								format: 'MM/DD/YYYY',
								message: 'The date is not valid birthdate for an applicant!',
								// min and max options can be strings or Date objects
								min: minDate,
								max: maxDate
							}
						}
					}
				},
				plugins: {
					trigger: new FormValidation.plugins.Trigger(),
					// Bootstrap Framework Integration
					bootstrap: new FormValidation.plugins.Bootstrap({
						//eleInvalidClass: '',
						eleValidClass: '',
					})
				}
			}
		));

		_validations.push(FormValidation.formValidation(
			_formEl,
			{
				fields: {
					address1: {
						validators: {
							notEmpty: {
								message: 'Address is required'
							}
						}
					},
					postcode: {
						validators: {
							notEmpty: {
								message: 'Postcode is required'
							}
						}
					},
					city: {
						validators: {
							notEmpty: {
								message: 'City is required'
							}
						}
					},
					state: {
						validators: {
							notEmpty: {
								message: 'state is required'
							}
						}
					},
					country: {
						validators: {
							notEmpty: {
								message: 'Country is required'
							}
						}
					},
				},
				plugins: {
					trigger: new FormValidation.plugins.Trigger(),
					// Bootstrap Framework Integration
					bootstrap: new FormValidation.plugins.Bootstrap({
						//eleInvalidClass: '',
						eleValidClass: '',
					})
				}
			}
		));

		_validations.push(FormValidation.formValidation(
			_formEl,
			{
				fields: {
					// Step 2
					PayAmountText: {
						validators: {
							notEmpty: {
								message: 'Payment Amount is required'
							}
						}
					},
					OrNumber: {
						validators: {
							notEmpty: {
								message: 'OR Number is required'
							}
						}
					}
				},
				plugins: {
					trigger: new FormValidation.plugins.Trigger(),
					// Bootstrap Framework Integration
					bootstrap: new FormValidation.plugins.Bootstrap({
						//eleInvalidClass: '',
						eleValidClass: '',
					})
				}
			}
		));
	}

	var _initAvatar = function () {
		_avatar = new KTImageInput('kt_user_add_avatar');
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
			// input group layout 
			$('#DateOfBirth').datepicker({
				rtl: KTUtil.isRTL(),
				todayHighlight: true,
				orientation: "bottom left",
				templates: arrows
			});

			_wizardEl = KTUtil.getById('kt_wizard');
			_formEl = KTUtil.getById('kt_form');

			_initWizard();
			_initValidations();
			_initAvatar();
		},
		getSelectedCourse: function(el) {
			const selected = $(el);
			const data = $(el).data();
			//set values 
			$("#CourseId").val(data.courseid);
			$("#TotalAmount").val(data.amount);
			$("#TotalDiscount").val(data.discount);
			$("#TotalHours").val(data.hours);

			$("#iv-desc").text(data.desc);
			$("#iv-amount").text(data.displayamount);
			$("#iv-totalamount").text(data.displaydiscountedprice);
			$("#iv-unitprice").text(data.displayamount);
			$("#iv-discount").text(data.displaydiscount);
			$("#iv-hours").text(data.hours);
			const ivaddress = $("#Address").val();
			const ivname = `${$("#LastName").val()}, ${$("#FirstName").val()} ${$("#MiddleName").val()}`;
			$("#iv-address").text(ivaddress);
			$("#iv-to").text(ivname);

			//set summary details
			$("#sd-address").text(ivaddress);

			$("#sd-coursedesc").text(data.desc);
			$("#sd-email").text($("#Email").val());
			$("#sd-fullname").text(ivname);
			var radioValue = $("input[name='Gender']:checked").val();
			if (radioValue === "False") {
				$("#sd-gender").text("Female");
            } else {
				$("#sd-gender").text("Male");
            }
			$("#sd-height").text(`${$("#HeightCm").val()} cm`);
			$("#sd-hours").text(data.hoursdisplay);
			$("#sd-nationality").text($("#Nationality").val());
			$("#sd-phone").text($("#PhoneNumber").val());
			$("#sd-totalamount").text(data.displayamount);
			$("#sd-totaldiscount").text(data.displaydiscount);
			$("#sd-weight").text(`${$("#Weight").val()} kg`);
		},
		printInvoice: function() {
			$(".invoice").printThis();
		},
		computeBalance: function(el) {
			const payment = $(el);
			var totalAmount = $("#TotalAmount").val();
			var totalDiscount= $("#TotalDiscount").val();
			var balance = parseFloat(totalAmount)-(parseFloat(payment[0].value)+parseFloat(totalDiscount));
			$("#ivBalance").val(new Intl.NumberFormat('en-PH', { style: 'currency', currency: 'PHP' }).format(balance));

			$("#sd-balance").text($("#ivBalance").val());
			$("#PayAmount").val(payment[0].value);
		},
		formatNumber:function(el) {
			const payment = $(el);
			var value = parseFloat(payment[0].value);
			var formattedValue = new Intl.NumberFormat('en-PH', { style: 'currency', currency: 'PHP' }).format(value);
			$(el).val(formattedValue);
			$("#sd-totalpayment").text(formattedValue);
		},
		onFocus: function(el) {
			const payment = $(el);
			var result = payment[0].value.replace(/[^a-zA-Z0-9 .]/g, '');
			console.log(result);
			$(el).val(result);
		},
        getAvailableCourses: function (el) {
	        const selected = $(el)[0];
	        var selectedValue= $(el).find('option:selected').val();
	        console.log(selectedValue);
	        KTApp.block('#dvAvailableCourses', {
		        overlayColor: '#000000',
		        state: 'danger',
		        message: 'Please wait...'
	        });
	        this.loadForm(`/students/GetAvailableCoursesByCategoryId/${selectedValue}`, '#dvAvailableCourses');
	        $("#orderPaymentFormModal").modal("toggle");
	        KTApp.unblock('#dvAvailableCourses');
        }
	};
}();

jQuery(document).ready(function () {
	KTAddStudent.init();
});
