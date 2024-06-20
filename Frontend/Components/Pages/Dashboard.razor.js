export class Dashboard {
    static initCharts(sel, skillNames) {
        
        console.log("entrou no init");
        console.log("Received skill names:", skillNames);
        
            // Conta as ocorrências de cada nome de habilidade
            const skillCounts = skillNames.reduce((acc, skill) => {
                acc[skill] = (acc[skill] || 0) + 1;
                return acc;
            }, {});

            const labels = Object.keys(skillCounts);
            const counts = Object.values(skillCounts);

            const target = document.querySelector(sel);
            let ctx1 = target.getContext('2d');
            let commonSkills = new Chart(ctx1, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        data: counts,
                        backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40']
                    }]
                }
            });

        const target1 = document.querySelector("#applicationSourceChart");
        let ctx2 = document.getElementById('applicationSourceChart').getContext('2d');
        let applicationSourceChart = new Chart(ctx2, {
            type: 'bar',
            data: {
                labels: ['Source 1', 'Source 2', 'Source 3'],
                datasets: [{
                    label: 'Applications',
                    data: [30, 25, 22],
                    backgroundColor: '#36A2EB'
                }]
            }
        });

        const target2 = document.querySelector("#recruitmentFunnelChart");
        let ctx3 = document.getElementById('recruitmentFunnelChart').getContext('2d');
        let recruitmentFunnelChart = new Chart(ctx3, {
            type: 'funnel',
            data: {
                labels: ['Applicants', 'Screening', 'Interview', 'Hired'],
                datasets: [{
                    data: [77, 25, 25, 31],
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0']
                }]
            }
        });
    }
}

window.Dashboard = Dashboard;
