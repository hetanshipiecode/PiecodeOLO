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
               "data":"address1"
            },
            {
                "data":"address2"
            },
            {
                "data":"phone"
            },
            {
                orderable: false,
                "render": function (data, type, full, meta) {
                    return ` <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-focused bootstrap-switch-animate bootstrap-switch-on" style="width: 86px;">
<div class="bootstrap-switch-container" style="width: 126px; margin-left: 0px;">
<span class="bootstrap-switch-handle-on bootstrap-switch-primary" style="width: 42px;">ON</span>
<span class="bootstrap-switch-label" style="width: 42px;">&nbsp;</span>
      <span class="bootstrap-switch-handle-off bootstrap-switch-default" style="width: 42px;">
OFF
            </span><input type="checkbox" name="my-checkbox" checked="" data-bootstrap-switch="">
</div>
</div>
               `;

                }
            }
            

        ],

    });

}
