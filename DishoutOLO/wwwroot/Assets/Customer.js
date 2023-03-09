var table;
$("document").ready(function () {
    loadAllCustomer();
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

                    return ` <input type="checkbox" name="option" id="back">
                                             
                     
`

                }
            },

        ],

    }); 


        
    

}


 
    
