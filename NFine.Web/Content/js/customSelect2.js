    //数据字典项自定义加载实现思路脑图
    //1、接口一次返回所有自定义过的字典编码记录
    //2、字典项控件初始化，对比已自定义过的编码记录，添加是否有自定义记录的属性
    //3、无自定义记录控件  直接加载平台默认数据源
    //4、有自定义记录控件  判断页面是否选择了项目ID
    //5、无项目ID 不加载数据（页面首次加载，操作控件提示先选择厂站）
    //6、有项目ID  获取项目自定义数据源加载，该项目下无自定义，加载平台默认数据
    //7、选择厂站后 有自定义记录控件按6进行加载数据

var definedCodelist;//有自定义记录的字典编码列表//用于增加自定义项控件
$(function () {
    
    GetDefinedLists(function (r) {
        definedCodelist = r;//回调一次获取所有自定义过的字典编码记录//增加自定义项控件

    });
})
//回调一次获取所有自定义过的字典编码记录
function GetDefinedLists(callback) {
    $.ajax({
        url: "/SystemManage/ItemsType/GetDefinedLists",
        data: {},
        dataType: "json",
        async: false,
        cache: false,

        success: function (data) {
            debugger;
            callback(data);


        }


    });
}
//数据字典自定义项下拉新增方法MJW 2021年4月16日
$.fn.bindDefined = function (options) {
    debugger;
    
    if (!!definedCodelist) {//获取有自定义记录的编码  
       definedCodelist.some(function (item, index, array) {
           return item.F_EnCode == options.param.enCode;//传入的字典编码在自定义记录中，则添加属性defined=1
       }) ? $(this).attr("defined", 1) : $(this).attr("defined", 0);//兼容模式         //definedCodelist.some(({ F_EnCode }) => F_EnCode == options.param.enCode) ? $(this).attr("defined", 1) : $(this).attr("defined", 0);//Chrome支持的写法       
        debugger;

    } else {
        $(this).attr("defined", 0);//无自定义记录设置为未自定义属性0 
    }
    
    if ($(this).attr("defined") == "1") {//如果控件有自定义记录 
        var data = [{
            id: 0,
            text: '==请选择==',
            value: ''
        }];
        $(this).empty();//初始化清空选项
        $(this).select2({
            data: data

        })//创建默认项请选择
        if (!!options.param.projectId) {//有所属项目则按所属项目加载自定义数据源 
           
            $(this).bindSelect(options);
            $(this).unbind("select2:open")//移除打开事件
            $(this).on("change", function (e) {//兼容历史数据切换处理，无匹配下拉默认请选择
                if (!$(this).val()) {
                    $(this).val(['0']).trigger('change');
                }
            });
        }
        else {//无所属项目 添加打开事件，提示选择
            var $element = $(this);
            //增加提示信息参数 默认提示所属项目或场站选择
            if (!!options.param.msg) {
                $element.on("select2:open", function (e) {
                    $.modalMsg(options.param.msg, "warning");
                    $element.select2("close");
                });
            }
            else {
                $element.on("select2:open", function (e) {
                    $.modalMsg("请先选择所属项目或厂站！", "warning");
                    $element.select2("close");
                });
            }
           
            
        }
    }
    if ($(this).attr("defined") == "0") {//无自定义记录 按平台默认数据源加载
        $(this).empty();
        options.param.projectId = "";
        var data = [{
            id: 0,
            text: '==请选择==',
            value: ''
        }];
        $(this).select2({
            data: data

        })
        $(this).bindSelect(options);
    }
}
