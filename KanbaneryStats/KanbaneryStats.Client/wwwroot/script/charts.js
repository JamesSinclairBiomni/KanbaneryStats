Blazor.registerFunction('drawChart', (dataItems) => {

    google.charts.load('current', {
        'packages': ['line']
    });

    google.charts.setOnLoadCallback(() => {

        var result = dataItems.map(a => [new Date(a.movedAt), a.averageFactor]);
        result.splice(0, 0, ['Date', 'Average Velocity']);
        var data = google.visualization.arrayToDataTable(result, false);

        var options = {
            chart: {
                title: 'Velocity',
                subtitle: 'graph of the average velocity over time'
            },
            curveType: 'function',
            width: 900,
            height: 500
        };

        var chart = new google.charts.Line(document.getElementById('linechart_material'));

        chart.draw(data, google.charts.Line.convertOptions(options));
    });    
    
    return true;
})
