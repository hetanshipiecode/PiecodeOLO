//const { data } = require("jquery");

var table;
$("document").ready(function () {
    loadAllMenu();
    
})
$("#menuTbl").on("click", "a#btn-delete", function () {
    var id = $(this).data('id');
    $('#deleteModal').data('id', id).modal('show');
    $('#deleteModal').modal('show');
});
$('#delete-btn').click(function () {
    var id = $('#deleteModal').data('id');
    $.ajax({
        type: "GET",
        url: "/Menu/DeleteMenu",
        data: { id: id },
        success: function (response) {
            if (!response.isSuccess) {
                $('#deleteModal').modal('hide');
            }
            else {
                $('#deleteModal').modal('hide');
                table.ajax.reload()
                //    funToastr(true, response.message);
            }
        },
        error: function (error) {
        }
    });
});


function loadAllMenu() {
    var url = "/Menu/GetAllMenu"

    table = $("#menuTbl").DataTable({

        "searching": true,
        "serverSide": true,
        "bFilter": true,
        "orderMulti": false,
        "ajax": {
            url: url,
            type: "POST",
            datatype: "json"
        },

        "columns": [
            {
                "data": "menuName"
            },
            {
                "data": "categoryName"
            },
            {
                "data": "menuPrice"
            },
            {
                orderable: false,

                "data": function (full) {
                    var imgPath = '/Content/Menu/' + full.image;
                    return "<img src=" + imgPath +" >";
                }   
            },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return ` <a href="/Menu/Edit/` + full.id + `" data-id="` + full.id + `" class="btn btn-success btn-sm" title="Edit">
                                    <i class="fa fa-edit"></i>
                             </a>
                             <a href="javascript:void(0)" id="btn-delete" data-id="`+ full.id + `" class="btn btn-danger btn-sm" title="Delete">
                                    <i class="fa fa-trash"></i>
                             </a>`;
                }
            }

        ],

    });

}

