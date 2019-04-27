$(".month").on('change', function () {
    var id = $(".month").val();

    $.ajax({
        url: "Expenses/MonthlyIncomesExpenses",
        method: "POST",
        data: { id: id },
        success: function (data) {
            $("canvas#IncomesExpenses").remove();
            $("div.IncomesExpenses").append('<canvas id="IncomesExpenses" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('IncomesExpenses').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'bar',   
                data: {
                    labels: [''],
                    datasets: [{
                        label: 'Receitas',
                        backgroundColor: ["#27ae60", "#c0392b", "#00BFFF"],
                        data: [data.incomes],
                    },
                    {
                        label: 'Despesas',
                        backgroundColor: "#c0392b",
                        data: [data.expenses]
                    },
                    {
                        label: 'Saldo',
                        backgroundColor: "#00BFFF",
                        data: [data.balance]
                    },
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Receitas x Despesas x Saldo"
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
});


function LoadDataIncomesExpenses() {

    $.ajax({
        url: "Expenses/MonthlyIncomesExpenses",
        method: "POST",
        data: { id: 1 },
        success: function (data) {
            $("canvas#IncomesExpenses").remove();
            $("div.IncomesExpenses").append('<canvas id="IncomesExpenses" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('IncomesExpenses').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'bar',

                data: {
                    labels: [''],
                    datasets: [{
                        label: 'Receitas',
                        backgroundColor: ["#27ae60", "#c0392b", "#00BFFF"],
                        data: [data.incomes],
                    },
                    {
                        label: 'Despesas',
                        backgroundColor: "#c0392b",
                        data: [data.expenses]
                    },
                    {
                        label: 'Saldo',
                        backgroundColor: "#00BFFF",
                        data: [data.balance]
                    },
                    ]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Receitas x Despesas x Saldo"
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }],
                        xAxes: [{
                            barPercentage: 0.6
                        }]
                    }
                }
            });
        }
    });
}