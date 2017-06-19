$hintButton = $("#audienceHint");

$hintButton.on("click",
    () => {
        $hintButton.prop("disabled", true);
        $.post("/Home/AudienceHint",
            {
                questionIndex: window.questionIndex
            },
            (data) => {
                var url = JSON.parse(data);
                window.open(url, "_blank");
            });
    });
