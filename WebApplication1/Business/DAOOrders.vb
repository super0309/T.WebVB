
Imports System.Globalization
Imports WebApplication1.ZLChat.Common

Public Class DAOOrders

    ''' <summary>
    ''' GetOrdersCustomer
    ''' </summary>
    ''' <param name="search"></param>
    ''' <returns></returns>
    Public Function GetOrdersCustomer(ByVal Optional customerId As Int64 = 0, ByVal Optional search As String = "") As List(Of OrdersCustomer)
        Dim dt As DataTable
        Dim lstOrdersCustomer As New List(Of OrdersCustomer)

        Try
            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Search", search)
            dic.Add("@PageIndex", 0)
            dic.Add("@PageSize", 0)
            dic.Add("@CustomerId", customerId)
            dic.Add("@IsDesceding", 0)
            dt = BaseDAO.GetDataTable("SPGetOrders", CommandType.StoredProcedure, dic)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    Dim customer As New Customer
                    customer.Id = Convert.ToInt64(row.Item("CustomerId"))
                    customer.Name = IIf(IsDBNull(row.Item("Name")), String.Empty, row.Item("Name"))
                    customer.Address = IIf(IsDBNull(row.Item("Address")), String.Empty, row.Item("Address"))

                    Dim order As New Order
                    order.Id = Convert.ToInt64(row.Item("Id"))
                    order.Amount = Convert.ToDecimal(row.Item("Amount"))
                    order.CAmount = order.Amount.ToString("C", New CultureInfo("EN-us"))
                    order.Number = Convert.ToInt64(row.Item("Number"))
                    order.Description = IIf(IsDBNull(row.Item("Description")), String.Empty, row.Item("Description"))
                    order.DDate = Convert.ToDateTime(row.Item("Date")).ToString("dd/MM/yyyy hh:mm:ss")

                    Dim orderCustomer As New OrdersCustomer
                    orderCustomer.Customer = customer
                    orderCustomer.Order = order
                    lstOrdersCustomer.Add(orderCustomer)
                Next row
            End If

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return lstOrdersCustomer
    End Function

    ''' <summary>
    ''' AddOrderToCustomer
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <param name="amount"></param>
    ''' <param name="number"></param>
    ''' <param name="description"></param>
    ''' <returns></returns>
    Public Function AddOrderToCustomer(ByVal customerId As String, ByVal amount As String, ByVal number As String, ByVal description As String) As Int64
        Try

            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@CustomerId", customerId)
            dic.Add("@Amount", Convert.ToDecimal(amount))
            dic.Add("@Number", Convert.ToInt64(number))
            dic.Add("@Description", description)
            Dim id = BaseDAO.Insert("SPAddOrder", CommandType.StoredProcedure, dic)
            Return id
        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' GetOrderById
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetOrdercustomerByOrderId(ByVal id As Int64) As OrdersCustomer

        Dim orderCustomer As New OrdersCustomer
        Try
            Dim dic As New Dictionary(Of String, Object)

            dic.Add("@Id", id)
            Dim dt = BaseDAO.GetDataTable("SPGetOrderById", CommandType.StoredProcedure, dic)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim row = dt.Rows(0)
                Dim order As New Order
                order.Id = Convert.ToInt64(row.Item("Id"))
                order.Amount = Convert.ToDecimal(row.Item("Amount"))
                order.CAmount = order.Amount.ToString("C", New CultureInfo("EN-us"))
                order.Number = Convert.ToInt64(row.Item("Number"))
                order.Description = IIf(IsDBNull(row.Item("Description")), String.Empty, row.Item("Description"))
                order.DDate = Convert.ToDateTime(row.Item("Date")).ToString("dd/MM/yyyy hh:mm:ss")
                order.CustomerId = Convert.ToInt64(row.Item("CustomerId"))

                Dim dic2 As New Dictionary(Of String, Object)

                dic2.Add("@Id", order.CustomerId)
                Dim dtCus = BaseDAO.GetDataTable("SPGetCustomerById", CommandType.StoredProcedure, dic2)
                If dtCus IsNot Nothing And dtCus.Rows.Count > 0 Then
                    Dim cusRow = dtCus.Rows(0)
                    Dim customer As New Customer
                    customer.Id = Convert.ToInt64(cusRow.Item("Id"))
                    customer.Name = IIf(IsDBNull(cusRow.Item("Name")), String.Empty, cusRow.Item("Name"))
                    customer.Address = IIf(IsDBNull(cusRow.Item("Address")), String.Empty, cusRow.Item("Address"))

                    orderCustomer.Customer = customer
                    orderCustomer.Order = order

                    Return orderCustomer
                End If
            End If

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' EditOrder
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <param name="amount"></param>
    ''' <param name="description"></param>
    ''' <returns></returns>
    Public Function EditOrder(ByVal orderId As String, ByVal amount As String, ByVal description As String) As Boolean
        Try

            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Id", orderId)
            dic.Add("@Amount", Convert.ToDecimal(amount))
            dic.Add("@Description", description)
            Return BaseDAO.Update("SPEditOrder", CommandType.StoredProcedure, dic)

        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' DeleteOrder
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function DeleteOrder(ByVal orderId As Int64) As Boolean
        Try

            Dim dic As New Dictionary(Of String, Object)
            dic.Add("@Id", orderId)
            Return BaseDAO.Update("SPDeleteOrder", CommandType.StoredProcedure, dic)

        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return False
        End Try
    End Function

End Class
