$(function () {
    $(".createExpense").click(function () {
        $("#modal").load("/Expenses/CreateCurrent", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".createIncome").click(function () {
        $("#modal").load("/Incomes/CreateCurrent", function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editExpense").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Expenses/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".editIncome").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Incomes/Edit?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteIncome").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Incomes/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});

$(function () {
    $(".deleteExpense").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/Expenses/Delete?Id=" + id, function () {
            $("#modal").modal();
        })
    })
});