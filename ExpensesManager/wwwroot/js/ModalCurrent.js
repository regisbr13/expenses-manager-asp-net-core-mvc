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

$(".add").click(function () {
    $("#modal").load("ExpenseType/Create", function () {
        $("#modal").modal();
    })
})

$(".addIt").click(function () {
    $("#modal").load("/IncomeType/Create", function () {
        $("#modal").modal();
    })
})

$(document).ready(function () {
    $('.new').tooltip()
});

function k(i) {
    var v = i.value.replace(/\D/g, '');
    v = (v / 100).toFixed(2) + '';
    v = v.replace(".", ",");
    v = v.replace(/(\d)(\d{3})(\d{3}),/g, "$1.$2.$3,");
    v = v.replace(/(\d)(\d{3}),/g, "$1.$2,");
    i.value = v;
}