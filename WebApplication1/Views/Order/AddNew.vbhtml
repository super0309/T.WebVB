
@Code
    ViewData("Title") = "Add"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Add Order</h2>

<table>
    <tr>
        <td style="width:100px;">
            Name
        </td>
        <td style="width:300px;">
            @*<input type="text" id="name" class="inputText" placeholder="name" value="Hiii" readonly /><br /><br />*@

            
            @*<input type="text" id="name" class="inputText" placeholder="name" />*@
            @Html.DropDownList("lstCustomer", Nothing, New With {Key .class = "dropdown", Key .placeholder = "name"})<br /><br />
        </td>
    </tr>

    <tr>
        <td>
            Number
        </td>
        <td>
            <input type="text" id="number" class="readText" placeholder="number" value="@ViewData("num").ToString()" readonly /><br /><br />
        </td>
    </tr>

    <tr>
        <td>
            Amount
        </td>
        <td>
            <input type="number" id="amount" class="inputText" placeholder="amount" /><br /><br />
        </td>
    </tr>

    <tr>
        <td>
            Description
        </td>
        <td>
            <textarea type="text" id="description" class="inputText" placeholder="description"></textarea>
            <br /><br />
        </td>
    </tr>

    <tr>
        <td>
        </td>
        <td>
            <input type="button" id="btnSave" value="Save" />
        </td>
    </tr>
</table>


