function LoadDataIncomesExpensesTotal() {
    $.ajax({
        url: "/Expenses/IncomesExpensesTotal",
        method: "POST",
        success: function (data) {

            new Chart(document.getElementById("Total"), {
                type: 'line',

                data: {
                    labels: data.months,
                    datasets: [{
                        label: "Receitas",
                        backgroundColor: "#27ae60",
                        borderColor: "#27ae60",
                        data: data.incomes,
                        fill: false,
                        spanGaps: false
                    },
                        {
                            label: "Despesas",
                            backgroundColor: "#c0392b",
                            borderColor: "#c0392b",
                            data: data.expenses,
                            fill: false,
                            spanGaps: false
                        }
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Receitas e despesas por mês"
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }],
                        xAxes: [{
                            barPercentage: 0.5
                        }]
                    }
                }
            });
        }
    });
}