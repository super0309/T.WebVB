@ModelType Customer
@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    'Dim id = Convert.ToInt64(ViewData("id").ToString())

End Code

<h2>Edit Customer</h2>

    <table>
        <tr>
            <td style="width:100px;">
                @Html.LabelFor(Function(model) model.Name)
            </td>
            <td style="width:300px;">

                @Html.HiddenFor(Function(model) model.Id)
                @*<input type="text" id="name" class="inputText" placeholder="name" />*@
                @Html.TextBoxFor(Function(model) model.Name, New With {Key .class = "inputText", Key .placeholder = "name"})<br /><br />
            </td>
        </tr>

        <tr>
            <td>
                @Html.LabelFor(Function(model) model.Address)
            </td>
            <td>
                @*<input type="text" id="address" class="inputText" placeholder="address" /><br /><br />*@
                @Html.TextBoxFor(Function(model) model.Address, New With {Key .class = "inputText", Key .placeholder = "address"})<br /><br />
            </td>
        </tr>

        <tr>
            <td>
                @Html.LabelFor(Function(model) model.Description)
            </td>
            <td>
                @*<textarea type="text" id="description" class="inputText" placeholder="description"></textarea>*@
                @Html.TextAreaFor(Function(model) model.Description, New With {Key .class = "inputText", Key .placeholder = "description"})
                <br /><br />
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


