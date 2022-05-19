Imports WebApplication1.ZLChat.Common

Public Class DAOCustomers

    ''' <summary>
    ''' GetCustomers
    ''' </summary>
    ''' <param name="search"></param>
    ''' <returns></returns>
    Public Function GetCustomers(ByVal Optional search As String = "") As List(Of Customer)
        Dim dt As DataTable
        Dim lstCustomer As New List(Of Customer)

        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Search", search)
            dic.Add("@PageIndex", 1)
            dic.Add("@PageSize", 20)
            dic.Add("@Id", 0)
            dic.Add("@IsDesceding", 0)
            dt = BaseDAO.GetDataTable("SPGetCustomers", CommandType.StoredProcedure, dic)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    Dim customer As New Customer
                    customer.Id = Convert.ToInt64(row.Item("Id"))
                    customer.Description = IIf(IsDBNull(row.Item("Description")), String.Empty, row.Item("Description"))
                    customer.Name = IIf(IsDBNull(row.Item("Name")), String.Empty, row.Item("Name"))
                    customer.Address = IIf(IsDBNull(row.Item("Address")), String.Empty, row.Item("Address"))
                    lstCustomer.Add(customer)
                Next row
            End If

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return lstCustomer
    End Function

    ''' <summary>
    ''' GetCustomersbyPage
    ''' </summary>
    ''' <param name="pageIndex"></param>
    ''' <param name="pageSize"></param>
    ''' <param name="search"></param>
    ''' <returns></returns>
    Public Function GetCustomersbyPage(ByVal pageIndex As Int32, ByVal pageSize As Int32, ByVal Optional search As String = "") As List(Of Customer)
        Dim dt As DataTable
        Dim lstCustomer As New List(Of Customer)

        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Search", search)
            dic.Add("@PageIndex", pageIndex)
            dic.Add("@PageSize", pageSize)
            dic.Add("@Id", 0)
            dic.Add("@IsDesceding", 0)
            dt = BaseDAO.GetDataTable("SPGetCustomers", CommandType.StoredProcedure, dic)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    Dim customer As New Customer
                    customer.Id = Convert.ToInt64(row.Item("Id"))
                    customer.Description = row.Item("Description")
                    customer.Name = row.Item("Name")
                    lstCustomer.Add(customer)
                Next row
            End If

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return lstCustomer
    End Function

    ''' <summary>
    ''' GetCustomerById
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetCustomerById(ByVal Id As Int64) As Customer
        Dim dt As DataTable
        'Dim lstCustomer As New List(Of Customer)

        Try
            Dim dic As New Dictionary(Of String, Object)

            dic.Add("@Id", Id)
            dt = BaseDAO.GetDataTable("SPGetCustomerById", CommandType.StoredProcedure, dic)
            If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then


                Dim customer As New Customer
                customer.Id = Convert.ToInt64(dt.Rows(0).Item("Id"))
                customer.Description = IIf(IsDBNull(dt.Rows(0).Item("Description")), String.Empty, dt.Rows(0).Item("Description"))
                customer.Name = IIf(IsDBNull(dt.Rows(0).Item("Name")), String.Empty, dt.Rows(0).Item("Name"))
                customer.Address = IIf(IsDBNull(dt.Rows(0).Item("Address")), String.Empty, dt.Rows(0).Item("Address"))
                Return customer
            End If

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' AddCustomer
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="address"></param>
    ''' <param name="description"></param>
    ''' <returns></returns>
    Public Function AddCustomer(ByVal name As String, ByVal address As String, ByVal description As String) As Int64
        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Name", name)
            dic.Add("@Address", address)
            dic.Add("@Description", description)
            Return BaseDAO.Insert("SPAddCustomer", CommandType.StoredProcedure, dic)

        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return 0
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' UpdateCustomer
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="name"></param>
    ''' <param name="address"></param>
    ''' <param name="description"></param>
    ''' <returns></returns>
    Public Function UpdateCustomer(ByVal id As Int64, ByVal name As String, ByVal address As String, ByVal description As String) As Boolean
        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Id", id)
            dic.Add("@Name", name)
            dic.Add("@Address", address)
            dic.Add("@Description", description)
            Return BaseDAO.Update("SPEditCustomer", CommandType.StoredProcedure, dic)

        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return False
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Delete
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="ids"></param>
    ''' <returns></returns>
    Public Function Delete(ByVal id As String) As Boolean
        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Id", id)
            Return BaseDAO.Update("SDeleteCustomer", CommandType.StoredProcedure, dic)
        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try
    End Function

End Class
