Imports System.Data
Imports System.Data.OleDb

Public Class VentanaInicio

    Dim conexion As New OleDbConnection
    Dim sqlAdaptador As New OleDbDataAdapter
    Dim datosSet As New DataSet
    Dim consulta As String



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtUsuario.Text <> "" Then
            Try
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
                conexion.Open()
                consulta = "Select * from estudiante where nombre = '" & txtUsuario.Text & "' and contrasena = '" & txtContrasena.Text & "'"
                sqlAdaptador = New OleDbDataAdapter(consulta, conexion)

                sqlAdaptador.Fill(datosSet, "estudiante")

                If datosSet.Tables("estudiante").Rows.Count <> 0 Then
                    conexion.Close()
                    Dim ve As New VentanaEstudiante
                    ve.Show()
                    Hide()

                Else
                    MsgBox("El usuario no existe", vbInformation, "Error")
                End If

            Catch ex As Exception
                MsgBox("Error")
            End Try

        Else
            MsgBox("No se encontro el usuario", vbObjectError, "Error")


        End If


    End Sub

    Private Sub InicioVentana_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim vr As New VentanaRegistro
        vr.Show()
        Hide()
    End Sub

End Class
