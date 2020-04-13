var playnum;
var times = [];
var alldata;
var index = 0;
var alllength = -1;

calling();


function timeout() {
    if (index == alllength) {   //第一次叫号播放完毕，请求下次，极端情况！！！
        index = 0;
        alllength = -1
        alldata = "";
        calling();
    }
    var audioEle = document.getElementById("audio");    //播放下个人的
    audioEle.src = "http://" + alldata[index].Voice;
    audioEle.load();
    audioEle.play();
    AudioPlay(audioEle, playnum, 500);
    appentlefthtml(alldata[index]);
    index++;
    if (index == alllength) {   //当次叫号播放完毕，请求下次
        temp = index;   //初始化这些变量
        index = 0;
        alllength = -1
        alldata = "";
        setTimeout(function () { calling(); }, times[temp - 1] * 1000 * playnum + 1000);
    }
    else {
        setTimeout(function () { timeout(); }, times[index - 1] * 1000 * playnum + 1000);
    }

}

function calling() {

    $.ajax({
        async: true,
        cache: false,
        timeout: 60 * 60 * 1000,
        data: {},
        type: "GET",
        datatype: "JSON",
        url: "/Screen/OnCalling",
        success: function (result) {
            var data = jQuery.parseJSON(result);

            /* if(data.Exception!=null)
             {
                 //错误提示先注释掉了 以后可以换成刷新
                 //var html="<span style='font-size:30px;color:#DC143C;float:left;'>服务内部错误!错误码："+data.Exception+"，请与开发人员联系！</span>";
                 //$("#left_bottom").html(html);
                 return;
             }
             */
            if (data.Error == "null") {
                //alert("队列暂时为空");
                setTimeout(calling, 2000);
                return;
            }
            else {
                playnum = data.PlayCount;   //播放次数

                for (var i = 0; i < data.LeftArea.length; i++) {    //所有患者人数
                    times[i] = data.LeftArea[i].Time;       //每个音频文件的时长
                }

                var leftarea = data.LeftArea;   //临时变量
                alldata = data.LeftArea;
                alllength = data.LeftArea.length;

                var audioEle = document.getElementById("audio");    //音频控件
                //alert("Voice"+leftarea[index].Voice)
                audioEle.src = "http://" + leftarea[index].Voice;
                audioEle.load();
                audioEle.play();
                AudioPlay(audioEle, playnum, 500);
                if (audio.paused) {
                    alert("您的浏览器暂不支持自动播放，请开启浏览器自动播放功能！");       //检测是否支持自动播放
                }
                document.getElementById("audio").volume = 1;
                appentlefthtml(leftarea[index]);    //绑定这个人的元素
                index++;
                setTimeout(function () {
                    timeout();
                }, times[index - 1] * 1000 * playnum + 1000);   //当次音频的播放时长

                // appentlefthtml(leftarea[index++]);

                //var time = setInterval(function () {
                //     if(index<length)
                //     {
                //         appentlefthtml(leftarea[index++]);
                //     }
                //     else
                //     {
                //         window.clearInterval(time);
                //         calling();
                //     }
                //}, playnum*5500);
            }
        },
        error: function (result) {
            //alert("服务器故障!");
            var html = "<span style='font-size:30px;color:#DC143C;float:left;'>服务器故障!错误码：" + result + "，页面将在5S后刷新！</span>";
            $("#left_bottom").html(html);
            index = 0;
            alllength = -1
            alldata = "";
            setTimeout(function () { calling(); }, 5000);
        }
    });
}