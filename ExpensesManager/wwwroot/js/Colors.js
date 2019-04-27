function Colors(qtt) {
    var colors = [];

    while (qtt > 0) {
        var r = Math.floor(Math.random() * 255);
        var g = Math.floor(Math.random() * 255);
        var b = Math.floor(Math.random() * 255);

        colors.push("rgb(" + r + "," + g + "," + b + ")");
        qtt--;
    }
    return colors;
}