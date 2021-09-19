Imports System
Imports System.IO.Ports
Imports System.Threading

Public Class Form1

    Public mensajesalida As String, mensajeentrada As String, p As String, x As Boolean = False

    Private Delegate Sub DelegadoAcceso(ByVal Adicional As String)
    Private Sub AccesoFormPrincipal(ByVal TextoForm As String)

        mensajeentrada = TextoForm
        TextBox3.Text = mensajeentrada

        Dim a() As String, s As String
        Dim x As Integer, y As Integer, imagen As Integer



        s = mensajeentrada
        a = Split(s, ";")
        TextBox4.Text = a(0) 'eje x
        TextBox5.Text = a(1) 'eje y
       
        x = Val(a(0))
        y = Val(a(1))
        imagen = Val(a(2))

        '' Graficar
        Me.Chart1.Series(0).Points.AddXY(0, x)
        Me.Chart2.Series(0).Points.AddXY(0, y)

        If (imagen = 0) Then
            PictureBox1.Visible = True
        Else
            PictureBox1.Visible = False

        End If





    End Sub
    Private Sub PuertaAccesoInterrupcion(ByVal BufferIn As String)

        Dim TextoInterrupcion() As Object = {BufferIn}
        Dim DelegadoInterrupcion As DelegadoAcceso
        DelegadoInterrupcion = New DelegadoAcceso(AddressOf AccesoFormPrincipal)
        MyBase.Invoke(DelegadoInterrupcion, TextoInterrupcion)





    End Sub
    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'limpia los datos del combo
        ComboBox1.Items.Clear()
        'ínicio un ciclo para cargar los puertos
        For Each puerto_disponible In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(puerto_disponible)
        Next
        If (ComboBox1.Items.Count > 0) Then
            ComboBox1.Text = ComboBox1.Items(0)
            Button2.Enabled = True
        Else
            MsgBox("No existe puertos disponibles")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (Button2.Text = "Conectar") Then
            Button2.Text = "Desconectar"
            SerialPort1.PortName = ComboBox1.Text
            SerialPort1.Open()

            Timer1.Enabled = True
        ElseIf (Button2.Text = "Desconectar") Then
            Button2.Text = "Conectar"
            SerialPort1.Close()

            Timer1.Enabled = False
        End If
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        
    End Sub
    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

        Dim DatoInterrupcion As String
        DatoInterrupcion = SerialPort1.ReadLine
        PuertaAccesoInterrupcion(DatoInterrupcion)

        'MsgBox(DatoInterrupcion)
        SerialPort1.DiscardInBuffer()




    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        'ínicio un ciclo para cargar los puertos
        For Each puerto_disponible In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(puerto_disponible)
        Next
        If (ComboBox1.Items.Count > 0) Then
            ComboBox1.Text = ComboBox1.Items(0)
            Button2.Enabled = True
        Else
            MsgBox("No existe puertos disponibles")
        End If

       
    End Sub

End Class
