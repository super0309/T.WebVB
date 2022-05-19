@Code
    ViewData("Title") = "Index"
End Code

<h2>List of Customer <span id="add_customer" class="btnText">Add new</span> </h2> 

<table>
    <thead>
        <tr>
            <td style="width:50px">
                Id
            </td>

            <td style="width:100px">
                Name
            </td>

            <td style="width:300px">
                Address
            </td>

            <td style="width:100px">
                Description
            </td>

            <td style="width: 300px;">
                Action
            </td>
        </tr>
    </thead>
    <tbody id="lstCustomer">
        
    </tbody>
</table>

