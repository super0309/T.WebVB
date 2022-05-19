Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports WebApplication1.ZLChat.Common

Namespace Controllers
    Public Class CustomerController
        Inherits Controller

        ' GET: Customer
        ''' <summary>
        ''' Index
        ''' </summary>
        ''' <returns></returns>
        Function Index() As ActionResult
            Try

            Catch ex As Exception
                Utilities.WriteToLog(ex)
            End Try
            Return View()
        End Function

        ''' <summary>
        ''' Add
        ''' </summary>
        ''' <returns></returns>

        Function Add() As ActionResult
            Return View()
        End Function

        ''' <summary>
        ''' Add
        ''' </summary>
        ''' <param name="customer"></param>
        ''' <returns></returns>
        <HttpPost()>
        Function Add(ByVal name As String, ByVal address As String, ByVal description As String) As JsonResult

            Try
                Dim daoCus = New DAOCustomers()
                Dim id = daoCus.AddCustomer(name, address, description)

                If id > 0 Then
                    Return Json(New With {Key .number = 200, Key .message = "OK", Key .objects = id}, JsonRequestBehavior.AllowGet)
                Else
                    Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = id}, JsonRequestBehavior.AllowGet)
                End If



            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try
        End Function


        Function List(ByVal search As String) As JsonResult
            Try
                If search Is Nothing Then
                    search = String.Empty
                End If
                Dim daoCus = New DAOCustomers()
                Dim lst = daoCus.GetCustomers(search)

                Return Json(New With {Key .number = 200, Key .message = "OK", Key .objects = lst}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                Utilities.WriteToLog(ex)
                Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = vbNull}, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        Function PageList(ByVal id As String, ByVal pageNumber As Int32, ByVal pageSize As Int32, ByVal search As String) As JsonResult
            Try

            Catch ex As Exception
                Utilities.WriteToLog(ex)

            End Try
        End Function

        ''' <summary>
        ''' Edit
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        Public Function Edit(ByVal id As String) As ActionResult
            Try
                Dim daoCus = New DAOCustomers()
                Dim item = daoCus.GetCustomerById(Convert.ToInt64(id))
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
        ''' <returns></returns>

        <HttpPost()>
        Public Function Edit(ByVal id As String, ByVal name As String, ByVal address As String, ByVal description As String) As JsonResult
            Try
                Dim daoCus = New DAOCustomers()
                If daoCus.UpdateCustomer(Convert.ToInt64(id), name, address, description) Then
                    Return Json(New With {Key .number = 200, Key .message = "OK", Key .objects = id}, JsonRequestBehavior.AllowGet)
                Else
                    Return Json(New With {Key .number = 100, Key .message = "NO", Key .objects = id}, JsonRequestBehavior.AllowGet)
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
                Dim dao = New DAOCustomers

                If dao.Delete(id) Then
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