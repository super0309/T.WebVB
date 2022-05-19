Imports System.Web.Mvc
Imports System.Threading.Tasks
Imports WebApplication1.ZLChat.Common

Namespace Controllers
    Public Class OrderController
        Inherits Controller

        ''' <summary>
        ''' Index
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        Function Index(ByVal id As String) As ActionResult
            ViewData("id") = id
            Return View()
        End Function

        ''' <summary>
        ''' List
        ''' </summary>
        ''' <param name="search"></param>
        ''' <returns></returns>
        Function List(ByVal search As String) As JsonResult
            Try
                If search Is Nothing Then
                    search = String.Empty
                End If
                Dim daoOrders = New DAOOrders()
                Dim lst = daoOrders.GetOrdersCustomer(0, search)

                Return Json(New With {Key .number = 200, Key .message = "OK", Key .objects = lst}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        ''' <summary>
        ''' ListByCustomerId
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        Function ListByCustomerId(ByVal id As String) As JsonResult
            Try

                Dim daoOrders = New DAOOrders()
                Dim lst = daoOrders.GetOrdersCustomer(Convert.ToInt64(id))

                Return Json(New With {Key .number = 200, Key .message = "OK", Key .objects = lst}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        ''' <summary>
        ''' Add
        ''' </summary>
        ''' <returns></returns>
        Function Add(ByVal id As String) As ActionResult
            Try
                Dim daoOrders = New DAOCustomers()
                Dim item = daoOrders.GetCustomerById(id)
                Dim num = DateTime.Now.ToString("yyyyddMMhhmmssfff")
                ViewData("num") = num
                Return View(item)
            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return View()
            End Try

        End Function

        ''' <summary>
        ''' Add
        ''' </summary>
        ''' <returns></returns>
        Function AddNew(ByVal id As String) As ActionResult
            Try
                Dim dao = New DAOCustomers()
                Dim customers = dao.GetCustomers()
                If customers IsNot Nothing And customers.Count > 0 Then
                    Dim items As List(Of SelectListItem) = New List(Of SelectListItem)()
                    For Each customer As Customer In customers
                        items.Add(New SelectListItem With {.Text = customer.Name, .Value = customer.Id})
                    Next
                    ViewData("lstCustomer") = items
                End If
                'Dim item = dao.GetCustomerById(id)
                Dim num = DateTime.Now.ToString("yyyyddMMhhmmssfff")
                ViewData("num") = num
                Return View()
            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return View()
            End Try

        End Function

        ''' <summary>
        ''' Add
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <param name="amount"></param>
        ''' <param name="number"></param>
        ''' <param name="description"></param>
        ''' <returns></returns>
        <HttpPost()>
        Function Add(ByVal customerId As String, ByVal amount As String, ByVal number As String, ByVal description As String) As JsonResult
            Try
                Dim daoOrders = New DAOOrders()
                Dim id = daoOrders.AddOrderToCustomer(customerId, amount, number, description)
                If id > 0 Then
                    Return Json(New With {Key .number = 200, Key .message = "OK", Key .Id = id}, JsonRequestBehavior.AllowGet)
                Else
                    Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
                End If

            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        ''' <summary>
        ''' Edit
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        Function Edit(ByVal id As String) As ActionResult
            Try
                Dim dao = New DAOOrders
                Dim item = dao.GetOrdercustomerByOrderId(Convert.ToInt64(id))

                Return View(item)
            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return View()
            End Try
        End Function

        ''' <summary>
        ''' Edit
        ''' </summary>
        ''' <param name="id"></param>
        ''' <param name="amount"></param>
        ''' <param name="description"></param>
        ''' <returns></returns>
        <HttpPost()>
        Function Edit(ByVal id As String, ByVal amount As String, ByVal description As String) As JsonResult
            Try
                Dim dao = New DAOOrders
                If dao.EditOrder(id, amount, description) Then
                    Return Json(New With {Key .number = 200, Key .message = "OK"}, JsonRequestBehavior.AllowGet)
                Else
                    Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
                End If
            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try
        End Function

        ''' <summary>
        ''' Delete
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        <HttpPost()>
        Function Delete(ByVal id As String) As JsonResult
            Try
                Dim dao = New DAOOrders
                If dao.DeleteOrder(id) Then
                    Return Json(New With {Key .number = 200, Key .message = "OK"}, JsonRequestBehavior.AllowGet)
                Else
                    Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
                End If
            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try
        End Function



    End Class


End Namespace