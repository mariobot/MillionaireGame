$fiftyHint = $('#fiftyHint');

$fiftyHint.on('click', (e) => {
    $fiftyHint.prop('disabled', true);
    //request
    $.post('/Home/FiftyPercentsHint', { questionIndex: window.questionIndex },
        (data) => {
            data = JSON.parse(data);
            refreshAnswers(data);
            //global obj that indicates whenever 50x50 hint was used
            window.isFiftyPercentsUsed = true;
        });
});

function refreshAnswers (question) {
    let nextChar = 'A';
    $btns.each((i, obj) => {
        //disables excluded answers
        if (!question.Answers[i].Title) {
            obj.disabled = true;
        }
        obj.value = nextChar + '. ' + question.Answers[i].Title;
        nextChar = String.fromCharCode(nextChar.charCodeAt() + 1);
    });
}