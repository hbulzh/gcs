function getjson(id) {
    $.ajax({
        async: true,
        cache: true,
        timeout: 60 * 60 * 1000,
        data: { "id": id },
        type: "POST",
        datatype: "JSON",
        url: "/Screen/ScrrenDisplay",
        success: function (result) {
            if (result == "[]") {
                var html = "<span style='font-size:50px;color:#DC143C;margin-top:27%;margin-left:36%;float:left;'>当前暂无患者就诊</span>";
                $("#content_div").html(html);
            }
            else {
                appendrighthtml(result);

            }

        },
        error: function (result) {
            /*
            var html="<span style='font-size:40px;color:#DC143C;margin-top:27%;margin-left:25%;float:left;'>服务器故障，页面将在10S后重新加载！</span>";
            $("#content_div").html(html);
            */
        }
    })
}

function appendrighthtml(result) {
    var html = "";
    $.each(eval(result), function (l1, item1) {
        //alert(item1.room);
        html += "<div class='" + classarray[(parseInt(l1, 10) % 2)][0] + "'><div class='" + classarray[(parseInt(l1, 10) % 2)][1] + "'><span>" + item1.room + "</span></div>";

        html += "<div class='" + classarray[(parseInt(l1, 10) % 2)][2] + "'><div class='" + classarray[(parseInt(l1, 10) % 2)][3] + "'><span>初诊队列</span></div>";
        html += "<table><tbody>";
        $.each(eval(item1.FirstVist), function (i2, item2) {

            //出诊患者名字中间的字用*代替
            item2.patientName = formatName(item2.patientName);
            if (item2.status == 1) {
                html += "<tr class='" + classarray[(parseInt(l1, 10) % 2)][6] + "'>";
                html += "<th>" + item2.recomNum + "</th>";
                html += "<th '>" + item2.patientName + "</th>";
                html += "<th>就诊</th>";
                html += "</tr>";
            }
            else {
                html += "<tr>";
                html += "<th>" + item2.recomNum + "</th>";
                html += "<th >" + item2.patientName + "</th>";
                html += "<th>等待</th>";
                html += "</tr>";
            }
        });
        html += "</tbody></table></div>";
        html += "<div class='" + classarray[(parseInt(l1, 10) % 2)][4] + "'><div class='" + classarray[(parseInt(l1, 10) % 2)][5] + "'><span>回诊队列</span></div>";
        html += "<table><tbody>";
        $.each(eval(item1.OtherVist), function (i2, item2) {
            //回诊患者名字中间的字用*代替
            item2.patientName = formatName(item2.patientName);

            if (item2.status == 1) {
                html += "<tr class='checking'>";
                //html += "<th>" + item2.recomNum + "</th>";
                html += "<th  >" + item2.patientName + "</th>";
                html += "<th>就诊</th>";
                html += "</tr>";
            }
            else {
                html += "<tr>";
                //html += "<th>" + item2.recomNum + "</th>";
                html += "<th >" + item2.patientName + "</th>";
                html += "<th>等待</th>";
                html += "</tr>";
            }
        });

        html += "</tbody></table>";
        html += "</div></div>";
    });

    $("#content_div").html(html);
}

//将名字中间的字用*代替 
function formatName(name) {



    var newStr;
    if (name.length === 2) {
        newStr = name.substr(0, 1) + '*';
    }
    else if (name.length > 2) {
        let char = '';
        for (let i = 0, len = name.length - 2; i < len; i++) {
            char += '*';
        }
        newStr = name.substr(0, 1) + char + name.substr(-1, 1);
    }
    else {
        newStr = name;
    }
    return newStr;

}
