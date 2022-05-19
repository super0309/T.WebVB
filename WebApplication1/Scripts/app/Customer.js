Customer =
{
    action: { _add: "Add", _edit: "Edit", _list: "List", _delete: "Delete", _navigation: "Navigation", _index: "Index" },
    controller: "Customer",
    host: window.location.origin,
    currentPage: 0,
    currentSize: 10,
    customers: null,
    data: null,
    init: function (protocol) {
        LoadInnit(protocol);
        loadData(protocol, '')
    }
}

function LoadInnit(protocol) {
    $('#add_customer').click(function () {
        window.location.href = Customer.host + "/" + Customer.controller + "/" + Customer.action._add;

    });

    if ($('#btnSave') != null) {
        $('#btnSave').click(function () {
            var customer =
            {
                name: $('#name').val(),
                address: $('#address').val(),
                description: $('#description').val()
            };
            addCustomer(customer);
        });
    }

    if ($('#btnUpdate') != null) {
        $('#btnUpdate').click(function () {
            var customer =
            {
                id: $('#Id').val(),
                name: $('#Name').val(),
                address: $('#Address').val(),
                description: $('#Description').val()
            };
            updateCustomer(customer);
        });
    }

}

function customerValidate(customer) {
    if (customer.name  == null || customer.name == '' || customer.name == 'undefined') {
        alert('input Name field incorrect');
        return false;
    }

    if (customer.address == null || customer.address == '' || customer.address == 'undefined') {
        alert('input Address field incorrect');
        return false;
    }
    return true;
}

function loadData(protocol, json) {

    loadProgessAndProcess(true);
    var url = Customer.host + "/" + Customer.controller + "/" + Customer.action._list;
    console.log(url);
    $.ajax({
        type: "GET",
        url: url,
        data: json,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200) {
                //ProductCIC.page_meta = data.page_meta;
                if (data.objects != null) {
                    $('#lstCustomer').html(builData(data.objects));
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}

function builData(items) {
    var hmlsource = '';
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var htmlItem = '<tr><td>' + item.Id + '</td><td>' + item.Name + '</td><td>' + item.Address + '</td><td>' + item.Description + '</td><td>|&nbsp;<span class="btnText" onclick="editCustomer(' + item.Id + ')">Edit</span>&nbsp;|&nbsp;<span class="btnText" onclick="deleteCustomer(' + item.Id + ')">Delete</span>&nbsp;|&nbsp;|&nbsp;<span class="btnText" onclick="viewOrders(' + item.Id +')">View Orders</span>&nbsp;|&nbsp;</td></tr>';
        hmlsource += htmlItem;
    }
    return hmlsource;
}

function editCustomer(id) {
    var url = window.location.origin + "/" + Customer.controller + "/" + Customer.action._edit;
    window.location.href = url + "?id="+id;
}

function viewOrders(id) {
    var url = window.location.origin + "/Order" + "/" + Customer.action._index;
    window.location.href = url + "?id=" + id;
}

function updateCustomer(customer) {
    console.log(customer);
    if (!customerValidate(customer)) return;

    loadProgessAndProcess(true);
    var url = window.location.origin + "/" + Customer.controller + "/" + Customer.action._edit;
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        data: customer,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200) {
                var url = window.location.origin + "/" + Customer.controller + "/" + Customer.action._index;
                console.log(url);
                window.location.href = url;
                if (data.objects != null) {
                    //$('#lstCustomer').html(builData(data.objects));
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}

function deleteCustomer(id)
{
    if (id == null || id <= 0) return;
    else {
        var obj = {
            id: id
        };
        var cf = confirm("All orders associated with this customer will be removed from the system. Are you sure delete this customer?");
        if (cf) {
            loadProgessAndProcess(true);
            var url = Customer.host + "/" + Customer.controller + "/" + Customer.action._delete;
            console.log(url);
            $.ajax({
                type: "POST",
                url: url,
                data: obj,
                dataType: 'json',
                success: function (data) {
                    if (data.number == 200) {
                        
                        var url = Customer.host + "/" + Customer.controller + "/" + Customer.action._index;
                        console.log(url);
                        window.location.href = url;
                       
                    }
                    loadProgessAndProcess(false);
                },
                error: function (data) {

                    loadProgessAndProcess(false);
                }
            });
        }

      
    }
    
}

function addCustomer(customer)
{
    console.log(customer);
    if (!customerValidate(customer)) return;

    loadProgessAndProcess(true);
    var url = Customer.host + "/" + Customer.controller + "/" + Customer.action._add;
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        data: customer,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200) {
                //ProductCIC.page_meta = data.page_meta;
                //alert('ok');
                var url = Customer.host + "/" + Customer.controller + "/" + Customer.action._index;
                console.log(url);
                window.location.href = url;
                if (data.objects != null) {
                    //$('#lstCustomer').html(builData(data.objects));
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}

