// Chart helper functions for Blazor
window.chartHelper = {
    createBarChart: function (canvasId, labels, data, label, backgroundColor, borderColor, isCurrency = true) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        // Destroy existing chart if exists
        if (window.chartInstances && window.chartInstances[canvasId]) {
            window.chartInstances[canvasId].destroy();
        }

        if (!window.chartInstances) {
            window.chartInstances = {};
        }

        const formatValue = (value) => {
            if (isCurrency) {
                return value.toLocaleString('vi-VN') + ' VNĐ';
            } else {
                return value.toLocaleString('vi-VN');
            }
        };

        window.chartInstances[canvasId] = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: label,
                    data: data,
                    backgroundColor: backgroundColor || 'rgba(54, 162, 235, 0.6)',
                    borderColor: borderColor || 'rgba(54, 162, 235, 1)',
                    borderWidth: 2,
                    borderRadius: 8,
                    borderSkipped: false,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        position: 'top',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return context.dataset.label + ': ' + formatValue(context.parsed.y);
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            callback: function (value) {
                                return formatValue(value);
                            }
                        }
                    }
                }
            }
        });
    },

    createLineChart: function (canvasId, labels, data, label, backgroundColor, borderColor, isCurrency = true) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        if (window.chartInstances && window.chartInstances[canvasId]) {
            window.chartInstances[canvasId].destroy();
        }

        if (!window.chartInstances) {
            window.chartInstances = {};
        }

        const formatValue = (value) => {
            if (isCurrency) {
                return value.toLocaleString('vi-VN') + ' VNĐ';
            } else {
                return value.toLocaleString('vi-VN');
            }
        };

        window.chartInstances[canvasId] = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: label,
                    data: data,
                    backgroundColor: backgroundColor || 'rgba(54, 162, 235, 0.1)',
                    borderColor: borderColor || 'rgba(54, 162, 235, 1)',
                    borderWidth: 3,
                    fill: true,
                    tension: 0.4,
                    pointRadius: 5,
                    pointHoverRadius: 7,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        position: 'top',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return context.dataset.label + ': ' + formatValue(context.parsed.y);
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            callback: function (value) {
                                return formatValue(value);
                            }
                        }
                    }
                }
            }
        });
    },

    createPieChart: function (canvasId, labels, data, backgroundColor) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        if (window.chartInstances && window.chartInstances[canvasId]) {
            window.chartInstances[canvasId].destroy();
        }

        if (!window.chartInstances) {
            window.chartInstances = {};
        }

        const defaultColors = [
            'rgba(75, 192, 192, 0.8)',
            'rgba(255, 206, 86, 0.8)',
            'rgba(255, 99, 132, 0.8)',
            'rgba(54, 162, 235, 0.8)',
            'rgba(153, 102, 255, 0.8)',
            'rgba(255, 159, 64, 0.8)',
        ];

        window.chartInstances[canvasId] = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: backgroundColor || defaultColors,
                    borderWidth: 2,
                    borderColor: '#fff',
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        position: 'right',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const label = context.label || '';
                                const value = context.parsed || 0;
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = ((value / total) * 100).toFixed(1);
                                return label + ': ' + value + ' (' + percentage + '%)';
                            }
                        }
                    }
                }
            }
        });
    },

    createDoughnutChart: function (canvasId, labels, data, backgroundColor) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        if (window.chartInstances && window.chartInstances[canvasId]) {
            window.chartInstances[canvasId].destroy();
        }

        if (!window.chartInstances) {
            window.chartInstances = {};
        }

        const defaultColors = [
            'rgba(75, 192, 192, 0.8)',
            'rgba(255, 206, 86, 0.8)',
            'rgba(255, 99, 132, 0.8)',
            'rgba(54, 162, 235, 0.8)',
        ];

        window.chartInstances[canvasId] = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: backgroundColor || defaultColors,
                    borderWidth: 2,
                    borderColor: '#fff',
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        position: 'right',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const label = context.label || '';
                                const value = context.parsed || 0;
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = ((value / total) * 100).toFixed(1);
                                return label + ': ' + value + ' (' + percentage + '%)';
                            }
                        }
                    }
                }
            }
        });
    },

    destroyChart: function (canvasId) {
        if (window.chartInstances && window.chartInstances[canvasId]) {
            window.chartInstances[canvasId].destroy();
            delete window.chartInstances[canvasId];
        }
    }
};

