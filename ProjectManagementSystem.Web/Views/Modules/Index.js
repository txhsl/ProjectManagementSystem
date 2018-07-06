var moduleService = abp.services.app.module;

(function ($) {

    $(function () {

        var $moduleStateCombobox = $('#ModuleStateCombobox');
        var $selectedUserId = $('#SelectedUserId');
        var $selectedProjectId = $('#SelectedProjectId');

        $moduleStateCombobox.change(function () {
            getModuleList();
        });

        $selectedUserId.change(function () {
            getModuleList();
        });

        $selectedProjectId.change(function () {
            getModuleList();
        });

        var $modal = $(".modal");
        //显示modal时，光标显示在第一个输入框
        $modal.on('shown.bs.modal',
            function () {
                $modal.find('input:not([type=hidden]):first').focus();
            });

    });
})(jQuery);

//异步开始提交时，显示遮罩层
function beginPost(modalId) {
    var $modal = $(modalId);

    abp.ui.setBusy($modal);
}

//异步开始提交结束后，隐藏遮罩层并清空Form
function hideForm(modalId) {
    var $modal = $(modalId);

    var $form = $modal.find("form");
    abp.ui.clearBusy($modal);
    $modal.modal("hide");
    //创建成功后，要清空form表单
    $form[0].reset();
}

//处理异步提交异常
function errorPost(xhr, status, error, modalId) {
    if (error.length > 0) {
        abp.notify.error('Something is going wrong, please retry again later!');
        var $modal = $(modalId);
        abp.ui.clearBusy($modal);
    }
}

function editModule(id) {
    abp.ajax({
        url: "/Modules/Edit",
        data: { "id": id },
        type: "GET",
        dataType: "html"
    })
        .done(function (data) {
            $("#edit").html(data);
            $("#editModule").modal("show");
        })
        .fail(function (data) {
            abp.notify.error('Something is wrong!');
        });
}

function sendEmail(id, name) {
    abp.message.confirm(
        "是否给 " + name +" 的负责人发送提醒信息",
        function (isConfirmed) {
            if (isConfirmed) {
                moduleService.sendEmail(id, name)
                    .done(function () {
                        abp.notify.info("发送成功！");
                        getModuleList();
                    });
            }
        }
    );
}

function deleteModule(id) {
    abp.message.confirm(
        "是否删除Id为" + id + "的模块信息",
        function (isConfirmed) {
            if (isConfirmed) {
                moduleService.deleteModule(id)
                    .done(function () {
                        abp.notify.info("删除模块成功！");
                        getModuleList();
                    });
            }
        }
    );

}

function getModuleList() {
    var $moduleStateCombobox = $('#ModuleStateCombobox');
    var $selectedUserId = $('#SelectedUserId');
    var $selectedProjectId = $('#SelectedProjectId');
    var url = '/Modules/GetList?State=' + $moduleStateCombobox.val();

    if ($selectedUserId.val() != -1) {
        url += '&MemberId=' + $selectedUserId.val();
    }
    if ($selectedProjectId.val() != -1) {
        url += '&ProjectId=' + $selectedProjectId.val();
    }

    abp.ajax({
        url: url,
        type: "GET",
        dataType: "html"
    })
        .done(function (data) {
            $("#moduleList").html(data);
        });
}