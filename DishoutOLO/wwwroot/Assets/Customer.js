

var table;
$("document").ready(function () {
    loadAllCustomer();

    $('#btn-deactive').on('click', function () {
        $('#deactivemodel').modal('toggle');
        toastr.success('Status Has Been Changed!')
    })
    $('#btn-active').on('click', function () {
        $('#activemodel').modal('toggle');
        toastr.success('Status Has Been Changed!')
    })
});

function loadAllCustomer() {

    var url = "/Customer/GetAllCustomer"
    table = $("#customerTbl").DataTable({

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
                "data": "firstName"
            },
            {
                "data": "lastName"
            },
            {
                "data": "email"
            },
            {
                "data": "address1"
            },
            {
                "data": "address2"
            },
            {
                "data": "phone"
            },
            {
                orderable: false,
                "render": function () {

                    return ` <input type="checkbox" name="option" id="back">`
                                                               

                }
            },

        ],
        "fnDrawCallback": function () {

            $('#back').on('change', function () {
                if ($(this).is(":checked")) {
                    $('#deactivemodel').modal('toggle');
                      
                }
                else {
                    $('#activemodel').modal('toggle');
                }
            })

            
            
        }

    }); 
         
    

}


 
    
