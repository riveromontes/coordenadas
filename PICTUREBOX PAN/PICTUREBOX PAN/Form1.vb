Public Class Form1

    Dim BM1 As Bitmap 'GUARDA LA IMAGEN INICIAL
    Dim MIX As Integer 'GUARDA LA POSICION INICIAL EN X DEL MOUSE
    Dim MIY As Integer 'GUARDA LA POSICION INICIAL EN Y DEL MOUSE
    Dim MUEVE As Boolean 'CONTROLA EL MOVIMIENTO DEL MOUSE
    Dim posX As Integer 'PARA EL MOVIMIENTO POR BOTONES
    Dim posY As Integer 'PARA EL MOVIMIENTO POR BOTONES

    'PARA PINTAR PUNTOS
    Dim PuedoPintar As Boolean = False
    Dim p As New Pen(Brushes.Red, 4.9)
    Dim p2 As New Pen(Brushes.Red, 3.5)
    Dim g As Graphics
    Dim ph As New Drawing2D.GraphicsPath(Drawing2D.FillMode.Alternate)
    Dim Swich As Boolean = False
    Dim x1, y1, x2, y2
    Dim x_inicial, y_inicial As Integer 'PARA PINTAR LINEAS Y PUNTOS


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'PARAMETROS NECESARIOS
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize 'AJUSTA EL TAMAÑO DEL PICTUREBOX AL TAMAÑO DE LA IMAGEN
        Panel1.AutoScroll = True   'MUESTRA LAS BARRAS DE SCROLL

        posX = 0
        posY = 0

        x_inicial = 0
        y_inicial = 0





    End Sub

    Private Sub ButtonIMAGEN_Click(sender As System.Object, e As System.EventArgs) Handles ButtonIMAGEN.Click

        'LOCALIZA LA IMAGEN. GUARDA UNA COPIA EN BM1. LA PRESENTA EN EL PICTUREBOX
        If OpenFileDialog1.ShowDialog Then
            BM1 = Bitmap.FromFile(OpenFileDialog1.FileName)
            PictureBox1.Image = BM1

            'PARA PINTAR PUNTOS
            g = Graphics.FromImage(Me.PictureBox1.Image)
        End If

    End Sub

    Private Sub ButtonAUMENTO_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAUMENTO.Click

        'CADA VEZ QUE SE CLICA EL BOTON SE AUMENTA EL TAMAÑO DE LA IMAGEN UN 10%
        Dim BM2 As New Bitmap(BM1, PictureBox1.Image.Width * 1.1, PictureBox1.Image.Height * 1.1)
        PictureBox1.Image = BM2

    End Sub

    Private Sub ButtonDISMINUCION_Click(sender As System.Object, e As System.EventArgs) Handles ButtonDISMINUCION.Click

        'CADA VEZ QUE SE CLICA EL BOTON SE DISMINUYE EL TAMAÑO DE LA IMAGEN UN 10%
        Dim BM2 As New Bitmap(BM1, PictureBox1.Image.Width * 0.9, PictureBox1.Image.Height * 0.9)
        PictureBox1.Image = BM2

    End Sub
   
    Private Sub PictureBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown

        'DETERMINA LA POSICION EN LA QUE SE HA PRESIONADO EL MOUSE
        MIX = e.Location.X
        MIY = e.Location.Y

        MUEVE = True 'SE HA INICIADO EL DESPLAZAMIENTO

    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove

        If MUEVE Then

            'DETERMINA LA DIFERENCIA DE POSICION DEL MOUSE (donde se presiona - donde se mueve)
            Dim DIFX As Integer = (MIX - e.X)
            Dim DIFY As Integer = (MIY - e.Y)

            'NUEVA POSICION DE LAS BARRAS DE SCROLL DEL PANEL
            Panel1.AutoScrollPosition = New Point((DIFX - Panel1.AutoScrollPosition.X), (DIFY - Panel1.AutoScrollPosition.Y))

        End If

    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp

        MUEVE = False 'SE HA TERMINADO EL DESPLAZAMIENTO

    End Sub

    Private Sub ButtonINICIAL_Click(sender As System.Object, e As System.EventArgs) Handles ButtonINICIAL.Click

        'IMAGEN Y PICTUREBOX VUELVEN A LOS VALORES INICIALES
        PictureBox1.Image = BM1
        Panel1.AutoScrollPosition = New Point(0, 0)

    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        posX = Panel1.HorizontalScroll.Value


        'esto es para llegar al maximo valor modificable
        If ((Panel1.HorizontalScroll.Maximum + 1) - Panel1.HorizontalScroll.LargeChange) > (posX - 10) Then
            posX = posX + 10
        End If

        Panel1.AutoScrollPosition = New Point(posX, posY)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        posX = posX - 10
        Panel1.AutoScrollPosition = New Point(posX, posY)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        posY = Panel1.VerticalScroll.Value


        'esto es para llegar al maximo valor modificable
        If ((Panel1.VerticalScroll.Maximum + 1) - Panel1.VerticalScroll.LargeChange) > (posY - 10) Then
            posY = posY + 10
        End If

        Panel1.AutoScrollPosition = New Point(posX, posY)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click



    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim x_final As Integer
        Dim y_final As Integer


        If (x_inicial = y_final) And (x_inicial = 0) Then
            x_inicial = TextBox1.Text
            y_inicial = TextBox2.Text
            g.DrawEllipse(Me.p, x_inicial, y_inicial, 3, 3)
        Else
            x_final = TextBox1.Text
            y_final = TextBox2.Text
            g.DrawEllipse(Me.p, x_final, y_final, 3, 3)
            'DEFINO LOS EXTREMOS DE LA LINEA ANTES DE PINTARLA
            p2.StartCap = Drawing2D.LineCap.RoundAnchor
            p2.EndCap = Drawing2D.LineCap.ArrowAnchor
            g.DrawLine(Me.p2, x_inicial, y_inicial, x_final, y_final)

            x_inicial = x_final
            y_inicial = y_final
        End If






        g.Flush()
        Me.PictureBox1.Refresh()

    End Sub
End Class
