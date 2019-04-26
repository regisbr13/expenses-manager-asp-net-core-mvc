Chart.defaults.global.legend.display = false;
$(".month").on('change', function () {
    var id = $(".month").val();

    $.ajax({
        url: "Expenses/MonthlyTotal",
        method: "POST",
        data: { id: id },
        success: function (data) {
            $("canvas#MonthlyExpenses").remove();
            $("div.MonthlyExpenses").append('<canvas id="MonthlyExpenses" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('MonthlyExpenses').getContext('2d');

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


function LoadData() {

    $.ajax({
        url: "Expenses/MonthlyTotal",
        method: "POST",
        data: { id: 1 },
        success: function (data) {
            $("canvas#MonthlyExpenses").remove();
            $("div.MonthlyExpenses").append('<canvas id="MonthlyExpenses" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('MonthlyExpenses').getContext('2d');

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