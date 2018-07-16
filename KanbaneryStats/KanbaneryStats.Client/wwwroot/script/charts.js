Blazor.registerFunction('drawChart', (dataItems) => {

    console.log(dataItems);

    google.charts.load('current', {
        'packages': ['corechart']
    });

    google.charts.setOnLoadCallback(() => {

        var result = dataItems.map(a => [new Date(a.movedAt), a.factor]);
        result.splice(0, 0, ['Date', 'Velocity']);
        var data = google.visualization.arrayToDataTable(result, false);

        var options = {
            title: 'Velocity over Time',
            width: 1000,
            height: 700,
            trendlines: { 0: {} }    // Draw a trendline for data series 0.
        };

        var chart = new google.visualization.ScatterChart(document.getElementById('linechart_material'));
        
        chart.draw(data, options);
    });    
    
    return true;
})
