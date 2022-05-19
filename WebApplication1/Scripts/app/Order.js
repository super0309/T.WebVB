Order =
{
    action: { _add: "Add", _addNew: "AddNew", _edit: "Edit", _list: "List", _listCusId: "ListByCustomerId", _delete: "Delete", _navigation: "Navigation", _index: "Index" },
    Id: GetIdValue(),
    Search: GetSearch(),
    controller: "Order",
    host: window.location.origin,
    currentPage: 0,
    currentSize: 10,
    Orders: null,
    data: null,
    init: function (protocol) {
        LoadInnitOrder(protocol);
        loadDataOrder(protocol, '')
    }
}

function GetIdValue() {
    if ($('#vlh') != null && $('#vlh').val() != null && parseInt($('#vlh').val()) > 0) {
        return parseInt($('#vlh').val())
    }
    else return 0;
}

function GetSearch() {
    return $('#txtSearch').val();
}

function LoadInnitOrder(protocol) {
    if ($('#btnAddNew') != null) {
        $('#btnAddNew').click(function () {
            var url = Order.host + "/" + Order.controller + "/" + Order.action._addNew;
            window.location.href = url;
        });
    }

    if ($('#btnSearch') != null) {
        $('#btnSearch').click(function () {

            Order.Id = 0;
            Order.Search = GetSearch();
            loadDataOrder(protocol, '');

        });
    }

    if ($('#btnRefresh') != null) {
        $('#btnRefresh').click(function () {
            Order.Id = 0;
            Order.Search = '';
            //loadDataOrder(protocol, '');
            window.location.href = Order.host + "/" + Order.controller + "/" + Order.action._index;
        });
    }

    if ($('#btnSave') != null) {
        $('#btnSave').click(function () {
            var json = {};
            if ($('#lstCustomer') != null && $('#lstCustomer').val() != 'undefined' && $('#lstCustomer').val() != null) {
                json =
                {
                    customerId: $('#lstCustomer').val(),
                    amount: $('#amount').val(),
                    number: $('#number').val(),
                    description: $('#description').val()
                };
            }
            else {
                json =
                {
                    customerId: $('#customerId').val(),
                    amount: $('#amount').val(),
                    number: $('#number').val(),
                    description: $('#description').val()
                };
            }
            if (!OrderValidate(json)) return;
            
            var url = Order.host + "/" + Order.controller + "/" + Order.action._add;
            console.log(json);
            $.ajax({
                type: "POST",
                url: url,
                data: json,
                dataType: 'json',
                success: function (data) {
                    if (data.number == 200) {
                        alert('Insert order success!');
                        window.location.href = Order.host + "/" + Order.controller + "/" + Order.action._index;
                    }
                    loadProgessAndProcess(false);
                },
                error: function (data) {
                    loadProgessAndProcess(false);
                }
            });
        });
    }


    if ($('#btnUpdate') != null) {
        $('#btnUpdate').click(function () {
            var objOrder =
            {
                id: $('#Order_Id').val(),
                amount: $('#Order_Amount').val(),
                description: $('#Order_Description').val()
            };
            if (!OrderValidate(objOrder)) return;
            updateOrder(objOrder);
        });
    }
}

function OrderValidate(Order) {
    if (Order.amount == null || Order.amount == '' || parseFloat(Order.amount) < 0) {
        alert('input amount field incorrect');
        return false;
    }
    return true;
}

function loadDataOrder(protocol, json) {

    var json = {
        id: Order.Id,
        search: Order.Search
    }

    if ((Order.Search != null && Order.Search != 'undefined' && Order.Search.length > 0) || (Order.Search == '' && Order.Id <= 0)) {
        loadProgessAndProcess(true);
        var url = Order.host + "/" + Order.controller + "/" + Order.action._list;
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
                        $('#lstOrder').html(builOrderData(data.objects));
                    }
                }
                loadProgessAndProcess(false);
            },
            error: function (data) {
                loadProgessAndProcess(false);
            }
        });
    }
    else if (Order.Search == '' && Order.Id > 0) {
        loadProgessAndProcess(true);
        var url = Order.host + "/" + Order.controller + "/" + Order.action._listCusId;
        json = { id: Order.Id }
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
                        $('#lstOrder').html(builOrderData(data.objects));
                    }
                }
                loadProgessAndProcess(false);
            },
            error: function (data) {
                loadProgessAndProcess(false);
            }
        });

    }


}

function builOrderData(items) {
    var hmlsource = '';
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var htmlItem = '<tr><td>' + item.Customer.Name + '</td><td>' + item.Customer.Address + '</td><td>' + item.Order.Number + '</td><td>' + item.Order.DDate + '</td><td>' + item.Order.Description + '</td><td>' + item.Order.CAmount + '</td><td><span class="btnText" onclick="addOrder(' + item.Customer.Id + ')">| Add </span><span class="btnText" onclick="editOrder(' + item.Order.Id + ')">| Edit </span><span class="btnText" onclick="deleteOrder(' + item.Order.Id + ')">| Delete |</span></td></tr>';
        hmlsource += htmlItem;
    }
    return hmlsource;
}

function editOrder(id) {
    var url = Order.host + "/" + Order.controller + "/" + Order.action._edit;
    window.location.href = url + "?id=" + id;
}

function updateOrder(obj) {
    console.log(obj);
    if (!OrderValidate(obj)) return;

    loadProgessAndProcess(true);
    var url = Order.host + "/" + Order.controller + "/" + Order.action._edit;
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        data: obj,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200) {
                var url = Order.host + "/" + Order.controller + "/" + Order.action._index;
                alert('Update order success!');
                console.log(url);
                window.location.href = url;
                if (data.objects != null) {
                    //$('#lstOrder').html(builOrderData(data.objects));
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}

function addOrder(customerId) {
    var url = Order.host + "/Order" + "/" + Customer.action._add;
    window.location.href = url + "?id=" + customerId;
}

function addOrderCustomer(Order) {
    console.log(Order);
    if (!OrderValidate(Order)) return;

    loadProgessAndProcess(true);
    var url = Order.host + "/" + Order.controller + "/" + Order.action._add;
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        data: Order,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200) {
                //ProductCIC.page_meta = data.page_meta;
                //alert('ok');
                alert('Insert order success!');
                var url = Order.host + "/" + Order.controller + "/" + Order.action._index;
                console.log(url);
                window.location.href = url;
                if (data.objects != null) {
                    //$('#lstOrder').html(builOrderData(data.objects));
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}


function deleteOrder(id) {
    if (id <= 0) return;
    else {

        var cf = confirm("Are you sure you want to delete this order?");
        if (cf) {
            var url = Order.host + "/" + Order.controller + "/" + Order.action._delete;
            var obj = { id: id };
            console.log(url);
            $.ajax({
                type: "POST",
                url: url,
                data: obj,
                dataType: 'json',
                success: function (data) {
                    if (data.number == 200) {
                        var url = Order.host + "/" + Order.controller + "/" + Order.action._index;
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