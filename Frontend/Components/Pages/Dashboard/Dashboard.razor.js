export class Dashboard {
    static initPieChart(selector, skillNames) {
        const skillCounts = skillNames.reduce((acc, skill) => {
            const skillLower = skill.toLowerCase(); 
            acc[skillLower] = (acc[skillLower] || 0) + 1; 
            return acc;
        }, {});

        const sortedSkills = Object.entries(skillCounts)
            .sort((a, b) => b[1] - a[1]);

        const topSkills = sortedSkills.slice(0, 9);
        const otherSkills = sortedSkills.slice(9);

        const labels = topSkills.map(skill => skill[0]);
        const counts = topSkills.map(skill => skill[1]);

        if (otherSkills.length > 0) {
            const otherCount = otherSkills.reduce((acc, skill) => acc + skill[1], 0);
            labels.push('Other');
            counts.push(otherCount);
        }

        const target = document.querySelector(selector);
        let ctx = target.getContext('2d');
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: labels.map((_, index) => Dashboard.getColor(index))
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

    static initLineChart(selector, interviewData) {
        const target = document.querySelector(selector);
        const ctx = target.getContext('2d');
        window.lineChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: interviewData.labels,
                datasets: [{
                    label: 'Interviews',
                    data: interviewData.counts,
                    fill: false,
                    borderColor: 'rgb(75, 192, 192)',
                    tension: 0.1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 10
                        }
                    }
                }
            }
        });
    }

    static updateLineChart(selector, interviewData) {
        if (window.lineChart) {
            window.lineChart.data.labels = interviewData.labels;
            window.lineChart.data.datasets[0].data = interviewData.counts;
            window.lineChart.update();
        } else {
            Dashboard.initLineChart(selector, interviewData);
        }
    }

    static getColor(index) {
        const hue = (index * 137.5) % 360;
        return `hsl(${hue}, 70%, 50%)`;
    }
}

window.Dashboard = Dashboard;
