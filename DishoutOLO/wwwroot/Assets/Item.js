//const { data } = require("jquery");

var table;
$("document").ready(function () {
    loadAllItem();

})
$("#itemTbl").on("click", "a#btn-delete", function () {
    var id = $(this).data('id');
    $('#deleteModal').data('id', id).modal('show');
    $('#deleteModal').modal('show');
});
$('#delete-btn').click(function () {
    var id = $('#deleteModal').data('id');
    $.ajax({
        type: "GET",
        url: "/Item/DeleteItem",
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

var index = 0;
function loadAllItem() {
    var url = "/Item/GetAllItem"

       table = $("#itemTbl").DataTable({
        "orderCellsTop": true,
        "fixedHeader": true,
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
                "data": "categoryName"
            },
            {
                "data": "itemName"  
            },
            {
                orderable: false,

                "data": function (full) {

                    var imgPath = '/Content/Item/' + full.itemImage;

                    console.log(full);
                    return "<img src=" + imgPath + " height='50'width='90'>";
                   
                }
            },
            {
                "data": function (show) {
                    if (show.isCombo == true) {
                        return "Yes";
                    }
                    else {
                        return "No";
                    }
                 }
            },      
                 
{
    orderable: false,
    "render": function (data, type, full, meta) {
        return ` <a href="/Item/Edit/` + full.id + `" data-id="` + full.id + `" class="btn btn-success btn-sm" title="Edit">
                 <i class="fa fa-edit"></i>
                 </a>

                 <a href="javascript:void(0)" id="btn-delete" data-id="`+ full.id + `" class="btn btn-danger btn-sm" title="Delete">
                 <i class="fa fa-trash"></i>
                 </a>`;

    }
}
        ],
});
       
    $('#CategoryName').on('change',function () {
        table.columns(1).search($("#CategoryName option:selected").text().trim());
        table.draw();
    }); 

    $('#txtItemName').on('keyup', function () {
       table.columns(2).search($('#txtItemName').val().trim());
        table.draw();
    }); 
 }



