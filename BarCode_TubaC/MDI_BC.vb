
Public Class MDI_BC
    Public con As New Conexion
    Dim PermisoID As Integer
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
    Public Sub New(ByVal user As String, ByVal permiso As Integer)
        MyBase.New()
        InitializeComponent()
        '  Note which form has called this one
        ToolStripStatusLabel1.Text = user
        PermisoID = permiso

        Fase1ToolStripMenuItem.Visible = False
        PlanificacionProduccionToolStripMenuItem.Visible = False
        TirasToolStripMenuItem.Visible = False
        If PermisoID = 1 Then
            Fase1ToolStripMenuItem.Visible = True

            ImpresionBatchToolStripMenuItem.Visible = True
            TrasladoStockToolStripMenuItem.Visible = True

        End If
        If PermisoID = 2 Then
            PlanificacionProduccionToolStripMenuItem.Visible = True

            ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem.Visible = True
            ProduccionEmisionConsumoMPToolStripMenuItem.Visible = True
            ProduccionReciboProduccionReportadaToolStripMenuItem.Visible = True
        End If
        If PermisoID = 3 Then

            TirasToolStripMenuItem.Visible = True

        End If

        If PermisoID = 4 Then
            Fase1ToolStripMenuItem.Visible = True
            PlanificacionProduccionToolStripMenuItem.Visible = True
            TirasToolStripMenuItem.Visible = True
        End If


    End Sub
    Private Sub MDI_BC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ImpresionBatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImpresionBatchToolStripMenuItem.Click
        Dim frm1 As New Produccion_F1.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub TrasladoStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrasladoStockToolStripMenuItem.Click
        Dim frm1 As New Produccion_F2.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub ImpresionOrdenesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImpresionOrdenesToolStripMenuItem.Click
        Dim frm1 As New Produccion_F3.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub ProduccionReciboToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProduccionReciboToolStripMenuItem.Click
        Dim frm1 As New Produccion_Emision.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub ProduccionReciboToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProduccionReciboToolStripMenuItem1.Click
        Dim frm1 As New Produccion_Recibo.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
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

    Private Sub ProduccionEmisionConsumoMPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProduccionEmisionConsumoMPToolStripMenuItem.Click
        Dim frm1 As New Produccion_Emision.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem.Click
        Dim frm1 As New Produccion_F3.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub ProduccionReciboProduccionReportadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProduccionReciboProduccionReportadaToolStripMenuItem.Click
        Dim frm1 As New Produccion_Recibo.FrmP()
        frm1.MdiParent = Me
        frm1.Show()
    End Sub

    Private Sub CamposDeUsuarioToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frm1 As New Cusuarios
        frm1.MdiParent = Me
        frm1.Show()
    End Sub
End Class
