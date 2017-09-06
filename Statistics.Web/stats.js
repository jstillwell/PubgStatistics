(function () {
    var userId = 'jbravo01',  //change this to your id
        timeout = 10 * 1000,  //timeout to check for updates
        url = "http://pubgstatistics.azurewebsites.net/api/stats/";    // url to the API

    getStats();

    setInterval(function () {
        getStats();
    }, timeout);

    function getStats() {
        $.ajax({
            method: "GET",
            url: url + userId,
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        }).done(function (data) {
            console.info("Data", data);
            updateUI(data);
        });
    }
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
})();