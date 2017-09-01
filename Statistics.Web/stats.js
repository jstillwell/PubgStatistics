var userId = 'jbravo01';    //change this to your id

(function () {
    setInterval(function () {
        $.ajax({
            method: "GET",
            url: "http://localhost:62165/api/stats/" + userId,
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        }).done(function (data) {
            console.info("Data", data);
            updateUI(data);
        });
    }, 5000);
})();

function updateUI(data) {
    var solokd = data.Solo[0];
    $('.solo-kd').html(solokd);
    var duoskd = data.Duos[0];
    $('.duos-kd').html(duoskd);
    var squadskd = data.Squads[0];
    $('.squads-kd').html(squadskd);

    //wins
    var solowins = data.Solo[1];
    $('.solo-wins').html(solowins);
    var duowins = data.Duos[1];
    $('.duos-wins').html(duowins);
    var squadwins = data.Squads[1];
    $('.squads-wins').html(squadwins);
}