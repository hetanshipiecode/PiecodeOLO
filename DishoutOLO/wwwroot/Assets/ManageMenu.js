$(document).ready(function () {
    $("#lblError").removeClass("success").removeClass("error").text('');
    debugger
    $("#btn-submit").on("click", function () {
     
        $("#lblError").removeClass("success").removeClass("error").text('');
        var retval = true;
        $("#myForm .required").each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                retval = false;
            }
            else {
                $(this).removeClass("error");
            }
        });
      
        if (retval) {
            debugger
            var data = {
               
                id: $("#Id").val(),
                MenuName: $("#MenuName").val(),
                CategoryId: $("#CategoryId").val(),
                MenuPrice: $("#MenuPrice").val(),
                Image: $("#Image").val(),
                IsActive: $("#IsActive").val() == "true" ? true : false
            }

            var formData = new FormData();
            formData.append("Id", data.id);
            formData.append("MenuName", data.MenuName);
            formData.append("CategoryId", data.CategoryId);
            formData.append("MenuPrice", data.MenuPrice);
            formData.append("File",$("#menuId")[0].files[0]);
            formData.append("IsActive", data.IsActive);

            //StartProcess();
            $.ajax({
                url: "/Menu/AddOrUpdateMenu",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST', // For jQuery < 1.9
                success: function (data) {
                    if (!data.isSuccess) {
                        console.log(data);
                        //StopProcess();
                        $("#lblError").addClass("error").text(data.message.toString()).show();
                    }
                    else {
                        window.location.href = '/Menu/Index'
                    }
                }
            });
        }
    })
});


$("#myTable").on("click", "a.btn-delete", function () {
    var id = $(this).data('id');
    $('#deleteModal').data('id', id).modal('show');
    $('#deleteModal').modal('show');
});
$('#delete-btn').click(function () {
    var id = $('#deleteModal').data('id');
    $.ajax({
        type: "POST",
        url: "/Categories/Delete",
        data: { id: id },
        success: function (response) {
            if (response.status != "Fail") {
                $('#deleteModal').modal('hide');
                location.reload();
            }
            else {
                $('#deleteModal').modal('hide');
                funToastr(false, response.error);
            }
        },
        error: function (error) {
            toastr.error(error)
        }
    });
});