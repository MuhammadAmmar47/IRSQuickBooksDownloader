Imports system.Web
Imports System.Text
Imports System.Net
Imports System.IO

Namespace Payments
    Public Class PaymentTerminal
#Region " Properties "

        Private _PaymentProcessorID As String = ""

        Private _AddressLine1 As String = ""
        Private _AddressLine2 As String = ""
        Private _Amount As Decimal = 0.0
        Private _AVSResponseCode As String = ""
        Private _City As String = ""
        Private _CompanyName As String = ""

        Private _Country As String = ""

        Private _CreditCardHolderName As String = ""
        Private _CreditCardExpirationMonth As Integer = 1
        Private _CreditCardExpirationYear As Integer = 2003
        Private _CreditCardNumber As String = ""
        Private _CreditCardType As CreditCardType = CreditCardType.Visa
        Private _CreditCardVerificationCode As String = ""
        Private _CurrencyCode As String = "USD"
        Private _EmailAddress As String = ""
        Private _FirstName As String = ""
        Private _LastName As String = ""
        Private _MerchantOrderNumber As String = ""
        Private _MiddleInitial As String = ""
        Private _OrderNumber As String = ""
        Private _OriginalTransactionID As String = ""
        Private _PhoneNumber As String = ""
        Private _PostalCode As String = ""
        Private _Region As String = ""
        Private _RequireVerificationCode As Boolean = False
        Private _ResponseCode As Response_Code = Response_Code.NONE
        Private _ResponseMessage As String = ""
        Private _ResponseTransactionID As String = ""
        Private _ShipFirstName As String = ""
        Private _ShipLastName As String = ""
        Private _ShipMiddleInitial As String = ""
        Private _ShipCompanyName As String = ""
        Private _ShipAddressLine1 As String = ""
        Private _ShipAddressLine2 As String = ""
        Private _ShipCity As String = ""
        Private _ShipRegion As String = ""
        Private _ShipPostalCode As String = ""
        Private _ShipCountry As String = ""
        Private _UseAVS As Boolean = False
        Private _FullRequest As String = ""
        Private _FullResponse As String = ""
        Private _CardID As Integer = 0
        Private _customerid As Integer = 0

        Private _ABARoutingNumber As String = ""
        Private _AccountNumber As String = ""
        Private _BankAccountType As BankAccountType
        Private _BankAccountTitle As String = ""
        Private _BankName As String = ""
        Private _BankAccountID As Integer


        Public Property BankAccountId() As Integer
            Get
                Return _BankAccountID
            End Get
            Set(ByVal value As Integer)
                _BankAccountID = value
            End Set
        End Property

        Public Property BankName() As String
            Get
                Return _BankName
            End Get
            Set(ByVal value As String)
                _BankName = value
            End Set
        End Property
        Public Property BankAccountTitle() As String
            Get
                Return _BankAccountTitle
            End Get
            Set(ByVal value As String)
                _BankAccountTitle = value
            End Set
        End Property
        Public Property BankAccountType() As BankAccountType
            Get
                Return _BankAccountType
            End Get
            Set(ByVal value As BankAccountType)
                _BankAccountType = value
            End Set
        End Property
        Public Property BankAccountNumber() As String
            Get
                Return _AccountNumber
            End Get
            Set(ByVal value As String)
                _AccountNumber = value
            End Set
        End Property
        Public Property BankRoutingNumber() As String
            Get
                Return _ABARoutingNumber
            End Get
            Set(ByVal value As String)
                _ABARoutingNumber = value
            End Set
        End Property

        'Private Function GetSetting(ByVal settingName As String) As String
        '    Return System.Configuration.ConfigurationManager.AppSettings(settingName)
        'End Function
        Private ReadOnly Property LogEnabled() As Boolean
            Get
                Return True
            End Get
        End Property

        Public Property CustomerId() As Integer
            Get
                Return _customerid
            End Get
            Set(ByVal Value As Integer)
                _customerid = Value
            End Set
        End Property
        Public Property CreditCardId() As Integer
            Get
                Return _CardID
            End Get
            Set(ByVal Value As Integer)
                _CardID = Value
            End Set
        End Property
        Public Property AddressLine1() As String
            Get
                Return _AddressLine1
            End Get
            Set(ByVal Value As String)
                _AddressLine1 = Value
            End Set
        End Property
        Public Property AddressLine2() As String
            Get
                Return _AddressLine2
            End Get
            Set(ByVal Value As String)
                _AddressLine2 = Value
            End Set
        End Property
        Public Property Amount() As Decimal
            Get
                Return _Amount
            End Get
            Set(ByVal Value As Decimal)
                _Amount = Value
            End Set
        End Property
        Public Property AVSResponseCode() As String
            Get
                Return _AVSResponseCode
            End Get
            Set(ByVal Value As String)
                _AVSResponseCode = Value
            End Set
        End Property
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal Value As String)
                _City = Value
            End Set
        End Property
        Public Property CompanyName() As String
            Get
                Return _CompanyName
            End Get
            Set(ByVal Value As String)
                _CompanyName = Value
            End Set
        End Property
        Public Property Country() As String
            Get
                Return _Country
            End Get
            Set(ByVal Value As String)
                _Country = Value
            End Set
        End Property
        Public Property CreditCardHolderName() As String
            Get
                Return _CreditCardHolderName
            End Get
            Set(ByVal Value As String)
                _CreditCardHolderName = Value
            End Set
        End Property
        Public Property CreditCardExpirationMonth() As Integer
            Get
                Return _CreditCardExpirationMonth
            End Get
            Set(ByVal Value As Integer)
                _CreditCardExpirationMonth = Value
            End Set
        End Property
        Public Property CreditCardExpirationYear() As Integer
            Get
                Return _CreditCardExpirationYear
            End Get
            Set(ByVal Value As Integer)
                _CreditCardExpirationYear = Value
            End Set
        End Property
        Public Property CreditCardNumber() As String
            Get
                Return _CreditCardNumber
            End Get
            Set(ByVal Value As String)
                _CreditCardNumber = Value
            End Set
        End Property
        Public Property CardType() As CreditCardType
            Get
                Return _CreditCardType
            End Get
            Set(ByVal Value As CreditCardType)
                _CreditCardType = Value
            End Set
        End Property
        Public Property CreditCardVerificationCode() As String
            Get
                Return _CreditCardVerificationCode
            End Get
            Set(ByVal Value As String)
                _CreditCardVerificationCode = Value
            End Set
        End Property
        Public Property CurrencyCode() As String
            Get
                Return _CurrencyCode
            End Get
            Set(ByVal Value As String)
                _CurrencyCode = Value
            End Set
        End Property
        Public Property EmailAddress() As String
            Get
                Return _EmailAddress
            End Get
            Set(ByVal Value As String)
                _EmailAddress = Value
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal Value As String)
                _FirstName = Value
            End Set
        End Property
        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal Value As String)
                _LastName = Value
            End Set
        End Property
        Public Property MerchantOrderNumber() As String
            Get
                Return _MerchantOrderNumber
            End Get
            Set(ByVal Value As String)
                _MerchantOrderNumber = Value
            End Set
        End Property
        Public Property MiddleInitial() As String
            Get
                Return _MiddleInitial
            End Get
            Set(ByVal Value As String)
                _MiddleInitial = Value
            End Set
        End Property
        Public Property OrderNumber() As String
            Get
                Return _OrderNumber
            End Get
            Set(ByVal Value As String)
                _OrderNumber = Value
            End Set
        End Property
        Public Property OriginalTransactionID() As String
            Get
                Return _OriginalTransactionID
            End Get
            Set(ByVal Value As String)
                _OriginalTransactionID = Value
            End Set
        End Property
        Public ReadOnly Property PaymentProcessorID() As String
            Get
                Return _PaymentProcessorID
            End Get
        End Property
        Public Property PhoneNumber() As String
            Get
                Return _PhoneNumber
            End Get
            Set(ByVal Value As String)
                _PhoneNumber = Value
            End Set
        End Property
        Public Property PostalCode() As String
            Get
                Return _PostalCode
            End Get
            Set(ByVal Value As String)
                _PostalCode = Value
            End Set
        End Property
        Public Property Region() As String
            Get
                Return _Region
            End Get
            Set(ByVal Value As String)
                _Region = Value
            End Set
        End Property
        Public Property RequireVerificationCode() As Boolean
            Get
                Return _RequireVerificationCode
            End Get
            Set(ByVal Value As Boolean)
                _RequireVerificationCode = Value
            End Set
        End Property
        Public Property ResponseCode() As Response_Code
            Get
                Return _ResponseCode
            End Get
            Set(ByVal Value As Response_Code)
                _ResponseCode = Value
            End Set
        End Property
        Public Property ResponseMessage() As String
            Get
                Return _ResponseMessage
            End Get
            Set(ByVal Value As String)
                _ResponseMessage = Value
            End Set
        End Property
        Public Property ResponseTransactionID() As String
            Get
                Return _ResponseTransactionID
            End Get
            Set(ByVal Value As String)
                _ResponseTransactionID = Value
            End Set
        End Property
        Public Property ShipFirstName() As String
            Get
                Return _ShipFirstName
            End Get
            Set(ByVal Value As String)
                _ShipFirstName = Value
            End Set
        End Property
        Public Property ShipMiddleInitial() As String
            Get
                Return _ShipMiddleInitial
            End Get
            Set(ByVal Value As String)
                _ShipMiddleInitial = Value
            End Set
        End Property
        Public Property ShipLastName() As String
            Get
                Return _ShipLastName
            End Get
            Set(ByVal Value As String)
                _ShipLastName = Value
            End Set
        End Property
        Public Property ShipCompanyName() As String
            Get
                Return _ShipCompanyName
            End Get
            Set(ByVal Value As String)
                _ShipCompanyName = Value
            End Set
        End Property
        Public Property ShipAddressLine1() As String
            Get
                Return _ShipAddressLine1
            End Get
            Set(ByVal Value As String)
                _ShipAddressLine1 = Value
            End Set
        End Property
        Public Property ShipAddressLine2() As String
            Get
                Return _ShipAddressLine2
            End Get
            Set(ByVal Value As String)
                _ShipAddressLine2 = Value
            End Set
        End Property
        Public Property ShipCity() As String
            Get
                Return _ShipCity
            End Get
            Set(ByVal Value As String)
                _ShipCity = Value
            End Set
        End Property
        Public Property ShipRegion() As String
            Get
                Return _ShipRegion
            End Get
            Set(ByVal Value As String)
                _ShipRegion = Value
            End Set
        End Property
        Public Property ShipPostalCode() As String
            Get
                Return _ShipPostalCode
            End Get
            Set(ByVal Value As String)
                _ShipPostalCode = Value
            End Set
        End Property
        Public Property ShipCountry() As String
            Get
                Return _ShipCountry
            End Get
            Set(ByVal Value As String)
                _ShipCountry = Value
            End Set
        End Property
        Public Property UseAVS() As Boolean
            Get
                Return _UseAVS
            End Get
            Set(ByVal Value As Boolean)
                _UseAVS = Value
            End Set
        End Property
        Public Property FullRequest() As String
            Get
                Return _FullRequest
            End Get
            Set(ByVal value As String)
                _FullRequest = value
            End Set
        End Property
        Public Property FullResponse() As String
            Get
                Return _FullResponse
            End Get
            Set(ByVal value As String)
                _FullResponse = value
            End Set
        End Property
#End Region

#Region " Methods "

        Public Sub New()
        End Sub

#Region "Charging Methods"
        Public Function Charge() As Boolean
            Dim bRet As Boolean = False

            Try
                bRet = ProcessCreditCard(Charge_Type.Charge)
            Catch ex As Exception
                bRet = False
                Throw New ArgumentException("Charge Failed", ex)
            End Try

            Return bRet
        End Function
        Public Function Authorize() As Boolean
            Dim bRet As Boolean = False


            Try
                bRet = ProcessCreditCard(Charge_Type.Authorize)
            Catch ex As Exception
                bRet = False
                Throw New ArgumentException("Authorize Failed", ex)
            End Try

            Return bRet
        End Function
        Public Function PostAuthorize() As Boolean
            Dim bRet As Boolean = False

            Try
                bRet = ProcessCreditCard(Charge_Type.Post_Authorize)
            Catch ex As Exception
                bRet = False
                Throw New ArgumentException("Post Authorize Failed", ex)
            End Try
            Return bRet
        End Function
        Public Function Void() As Boolean
            Dim bRet As Boolean = False

            Try
                bRet = ProcessCreditCard(Charge_Type.Void)
            Catch ex As Exception
                bRet = False
                Throw New ArgumentException("Void Failed", ex)
            End Try

            Return bRet
        End Function
        Public Function Refund() As Boolean
            Dim bRet As Boolean = False

            Try
                bRet = ProcessCreditCard(Charge_Type.Refund)
            Catch ex As Exception
                bRet = False
                Throw New ArgumentException("Refund Failed", ex)
            End Try

            Return bRet
        End Function

        Private Function ProcessCreditCard(ByVal processType As Charge_Type) As Boolean
            Dim result As Boolean = False
            Try
                Dim sExpDate As String = ""
                If CType(CreditCardExpirationMonth, String).Length < 2 Then
                    sExpDate = "0" & CreditCardExpirationMonth
                Else
                    sExpDate = CreditCardExpirationMonth
                End If
                If CType(CreditCardExpirationYear, String).Length > 2 Then
                    sExpDate += CType(CreditCardExpirationYear, String).Substring(2, 2)
                Else
                    sExpDate += CreditCardExpirationYear
                End If

                ' Set Parameters
                Dim sb As New StringBuilder
                Dim sPostData As String = ""

                sb.Append("x_version=3.1")

                sb.Append("&x_login=")
                sb.Append(HttpContext.Current.Server.UrlEncode(AppSettings.AuthorizeLoginID))

                sb.Append("&x_tran_key=")
                sb.Append(HttpContext.Current.Server.UrlEncode(AppSettings.Authorize_Trans_Key))

                sb.Append("&x_Amount=")
                sb.Append(HttpContext.Current.Server.UrlEncode(Amount))

                If CustomerId > 0 Then
                    sb.Append("&x_Cust_ID=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(CustomerId))
                End If


                'If EmailAddress.Trim <> "" Then
                '    sb.Append("&x_Cust_ID=")
                '    sb.Append(HttpContext.Current.Server.UrlEncode(EmailAddress))
                'End If

                If OrderNumber.Trim <> "" Then
                    sb.Append("&x_Description=")
                    sb.Append(HttpContext.Current.Server.UrlEncode("Order " & OrderNumber))
                End If
                sb.Append("&x_Email_Customer=")
                sb.Append(HttpContext.Current.Server.UrlEncode(AppSettings.AuthorizeEmailCustomer))

                sb.Append("&x_delim_data=")
                sb.Append(HttpContext.Current.Server.UrlEncode("TRUE"))

                sb.Append("&x_ADC_URL=")
                sb.Append(HttpContext.Current.Server.UrlEncode("FALSE"))

                sb.Append("&x_delim_char=")
                sb.Append(HttpContext.Current.Server.UrlEncode(","))

                sb.Append("&x_relay_response=")
                sb.Append(HttpContext.Current.Server.UrlEncode("FALSE"))

                If EmailAddress.Trim <> "" Then
                    sb.Append("&x_Email=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(EmailAddress))
                End If

                sb.Append("&x_Customer_IP=")
                sb.Append(HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.UserHostAddress))

                If FirstName.Trim <> "" Then
                    sb.Append("&x_First_Name=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(FirstName))
                End If

                If LastName.Trim <> "" Then
                    sb.Append("&x_Last_Name=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(LastName))
                End If
                If CompanyName.Trim <> "" Then
                    sb.Append("&x_Company=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(CompanyName))
                End If

                If AddressLine1.Trim <> "" Then
                    sb.Append("&x_Address=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(AddressLine1))
                End If

                If City.Trim <> "" Then
                    sb.Append("&x_City=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(City))
                End If

                If Country.Trim <> "" Then
                    sb.Append("&x_Country=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(Country.Substring(Country.Length - 2, 2)))
                End If
                If Region.Trim <> "" Then
                    sb.Append("&x_State=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(Region))
                End If

                If PostalCode.Trim <> "" Then
                    sb.Append("&x_Zip=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(PostalCode))
                End If

                If PhoneNumber.Trim <> "" Then
                    sb.Append("&x_Phone=")
                    sb.Append(HttpContext.Current.Server.UrlEncode(PhoneNumber))
                End If

                sb.Append("&x_Method=")
                sb.Append(HttpContext.Current.Server.UrlEncode("CC"))

                ' Add Test Mode Flag if needed
                Dim sURL As String = AppSettings.Authorize_TransURL
                If AppSettings.Authorize_TestMode Then
                    'sb.Append(("&x_test_request=TRUE"))
                    sb.Append(HttpContext.Current.Server.UrlEncode("&x_test_request=TRUE"))
                Else
                    sb.Append(("&x_test_request=FALSE"))
                End If

                Select Case processType
                    Case Charge_Type.Charge
                        ' Sale / Charge
                        sb.Append("&x_Type=")
                        sb.Append(HttpContext.Current.Server.UrlEncode("AUTH_CAPTURE"))
                        sb.Append("&x_Card_Num=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardNumber))
                        If CreditCardVerificationCode.Length() > 0 Then
                            sb.Append("&x_Card_Code=")
                            sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardVerificationCode))
                        End If
                        sb.Append("&x_Exp_Date=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(sExpDate))

                    Case Charge_Type.Authorize
                        ' Pre Auth
                        sb.Append("&x_Type=")
                        sb.Append(HttpContext.Current.Server.UrlEncode("AUTH_ONLY"))
                        sb.Append("&x_Card_Num=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardNumber))
                        If CreditCardVerificationCode.Length() > 0 Then
                            sb.Append("&x_Card_Code=")
                            sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardVerificationCode))
                        End If
                        sb.Append("&x_Exp_Date=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(sExpDate))

                    Case Charge_Type.Post_Authorize
                        ' Post Auth
                        sb.Append("&x_Type=")
                        sb.Append(HttpContext.Current.Server.UrlEncode("PRIOR_AUTH_CAPTURE"))
                        sb.Append("&x_trans_id=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(OriginalTransactionID))
                        sb.Append("&x_Card_Num=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardNumber))
                        If CreditCardVerificationCode.Length() > 0 Then
                            sb.Append("&x_Card_Code=")
                            sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardVerificationCode))
                        End If
                        sb.Append("&x_Exp_Date=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(sExpDate))

                    Case Charge_Type.Void
                        ' Void Transaction
                        sb.Append("&x_Type=")
                        sb.Append(HttpContext.Current.Server.UrlEncode("VOID"))
                        sb.Append("&x_Trans_ID=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(OriginalTransactionID))
                        sb.Append("&x_Card_Num=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardNumber))
                        If CreditCardVerificationCode.Length() > 0 Then
                            sb.Append("&x_Card_Code=")
                            sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardVerificationCode))
                        End If
                        sb.Append("&x_Exp_Date=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(sExpDate))


                    Case Charge_Type.Refund
                        ' Credit
                        sb.Append("&x_Type=")
                        sb.Append(HttpContext.Current.Server.UrlEncode("CREDIT"))
                        sb.Append("&x_Trans_ID=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(OriginalTransactionID))
                        sb.Append("&x_Card_Num=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardNumber))
                        If CreditCardVerificationCode.Length() > 0 Then
                            sb.Append("&x_Card_Code=")
                            sb.Append(HttpContext.Current.Server.UrlEncode(CreditCardVerificationCode))
                        End If
                        sb.Append("&x_Exp_Date=")
                        sb.Append(HttpContext.Current.Server.UrlEncode(sExpDate))

                End Select

                sPostData = sb.ToString

                HttpContext.Current.Trace.Write("postdata=" & sPostData)

                Me.FullRequest = sPostData

                Dim sResponse As String = ""
                sResponse = ReadHtmlPagePost(sURL, sPostData)

                Me.FullResponse = sResponse

                HttpContext.Current.Trace.Write("response=" & sResponse)

                ' Split response string
                Dim sOutput() As String
                sOutput = sResponse.Split(",")

                Dim iCounter As Integer = 0
                Dim htVars As Hashtable = New Hashtable
                Dim sVar As String = ""

                ' Move strings into hash table for easy reference
                For Each sVar In sOutput
                    htVars.Add(iCounter, sVar)
                    iCounter += 1
                Next

                If htVars.Count < 7 Then
                    result = False
                Else
                    Dim sResponseCode As String = htVars(0)
                    Dim sResponseDescription As String = htVars(3)
                    Dim sResponseAuthCode As String = htVars(4)
                    Dim sResponseAVSCode As String = htVars(5)
                    Dim sTransRef As String = htVars(6)

                    Dim sCVVResponse As String = ""
                    If htVars.Count > 38 Then
                        sCVVResponse = htVars(38)
                    End If

                    ' Trim off Extra Quotes on response codes
                    sResponseCode = sResponseCode.Trim("""")

                    AVSResponseCode = sResponseAVSCode

                    Select Case sResponseCode
                        Case "1"
                            ' Approved
                            result = True
                            ResponseCode = Response_Code.APPROVED
                            ResponseTransactionID = sTransRef
                            ResponseMessage = sResponseDescription
                        Case "2"
                            ' Declined
                            result = False
                            ResponseCode = Response_Code.DECLINED
                            ResponseTransactionID = sTransRef
                            ResponseMessage = "Transaction Declined: " & sResponseDescription


                        Case "3"
                            result = False
                            ResponseCode = Response_Code.ERROR
                            ResponseMessage = "Authorize.Net Processing Error: "
                            ResponseMessage += sResponseDescription
                    End Select


                    ' AVS
                    Select Case sResponseAVSCode
                        Case "A"
                            ResponseMessage += " [AVS - Address (Street) matches, ZIP does not]"
                        Case "B"
                            ResponseMessage += " [AVS - Address information not provided for AVS check]"
                        Case "E"
                            ResponseMessage += " [AVS - Error]"
                        Case "G"
                            ResponseMessage += " [AVS - Non-U.S. Card Issuing Bank]"
                        Case "N"
                            ResponseMessage += " [AVS - No Match on Address (Street) or ZIP]"
                        Case "P"
                            ResponseMessage += " [AVS - AVS not applicable for this transaction]"
                        Case "R"
                            ResponseMessage += " [AVS - Retry – System unavailable or timed out]"
                        Case "S"
                            ResponseMessage += " [AVS - Service not supported by issuer]"
                        Case "U"
                            ResponseMessage += " [AVS - Address information is unavailable]"
                        Case "W"
                            ResponseMessage += " [AVS - 9 digit ZIP matches, Address (Street) does not]"
                        Case "X"
                            ResponseMessage += " [AVS - Address (Street) and 9 digit ZIP match]"
                        Case "Y"
                            ResponseMessage += " [AVS - Address (Street) and 5 digit ZIP match]"
                        Case "Z"
                            ResponseMessage += " [AVS - 5 digit ZIP matches, Address (Street) does not]"
                    End Select

                    ' CVV
                    Select Case sCVVResponse
                        Case "M"
                            ResponseMessage += " [CVV - Match]"
                        Case "N"
                            ResponseMessage += " [CVV - No Match]"
                        Case "P"
                            ResponseMessage += " [CVV - Not Processed]"
                        Case "S"
                            ResponseMessage += " [CVV - Should have been present]"
                        Case "U"
                            ResponseMessage += " [CVV - Issuer unable to process request]"
                    End Select
                End If


            Catch Ex As Exception
                result = False
                ResponseMessage = "An Unknown Payment Error Occred: " & Ex.Message & " " & Ex.StackTrace
            End Try

            Return result
        End Function

        Private Function ReadHtmlPagePost(ByVal sURL As String, ByVal sPostData As String) As String
            Dim objResponse As WebResponse
            Dim objRequest As WebRequest
            Dim result As String = ""
            Dim myWriter As StreamWriter = Nothing

            Try
                objRequest = WebRequest.Create(sURL)
                objRequest.Method = "POST"
                objRequest.ContentLength = sPostData.Length()
                objRequest.ContentType = "application/x-www-form-urlencoded"
            Catch E As Exception
                Debug.Write("URL=" & sURL)
                Debug.Write("PostData=" & sPostData)
                Debug.Write("Error: " & E.Message)
                Throw New ArgumentException("Error Creating Web Request: " & E.Message)
            End Try


            Try
                myWriter = New StreamWriter(objRequest.GetRequestStream())
                myWriter.Write(sPostData)

            Catch Ex As Exception
                Debug.Write(Ex.Message)
                Throw New ArgumentException("Error Posting Data: " & Ex.Message)
            Finally
                myWriter.Close()
            End Try


            objResponse = objRequest.GetResponse()
            Dim sr As New StreamReader(objResponse.GetResponseStream())
            result += sr.ReadToEnd()

            'clean up StreamReader
            sr.Close()

            Return result
        End Function
#End Region
#Region "eCheck Methods"
        Public Function ProcesseCheck(ByVal processType As Charge_Type) As Boolean
            Dim result As Boolean = False
            Try
                Dim sExpDate As String = ""
                If CType(CreditCardExpirationMonth, String).Length < 2 Then
                    sExpDate = "0" & CreditCardExpirationMonth
                Else
                    sExpDate = CreditCardExpirationMonth
                End If
                If CType(CreditCardExpirationYear, String).Length > 2 Then
                    sExpDate += CType(CreditCardExpirationYear, String).Substring(2, 2)
                Else
                    sExpDate += CreditCardExpirationYear
                End If

                ' Set Parameters
                Dim sb As New StringBuilder
                Dim sPostData As String = ""

                sb.Append("x_version=3.1")

                sb.Append("&x_login=")
                sb.Append(URLEncode(AppSettings.AuthorizeLoginID))

                sb.Append("&x_tran_key=")
                sb.Append(URLEncode(AppSettings.Authorize_Trans_Key))

                sb.Append("&x_Amount=")
                sb.Append(URLEncode(Amount))

                If CustomerId > 0 Then
                    sb.Append("&x_Cust_ID=")
                    sb.Append(URLEncode(CustomerId))
                End If


                'If EmailAddress.Trim <> "" Then
                '    sb.Append("&x_Cust_ID=")
                '    sb.Append(URLEncode(EmailAddress))
                'End If

                If OrderNumber.Trim <> "" Then
                    sb.Append("&x_Description=")
                    sb.Append(URLEncode("Order " & OrderNumber))
                End If
                sb.Append("&x_Email_Customer=")
                sb.Append(URLEncode("FALSE"))

                sb.Append("&x_delim_data=")
                sb.Append(URLEncode("TRUE"))

                sb.Append("&x_ADC_URL=")
                sb.Append(URLEncode("FALSE"))

                sb.Append("&x_delim_char=")
                sb.Append(URLEncode(","))

                sb.Append("&x_relay_response=")
                sb.Append(URLEncode("FALSE"))

                If EmailAddress.Trim <> "" Then
                    sb.Append("&x_Email=")
                    sb.Append(URLEncode(EmailAddress))
                End If

                sb.Append("&x_Customer_IP=")
                sb.Append(URLEncode(GetHostAddress))

                If FirstName.Trim <> "" Then
                    sb.Append("&x_First_Name=")
                    sb.Append(URLEncode(FirstName))
                End If

                If LastName.Trim <> "" Then
                    sb.Append("&x_Last_Name=")
                    sb.Append(URLEncode(LastName))
                End If
                If CompanyName.Trim <> "" Then
                    sb.Append("&x_Company=")
                    sb.Append(URLEncode(CompanyName))
                End If

                If AddressLine1.Trim <> "" Then
                    sb.Append("&x_Address=")
                    sb.Append(URLEncode(AddressLine1))
                End If

                If City.Trim <> "" Then
                    sb.Append("&x_City=")
                    sb.Append(URLEncode(City))
                End If

                If Country.Trim <> "" Then
                    sb.Append("&x_Country=")
                    sb.Append(URLEncode(Country.Substring(Country.Length - 2, 2)))
                End If
                If Region.Trim <> "" Then
                    sb.Append("&x_State=")
                    sb.Append(URLEncode(Region))
                End If

                If PostalCode.Trim <> "" Then
                    sb.Append("&x_Zip=")
                    sb.Append(URLEncode(PostalCode))
                End If

                If PhoneNumber.Trim <> "" Then
                    sb.Append("&x_Phone=")
                    sb.Append(URLEncode(PhoneNumber))
                End If

                sb.Append("&x_Method=")
                sb.Append(URLEncode("ECHECK"))

                ' Add Test Mode Flag if needed
                Dim sURL As String = AppSettings.Authorize_TransURL
                If AppSettings.Authorize_TestMode Then
                    'sb.Append(("&x_test_request=TRUE"))
                    sb.Append(URLEncode("&x_test_request=TRUE"))
                End If
                sb.Append("&x_bank_aba_code=" & URLEncode(Me.BankRoutingNumber))
                sb.Append("&x_bank_acct_num=" & URLEncode(Me.BankAccountNumber))
                sb.Append("&x_bank_acct_type=" & URLEncode(Me.BankAccountType.ToString))
                sb.Append("&x_bank_acct_name=" & URLEncode(Me.BankAccountTitle))

                sb.Append("&x_bank_name=" & URLEncode(Me.BankName))
                'sb.Append("&x_echeck_type=" & URLEncode(Me.CheckType.ToString))

                Select Case processType
                    Case Charge_Type.Charge

                        ' Sale / Charge
                        sb.Append("&x_Type=")
                        sb.Append(URLEncode("AUTH_CAPTURE"))
                        'sb.Append("&x_Card_Num=")
                        'sb.Append(URLEncode(CreditCardNumber))
                        'If CreditCardVerificationCode.Length() > 0 Then
                        '    sb.Append("&x_Card_Code=")
                        '    sb.Append(URLEncode(CreditCardVerificationCode))
                        'End If
                        'sb.Append("&x_Exp_Date=")
                        'sb.Append(URLEncode(sExpDate))

                    Case Charge_Type.Authorize
                        ' Pre Auth
                        sb.Append("&x_Type=")
                        sb.Append(URLEncode("AUTH_ONLY"))
                        'sb.Append("&x_Card_Num=")
                        'sb.Append(URLEncode(CreditCardNumber))
                        'If CreditCardVerificationCode.Length() > 0 Then
                        '    sb.Append("&x_Card_Code=")
                        '    sb.Append(URLEncode(CreditCardVerificationCode))
                        'End If
                        'sb.Append("&x_Exp_Date=")
                        'sb.Append(URLEncode(sExpDate))

                    Case Charge_Type.Post_Authorize
                        ' Post Auth
                        sb.Append("&x_Type=")
                        sb.Append(URLEncode("PRIOR_AUTH_CAPTURE"))
                        'sb.Append("&x_trans_id=")
                        'sb.Append(URLEncode(OriginalTransactionID))
                        'sb.Append("&x_Card_Num=")
                        'sb.Append(URLEncode(CreditCardNumber))
                        'If CreditCardVerificationCode.Length() > 0 Then
                        '    sb.Append("&x_Card_Code=")
                        '    sb.Append(URLEncode(CreditCardVerificationCode))
                        'End If
                        'sb.Append("&x_Exp_Date=")
                        'sb.Append(URLEncode(sExpDate))

                    Case Charge_Type.Void
                        ' Void Transaction
                        sb.Append("&x_Type=")
                        sb.Append(URLEncode("VOID"))
                        sb.Append("&x_Trans_ID=")
                        sb.Append(URLEncode(OriginalTransactionID))
                        'sb.Append("&x_Card_Num=")
                        'sb.Append(URLEncode(CreditCardNumber))
                        'If CreditCardVerificationCode.Length() > 0 Then
                        '    sb.Append("&x_Card_Code=")
                        '    sb.Append(URLEncode(CreditCardVerificationCode))
                        'End If
                        'sb.Append("&x_Exp_Date=")
                        'sb.Append(URLEncode(sExpDate))


                    Case Charge_Type.Refund
                        ' Credit
                        sb.Append("&x_Type=")
                        sb.Append(URLEncode("CREDIT"))
                        sb.Append("&x_Trans_ID=")
                        sb.Append(URLEncode(OriginalTransactionID))
                        'sb.Append("&x_Card_Num=")
                        'sb.Append(URLEncode(CreditCardNumber))
                        'If CreditCardVerificationCode.Length() > 0 Then
                        '    sb.Append("&x_Card_Code=")
                        '    sb.Append(URLEncode(CreditCardVerificationCode))
                        'End If
                        'sb.Append("&x_Exp_Date=")
                        'sb.Append(URLEncode(sExpDate))

                End Select

                sPostData = sb.ToString


                Me.FullRequest = sPostData

                Dim sResponse As String = ""
                sResponse = ReadHtmlPagePost(sURL, sPostData)

                Me.FullResponse = sResponse


                ' Split response string
                Dim sOutput() As String
                sOutput = sResponse.Split(",")

                Dim iCounter As Integer = 0
                Dim htVars As Hashtable = New Hashtable
                Dim sVar As String = ""

                ' Move strings into hash table for easy reference
                For Each sVar In sOutput
                    htVars.Add(iCounter, sVar)
                    iCounter += 1
                Next

                If htVars.Count < 7 Then
                    result = False
                Else
                    Dim sResponseCode As String = htVars(0)
                    Dim sResponseDescription As String = htVars(3)
                    Dim sResponseAuthCode As String = htVars(4)
                    Dim sResponseAVSCode As String = htVars(5)
                    Dim sTransRef As String = htVars(6)

                    Dim sCVVResponse As String = ""
                    If htVars.Count > 38 Then
                        sCVVResponse = htVars(38)
                    End If

                    ' Trim off Extra Quotes on response codes
                    sResponseCode = sResponseCode.Trim("""")

                    AVSResponseCode = sResponseAVSCode

                    Select Case sResponseCode
                        Case "1"
                            ' Approved
                            result = True
                            ResponseCode = Response_Code.APPROVED
                            ResponseTransactionID = sTransRef
                            ResponseMessage = sResponseDescription
                        Case "2"
                            ' Declined
                            result = False
                            ResponseCode = Response_Code.DECLINED
                            ResponseTransactionID = sTransRef
                            ResponseMessage = "Transaction Declined: " & sResponseDescription


                        Case "3"
                            result = False
                            ResponseCode = Response_Code.ERROR
                            ResponseMessage = "Authorize.Net Processing Error: "
                            ResponseMessage += sResponseDescription
                    End Select


                    ' AVS
                    Select Case sResponseAVSCode
                        Case "A"
                            ResponseMessage += " [AVS - Address (Street) matches, ZIP does not]"
                        Case "B"
                            ResponseMessage += " [AVS - Address information not provided for AVS check]"
                        Case "E"
                            ResponseMessage += " [AVS - Error]"
                        Case "G"
                            ResponseMessage += " [AVS - Non-U.S. Card Issuing Bank]"
                        Case "N"
                            ResponseMessage += " [AVS - No Match on Address (Street) or ZIP]"
                        Case "P"
                            ResponseMessage += " [AVS - AVS not applicable for this transaction]"
                        Case "R"
                            ResponseMessage += " [AVS - Retry – System unavailable or timed out]"
                        Case "S"
                            ResponseMessage += " [AVS - Service not supported by issuer]"
                        Case "U"
                            ResponseMessage += " [AVS - Address information is unavailable]"
                        Case "W"
                            ResponseMessage += " [AVS - 9 digit ZIP matches, Address (Street) does not]"
                        Case "X"
                            ResponseMessage += " [AVS - Address (Street) and 9 digit ZIP match]"
                        Case "Y"
                            ResponseMessage += " [AVS - Address (Street) and 5 digit ZIP match]"
                        Case "Z"
                            ResponseMessage += " [AVS - 5 digit ZIP matches, Address (Street) does not]"
                    End Select

                    ' CVV
                    Select Case sCVVResponse
                        Case "M"
                            ResponseMessage += " [CVV - Match]"
                        Case "N"
                            ResponseMessage += " [CVV - No Match]"
                        Case "P"
                            ResponseMessage += " [CVV - Not Processed]"
                        Case "S"
                            ResponseMessage += " [CVV - Should have been present]"
                        Case "U"
                            ResponseMessage += " [CVV - Issuer unable to process request]"
                    End Select
                End If


            Catch Ex As Exception
                result = False
                ResponseMessage = "An Unknown Payment Error Occred: " & Ex.Message & " " & Ex.StackTrace
            End Try

            Return result
        End Function
#End Region
#Region "Private functions"
        Private Function URLEncode(ByVal text As String) As String
            Return System.Web.HttpUtility.UrlEncode(text)
        End Function
        Private Function GetHostAddress() As String

            If HttpContext.Current Is Nothing OrElse HttpContext.Current.Request Is Nothing Then
                Return "127.0.0.1"
            Else
                Return HttpContext.Current.Request.UserHostAddress
            End If

        End Function
#End Region

#End Region

    End Class
End Namespace