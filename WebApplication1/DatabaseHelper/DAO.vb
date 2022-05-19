Imports System.Data.SqlClient
Imports WebApplication1.ZLChat.Common

Public Class BaseDAO

    Public Shared Function GetDataset(ByVal procedure As String,
                                      ByVal Optional parms As Dictionary(Of String, Object) = Nothing,
                                      ByVal Optional commandType As CommandType = CommandType.StoredProcedure) As DataSet
        Dim connetionString As String
        Dim ds As New DataSet
        Dim sql As String

        connetionString = "Data Source=123.16.55.62;Initial Catalog=TWeb;User ID=tuser;Password=11"
        sql = procedure

        Try
            Using connection As New SqlConnection(connetionString)
                connection.Open()
                Using command As New SqlCommand(sql, connection)
                    If parms IsNot Nothing And parms.Count() > 0 Then

                        For Each item As KeyValuePair(Of String, Object) In parms
                            'Dim type = TypeCode.GetTypeCode(item.Value.GetType())
                            command.Parameters.AddWithValue(item.Key, item.Value)
                        Next
                    End If

                    Using adapter As New SqlDataAdapter(command)
                        adapter.SelectCommand = command
                        adapter.Fill(ds)
                    End Using
                End Using
                connection.Close()
            End Using
            Return ds

        Catch ex As Exception
            Utilities.WriteToLog(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataTable(ByVal procedure As String, ByVal cmdType As CommandType, ByVal cmdParams As Dictionary(Of String, Object)) As DataTable
        Dim dataTable = New DataTable()


        Try

            Using conn = New SqlConnection()

                Dim sqlsb = ConfigurationManager.AppSettings("ConnectDB")

                If sqlsb IsNot Nothing Then
                    conn.ConnectionString = sqlsb
                    conn.Open()
                    Try

                        Using comm = New SqlCommand()
                            comm.Connection = conn
                            comm.CommandText = procedure

                            comm.CommandType = cmdType
                            comm.CommandTimeout = 360

                            If cmdParams IsNot Nothing Then

                                For Each cmdParam In cmdParams
                                    comm.Parameters.AddWithValue(cmdParam.Key, cmdParam.Value)
                                Next
                            End If

                            Using adapter = New SqlDataAdapter(comm)
                                adapter.Fill(dataTable)
                                Return dataTable
                            End Using
                        End Using

                    Catch ex As Exception
                        Utilities.WriteToLog(ex)
                    End Try
                End If
            End Using

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return Nothing
    End Function

    Public Shared Function Insert(ByVal procedure As String, ByVal cmdType As CommandType, ByVal cmdParams As Dictionary(Of String, Object)) As Integer
        Dim dataTable = New DataTable()


        Try

            Using conn = New SqlConnection()

                Dim sqlsb = ConfigurationManager.AppSettings("ConnectDB")

                If sqlsb IsNot Nothing Then
                    conn.ConnectionString = sqlsb
                    conn.Open()
                    Try

                        Using comm = New SqlCommand()
                            comm.Connection = conn
                            comm.CommandText = procedure

                            comm.CommandType = cmdType
                            comm.CommandTimeout = 360

                            If cmdParams IsNot Nothing Then

                                For Each cmdParam In cmdParams
                                    comm.Parameters.AddWithValue(cmdParam.Key, cmdParam.Value)
                                Next
                            End If

                            Using adapter = New SqlDataAdapter(comm)
                                adapter.Fill(dataTable)

                            End Using
                        End Using

                        If dataTable IsNot Nothing And dataTable.Rows.Count > 0 Then
                            Dim id = Convert.ToInt32(dataTable.Rows(0).Item("Id"))
                            Return id
                        End If
                    Catch ex As Exception
                        Utilities.WriteToLog(ex)
                        Return 0
                    End Try
                End If
            End Using

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return Nothing
    End Function



    Public Shared Function Update(ByVal procedure As String, ByVal cmdType As CommandType, ByVal cmdParams As Dictionary(Of String, Object)) As Boolean
        Dim dataTable = New DataTable()


        Try

            Using conn = New SqlConnection()

                Dim sqlsb = ConfigurationManager.AppSettings("ConnectDB")

                If sqlsb IsNot Nothing Then
                    conn.ConnectionString = sqlsb
                    conn.Open()
                    Try

                        Using comm = New SqlCommand()
                            comm.Connection = conn
                            comm.CommandText = procedure

                            comm.CommandType = cmdType
                            comm.CommandTimeout = 360

                            If cmdParams IsNot Nothing Then

                                For Each cmdParam In cmdParams
                                    comm.Parameters.AddWithValue(cmdParam.Key, cmdParam.Value)
                                Next
                            End If

                            Using adapter = New SqlDataAdapter(comm)
                                adapter.Fill(dataTable)

                            End Using
                        End Using

                        If dataTable IsNot Nothing And dataTable.Rows.Count > 0 Then
                            Dim value = Convert.ToInt32(dataTable.Rows(0).Item("value"))
                            Return value > 0
                        End If
                    Catch ex As Exception
                        Utilities.WriteToLog(ex)
                        Return False
                    End Try
                End If
            End Using

        Catch ex As Exception
            Utilities.WriteToLog(ex)
        End Try

        Return Nothing
    End Function
End Class
