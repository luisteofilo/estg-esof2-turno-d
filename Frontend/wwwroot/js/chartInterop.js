window.chartInterop = {
    createChart: function(canvasId, chartType, labels, data) {
        var ctx = document.getElementById(canvasId).getContext('2d');
        var chart = new Chart(ctx, {
            type: chartType,
            data: {
                labels: labels,
                datasets: [{
                    label: 'Dataset',
                    data: data
                }]
            }
        });
        return chart;
    }
};
