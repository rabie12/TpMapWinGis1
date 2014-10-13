Imports MapWinGIS
Public Class Form1


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sh As New Shapefile
        Dim odf As New OpenFileDialog

        If odf.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim ouvert As Boolean = sh.Open(odf.FileName)
            If ouvert Then
                AxMap1.AddLayer(sh, True)
                AxMap1.ZoomToMaxExtents()
            Else
                MessageBox.Show("le fichier est endommagé")
            End If
        End If
        

    End Sub
End Class
