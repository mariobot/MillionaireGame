$audienceHint = $('#audienceHint');

$audienceHint.on('click',
    () => {
        $audienceHint.prop('disabled', true);
        $.post('/Home/AudienceHint',
            {
                questionIndex: window.questionIndex
            },
            (data) => {
                var url = JSON.parse(data);
                window.open(url, '_blank');
            });
    });
