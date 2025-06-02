window.renderPresupuestoChart = (canvasId, chartData) => {
    if (!window.Chart) {
        console.error('Chart.js not loaded');
        return;
    }
    const ctx = document.getElementById(canvasId).getContext('2d');
    if (window._presupuestoChart) {
        window._presupuestoChart.destroy();
    }
    window._presupuestoChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: chartData.labels,
            datasets: [
                {
                    label: 'Presupuestado',
                    backgroundColor: 'rgba(54, 162, 235, 0.7)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1,
                    data: chartData.presupuesto
                },
                {
                    label: 'Ejecutado',
                    backgroundColor: 'rgba(255, 99, 132, 0.7)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                    data: chartData.ejecucion
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' },
                title: { display: true, text: 'Presupuesto vs Ejecución' }
            },
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
};

window.renderPresupuestoPieChart = (canvasId, chartData) => {
    if (!window.Chart) {
        console.error('Chart.js not loaded');
        return;
    }
    const ctx = document.getElementById(canvasId).getContext('2d');
    if (window._presupuestoPieChart) {
        window._presupuestoPieChart.destroy();
    }
    window._presupuestoPieChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: chartData.labels,
            datasets: [
                {
                    label: 'Ejecutado',
                    data: chartData.ejecucion,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.7)',
                        'rgba(54, 162, 235, 0.7)',
                        'rgba(255, 206, 86, 0.7)',
                        'rgba(75, 192, 192, 0.7)',
                        'rgba(153, 102, 255, 0.7)',
                        'rgba(255, 159, 64, 0.7)'
                    ]
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' },
                title: { display: true, text: 'Distribución de Ejecución por Tipo de Gasto' }
            }
        }
    });
};
