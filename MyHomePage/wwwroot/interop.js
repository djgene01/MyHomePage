window.initializeCharts = (pvPower, batterySoc) => {
    const powerCtx = document.getElementById('powerChart').getContext('2d');
    const powerChart = new Chart(powerCtx, {
        type: 'line',
        data: {
            labels: ['6AM', '7AM', '8AM', '9AM', '10AM', '11AM', '12PM', '1PM', '2PM', '3PM', '4PM', '5PM'],
            datasets: [{
                label: 'Power (W)',
                data: Array(12).fill(parseFloat(pvPower) || 0),
                borderColor: 'rgb(255, 159, 64)',
                backgroundColor: 'rgba(255, 159, 64, 0.2)',
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: { beginAtZero: true, title: { display: true, text: 'Power (W)' } },
                x: { title: { display: true, text: 'Time' } }
            }
        }
    });

    const batteryCtx = document.getElementById('batteryChart').getContext('2d');
    const batteryChart = new Chart(batteryCtx, {
        type: 'line',
        data: {
            labels: ['6PM', '8PM', '10PM', '12AM', '2AM', '4AM', '6AM', '8AM', '10AM', '12PM', '2PM', '4PM'],
            datasets: [{
                label: 'Battery Level (%)',
                data: Array(12).fill(parseFloat(batterySoc) || 0),
                borderColor: 'rgb(75, 192, 192)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: { min: 0, max: 100, title: { display: true, text: 'Battery Level (%)' } },
                x: { title: { display: true, text: 'Time' } }
            }
        }
    });
};