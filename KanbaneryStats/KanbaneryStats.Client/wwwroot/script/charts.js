Blazor.registerFunction('drawChart', (dataItems) => {

    google.charts.load('current', {
        'packages': ['line']
    });

    google.charts.setOnLoadCallback(() => {

        var result = dataItems.map(a => [new Date(a.movedAt), a.factor]);
        result.splice(0, 0, ['Date', 'Velocity']);
        var data = google.visualization.arrayToDataTable(result, false);

        var options = {
            chart: {
                title: 'Velocity over time',
                subtitle: 'actual / estimate'
            },
            width: 900,
            height: 500
        };

        var chart = new google.charts.Line(document.getElementById('linechart_material'));

        chart.draw(data, google.charts.Line.convertOptions(options));
    });    
    
    return true;
})
