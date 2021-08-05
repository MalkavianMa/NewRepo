//保存选中id
var checkBox = [];
//定义页码值
var cPage = 0;

$.fn.dataGridMuls = function (options) {
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

    options["onSelectAll"] = function (rowids, statue) {
        //函数里做自己的处理
        var $grid = $(this);
        //全选选中 添加选中元素
        if (statue) {
            var checkedlists = $grid.jqGrid("getGridParam", "selarrrow");
            if (!!checkedlists) {
                for (var i = 0; i < checkedlists.length; i++) {
                    if (checkBox.indexOf(checkedlists[i]) == -1 && !!checkedlists[i]) {
                        checkBox.push(checkedlists[i]);
                    }
                }
            }
        }
        //取消全选 移除数组元素
        else {
            var checkedlists = $grid.jqGrid('getDataIDs');
            if (!!checkedlists) {
                for (var i = 0; i < checkedlists.length; i++) {
                    deleteCheckBox(checkedlists[i]);
                }
            }
        }
       //恢复工具栏显示
        showTipMuls($grid);
    };

    options["onSelectRow"] = function (rowId, status) {
        //添加选中元素，不重复添加
        if (status) {
            if (checkBox.indexOf(rowId) == -1 && !!rowId) {
                //根据id获取整行数据
                // var values = $("#gridList").jqGrid("getRowData", rowId);
                //向数组插入数据

                checkBox.push(rowId);


            }

        }
        //取消选中移除该id
        else {
            deleteCheckBox(rowId);
        }
        var $grid = $(this);
        showTipMuls($grid);
    };


    options["onPaging"] = function (pgButton) {
        cPage++; //翻页事件
    };
    options["loadComplete"] = function (data) {
        if (cPage == 0) {
            checkBox = [];//每次加载清空选中状态
        }
        cPage = 0;
        //此处需要注意页面值是动态加载数据还是一次性加载全部数据，写法不一样，这里是动态加载页面数据
        var rowArr = data.rows;

        if (checkBox.length > 0 && !!rowArr) {
            for (var i = 0; i < rowArr.length; i++) {
                for (var j = 0; j < checkBox.length; j++) {
                    if (rowArr[i].F_Id == checkBox[j]) {
                        $("#gridList").jqGrid('setSelection', rowArr[i].F_Id);
                        break;
                    }
                }
            }
        }
        var $grid = $(this);
        showTipMuls($grid);
    };

    $element.jqGrid(options);
};

//恢复工具栏显示
function showTipMuls(grid) {
    if (grid.jqGrid("getGridParam", "enableHideToolBar")) {
          
           // var length = grid.jqGrid("getGridParam", "selrow").length;
            var $operate = $(".operate");
            if (checkBox.length > 0) {
                $operate.stop().animate({ "left": 0 }, 100);
            } else {
                $operate.animate({ "left": '-100.1%' }, 100);
            }
            $operate.find('.close').click(function () {
                $operate.stop().animate({ "left": '-100.1%' }, 100);
            })
            $(".first").find("span").text(checkBox.length);
        
    }
}


function deleteCheckBox(rowId) {
    for (var i = 0; i < checkBox.length; i++) {
        if (checkBox[i] == rowId) {
            //根据索引值删除checkBox中的数据
            checkBox.splice(i, 1);
        }
    }
}