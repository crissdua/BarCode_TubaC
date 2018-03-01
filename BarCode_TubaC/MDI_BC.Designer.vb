<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDI_BC
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Fase1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImpresionBatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrasladoStockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImpresionOrdenesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProduccionReciboToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProduccionReciboToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TirasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PlanificacionProduccionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProduccionEmisionConsumoMPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProduccionReciboProduccionReportadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.Fase1ToolStripMenuItem, Me.PlanificacionProduccionToolStripMenuItem, Me.TirasToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1199, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.ArchivoToolStripMenuItem.Text = "Archivo"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'Fase1ToolStripMenuItem
        '
        Me.Fase1ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImpresionBatchToolStripMenuItem, Me.TrasladoStockToolStripMenuItem, Me.ProduccionReciboToolStripMenuItem, Me.ImpresionOrdenesToolStripMenuItem, Me.ProduccionReciboToolStripMenuItem1})
        Me.Fase1ToolStripMenuItem.Name = "Fase1ToolStripMenuItem"
        Me.Fase1ToolStripMenuItem.Size = New System.Drawing.Size(136, 20)
        Me.Fase1ToolStripMenuItem.Text = "Gestion Materia Prima"
        '
        'ImpresionBatchToolStripMenuItem
        '
        Me.ImpresionBatchToolStripMenuItem.Name = "ImpresionBatchToolStripMenuItem"
        Me.ImpresionBatchToolStripMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.ImpresionBatchToolStripMenuItem.Text = "Impresion Etiquetas Orden de Compra"
        '
        'TrasladoStockToolStripMenuItem
        '
        Me.TrasladoStockToolStripMenuItem.Name = "TrasladoStockToolStripMenuItem"
        Me.TrasladoStockToolStripMenuItem.Size = New System.Drawing.Size(276, 22)
        Me.TrasladoStockToolStripMenuItem.Text = "Traslado Materia Prima"
        '
        'ImpresionOrdenesToolStripMenuItem
        '
        Me.ImpresionOrdenesToolStripMenuItem.Name = "ImpresionOrdenesToolStripMenuItem"
        Me.ImpresionOrdenesToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ImpresionOrdenesToolStripMenuItem.Text = "Impresion Etiqueta Produccion Planificada"
        Me.ImpresionOrdenesToolStripMenuItem.Visible = False
        '
        'ProduccionReciboToolStripMenuItem
        '
        Me.ProduccionReciboToolStripMenuItem.Name = "ProduccionReciboToolStripMenuItem"
        Me.ProduccionReciboToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ProduccionReciboToolStripMenuItem.Text = "Produccion Emision (Consumo MP)"
        Me.ProduccionReciboToolStripMenuItem.Visible = False
        '
        'ProduccionReciboToolStripMenuItem1
        '
        Me.ProduccionReciboToolStripMenuItem1.Name = "ProduccionReciboToolStripMenuItem1"
        Me.ProduccionReciboToolStripMenuItem1.Size = New System.Drawing.Size(303, 22)
        Me.ProduccionReciboToolStripMenuItem1.Text = "Produccion Recibo (Produccion Reportada)"
        Me.ProduccionReciboToolStripMenuItem1.Visible = False
        '
        'TirasToolStripMenuItem
        '
        Me.TirasToolStripMenuItem.Name = "TirasToolStripMenuItem"
        Me.TirasToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.TirasToolStripMenuItem.Text = "Tiras"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 489)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1199, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel2.Text = "Usuario: "
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'PlanificacionProduccionToolStripMenuItem
        '
        Me.PlanificacionProduccionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem, Me.ProduccionEmisionConsumoMPToolStripMenuItem, Me.ProduccionReciboProduccionReportadaToolStripMenuItem})
        Me.PlanificacionProduccionToolStripMenuItem.Name = "PlanificacionProduccionToolStripMenuItem"
        Me.PlanificacionProduccionToolStripMenuItem.Size = New System.Drawing.Size(159, 20)
        Me.PlanificacionProduccionToolStripMenuItem.Text = "Planificacion / Produccion"
        '
        'ProduccionEmisionConsumoMPToolStripMenuItem
        '
        Me.ProduccionEmisionConsumoMPToolStripMenuItem.Name = "ProduccionEmisionConsumoMPToolStripMenuItem"
        Me.ProduccionEmisionConsumoMPToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ProduccionEmisionConsumoMPToolStripMenuItem.Text = "Produccion Emision (Consumo MP)"
        '
        'ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem
        '
        Me.ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem.Name = "ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem"
        Me.ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem.Text = "Impresion Etiqueta Produccion Planificada"
        '
        'ProduccionReciboProduccionReportadaToolStripMenuItem
        '
        Me.ProduccionReciboProduccionReportadaToolStripMenuItem.Name = "ProduccionReciboProduccionReportadaToolStripMenuItem"
        Me.ProduccionReciboProduccionReportadaToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ProduccionReciboProduccionReportadaToolStripMenuItem.Text = "Produccion Recibo (Produccion Reportada)"
        '
        'MDI_BC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.BarCode_TubaC.My.Resources.Resources.logo
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1199, 511)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MDI_BC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tubac"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Fase1ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImpresionBatchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TrasladoStockToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImpresionOrdenesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProduccionReciboToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProduccionReciboToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TirasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents PlanificacionProduccionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImpresionEtiquetaProduccionPlanificadaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProduccionEmisionConsumoMPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProduccionReciboProduccionReportadaToolStripMenuItem As ToolStripMenuItem
End Class
