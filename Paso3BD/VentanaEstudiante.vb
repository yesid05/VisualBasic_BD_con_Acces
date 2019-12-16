Imports System.Data
Imports System.Data.OleDb
Public Class VentanaEstudiante
    Dim conexion As New OleDbConnection
    Dim sqlAdaptador As New OleDbDataAdapter
    Dim sqlComando As New OleDbCommand
    Dim datosSet As New DataSet
    Dim id As Integer

    Private Sub ActualizarRegistro()
        datosSet = New DataSet
        dgvDatos.DataSource = datosSet
        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
            conexion.Open()
            Dim consulta As String = "Select * from estudiante"
            sqlAdaptador = New OleDbDataAdapter(consulta, conexion)

            sqlAdaptador.Fill(datosSet, "estudiante")

            If datosSet.Tables("estudiante").Rows.Count <> 0 Then
                dgvDatos.DataSource = datosSet
                dgvDatos.DataMember = "estudiante"
            End If

            conexion.Close()
        Catch ex As Exception
            MsgBox("Error")
        End Try
    End Sub

    Private Sub VentanaEstudiante_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        datosSet = New DataSet

        If txtNombre.Text <> "" Then
            Try
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
                conexion.Open()
                Dim consulta As String = "Select * from estudiante where nombre = '" & txtNombre.Text & "'"
                sqlAdaptador = New OleDbDataAdapter(consulta, conexion)

                sqlAdaptador.Fill(datosSet, "estudiante")

                If datosSet.Tables("estudiante").Rows.Count <> 0 Then
                    id = datosSet.Tables("estudiante").Rows(0).Item("Id")
                    txtNombre.Text = datosSet.Tables("estudiante").Rows(0).Item("nombre")
                    txtApellido.Text = datosSet.Tables("estudiante").Rows(0).Item("apellidos")
                    txtCorreo.Text = datosSet.Tables("estudiante").Rows(0).Item("correo")
                    txtTelefono.Text = datosSet.Tables("estudiante").Rows(0).Item("telefono")
                    txtEdad.Text = datosSet.Tables("estudiante").Rows(0).Item("edad")

                Else
                    MsgBox("El usuario no existe", vbInformation, "Error")
                End If
                conexion.Close()
            Catch ex As Exception
                MsgBox("Error")
            End Try

            ActualizarRegistro()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtNombre.Text <> " " Then
            Try
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
                conexion.Open()
                Dim consulta As String = "Update estudiante set nombre = '" & txtNombre.Text & "'," &
                "apellidos = '" & txtApellido.Text & "'," &
                "correo = '" & txtCorreo.Text & "'," &
                "telefono = '" & txtTelefono.Text & "'," &
                "edad = '" & txtEdad.Text & "' " &
                "where id = " & id & ""

                sqlComando = New OleDbCommand(consulta, conexion)
                sqlComando.ExecuteNonQuery()
                MsgBox("Estudiante actualizado.", vbInformation, "")

                conexion.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            ActualizarRegistro()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txtNombre.Text <> "" Then
            Try
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
                conexion.Open()

                Dim consulta As String = "Delete * from estudiante where id = " & id & ""

                sqlComando = New OleDbCommand(consulta, conexion)
                sqlComando.ExecuteNonQuery()
                MsgBox("Estudiante eliminado.", vbInformation, "")

                conexion.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            ActualizarRegistro()
        End If
    End Sub

    Private Sub VentanaEstudiante_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ActualizarRegistro()
    End Sub

    Private Sub VentanaEstudiante_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim vi As New VentanaInicio
        vi.Show()
        Dispose()
    End Sub
End Class