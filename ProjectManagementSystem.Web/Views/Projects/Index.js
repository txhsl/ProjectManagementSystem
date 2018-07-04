var projectService = abp.services.app.project;

(function ($) {

    $(function () {

        var $projectStateCombobox = $('#ProjectStateCombobox');

        $projectStateCombobox.change(function () {
            getProjectList();
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

function editProject(id) {
    abp.ajax({
        url: "/Projects/Edit",
        data: { "id": id },
        type: "GET",
        dataType: "html"
    })
        .done(function (data) {
            $("#edit").html(data);
            $("#editProject").modal("show");
        })
        .fail(function (data) {
            abp.notify.error('Something is wrong!');
        });
}

function deleteProject(id) {
    abp.message.confirm(
        "是否删除Id为" + id + "的项目信息",
        function (isConfirmed) {
            if (isConfirmed) {
                projectService.deleteProject(id)
                    .done(function () {
                        abp.notify.info("删除项目成功！");
                        getProjectList();
                    });
            }
        }
    );

}

function getProjectList() {
    var $projectStateCombobox = $('#ProjectStateCombobox');
    var url = '/Projects/GetList?state=' + $projectStateCombobox.val();
    abp.ajax({
        url: url,
        type: "GET",
        dataType: "html"
    })
        .done(function (data) {
            $("#projectList").html(data);
        });
}