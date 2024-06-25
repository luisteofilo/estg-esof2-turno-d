export class Dashboard {
    static initPieChart(selector, skillNames) {
        
        // Count occurrences of each skill name
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

        // Convert experience names to lowercase and count occurrences of each experience name
        const experienceCounts = experienceNames.reduce((acc, experience) => {
            const lowerCaseExperience = experience.toLowerCase();
            acc[lowerCaseExperience] = (acc[lowerCaseExperience] || 0) + 1;
            return acc;
        }, {});

        // Sort experiences by count, limit to top 5
        const sortedExperienceEntries = Object.entries(experienceCounts).sort((a, b) => b[1] - a[1]).slice(0, 5);
        const labels = sortedExperienceEntries.map(entry => entry[0]);
        const counts = sortedExperienceEntries.map(entry => entry[1]);

        // Generate a unique color for each label
        const colors = labels.map((label, index) => {
            const hue = (index * 137.5) % 360; // Use golden angle approximation for color diversity
            return `hsl(${hue}, 70%, 50%)`;
        });

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: colors,
                    label: labels
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 10 // Adjust the step size to 10
                        }
                    },
                    x: {
                        beginAtZero: true
                    }
                },
                indexAxis: 'y', // This option makes the bar chart horizontal
                plugins: {
                    legend: {
                        display: true, // Show the legend
                        labels: {
                            generateLabels: function(chart) {
                                const data = chart.data;
                                return data.labels.map((label, index) => ({
                                    text: label,
                                    fillStyle: data.datasets[0].backgroundColor[index]
                                }));
                            }
                        }
                    }
                }
            }
        });
    }
}

window.Dashboard = Dashboard;
