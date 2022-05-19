@Code
    ViewData("Title") = "Add"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Add Customer</h2>

<table>
    <tr>
        <td style="width:100px;">
            Name
        </td>
        <td style="width:300px;" >
            <input type="text" id="name" class="inputText" placeholder="name" /><br /><br />
        </td>
    </tr>

    <tr>
        <td>
            Address
        </td>
        <td>
            <input type="text" id="address" class="inputText" placeholder="address" /><br /><br />
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
            <input type="button" id="btnSave" value="Save"/> 
        </td>
    </tr>
</table>