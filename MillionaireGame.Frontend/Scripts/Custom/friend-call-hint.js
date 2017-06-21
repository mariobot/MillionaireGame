$friendCallHint = $("#friendCallHint");
$hintWindow = $("#friendCallHintWindow");
$emailField = $("#friendCallHintEmail");

$friendCallHint.on("click",
    () => {
        $hintWindow.modal("show");
    });

$("#friendCallHintBtnClose").on("click",
    () => {
        $hintWindow.modal("hide");
    });

$("#friendCallHintBtnSend").on("click",
    () => {
        var email = $emailField.val();
        if (!isEmail(email)) {
            $emailField.addClass("email-validation-error");
            $emailField.focus();
        } else {
            $friendCallHint.prop("disabled", true);
            $.post("/Home/FriendCallHint",
                {
                    questionIndex: window.questionIndex,
                    recipient: email
                });
            $hintWindow.modal("hide");
        }
    });

function isEmail(email) {
    const regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
