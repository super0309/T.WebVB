Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Security
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports ZLChat.Entity

Namespace ZLChat.Common
    Module Utilities
        Public Delegate Sub LogUpdatedEventHandler()
        Public Event LogUpdatedEvent As LogUpdatedEventHandler

        Sub WriteToLog(ByVal exc As Exception)
            If Not exc.Message.Contains("The DELETE statement conflicted") Then
                ErrorLog.ErrorRoutine(False, exc)
                RaiseLogUpdatedEvent()
            End If
        End Sub

        Private Sub RaiseLogUpdatedEvent()
            RaiseEvent LogUpdatedEvent()
        End Sub

        Sub WriteLogInfor(ByVal strMessage As String)
            If Not String.IsNullOrEmpty(strMessage) Then
                ErrorLog.ErrorRoutinLogInfor(strMessage)
                RaiseLogUpdatedEvent()
            End If
        End Sub

        Function ComputeSHA256Hash(ByVal text As String) As String
            Using sha256 = New SHA256Managed()
                Return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "")
            End Using
        End Function

        Function IsPhoneNumber(ByVal number As String) As Boolean
            If number Is Nothing OrElse number.ToString() = String.Empty Then
                Return False
            End If

            Dim strNumber As String = number.ToString()
            Return Regex.Match(number, "^(\+?[0-9]{6,11})$").Success
        End Function

        Function IsName(ByVal name As String) As Boolean
            If name Is Nothing OrElse name.ToString() = String.Empty Then
                Return False
            End If

            Dim items = name.ToCharArray()

            For i As Integer = 0 To items.Length - 1
                Dim item = items(i)
                Dim num As Integer
                If Integer.TryParse(item.ToString(), num) AndAlso (num <= 0 OrElse num >= 0) Then Return False
            Next

            Return True
        End Function

        Function ToBase64(ByVal text As String) As String
            Dim bytes As Byte() = Encoding.ASCII.GetBytes(text)
            Return Convert.ToBase64String(bytes)
        End Function

        Function sha256(ByVal randomString As String) As String
            Dim crypt = New SHA256Managed()
            Dim hash As String = String.Empty
            Dim crypto As Byte() = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString))

            For Each theByte As Byte In crypto
                hash += theByte.ToString("x2")
            Next

            Return hash
        End Function

        Function EncryptToAcii(ByVal toEncrypt As String, ByVal strKey As String) As String
            If String.IsNullOrEmpty(toEncrypt) Then Return String.Empty
            Dim keyArray As Byte()
            Dim toEncryptArray As Byte() = UTF8Encoding.ASCII.GetBytes(toEncrypt)
            Dim useHashing As Boolean = True

            If useHashing Then
                Dim hashmd5 = New MD5CryptoServiceProvider()
                keyArray = hashmd5.ComputeHash(UTF8Encoding.ASCII.GetBytes(strKey))
                hashmd5.Clear()
            Else
                keyArray = UTF8Encoding.ASCII.GetBytes(strKey)
            End If

            Dim tdes = New TripleDESCryptoServiceProvider()
            tdes.Key = keyArray
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.ANSIX923
            Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
            tdes.Clear()
            Return Convert.ToBase64String(resultArray, 0, resultArray.Length)
        End Function

        Function Decrypt(ByVal cipherString As String, ByVal strKey As String) As String
            If String.IsNullOrEmpty(cipherString) Then Return String.Empty
            Dim keyArray As Byte()
            Dim toEncryptArray As Byte() = Convert.FromBase64String(cipherString)
            Dim useHashing As Boolean = True

            If useHashing Then
                Dim hashmd5 = New MD5CryptoServiceProvider()
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strKey))
                hashmd5.Clear()
            Else
                keyArray = UTF8Encoding.UTF8.GetBytes(strKey)
            End If

            Dim tdes = New TripleDESCryptoServiceProvider()
            tdes.Key = keyArray
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
            tdes.Clear()
            Return UTF8Encoding.UTF8.GetString(resultArray)
        End Function

        Function DecryptFromAcii(ByVal cipherString As String, ByVal strKey As String) As String
            If String.IsNullOrEmpty(cipherString) Then Return String.Empty
            Dim keyArray As Byte()
            Dim toEncryptArray As Byte() = Convert.FromBase64String(cipherString)
            Dim useHashing As Boolean = True

            If useHashing Then
                Dim hashmd5 = New MD5CryptoServiceProvider()
                keyArray = hashmd5.ComputeHash(UTF8Encoding.ASCII.GetBytes(strKey))
                hashmd5.Clear()
            Else
                keyArray = UTF8Encoding.ASCII.GetBytes(strKey)
            End If

            Dim tdes = New TripleDESCryptoServiceProvider()
            tdes.Key = keyArray
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.ANSIX923
            Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
            tdes.Clear()
            Return UTF8Encoding.ASCII.GetString(resultArray)
        End Function

        Private Function CheckHttp(ByRef httpWebResponse As HttpWebResponse, ByVal Optional user As Object = Nothing) As Boolean
            Return CheckStatus(httpWebResponse.StatusCode)
        End Function

        Function CheckHttp(ByRef httpResponse As HttpResponseMessage, ByVal Optional user As Object = Nothing) As Boolean
            Return CheckStatus(httpResponse.StatusCode)
        End Function

        Function CheckStatus(ByVal code As HttpStatusCode, ByVal Optional user As Object = Nothing) As Boolean
            Select Case code
                Case System.Net.HttpStatusCode.[Continue]
                    Return False
                Case System.Net.HttpStatusCode.SwitchingProtocols
                    Return False
                Case System.Net.HttpStatusCode.OK
                    Return True
                Case System.Net.HttpStatusCode.Created
                    Return True
                Case System.Net.HttpStatusCode.Accepted
                    Return False
                Case System.Net.HttpStatusCode.NonAuthoritativeInformation
                    Return False
                Case System.Net.HttpStatusCode.NoContent
                    Return True
                Case System.Net.HttpStatusCode.ResetContent
                    Return False
                Case System.Net.HttpStatusCode.PartialContent
                    Return False
                Case System.Net.HttpStatusCode.MultipleChoices
                    Return False
                Case System.Net.HttpStatusCode.MovedPermanently
                    Return False
                Case System.Net.HttpStatusCode.Found
                    Return False
                Case System.Net.HttpStatusCode.SeeOther
                    Return False
                Case System.Net.HttpStatusCode.NotModified
                    Return False
                Case System.Net.HttpStatusCode.UseProxy
                    Return False
                Case System.Net.HttpStatusCode.Unused
                    Return False
                Case System.Net.HttpStatusCode.TemporaryRedirect
                    Return False
                Case System.Net.HttpStatusCode.BadRequest
                    Return False
                Case System.Net.HttpStatusCode.Unauthorized
                    Return False
                Case System.Net.HttpStatusCode.PaymentRequired
                    Return False
                Case System.Net.HttpStatusCode.Forbidden
                    Return False
                Case System.Net.HttpStatusCode.NotFound
                    Return False
                Case System.Net.HttpStatusCode.MethodNotAllowed
                    Return False
                Case System.Net.HttpStatusCode.NotAcceptable
                    Return False
                Case System.Net.HttpStatusCode.ProxyAuthenticationRequired
                    Return False
                Case System.Net.HttpStatusCode.RequestTimeout
                    Return False
                Case System.Net.HttpStatusCode.Conflict
                    Return False
                Case System.Net.HttpStatusCode.Gone
                    Return False
                Case System.Net.HttpStatusCode.LengthRequired
                    Return False
                Case System.Net.HttpStatusCode.PreconditionFailed
                    Return False
                Case System.Net.HttpStatusCode.RequestEntityTooLarge
                    Return False
                Case System.Net.HttpStatusCode.RequestUriTooLong
                    Return False
                Case System.Net.HttpStatusCode.UnsupportedMediaType
                    Return False
                Case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable
                    Return False
                Case System.Net.HttpStatusCode.ExpectationFailed
                    Return False
                Case System.Net.HttpStatusCode.UpgradeRequired
                    Return False
                Case System.Net.HttpStatusCode.InternalServerError
                    Return False
                Case System.Net.HttpStatusCode.NotImplemented
                    Return False
                Case System.Net.HttpStatusCode.BadGateway
                    Return False
                Case System.Net.HttpStatusCode.ServiceUnavailable
                    Return False
                Case System.Net.HttpStatusCode.GatewayTimeout
                    Return False
                Case System.Net.HttpStatusCode.HttpVersionNotSupported
                    Return False
            End Select

            Return False
        End Function

        Sub HttpsTrustRequest()
            Try
                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3
                ServicePointManager.ServerCertificateValidationCallback = Function(ByVal s As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) True
            Catch ex As Exception
                Utilities.WriteToLog(ex)
            End Try
        End Sub






    End Module
End Namespace
