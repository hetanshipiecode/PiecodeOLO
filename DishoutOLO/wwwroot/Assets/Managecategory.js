


$(document).ready(function () {
    debugger
    $("#lblError").removeClass("success").removeClass("error").text('');

    $("#btn-Add").on("click", function () {
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
            var data = {
                id: $("#Id").val(),
                CategoryName: $("#CategoryName").val(),
                IsActive: $("#IsActive").val() == "true" ? true : false
            }
            //StartProcess();
            $.ajax({
                type: "POST",
                url: "/Category/AddOrUpdateCategory",
                data: { categoryVM: data },
                success: function (data) {
                    if (!data.isSuccess) {
                        console.log(data);
                        //StopProcess();
                        $("#lblError").addClass("error").text(data.message.toString()).show();
                    }
                    else {
                        window.location.href = '/Category/Index'
                    }
                }
            });
        }
    })
});


$(document).ready(function () {
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