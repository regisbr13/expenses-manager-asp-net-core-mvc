$(".month").on('change', function () {
    var id = $(".month").val();

    $.ajax({
        url: "Expenses/MonthlyExpensesType",
        method: "POST",
        data: { id: id },
        success: function (data) {
            $("canvas#ExpensesType").remove();
            $("div.ExpensesType").append('<canvas id="ExpensesType" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('ExpensesType').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'pie',

                data: {
                    labels: data.types,
                    datasets: [{
                        label: "Despesas por categoria",
                        backgroundColor: Colors(data.types.length),
                        hoverBackgroudColor: Colors(data.types.length),
                        data: data.values
                    }]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Despesas por categoria"
                    }
                }
            });
        }
    });
});

function LoadDataMonthlyExpensesType() {
    var id = $(".month").val();

    $.ajax({
        url: "Expenses/MonthlyExpensesType",
        method: "POST",
        data: { id: 1 },
        success: function (data) {
            $("canvas#ExpensesType").remove();
            $("div.ExpensesType").append('<canvas id="ExpensesType" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('ExpensesType').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'pie',

                data: {
                    labels: data.types,
                    datasets: [{
                        label: "Despesas por categoria",
                        backgroundColor: Colors(data.types.length),
                        hoverBackgroudColor: Colors(data.types.length),
                        data: data.values
                    }]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Despesas por categoria"
                    }
                }
            });
        }
    });
}