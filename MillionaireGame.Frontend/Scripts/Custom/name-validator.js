

$.validator.addMethod('regx', function (value, element, regexpr) {
    return regexpr.test(value);
});

$(document).ready(function () {
    $('#playerNameForm').validate({
        errorClass: 'help-block animation-slideDown', //animation can be changed here
        errorElement: 'label',
        errorPlacement: function (error, e) {
            $(error).insertAfter(e.parents('.form-group'));
        },
        highlight: function (e) {
            $(e).closest('.form-group').removeClass('has-success has-error')
                .addClass('has-error');
            $(e).parents('.form-group').closest('.help-block').remove();
        },
        success: function (e) {
            $(e).closest('.form-group').removeClass('has-success has-error');
            $(e).parents('.form-group').closest('.help-block').remove();
        },
        rules: {
            'PlayerName': {
                required: true,
                minlength: 2,
                maxlength: 30,
                regx: /^[a-zA-Z ]|[а-яА-Я]+$/
            }
        },
        messages: {
            'PlayerName': {
                required: 'Будь ласка введіть ім`я',
                minlength: 'Ім`я занадто коротке',
                maxlength: 'Ім`я занадто довге',
                regx: 'Ім`я не повино містити цифр'
            }
        }
    });
});