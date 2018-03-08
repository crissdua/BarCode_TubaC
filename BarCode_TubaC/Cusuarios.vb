Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports SAPbobsCOM
Imports SAPbouiCOM
Imports System.Xml
Public Class Cusuarios

    Public Property _oCompany As SAPbobsCOM.Company
    Public Property oCompany() As SAPbobsCOM.Company
        Get
            Return _oCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _oCompany = value
        End Set
    End Property

    Public Connected As Boolean

    Public Function MakeConnectionSAP() As Boolean
        Connected = False
        Try
            Connected = -1
            oCompany = New SAPbobsCOM.Company
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
            Dim entra As String = System.Windows.Forms.Application.StartupPath + "\conexion.xml"
            Dim Xml As XmlDocument = New XmlDocument()
            Xml.Load(entra)
            Dim ArticleList As XmlNodeList = Xml.SelectNodes("body/HANA")
            For Each xnDoc As XmlNode In ArticleList
                oCompany.UserName = TextBox1.Text
                oCompany.Password = TextBox2.Text
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("Debe ingresar una contraseña")
        Else
            If MakeConnectionSAP() Then
                AddUserTables()
            End If
        End If
    End Sub

    Private Sub AddUserTables()
        Try
            AddUserTable(oCompany, "TUBAC_PRODUCCION", "Usuarios de Produccion", SAPbobsCOM.BoUTBTableType.bott_NoObject)
            AddUserField(oCompany, "TUBAC_PRODUCCION", "Permiso", "Permiso", SAPbobsCOM.BoFieldTypes.db_Alpha, 15, False)

            AddUserField(oCompany, "OWOR", "comment", "Comentario Cabecera", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, False)
            AddUserField(oCompany, "OWOR", "Ancho_Tira", "Ancho Tira", SAPbobsCOM.BoFieldTypes.db_Float, 19.6, False, SAPbobsCOM.BoFldSubTypes.st_Price)

            AddUserField(oCompany, "WOR1", "ancho", "Ancho", SAPbobsCOM.BoFieldTypes.db_Float, 19.6, False, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddUserField(oCompany, "WOR1", "peso", "Peso", SAPbobsCOM.BoFieldTypes.db_Float, 19.6, False, SAPbobsCOM.BoFldSubTypes.st_Price)
            AddUserField(oCompany, "WOR1", "lotes", "Lotes", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)
            AddUserField(oCompany, "WOR1", "comment", "Comentario", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, False)
            AddUserField(oCompany, "WOR1", "tiras", "Cantidad Tiras", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, False)

            Dim sql As String
            Dim oRecordSet As SAPbobsCOM.Recordset

            Dim sql2 As String
            Dim oRecordSet2 As SAPbobsCOM.Recordset
            Dim valor As Integer

            sql2 = ("SELECT Code FROM [dbo].[@TUBAC_PRODUCCION]  T0 where T0.Code ='" + TextBox1.Text + "'")
            oRecordSet2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet2.DoQuery(sql2)
            If oRecordSet2.RecordCount > 0 Then

            Else
                sql = ("insert into [dbo].[@TUBAC_PRODUCCION] (Code,Name,U_Permiso) values ('" + TextBox1.Text + "','" + TextBox2.Text + "','4')")
                oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecordSet.DoQuery(sql)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
                oRecordSet = Nothing
                GC.Collect()
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet2)
            oRecordSet2 = Nothing
            GC.Collect()

            oCompany.Disconnect()

            MessageBox.Show("Campos creados correctamente, Usuario Manager creado!")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub EnterClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            Button1_Click(1, e)
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        Call EnterClick(sender, e)
    End Sub

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
                    'oCompany.GetLastError(lerrcode, serrmsg)
                    'Throw New Exception(serrmsg)
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
End Class