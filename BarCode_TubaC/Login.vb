
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports TubacBarCodeProduction.Conexion
Public Class Login
    Public con As New Conexion
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property


    Public Connected As Boolean
    Public Property _oCompany As SAPbobsCOM.Company
    Public Property oCompany() As SAPbobsCOM.Company
        Get
            Return _oCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _oCompany = value
        End Set
    End Property
    Public Function MakeConnectionSAP() As Boolean
        Connected = False
        Try
            Connected = -1
            oCompany = New SAPbobsCOM.Company
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
            Dim entra As String = Application.StartupPath + "\conexion.xml"
            Dim Xml As XmlDocument = New XmlDocument()
            Xml.Load(entra)
            Dim ArticleList As XmlNodeList = Xml.SelectNodes("body/HANA")
            For Each xnDoc As XmlNode In ArticleList
                oCompany.UserName = xnDoc.SelectSingleNode("UserName").InnerText
                oCompany.Password = xnDoc.SelectSingleNode("Password").InnerText
                oCompany.DbUserName = xnDoc.SelectSingleNode("DbUserName").InnerText
                oCompany.DbPassword = xnDoc.SelectSingleNode("DbPassword").InnerText
                oCompany.Server = xnDoc.SelectSingleNode("Server").InnerText
                oCompany.CompanyDB = xnDoc.SelectSingleNode("CompanyDB").InnerText
                oCompany.LicenseServer = xnDoc.SelectSingleNode("Server").InnerText + ":30000"
            Next

            oCompany.UseTrusted = False
            Connected = oCompany.Connect()

            If Connected <> 0 Then
                Connected = False
                MsgBox(oCompany.GetLastErrorDescription)
            Else
                Connected = True
            End If
            Return Connected
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function


    Dim connectionString As String = Conexion.ObtenerConexion.ConnectionString
    Public Shared SQL_Conexion As SqlConnection = New SqlConnection()


    Private Function ExistField(ByVal oCompany As SAPbobsCOM.Company, ByVal TableName As String, ByVal FieldName As String, ByVal addSymbol As Boolean) As Boolean
        Dim RecSet As SAPbobsCOM.Recordset
        Dim QryStr As String = ""
        Dim result As Boolean = False

        Try
            If addSymbol Then
                TableName = "@" & TableName
            End If
            QryStr = "select TableID,FieldID,AliasID from CUFD WHERE TableID='" & TableName & "' and AliasID  ='" & FieldName & "'"
            RecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            RecSet.DoQuery(QryStr)
            If RecSet.RecordCount > 0 Then
                result = True
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(RecSet)
            RecSet = Nothing
            GC.Collect()
            Return result
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AddUserTable(ByVal oCompany As SAPbobsCOM.Company, ByVal TableName As String, ByVal TableDescription As String, ByVal typeTable As SAPbobsCOM.BoUTBTableType)

        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Dim iResult As Long
        Dim sMsg As String
        Dim sTable As String

        Try
            oUserTablesMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)

            If (oUserTablesMD.GetByKey(TableName) = False) Then

                oUserTablesMD.TableName = TableName
                oUserTablesMD.TableDescription = TableDescription
                oUserTablesMD.TableType = typeTable
                oUserTablesMD.Add()

                oUserTablesMD.Update()

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
                oUserTablesMD = Nothing
                GC.Collect()
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub AddUserField(ByVal oCompany As SAPbobsCOM.Company, ByVal TableName As String, ByVal FieldName As String, ByVal FieldDescription As String, ByVal FieldType As SAPbobsCOM.BoFieldTypes, ByVal Size As Integer, Optional ByVal addSymbol As Boolean = True, Optional ByVal SubType As SAPbobsCOM.BoFldSubTypes = Nothing)

        Dim AddT As Integer
        Dim lerrcode As Integer
        Dim serrmsg As String = ""

        Try

            If ExistField(oCompany, TableName, FieldName, addSymbol) = False Then

                Dim oUserFieldsMD As SAPbobsCOM.UserFieldsMD
                oUserFieldsMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                oUserFieldsMD.TableName = TableName
                oUserFieldsMD.Name = FieldName
                oUserFieldsMD.Description = FieldDescription
                oUserFieldsMD.Type = FieldType
                If Not IsNothing(oUserFieldsMD.SubType) Then oUserFieldsMD.SubType = SubType
                If FieldType = 2 Or FieldType = 0 Then
                    oUserFieldsMD.EditSize = Size
                End If
                AddT = oUserFieldsMD.Add

                If AddT <> 0 Then
                    oCompany.GetLastError(lerrcode, serrmsg)
                    Throw New Exception(serrmsg)
                Else
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
                    oUserFieldsMD = Nothing
                    GC.Collect()
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub AddUserTables()
        Try
            AddUserTable(oCompany, "FACE_SERIES", "FACE SERIES", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            AddUserTable(oCompany, "FACE_PARAMETROS", "PARAMETROS FACE", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddUserTable(oCompany, "FACE_RESOLUCION", "RES. FAC. FACE", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            AddUserTable(oCompany, "FACE_TIPODOC", "TIPOS DE DOC FACE", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)
            'AddUserTable(oCompany, "FACE_PAISEQUIV", "PAISES EQUIVALENCIAS GUATEF", SAPbobsCOM.BoUTBTableType.bott_MasterDataLines)


            'AddUserField(oCompany, "FACE_PAISEQUIV", "COD_PAIS_SAP", "CODIGO PAIS SAP", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            'AddUserField(oCompany, "FACE_PAISEQUIV", "COD_PAIS_GUATEF", "CODIGO PAIS GUATEFACTURAS", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            '*-----------------------FACE SERIES
            AddUserField(oCompany, "FACE_SERIES", "LINEA", "LINEA", SAPbobsCOM.BoFieldTypes.db_Alpha, 15,)
            AddUserField(oCompany, "CampoPruebaDesarrollo", "Prueba", "Prueba", SAPbobsCOM.BoFieldTypes.db_Alpha, 15,)

            AddUserField(oCompany, "FACE_TIPODOC", "CODIGO", "CODIGO DOCUMENTO", SAPbobsCOM.BoFieldTypes.db_Alpha, 15)
            AddUserField(oCompany, "FACE_TIPODOC", "DESCRIPCION", "DESC TIPO DE DOC", SAPbobsCOM.BoFieldTypes.db_Alpha, 250)

            AddUserField(oCompany, "FACE_PARAMETROS", "PARAMETRO", "PARAMETRO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddUserField(oCompany, "FACE_PARAMETROS", "VALOR", "VAL. PARAMETRO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 254)

            AddUserField(oCompany, "FACE_RESOLUCION", "SERIE", "SERIE FACTURA", SAPbobsCOM.BoFieldTypes.db_Numeric, 11)
            AddUserField(oCompany, "FACE_RESOLUCION", "RESOLUCION", "RES. FACTURA", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddUserField(oCompany, "FACE_RESOLUCION", "AUTORIZACION", "AUTO. FACTURA", SAPbobsCOM.BoFieldTypes.db_Alpha, 50)
            AddUserField(oCompany, "FACE_RESOLUCION", "FECHA_AUTORIZACION", "FECHA AUTO.", SAPbobsCOM.BoFieldTypes.db_Date, 8)
            AddUserField(oCompany, "FACE_RESOLUCION", "FACTURA_DEL", "FACTURA INICIAL", SAPbobsCOM.BoFieldTypes.db_Numeric, 11)
            AddUserField(oCompany, "FACE_RESOLUCION", "FACTURA_AL", "FACTURA FINAL", SAPbobsCOM.BoFieldTypes.db_Numeric, 11)
            AddUserField(oCompany, "FACE_RESOLUCION", "TIPO_DOC", "TIPO DE DOCUMENTO", SAPbobsCOM.BoFieldTypes.db_Alpha, 30)
            AddUserField(oCompany, "FACE_RESOLUCION", "ES_BATCH", "PROESO EN LINEA O BATCH", SAPbobsCOM.BoFieldTypes.db_Alpha, 1)
            AddUserField(oCompany, "FACE_RESOLUCION", "SUCURSAL", "NO. SUCURSAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddUserField(oCompany, "FACE_RESOLUCION", "NOMBRE_SUCURSAL", "NOMBRE SUCURSAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 100)
            AddUserField(oCompany, "FACE_RESOLUCION", "DISPOSITIVO", "No. DISPOSITIVO ELECTRONICO", SAPbobsCOM.BoFieldTypes.db_Alpha, 10)
            AddUserField(oCompany, "FACE_RESOLUCION", "DIR_SUCURSAL", "DIRECCION SUCURSAL", SAPbobsCOM.BoFieldTypes.db_Memo, 8000)
            AddUserField(oCompany, "FACE_RESOLUCION", "MUNI_SUCURSAL", "MUNICIPIO SUCURSAL", SAPbobsCOM.BoFieldTypes.db_Memo, 8000)
            AddUserField(oCompany, "FACE_RESOLUCION", "DEPTO_SUCURSAL", "DEPTO SUCURSAL", SAPbobsCOM.BoFieldTypes.db_Memo, 8000)
            AddUserField(oCompany, "FACE_RESOLUCION", "USUARIO", "USUARIO GFACE", SAPbobsCOM.BoFieldTypes.db_Memo, 8000)
            AddUserField(oCompany, "FACE_RESOLUCION", "CLAVE", "CLAVE GFACE", SAPbobsCOM.BoFieldTypes.db_Memo, 8000)

            AddUserField(oCompany, "OCRD", "NIT", "NIT", SAPbobsCOM.BoFieldTypes.db_Alpha, 15, False)
            AddUserField(oCompany, "OINV", "ESTADO_FACE", "ESTADO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, False)
            AddUserField(oCompany, "OINV", "FACE_XML", "XML ENVIADO FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "OINV", "MOTIVO_RECHAZO", "RECHAZO FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "OINV", "FACE_PDFFILE", "PDF FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "OINV", "FIRMA_ELETRONICA", "FIRMA ELECTRONICA FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "OINV", "NUMERO_DOCUMENTO", "NUMERO DOC FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, False)
            AddUserField(oCompany, "OINV", "NUMERO_RESOLUCION", "NUMERO RESOLUCION FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)
            AddUserField(oCompany, "OINV", "SERIE_FACE", "NUMERO DE SERIE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OINV", "FACTURA_INI", "FACTURA INICIAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OINV", "FACTURA_FIN", "FACTURA FINAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OINV", "FACTURA_SERIE", "SERIE FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OINV", "FACTURA_PREIMPRESO", "PREIMPRESO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OINV", "FECHA_ENVIO_FACE", "FECHA ENVIO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 22, False)
            AddUserField(oCompany, "OINV", "EMAIL_FACE", "EMAIL FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)


            AddUserField(oCompany, "ORIN", "ESTADO_FACE", "ESTADO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 2, False)
            AddUserField(oCompany, "ORIN", "FACE_XML", "XML ENVIADO FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "ORIN", "MOTIVO_RECHAZO", "RECHAZO FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "ORIN", "FACE_PDFFILE", "PDF FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "ORIN", "FIRMA_ELETRONICA", "FIRMA ELECTRONICA FACE", SAPbobsCOM.BoFieldTypes.db_Memo, 254, False)
            AddUserField(oCompany, "ORIN", "NUMERO_DOCUMENTO", "NUMERO DOC FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, False)
            AddUserField(oCompany, "ORIN", "NUMERO_RESOLUCION", "NUMERO RESOLUCION FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)
            AddUserField(oCompany, "ORIN", "SERIE_FACE", "NUMERO DE SERIE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "ORIN", "FACTURA_INI", "FACTURA INICIAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "ORIN", "FACTURA_FIN", "FACTURA FINAL", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "ORIN", "FACTURA_SERIE", "SERIE FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "ORIN", "FACTURA_PREIMPRESO", "PREIMPRESO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "ORIN", "FECHA_ENVIO_FACE", "FECHA ENVIO FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 22, False)
            AddUserField(oCompany, "ORIN", "EMAIL_FACE", "EMAIL FACE", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)
            AddUserField(oCompany, "ORIN", "DocstatusCC", "Estado Docto", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, False)



        Catch ex As Exception
            MessageBox.Show("error campos de usuarios")
        End Try
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Then
            MessageBox.Show("Verifique Nombre y contraseña")
        Else
            Dim SQL_da As SqlCommand = New SqlCommand("SELECT count(*) FROM [dbo].[@TUBAC_PRODUCCION]  T0 where T0.Code ='" + TextBox1.Text + "' and T0.Name ='" + TextBox2.Text + "';", con.ObtenerConexion())
            Dim obj As Object = SQL_da.ExecuteScalar()
            If obj > 0 Then
                Dim user As String
                user = TextBox1.Text
                Dim SQL_permiso As SqlCommand = New SqlCommand("SELECT U_permiso FROM [dbo].[@TUBAC_PRODUCCION]  T0 where T0.Code ='" + TextBox1.Text + "';", con.ObtenerConexion())
                Dim permiso As Integer
                permiso = IIf(IsDBNull(SQL_permiso.ExecuteScalar), "", SQL_permiso.ExecuteScalar)
                'MessageBox.Show(Connected.ToString)
                Me.Hide()
                Dim frms As New MDI_BC(user, permiso)
                frms.BackgroundImageLayout = ImageLayout.Stretch
                frms.BackgroundImage = Image.FromFile(Application.StartupPath + "\logo.jpg")
                frms.Show()


                con.ObtenerConexion.Close()
            Else
                MessageBox.Show("Error de Usuario o Contraseña")
                con.ObtenerConexion.Close()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.MakeConnectionSAP() Then
            AddUserTables()
        End If
        'Me.Hide()
        'Dim frm As New Cusuarios
        'frm.Show()
    End Sub

    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            Button1_Click(1, e)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Call EnterClick(sender, e)
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        Call EnterClick(sender, e)
    End Sub
End Class