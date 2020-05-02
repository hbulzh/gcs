  
var patinetName = new Array();
GetPatientname1();
GetPatientname2();
GetPatientname3();
GetPatientname4();

alert(1);
function  GetPatientname1()
{
    var name = document.getElementById("partname1").innerText;
    
    if (name != ""&& patinetName[0] != name)
    {
        patinetName[0] = name;
        call(name)
    }

    
}
function GetPatientname2() {
    var name = document.getElementById("partname2").innerText;

    if (name != "" && patinetName[1] != name) {
        patinetName[1] = name;
        call(name)
    }

}
function GetPatientname3() {
    var name = document.getElementById("partname3").innerText;

    if (name != "" && patinetName[2] != name) {
        patinetName[2] = name;
        call(name)
    }

}
function GetPatientname4() {
    var name = document.getElementById("partname4").innerText;

    if (name != "" && patinetName[3] != name) {
        patinetName[3] = name;
        call(name)
    }

}


function  call(name)
{
    var name = "";

    $.ajax({
        async: true,
        cache: false,
        timeout: 60 * 60 * 1000,
        data: { "name": name },
        type: "GET",
        datatype: "JSON",
        url: "/Screens/OnCalling",
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
            if (data.Error == "null")
            {
                //alert("队列暂时为空");
                setTimeout(calling,2000);
                return;
            }
            else
            {
                playnum = data.PlayCount;   //播放次数

               
                
                var audioEle = document.getElementById("audio");    //音频控件
                //alert("Voice"+leftarea[index].Voice)
                audioEle.src = data.Voice;
                audioEle.load();
                audioEle.play();
                AudioPlay(audioEle, playnum, 500);
                if (audio.paused)
                {
                    alert("您的浏览器暂不支持自动播放，请开启浏览器自动播放功能！");       //检测是否支持自动播放
                }
                document.getElementById("audio").volume = 1;
             
                setTimeout(function () { }, data.Time * 1000 * playnum + 1000);   //当次音频的播放时长

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
            alert("服务器故障!" + result);
            var html="<span style='font-size:30px;color:#DC143C;float:left;'>服务器故障!错误码："+result+"，页面将在5S后刷新！</span>";
          //  $("#left_bottom").html(html);
           
           // setTimeout(function () { calling(); },5000); 
        }
    });
}

function AudioPlay(elem, max, times) {
    elem.play();
    var start = 0;
    elem.addEventListener("ended", function () {
        start++;
        if (start < max) {
            setTimeout(function () { elem.play(); }, times);
        } else {
            elem.pause();
        }
    });
}

