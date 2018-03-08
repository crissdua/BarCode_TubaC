
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




    Dim connectionString As String = Conexion.ObtenerConexion.ConnectionString
    Public Shared SQL_Conexion As SqlConnection = New SqlConnection()




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
        Dim frm As New Cusuarios
        frm.Show()
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim result As Integer = MessageBox.Show("Desea salir del modulo?", "Atencion", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            MessageBox.Show("Puede continuar")
        ElseIf result = DialogResult.Yes Then
            MessageBox.Show("Finalizando modulo")
            Try
                con.oCompany.Disconnect()
            Catch
            End Try
            Application.Exit()
            Me.Close()
        End If
    End Sub
End Class