Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Configuration
Imports System.Diagnostics
Imports System.IO
Imports System.Xml

Namespace ZLChat.Common
    Public Class ErrorLog
        Protected Shared StrLogFilePath As String = String.Empty
        Protected Shared StrEventLogName As String = "ErrorSample"
        Protected Shared StrEventInforName As String = "LogInfor"
        Private Shared _sw As StreamWriter
        Public Delegate Sub LogUpdatedEventHandler()
        Public Shared Event LogUpdatedEvent As LogUpdatedEventHandler

        Private Shared Sub RaiseLogUpdatedEvent()
            RaiseEvent LogUpdatedEvent()
        End Sub

        Public Shared Property LogFilePath As String
            Set(ByVal value As String)
                StrLogFilePath = value
            End Set
            Get
                Return StrLogFilePath
            End Get
        End Property

        Public Shared Property EventLogName As String
            Set(ByVal value As String)
                StrEventLogName = value
            End Set
            Get
                Return StrEventLogName
            End Get
        End Property

        Public Shared Property EventLogInfor As String
            Set(ByVal value As String)
                StrEventInforName = value
            End Set
            Get
                Return StrEventInforName
            End Get
        End Property

        Public Shared Function ErrorRoutine(ByVal bLogType As Boolean, ByVal objException As Exception) As Boolean
            Try
                Dim bLoggingEnabled As Boolean
                bLoggingEnabled = CheckLoggingEnabled()
                If False = bLoggingEnabled Then Return True

                If bLogType Then
                    If Not EventLog.SourceExists(EventLogName) Then EventLog.CreateEventSource(objException.Message, EventLogName)
                    Dim log As EventLog = New EventLog()
                    log.Source = EventLogName
                    log.WriteEntry(objException.Message, EventLogEntryType.[Error])
                Else
                    If True <> CustomErrorRoutine(objException) Then Return False
                End If

                RaiseLogUpdatedEvent()
                Return True
            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Public Shared Function ErrorRoutinLogInfor(ByVal strMessage As String) As Boolean
            Try
                Dim bLoggingEnabled As Boolean
                bLoggingEnabled = CheckLoggingEnabled()
                If False = bLoggingEnabled Then Return True
                Dim strPathName As String = String.Empty

                If StrLogFilePath.Equals(String.Empty) Then
                    strPathName = GetLogFilePath()
                Else

                    If False = File.Exists(StrLogFilePath) Then
                        If False = CheckDirectory(StrLogFilePath) Then Return False
                        Dim fs As FileStream = New FileStream(StrLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                        fs.Close()
                    End If

                    strPathName = StrLogFilePath
                End If

                WriteErrorLog(strPathName, New Exception(strMessage), True)
                RaiseLogUpdatedEvent()
                Return True
            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Public Shared Function GetLogContents() As String
            Try
                Dim strFileContent As String = String.Empty
                Dim sr = New StreamReader(GetLogFilePath())
                sr.Read()
                strFileContent = sr.ReadToEnd()
                sr.Close()
                Return strFileContent
            Catch __unusedException1__ As Exception
                Return ""
            End Try
        End Function

        Private Shared Function CheckLoggingEnabled() As Boolean
            Dim strLoggingStatusConfig As String = String.Empty
            strLoggingStatusConfig = GetLoggingStatusConfigFileName()

            If strLoggingStatusConfig.Equals(String.Empty) Then
                Return True
            End If

            Dim bTemp As Boolean = GetValueFromXml(strLoggingStatusConfig)
            Return bTemp
        End Function

        Private Shared Function GetLoggingStatusConfigFileName() As String
            Dim strCheckinBaseDirecotry As String = AppDomain.CurrentDomain.BaseDirectory & "LoggingStatus.Config"
            If File.Exists(strCheckinBaseDirecotry) Then Return strCheckinBaseDirecotry
            Dim strCheckinApplicationDirecotry As String = GetApplicationPath() & "LoggingStatus.Config"

            If File.Exists(strCheckinApplicationDirecotry) Then
                Return strCheckinApplicationDirecotry
            Else
                Return String.Empty
            End If
        End Function

        Private Shared Function GetValueFromXml(ByVal strXmlPath As String) As Boolean
            Try
                Dim docIn As FileStream = New FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Dim contactDoc As XmlDocument = New XmlDocument()
                contactDoc.Load(docIn)
                Dim UserList As XmlNodeList = contactDoc.GetElementsByTagName("LoggingEnabled")
                Dim strGetValue As String = UserList.Item(0).InnerText.ToString()

                If strGetValue.Equals("0") Then
                    Return False
                ElseIf strGetValue.Equals("1") Then
                    Return True
                Else
                    Return False
                End If

            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Private Shared Function CustomErrorRoutine(ByVal objException As Exception) As Boolean
            Dim strPathName As String = String.Empty

            If StrLogFilePath.Equals(String.Empty) Then
                strPathName = GetLogFilePath()
            Else

                If False = File.Exists(StrLogFilePath) Then
                    If False = CheckDirectory(StrLogFilePath) Then Return False
                    Dim fs As FileStream = New FileStream(StrLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Close()
                End If

                strPathName = StrLogFilePath
            End If

            Dim bReturn As Boolean = True

            If True <> WriteErrorLog(strPathName, objException) Then
                bReturn = False
            End If

            Return bReturn
        End Function

        Public Shared Function ClearLog() As Boolean
            Dim sfilepath As String = GetLogFilePath()

            Try

                If File.Exists(sfilepath) Then
                    Dim sw As StreamWriter = New StreamWriter(sfilepath)
                    sw.Write(String.Empty)
                    sw.Close()
                    Return True
                End If

                Return False
            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Private Shared Function WriteErrorLog(ByVal strPathName As String, ByVal objException As Exception, ByVal Optional isLogInfor As Boolean = False) As Boolean
            Dim bReturn As Boolean = False
            Dim strException As String = String.Empty
            _sw = New StreamWriter(strPathName, True)

            If objException.StackTrace Is Nothing Then

                Try
                    _sw.WriteLine("")

                    If isLogInfor Then
                        _sw.WriteLine("Log Infor" & vbTab & vbTab & ": " & objException.Message.ToString().Trim())
                    Else
                        _sw.WriteLine("Error" & vbTab & vbTab & ": " & objException.Message.ToString().Trim())
                    End If

                    _sw.WriteLine("-------------------------------------------------------------------")
                    _sw.Flush()
                    _sw.Close()
                    bReturn = True
                Catch __unusedException1__ As Exception
                    _sw.Close()
                    bReturn = False
                End Try
            Else
                strException = objException.StackTrace.ToString().Trim()

                Try
                    _sw.WriteLine("Source" & vbTab & vbTab & ": " & objException.Source.ToString().Trim())
                    _sw.WriteLine("Method" & vbTab & vbTab & ": " & objException.TargetSite.Name.ToString())
                    _sw.WriteLine("Date" & vbTab & vbTab & ": " & DateTime.Now.ToLongTimeString())
                    _sw.WriteLine("Time" & vbTab & vbTab & ": " & DateTime.Now.ToShortDateString())
                    _sw.WriteLine("Error" & vbTab & vbTab & ": " & objException.Message.ToString().Trim())
                    _sw.WriteLine("Stack Trace" & vbTab & ": " & objException.StackTrace.ToString())
                    _sw.WriteLine("-------------------------------------------------------------------")
                    _sw.Flush()
                    _sw.Close()
                    bReturn = True
                Catch __unusedException1__ As Exception
                    _sw.Close()
                    bReturn = False
                End Try
            End If

            Return bReturn
        End Function

        Private Shared Function GetLogFilePath() As String
            Try
                Dim strLogFile As String = "LogFile" & DateTime.Now.ToString("ddMMyyyy") & ".txt"
                Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim retFilePath As String = Path.Combine(baseDir, "Log", strLogFile)
                Dim strPath As String = If(Not String.IsNullOrEmpty(retFilePath), Path.GetDirectoryName(retFilePath), String.Empty)
                If Not String.IsNullOrEmpty(strPath) AndAlso Not Directory.Exists(strPath) Then Directory.CreateDirectory(strPath)
                If File.Exists(retFilePath) Then Return retFilePath
                If False = CheckDirectory(retFilePath) Then Return String.Empty

                Using New FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    Return retFilePath
                End Using

            Catch __unusedException1__ As Exception
                Return String.Empty
            End Try
        End Function

        Private Shared Function CheckDirectory(ByVal strLogPath As String) As Boolean
            Try
                Dim nFindSlashPos As Integer = strLogPath.Trim().LastIndexOf("\")
                Dim strDirectoryname As String = strLogPath.Trim().Substring(0, nFindSlashPos)
                If False = Directory.Exists(strDirectoryname) Then Directory.CreateDirectory(strDirectoryname)
                Return True
            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Private Shared Function GetApplicationPath() As String
            Try
                Dim strBaseDirectory As String = AppDomain.CurrentDomain.BaseDirectory.ToString()
                Dim nFirstSlashPos As Integer = strBaseDirectory.LastIndexOf("\")
                Dim strTemp As String = String.Empty
                If 0 < nFirstSlashPos Then strTemp = strBaseDirectory.Substring(0, nFirstSlashPos)
                Dim nSecondSlashPos As Integer = strTemp.LastIndexOf("\")
                Dim strTempAppPath As String = String.Empty
                If 0 < nSecondSlashPos Then strTempAppPath = strTemp.Substring(0, nSecondSlashPos)
                Dim strAppPath As String = strTempAppPath.Replace("bin", "")
                Return strAppPath
            Catch __unusedException1__ As Exception
                Return String.Empty
            End Try
        End Function
    End Class
End Namespace
