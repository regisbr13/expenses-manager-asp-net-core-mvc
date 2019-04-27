Chart.defaults.global.legend.display = false;
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
                    labels: ['Receitas', 'Despesas'],
                    datasets: [{
                        label: null,
                        backgroundColor: ["#27ae60", "#c0392b"],
                        data: [data.incomes, data.expenses]
                    }]
                },
                options: {
                    responsive: false,
                    title: {
                        display: true,
                        text: "Receitas x Despesas"
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
                    labels: ['Receitas', 'Despesas'],
                    datasets: [{
                        label: null,
                        backgroundColor: ["#27ae60", "#c0392b"],
                        data: [data.incomes, data.expenses]
                    }]
                },
                options: {
                    responsive: false,
                    title: {
                        display: true,
                        text: "Receitas x Despesas"
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