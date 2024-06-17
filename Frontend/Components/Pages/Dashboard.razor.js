export class Dashboard {
    static initCharts() {
        console.log("entrou no init");
        const target = document.querySelector("#commonSkills");
        var ctx1 = document.getElementById('commonSkills').getContext('2d');
        var commonSkills = new Chart(ctx1, {
            type: 'pie',
            data: {
                labels: ['Analytics', 'Accounts', 'HR', 'IT', 'Marketing', 'Operations'],
                datasets: [{
                    data: [25, 25, 9, 16, 26, 5],
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF', '#FF9F40']
                }]
            }
        });

        const target1 = document.querySelector("#applicationSourceChart");
        var ctx2 = document.getElementById('applicationSourceChart').getContext('2d');
        var applicationSourceChart = new Chart(ctx2, {
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
        var ctx3 = document.getElementById('recruitmentFunnelChart').getContext('2d');
        var recruitmentFunnelChart = new Chart(ctx3, {
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
