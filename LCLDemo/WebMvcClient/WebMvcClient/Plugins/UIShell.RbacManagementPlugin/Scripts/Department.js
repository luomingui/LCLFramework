$(document).ready(function () {
    LCLTree.showTree();
    LCLTree.add();

});
var LCLTree = {
    showTree: function () {
        $('#uitree').tree({
            url: 'AjaxEasyUITree_Department',
            lines: true,
            onClick: function (node) {
                var cc = node.id;
                dicType = cc;
                DicCategory.edit();
            }
        });
    },
    reload: function () {
        $('#uitree').tree('reload');
    },
    getSelected: function () {
        return $('#uitree').tree('getSelected');
    },
    add: function () {
        $('#p').panel({
            href: 'AddOrEdit'
        });
    },
    edit: function () {
        $('#p').panel({
            href: 'AddOrEdit?id=' + trim(dicType)
        });
    },
    del: function () {
        var node = DicCategory.getSelected();
        if (node) {
            $.messager.confirm('确认', '确认要删除选中记录吗?', function (y) {
                if (y) {
                    //提交
                    $.post('AjaxDelete/', node.id,
                    function (msg) {
                        if (msg.Success) {
                            $.messager.alert('提示', msg.Message, 'info', function () {
                                //重新加载当前页
                                $('#grid').datagrid('reload');
                            });
                        } else {
                            $.messager.alert('提示', msg.Message, 'info')
                        }
                    }, "json");
                }
            });
        }
        else {
            alert('请选择');
        }
        return false;
    }
}