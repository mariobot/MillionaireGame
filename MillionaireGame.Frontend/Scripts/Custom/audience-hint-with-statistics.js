$audienceHint = $('#audienceHint');

google.load('visualization', '1', { 'packages': ['corechart'] });

$audienceHint.on('click',
    () => {
        $audienceHint.prop('disabled', true);
        $('#audienceHintWindow').modal('show');

        $.post('/Home/AudienceHintWithStatistic',
            { questionIndex: window.questionIndex },
            (responseData) => {
                google.setOnLoadCallback(drawChart(responseData));
            });
    });

$('#audienceHintChartBtnClose').on('click',
    () => {
        $('#audienceHintWindow').modal('hide');
    });

function drawChart(responseData) {
    var result = JSON.parse(responseData);
    var data = google.visualization.arrayToDataTable([
        ['Питання', '%'],
        ['A', result[0].AudienceRate],
        ['B', result[1].AudienceRate],
        ['C', result[2].AudienceRate],
        ['D', result[3].AudienceRate]
    ]);
    var options = {
        hAxis: { title: 'Підтримка залу, %' },
        vAxis: { title: 'Відповідь' }
    };
    var chart = new google.visualization.BarChart(document.getElementById('audienceHintChart'));
    chart.draw(data, options);
}