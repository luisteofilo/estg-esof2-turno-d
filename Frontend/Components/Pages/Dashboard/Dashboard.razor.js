export class Dashboard {
    static initChart(selector, skillNames, chartType) {
        console.log("Chart initialization started");
        console.log("Received skill names:", skillNames);

        // Count occurrences of each skill name
        const skillCounts = skillNames.reduce((acc, skill) => {
            acc[skill] = (acc[skill] || 0) + 1;
            return acc;
        }, {});

        const labels = Object.keys(skillCounts);
        const counts = Object.values(skillCounts);

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        let chart = new Chart(ctx, {
            type: chartType,
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40']
                }]
            }
        });
    }
}

window.Dashboard = Dashboard;
