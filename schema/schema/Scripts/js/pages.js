/*  分页函数 
    pno...页数 
	psize...每页显示记录数
	分页部分是从真实数据行开始，因而存在加减某个常数，以确定真正的记录数
	纯js分页实质是数据行全部加载，通过是否显示属性完成分页功能
*/
var currentPage_=1; //当前页全局变量，用于跳转时判断是否在相同页，在就不跳，否则跳转
var pageSize=0;
var totalPage;   //总页数
function goPage(pno,psize){
	var itable=document.getElementById("ittable");  /*获得tobody的id*/
	var num=itable.rows.length;  /*获得数据行的数目*/
	console.log(num);
	
	
	pageSize=psize;   //每页显示行数
	//总共分几页
	if(num/pageSize>parseInt(num/pageSize)){
		totalPage=parseInt(num/pageSize)+1;
	}
	else
	{
		totalPage=parseInt(num/pageSize);
	}
	var currentPage=pno;//当前页数
	    currentPage_=currentPage;
	var startRow=(currentPage-1)*pageSize+1;//开始显示的行
	var endRow=currentPage*pageSize;//结束的行
	endRow=(endRow>num)?num:endRow;
	console.log(endRow);
	//遍历显示数据实现分页
	for(var i=1;i<(num+1);i++)
	{
		var irow=itable.rows[i-1];
		if(i>=startRow&&i<=endRow)
		{
			irow.style.display="table-row";
		}else{
			irow.style.display="none";
		}
	}
	var tempStr="共"+num+"条记录"+",分"+totalPage+"页"+",当前第"+currentPage+"页";

	if(currentPage>1){
		tempStr+="<a href=\"#\" class=\"first\" onClick=\"goPage("+(1)+","+psize+")\">首页</a>";
		tempStr+="<a href=\"#\" class=\"first\" onClick=\"goPage("+(currentPage-1)+","+psize+")\">\<\<上一页</a>";
	}else{
		tempStr+="<span class=\"first\">首页</span>";
		tempStr+="<span class=\"first\">上一页</span>";
	}
	if(currentPage<totalPage){
		tempStr+="<a href=\"#\" class=\"first\" onClick=\"goPage("+(currentPage+1)+","+psize+")\">下一页\>\></a>";
		tempStr+="<a href=\"#\" class=\"first\" onClick=\"goPage("+(totalPage)+","+psize+")\">尾页</a>";
	}else{
		tempStr+="<span class=\"first\">下一页</span>";
		tempStr+="<span class=\"first\">尾页</span>";
	}
	document.getElementById("barcon").innerHTML=tempStr;

}
function jumpPage(){
		var pagenum=parseInt($("#gopages").val());
		if(pagenum<=totalPage&&pagenum>0){
			if(pagenum!=currentPage_)
			{
				goPage(pagenum,pageSize);
			}
		}else{
			alert("页面不存在");
		}
	}
goPage(1,10);
