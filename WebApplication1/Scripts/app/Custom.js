
function RemoveDuplicateVal(items) {
    //var items = ["Mike", "Matt", "Nancy", "Adam", "Jenny", "Nancy", "Carl"];
    var uniqueNames = [];
    $.each(items, function (i, el) {
        if ($.inArray(el, uniqueNames) === -1) uniqueNames.push(el.trim());
    });
    return uniqueNames;
}

function getItemById(id, items) {
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (item.id.toString() == id.toString()) {
            return item;
        }
    }
}

function getItemByName(name, items) {
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (item.name.toString().toLowerCase() == name.toString().toLowerCase()) {
            return item;
        }
    }
}

function getItemsFromArrayByIds(strIds, items) {
    var subItems = [];
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (strIds.indexOf(item.id.toString()) >= 0) {
            subItems.push(item);
        }
    }
    return subItems;
}

function getIdsFromItems(items) {
    var ids = '';
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        ids += ',' + item.id;
    }
    ids = TrimMark(ids);
    return ids;
}

function validate(evt, obj) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault)
            theEvent.preventDefault();

    }
}

function checkElement(strId) {
    var element = document.getElementById(strId);
    if (typeof (element) != 'undefined' && element != null) return true;
    else return false;
}

function pushData(items) {
    var arraytoPush = [];
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        arraytoPush.push(item);
    }
    return arraytoPush;
}

function getItemsFieldFromObjectList(fieldName, items) {
    var newArray = [];
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var obj = {
            id: item.id, text: strip(item[fieldName])
        }
        newArray.push(obj);
    }
    return newArray;
}

function getValueSelectedItems(items, ids) {
    if (ids == null) {
        return '';
    }
    var value = '';
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (ids.indexOf(item.id) >= 0) {
            value += "," + strip(item.name);
        }
    }
    value = value.substring(1, value.length);
    return value;
}

function TrimMark(strText) {

    if (strText != null && strText != '') {
        while (strText.indexOf(',') == 0) {
            strText = strText.substring(1, strText.length);
        }

        while (strText.lastIndexOf(',') == strText.length - 1) {
            strText = strText.slice(0, strText.length - 1);
        }

        while (strText.indexOf(',,') >= 0) {
            strText = strText.replace(',,', ',');
        }

        return strText;
    }
    return '';
}

function AddIdToIds(ids, id) {
    if (id == ids || ids.lastIndexOf(id.toString()) >= 0)
        return ids;
    else {
        ids += "," + id;
        ids = TrimMark(ids);
    }
    return ids;
}

function strip(html) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}

function loadProgessAndProcess(show) {
    if (show) {
        $('#frmProgessBar').css("top", "0");
        $('#frmProgessBar').css("left", "0");
        $('#frmProgessBar').css("right", "0");
        $('#frmProgessBar').css("bottom", "0");
        $('#frmProgessBar').css("height", "100%");
        $('#frmProgessBar').css("width", "100%");
        $('#frmProgessBar').css("position", "fixed");
        $('#frmProgessBar').css("background-color", "rgb(255, 255, 255)");
        $('#frmProgessBar').css("opacity", "0.5");
        $('#frmProgessBar').css("filter", "alpha(opacity=50)");
        $('#frmProgessBar').css("display", "block");
        $('#frmProgessBar').css("z-index", "10000");

    } else {
        $('#frmProgessBar').css("top", "0");
        $('#frmProgessBar').css("left", "0");
        $('#frmProgessBar').css("right", "0");
        $('#frmProgessBar').css("bottom", "0");
        $('#frmProgessBar').css("height", "100%");
        $('#frmProgessBar').css("width", "100%");
        $('#frmProgessBar').css("position", "fixed");
        $('#frmProgessBar').css("background-color", "rgb(255, 255, 255)");
        $('#frmProgessBar').css("opacity", "0.5");
        $('#frmProgessBar').css("filter", "alpha(opacity=50)");
        $('#frmProgessBar').css("display", "none");
        $('#frmProgessBar').css("z-index", "10000");
    }
}

function removeEmptyItems(items) {
    var arr = [];
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        if (item == null || item == "" || item == "undefined")
            continue;
        arr.push(item);
    }
    return arr;
}

function join(items, mark) {
    var strReturn = "";
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        strReturn += item + mark;
    }
    return strReturn;
}

function GetData(method, url, json)
{
    $.ajax({
        type: method,
        url: url,
        data: json,
        dataType: 'json',
        success: function (data) {
            if (data.number == 200)
            {
                if (data.objects != null)
                {
                    return data.objects;
                }
            }
            loadProgessAndProcess(false);
        },
        error: function (data) {

            loadProgessAndProcess(false);
        }
    });
}