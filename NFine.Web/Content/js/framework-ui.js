$(function () {
    document.body.className = localStorage.getItem('config-skin');
    $("[data-toggle='tooltip']").tooltip();
   
})
$.reload = function () {
    location.reload();
    return false;
}
$.loading = function (bool, text) {
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}
$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}
$.currentWindow = function () {
    var iframeId = top.$(".NFine_iframe:visible").attr("id");
    return top.frames[iframeId];
}
$.browser = function () {
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1;
    if (isOpera) {
        return "Opera"
    };
    if (userAgent.indexOf("Firefox") > -1) {
        return "FF";
    }
    if (userAgent.indexOf("Chrome") > -1) {
        if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
            return "Chrome";
        } else {
            return "360";
        }
    }
    if (userAgent.indexOf("Safari") > -1) {
        return "Safari";
    }
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
        return "IE";
    };
}
$.download = function (url, data, method) {
    if (url && data) {
        data = typeof data == 'string' ? data : jQuery.param(data);
        var inputs = '';
        $.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
        });
        $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
    };
};
$.modalOpen = function (options) {
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };
    var options = $.extend(defaults, options);
    var _width = top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
    var _height = top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        yes: function () {
            options.callBack(options.id)
        }, cancel: function () {
            return true;
        }
    });
}
//支持自定义固定尺寸弹窗大小  zyk 20200902
$.modalOpenExt = function (options) {
    var defaults = {
        id: null,
        title: '系统窗口',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['确认', '关闭'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null
    };
    var options = $.extend(defaults, options);
    var _width = options.width;//top.$(window).width() > parseInt(options.width.replace('px', '')) ? options.width : top.$(window).width() + 'px';
    var _height = options.height;//top.$(window).height() > parseInt(options.height.replace('px', '')) ? options.height : top.$(window).height() + 'px';
    var index = top.layer.open({
        id: options.id,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        success: options.success,
        yes: function () {
            options.callBack(options.id)
        }, cancel: function () {
            return true;
        }

    });
}
$.modalConfirm = function (content, callBack) {
    top.layer.confirm(content, {
        icon: "fa-exclamation-circle",
        title: "系统提示",
        btn: ['确认', '取消'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
    }, function () {
        callBack(true);

    }, function () {
        callBack(false)
    });
}
//关闭确认窗口的封装   zyk20200908
$.modalCloseAll = function () {
    //关闭当前确认窗口
    top.layer.closeAll('dialog');
    //关闭父窗体
    //$.modalClose();
}
$.modalAlert = function (content, type) {
    var icon = "";
    if (type == 'success') {
        icon = "fa-check-circle";
    }
    if (type == 'error') {
        icon = "fa-times-circle";
    }
    if (type == 'warning') {
        icon = "fa-exclamation-circle";
    }
    top.layer.alert(content, {
        icon: icon,
        title: "系统提示",
        btn: ['确认'],
        btnclass: ['btn btn-primary'],
    });
}
$.modalMsg = function (content, type) {
    if (type != undefined) {
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
        }
        if (type == 'error') {
            icon = "fa-times-circle";
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
        }
        top.layer.msg(content, { icon: icon, time: 4000, shift: 5 });
        top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        top.layer.msg(content);
    }
}
$.modalClose = function () {
    var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
    var IsClose = $IsdialogClose.is(":checked");
    if ($IsdialogClose.length == 0) {
        IsClose = true;
    }
    if (IsClose) {
        top.layer.close(index);
    } else {
        location.reload();
    }
}
$.submitForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    $.loading(true, options.loading);
    window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.ajax({
            url: options.url,
            data: options.param,
            type: "post",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.state == "success") {
                    options.success(data);
                    $.modalMsg(data.message, data.state);
                    if (options.close == true) {
                        $.modalClose();
                    }
                } else {
                    $.modalAlert(data.message, data.state);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.loading(false);
                $.modalMsg(errorThrown, "error");
            },
            beforeSend: function () {
                $.loading(true, options.loading);
            },
            complete: function () {
                $.loading(false);
            }
        });
    }, 500);
}
$.submitFormData = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "正在提交数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    $.loading(true, options.loading);
    debugger;
    window.setTimeout(function () {
        debugger;
        var Is_FormData = options.param instanceof FormData;//传入对象是否为FormData
        if (Is_FormData) {
            if ($('[name=__RequestVerificationToken]').length > 0) {
                options.param.append("__RequestVerificationToken", $('[name=__RequestVerificationToken]').val());
            }
            $.ajax({
                url: options.url,
                data: options.param,
                type: "post",
                //dataType: "json",
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    debugger;
                    data = $.parseJSON(data);
                    if (data.state == "success") {
                        options.success(data);
                        $.modalMsg(data.message, data.state);
                        if (options.close == true) {
                            $.modalClose();
                        }
                    } else {
                        $.modalAlert(data.message, data.state);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.loading(false);
                    $.modalMsg(errorThrown, "error");
                },
                beforeSend: function () {
                    $.loading(true, options.loading);
                },
                complete: function () {
                    $.loading(false);
                }
            });
        }
        else {
            if ($('[name=__RequestVerificationToken]').length > 0) {
                options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
            }
            $.ajax({
                url: options.url,
                data: options.param,
                type: "post",
                dataType: "json",
                success: function (data) {
                    if (data.state == "success") {
                        options.success(data);
                        $.modalMsg(data.message, data.state);
                        if (options.close == true) {
                            $.modalClose();
                        }
                    } else {
                        $.modalAlert(data.message, data.state);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.loading(false);
                    $.modalMsg(errorThrown, "error");
                },
                beforeSend: function () {
                    $.loading(true, options.loading);
                },
                complete: function () {
                    $.loading(false);
                }
            });
        }
    }, 500);
}
$.deleteForm = function (options, name) {

    //var tip = (name!!)?"注：您确定要删除"+name+"吗？" : "注：您确定要删除该项数据吗？";
    var tip = "";
    if (!!name) {
        tip = "注：您确定要删除【" + name + "】吗？";
    }


    var defaults = {
        prompt: tip==""?"注：您确定要删除该项数据吗？" :tip ,
        url: "",
        param: [],
        loading: "正在删除数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        if (data.state == "success") {
                            options.success(data);
                            $.modalMsg(data.message, data.state);
                        } else {
                            $.modalAlert(data.message, data.state);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false);
                        $.modalMsg(errorThrown, "error");
                    },
                    beforeSend: function () {
                        $.loading(true, options.loading);
                    },
                    complete: function () {
                        $.loading(false);
                    }
                });
            }, 500);
        }
    });

}
$.jsonWhere = function (data, action) {
    if (action == null) return;
    var reval = new Array();
    $(data).each(function (i, v) {
        if (action(v)) {
            reval.push(v);
        }
    })
    return reval;
}
$.fn.jqGridRowValue = function () {
    var $grid = $(this);
    var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var json = [];
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
        return json;
    } else {
        return $grid.jqGrid('getRowData', $grid.jqGrid('getGridParam', 'selrow'));
    }
}
$.fn.formValid = function () {
    return $(this).valid({
        errorPlacement: function (error, element) {
            element.parents('.formValue').addClass('has-error');
            element.parents('.has-error').find('i.error').remove();
            element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>');
            $("[data-toggle='tooltip']").tooltip();
            if (element.parents('.input-group').hasClass('input-group')) {
                element.parents('.has-error').find('i.error').css('right', '33px')
            }
        },
        success: function (element) {
            element.parents('.has-error').find('i.error').remove();
            element.parent().removeClass('has-error');
        }
    });
}
$.fn.formSerialize = function (formdate) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "true") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "select":
                    $id.val(value).trigger("change");
                    break;
                case "file":
                    if (value) {
                        $id.after("<a href='" + value + "' target='_blank' class='Form_File' style='margin: 10px;color: blue;'>附件查看</a>");
                    }
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            default:
                //20200905 zyk修改
                //var value = $this.val() == "" ? "&nbsp;" : $this.val();
                var value = $this.val() == "" ? "" : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/&nbsp;/g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};
 // 20200926,zyk  增加回调函数
$.fn.bindSelect = function (options,CallBack) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                if (data.state != "error") {
                    $.each(data, function (i) {
                        $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
                    });
                    $element.select2({
                        minimumResultsForSearch: options.search == true ? 0 : -1
                    });
                    $element.on("change", function (e) {
                        if (options.change != null) {
                            options.change(data[$(this).find("option:selected").index()]);
                        }
                        $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
                    });

                    if (!!CallBack) {
                        CallBack(data);
                    }
                }
                else {
                    if (!!CallBack) {
                        CallBack(null);
                    }
                }
            },
            error: function (data) {
                if (!!CallBack) {
                    CallBack(data);
                }
            }
        });
    } else {
        $element.select2({
            minimumResultsForSearch: -1
        });
    }
}
$.fn.authorizeButton = function () {
    var moduleId = top.$(".NFine_iframe:visible").attr("id").substr(6);
    var dataJson = top.clients.authorizeButton[moduleId];
    var $element = $(this);
    $element.find('a[authorize=yes]').attr('authorize', 'no');
    if (dataJson != undefined) {
        $.each(dataJson, function (i) {
            $element.find("#" + dataJson[i].F_EnCode).attr('authorize', 'yes');
        });
    }
    $element.find("[authorize=no]").parents('li').prev('.split').remove();
    $element.find("[authorize=no]").parents('li').remove();
    $element.find('[authorize=no]').remove();
}
$.fn.dataGrid = function (options) {
    var defaults = {
        datatype: "json",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        enableHideToolBar: true//是否允许自动切换（隐藏）工具条
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    options["onSelectRow"] = function (rowid) {
        if ($(this).jqGrid("getGridParam", "enableHideToolBar")) {
            var length = $(this).jqGrid("getGridParam", "selrow").length;
            var $operate = $(".operate");
            if (length > 0) {
                $operate.stop().animate({ "left": 0 }, 100);
            } else {
                $operate.animate({ "left": '-100.1%' }, 100);
            }
            $operate.find('.close').click(function () {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            })
        }
    };
    $element.jqGrid(options);
};

//--------------------------------------------//
//多选模式下新增方法 获取选中多选的值-zyk 20200902
$.fn.jqGridRowValueMul = function () {
    var $grid = $(this);
    return $grid.jqGrid("getGridParam", "selarrrow")
}
//多选模式下新增方法-zyk 20200902
$.fn.dataGridMul = function (options) {
    var defaults = {
        datatype: "json",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        enableHideToolBar: true//是否允许自动切换（隐藏）工具条
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    options["onSelectRow"] = function () {
        if ($(this).jqGrid("getGridParam", "enableHideToolBar")) {
            var $operate = $(".operate");
            var SelectItem = $(this).jqGrid("getGridParam", "selrow");
            if (SelectItem != undefined && SelectItem != null && SelectItem.length > 0) {
                $operate.animate({ "left": 0 }, 200);
                //赋值判断已选中多少项
                var TempCount = $('#gridList input[type=checkbox]:checked').length;

                $(".first").find("span").text(TempCount);
            } else {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            }
            $operate.find('.close').click(function () {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            })

           
        }
    };
    options["onSelectAll"] = function () {
        if ($(this).jqGrid("getGridParam", "enableHideToolBar")) {
            var $operate = $(".operate");
            var SelectItem = $(this).jqGrid("getGridParam", "selrow");
            if (SelectItem != undefined && SelectItem != null && SelectItem.length > 0) {
                $operate.animate({ "left": 0 }, 200);
                //赋值判断已选中多少项
                var TempCount = $('#gridList input[type=checkbox]:checked').length;
                $(".first").find("span").text(TempCount);
            } else {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            }
            $operate.find('.close').click(function () {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            })
        }
    };
    $element.jqGrid(options);
};

//查询框回车检索 MJW 2021年4月9日
keySearchText=function keySearch() {
    var input = document.getElementById("txt_keyword");//    
    if (!!input) {
        input.addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode === 13) {
                document.getElementById("btn_search").click();
                return false;
            }
        });
    }
}
//--------------------------------------------//