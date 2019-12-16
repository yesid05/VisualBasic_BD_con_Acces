Imports System.Data
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Public Class VentanaRegistro

    Dim conexion As New OleDbConnection
    Dim sql As New OleDbCommand


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtNombre.Text = "" Then
            ErrorMsg.SetError(txtNombre, "Escriba un nombre de usuario.")
            Return
        End If
        ErrorMsg.SetError(txtNombre, "")

        If txtContrasena.Text = "" Then
            ErrorMsg.SetError(txtContrasena, "Escriba una contraseña.")
            Return
        End If
        ErrorMsg.SetError(txtContrasena, "")

        If txtApellido.Text = "" Then
            ErrorMsg.SetError(txtApellido, "Escriba un apellido.")
            Return
        End If
        ErrorMsg.SetError(txtApellido, "")

        If txtCorreo.Text = "" Then
            ErrorMsg.SetError(txtCorreo, "Escriba un correo.")
            Return
        End If
        ErrorMsg.SetError(txtCorreo, "")

        Dim expresionCorreo As String = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"
        If Not Regex.IsMatch(txtCorreo.Text, expresionCorreo) Then
            ErrorMsg.SetError(txtCorreo, "Escriba un correo valido.")
            Return
        End If
        ErrorMsg.SetError(txtCorreo, "")

        If txtTelefono.Text = "" Then
            ErrorMsg.SetError(txtTelefono, "Escriba un teléfono.")
            Return
        End If
        ErrorMsg.SetError(txtTelefono, "")

        Dim expresionTelefono As String = "\d"
        If Not Regex.IsMatch(txtTelefono.Text, expresionTelefono) Then
            ErrorMsg.SetError(txtTelefono, "Escriba un teléfono valido.")
            Return
        End If
        ErrorMsg.SetError(txtTelefono, "")

        If txtEdad.Text = "" Then
            ErrorMsg.SetError(txtEdad, "Escriba una edad.")
            Return
        End If
        ErrorMsg.SetError(txtEdad, "")

        Dim expresionEdad As String = "\d"
        If Not Regex.IsMatch(txtEdad.Text, expresionEdad) Then
            ErrorMsg.SetError(txtEdad, "Escriba una edad valido.")
            Return
        End If
        ErrorMsg.SetError(txtEdad, "")


        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\estudiantes.accdb"
            conexion.Open()

            sql = New OleDbCommand("Insert into estudiante(nombre,contrasena,apellidos,correo,telefono,edad)" &
                                   "values(txtNombre,txtContrasena.Text,txtApellido,txtCorreo,txtTelefono,txtEdad)", conexion)

            sql.Parameters.AddWithValue("@nombre", txtNombre.Text)
            sql.Parameters.AddWithValue("@contrasena", txtContrasena.Text)
            sql.Parameters.AddWithValue("@apellidos", txtApellido.Text)
            sql.Parameters.AddWithValue("@correo", txtCorreo.Text)
            sql.Parameters.AddWithValue("@telefono", txtTelefono.Text)
            sql.Parameters.AddWithValue("@edad", txtEdad.Text)
            sql.ExecuteNonQuery()

            MsgBox("Registrado exitosamente.", vbInformation, "")

            Dim ve As New VentanaEstudiante
            ve.Show()
            Hide()

        Catch ex As Exception
            MsgBox(ex.ToString, vbExclamation, "Error")
        End Try

        conexion.Close()



    End Sub

    Private Sub VentanaRegistro_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim vi As New VentanaInicio
        vi.Show()
        Dispose()
    End Sub
End Class