

$btns = $('.btn-answer');
$btns.on('click', (e) => {
    let clickedBtn = e.target;
    let answer = clickedBtn.value.substring(3);
    $.post('/Home/PlayerGame', { PlayerAnswer: answer, QuestionIndex: window.questionIndex },
        (data) => {
            data = JSON.parse(data);
            if (data.EndOfGame) {
                $.redirect('/Home/GameResult', {'step': data.QuestionIndex}, 'GET', '', true);
            }
            window.questionIndex = data.QuestionIndex;

            $('#' + questionIndex).removeClass('step-selected');
            $('#' + (questionIndex + 1)).addClass('step-selected');

            $('#questionP').text(data.Question.Title);
            //magic goes here
            var nextChar = 'A';
            $btns.each((i, obj) => {
                obj.value = nextChar + '. ' + data.Question.Answers[i].Title;
                nextChar = String.fromCharCode(nextChar.charCodeAt() + 1);
            });
            debugger;
            //restors buttons state if 50x50 hint was used
            if (window.isFiftyPercentsUsed) {
                restoreBtns();
                window.isFiftyPercentsUsed = false;
            }
        }, 'Json');
});

//enables buttons
function restoreBtns() {
    debugger;
    $('.btn-answer').each((i, btn) => {
        btn.disabled = false;
    });
}