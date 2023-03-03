$(document).ready(function () {
    $("#lblError").removeClass("success").removeClass("error").text('');

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
            var data = {
                id: $("#Id").val(),
                CategoryId: $("#CategoryId").val(),
                ItemName: $("#ItemName").val(),
                ItemImage: $("#ItemImage").val(),
                IsActive: $("#IsCombo").val() == "true" ? true : false
            }
            var formData = new FormData();
            formData.append("Id", data.id);
            formData.append("CategoryId", data.CategoryId);
            formData.append("ItemName", data.ItemName);
            formData.append("File", $("#itemId")[0].files[0]);
            formData.append("IsCombo", data.IsActive);

            //StartProcess();
            $.ajax({
                type: "POST",
                url: "/Item/AddOrUpdateItem",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,

                success: function (data) {
                    if (!data.isSuccess) {
                        console.log(data);
                        //StopProcess();
                        $("#lblError").addClass("error").text(data.message.toString()).show();
                    }
                    else {
                        window.location.href = '/Item/Index'
                    }
                }
            });
        }
        else {
            alert("retval not available");
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
        url: "/Item/DeleteItem",
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



    $(document).ready(function () {
        
        $(".taxmenu").hide();
    $("#t1").click(function () {
        $(".taxmenu").show();
        });
    $("#t2").click(function () {
        $(".taxmenu").hide();
        });
    });

$(document).ready(function () {

    $(".txtchoice").hide();
    $("#c1").click(function () {
        $(".txtchoice").show();
    });
    $("#c2").click(function () {
        $(".txtchoice").hide();
    });
});

    



