function createProjectCharts(projectProgressData) {
    var chartContainers = document.querySelectorAll('.chart-container');
    chartContainers.forEach(function (chartContainer) {
        var projectId = chartContainer.dataset.projectId;
        var progressData = projectProgressData[projectId];
        var values = Object.values(progressData);
        var labels = Object.keys(progressData);

        var chartData = [{
            values: values,
            labels: labels,
            type: 'pie',
            textinfo: 'none',
            //textinfo: 'label',
            textposition: 'inside',
            hoverinfo: 'label+percent',
            //texttemplate: "%{label}",
            marker: {
                colors: [
                    'rgb(255, 0, 0)',     // Not Started: Red
                    'rgb(255, 193, 7)',   // In Progress: Yellow
                    'rgb(0, 128, 0)'      // Completed: Green
                ]
            }
        }];

        var layout = {
            margin: { l: 3, r: 3, t: 3, b: 3 },
            showlegend: false
        };

        Plotly.newPlot(chartContainer, chartData, layout);
    });
}