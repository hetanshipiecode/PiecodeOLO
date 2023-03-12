$(document).ready(function () {
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
                ArticleName: $("#ArticleName").val().trim(),
                ArticleDescription: $("#ArticleDescription").val(),
                IsActive: $("#IsActive").val() == "true" ? true : false
            }
            //StartProcess();
            $.ajax({
                type: "POST",
                url: "/Article/AddOrUpdateArticle",
                data: { articleVM: data },
                success: function (data) {
                    if (!data.isSuccess) {
                        $("#lblError").addClass("error").text(data.message.toString()).show();
                    }
                    else {
                        window.location.href = '/Article/Index'
                    }
                }
            });
        }
    })
});