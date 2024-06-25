export class Dashboard {
    static initPieChart(selector, skillNames) {
        const skillCounts = skillNames.reduce((acc, skill) => {
            acc[skill] = (acc[skill] || 0) + 1;
            return acc;
        }, {});

        const labels = Object.keys(skillCounts);
        const counts = Object.values(skillCounts);

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40']
                }]
            }
        });
    }

    static initBarChart(selector, experienceNames) {
        const experienceCounts = {};
        experienceNames.forEach(name => {
            experienceCounts[name] = (experienceCounts[name] || 0) + 1;
        });

        const labels = Object.keys(experienceCounts);
        const datasets = labels.map((label, index) => ({
            label: label,
            data: [experienceCounts[label]],
            backgroundColor: Dashboard.getColor(index)
        }));

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Experiences'],
                datasets: datasets
            },
            options: {
                scales: {
                    x: {
                        beginAtZero: true
                    },
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1
                        }
                    }
                },
                plugins: {
                    legend: {
                        display: true
                    }
                }
            }
        });
    }

    static getColor(index) {
        const hue = (index * 137.5) % 360;
        return `hsl(${hue}, 70%, 50%)`;
    }
}
