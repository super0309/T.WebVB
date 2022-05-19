
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim id = ""
    If ViewData("id") IsNot Nothing Then
        id = ViewData("id").ToString()
    End If
End Code

<h2>Orders Customers</h2>
<input type="text" placeholder="input customer name" id="txtSearch"/>
<input type="button" value="search" id="btnSearch" />
<input type="button" value="refresh" id="btnRefresh"/>
<input type="button" value="add new" id="btnAddNew" />
<input type="hidden" value='@id' id="vlh"/>

<table class="tbl">
    <thead>
        <tr>
            

            <td style="width:100px">
                Name
            </td>

            <td style="width: auto">
                Address
            </td>

            <td style="width:auto">
                Number
            </td>

            <td style="width:200px">
                Date
            </td>

            <td style="width:auto">
                Description
            </td>

            <td style="width:100px">
                Amount
            </td>

            <td>
                Action
            </td>
        </tr>
    </thead>
    <tbody id="lstOrder">
    </tbody>
</table>
