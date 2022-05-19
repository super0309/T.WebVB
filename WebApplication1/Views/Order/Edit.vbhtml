@ModelType OrdersCustomer
@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Edit Order</h2>

<table>
    <tr>
        <td style="width:100px;">
            @Html.LabelFor(Function(model) model.Customer.Name)
        </td>
        <td style="width:300px;">
            @Html.HiddenFor(Function(model) model.Customer.Id)
            @*<input type="text" id="name" class="inputText" placeholder="name" />*@
            @Html.TextBoxFor(Function(model) model.Customer.Name,
                         New With {Key .class = "readText", Key .placeholder = "name", Key .readonly = "readonly"})<br /><br />
        </td>
    </tr>

    <tr>
        <td>
            @Html.LabelFor(Function(model) model.Order.Number)
        </td>
        <td>
            @Html.HiddenFor(Function(model) model.Order.Id)
            @*<input type="text" id="name" class="inputText" placeholder="name" />*@
            @Html.TextBoxFor(Function(model) model.Order.Number,
                                        New With {Key .class = "readText", Key .placeholder = "number", Key .readonly = "readonly"})<br /><br />
        </td>
    </tr>

    <tr>
        <td>
            @Html.LabelFor(Function(model) model.Order.Amount)
        </td>
        <td>
            @Html.TextBoxFor(Function(model) model.Order.Amount,
                               New With {Key .class = "inputText", Key .placeholder = "amount", Key .type = "number"})<br /><br />
        </td>
    </tr>

    <tr>
        <td>
            @Html.LabelFor(Function(model) model.Order.Description)
        </td>
        <td>
            @Html.TextAreaFor(Function(model) model.Order.Description,
                                        New With {Key .class = "inputText", Key .placeholder = "description"})<br /><br />
        </td>
    </tr>

    <tr>
        <td>
        </td>
        <td>
            <input type="button" id="btnUpdate" value="Update" />
        </td>
    </tr>
</table>