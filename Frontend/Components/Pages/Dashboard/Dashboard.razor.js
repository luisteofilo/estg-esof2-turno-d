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

    static initDoughnutChart(selector, experienceNames) {
        const experienceCounts = {};
        experienceNames.forEach(name => {
            const lowerCaseExperience = name.toLowerCase();
            experienceCounts[lowerCaseExperience] = (experienceCounts[lowerCaseExperience] || 0) + 1;
        });

        const sortedExperienceEntries = Object.entries(experienceCounts)
            .sort((a, b) => b[1] - a[1])
            .slice(0, 5);

        const labels = sortedExperienceEntries.map(entry => entry[0]);
        const counts = sortedExperienceEntries.map(entry => entry[1]);

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: labels.map((label, index) => Dashboard.getColor(index)),
                    hoverBackgroundColor: labels.map((label, index) => Dashboard.getColor(index))
                }]
            },
            options: {
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

window.Dashboard = Dashboard;
