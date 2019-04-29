$(".month").on('change', function () {
    var id = $(".month").val();

    $.ajax({
        url: "/Incomes/MonthlyIncomesType",
        method: "POST",
        data: { id: id },
        success: function (data) {
            $("canvas#IncomesType").remove();
            $("div.IncomesType").append('<canvas id="IncomesType" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('IncomesType').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'doughnut',

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
                        text: "Receitas por categoria"
                    }
                }
            });
        }
    });
});

function LoadDataMonthlyIncomesType() {
    var id = $(".month").val();

    $.ajax({
        url: "/Incomes/MonthlyIncomesType",
        method: "POST",
        data: { id: 1 },
        success: function (data) {
            $("canvas#IncomesType").remove();
            $("div.IncomesType").append('<canvas id="IncomesType" style="width:400px; height:400px"></canvas>');

            var ctx = document.getElementById('IncomesType').getContext('2d');

            var graphic = new Chart(ctx, {
                type: 'doughnut',

                data: {
                    labels: data.types,
                    datasets: [{
                        label: "Receitas por categoria",
                        backgroundColor: Colors(data.types.length),
                        hoverBackgroudColor: Colors(data.types.length),
                        data: data.values
                    }]
                },
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: "Receitas por categoria"
                    }
                }
            });
        }
    });
}