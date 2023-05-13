Imports MySql.Data.MySqlClient

Module Module1
    Public connection As New MySqlConnection("datasource=localhost;port=3306;username=root;password=password")
    Public sqlcmd As MySqlCommand
    Public adapter As New MySqlDataAdapter
    Public bSource As New BindingSource
    Public Reader As MySqlDataReader
    Public data As New DataTable()
    Public Query As String
    Public conStr As String()

End Module

Public Class Form2

    Dim sID As String

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sID = intID.Text
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged

    End Sub

    Private Sub intPhoneNumber_TextChanged(sender As Object, e As EventArgs) Handles intPhoneNumber.TextChanged

    End Sub

    Private Sub txtSex_TextChanged(sender As Object, e As EventArgs) Handles txtSex.TextChanged

    End Sub

    Private Sub txtLastName_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged

    End Sub

    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged

    End Sub

    Private Sub ID_TextChanged(sender As Object, e As EventArgs) Handles intID.TextChanged
        Opacity = 90
    End Sub

    Private Sub intID_Click(sender As Object, e As EventArgs) Handles lblID.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim fsize As UInt32
            Dim ms As New System.IO.MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgArray() As Byte = ms.GetBuffer
            fsize = ms.Length
            ms.Close()

            connection.Open()
            Query = "UPDATE testing.sus SET `ID`=@ID, `First_Name`=@First_Name, `Last_Name`=@Last_name, `Sex`=@Sex,
                  `Date_of_Birth`=@Date_of_Birth, `Phone_Number`=@Phone_Number, `Address`=@Address, `Photo`=@Photo WHERE `ID`=@sID"
            sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
            sqlcmd.Parameters.AddWithValue("@ID", intID.Text)
            sqlcmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text)
            sqlcmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text)
            sqlcmd.Parameters.AddWithValue("@Sex", txtSex.Text)
            sqlcmd.Parameters.AddWithValue("@Date_of_Birth", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
            sqlcmd.Parameters.AddWithValue("@Phone_Number", intPhoneNumber.Text)
            sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            sqlcmd.Parameters.AddWithValue("@Photo", imgArray)
            sqlcmd.Parameters.AddWithValue("@sID", sID)
            sqlcmd.ExecuteNonQuery()
            MsgBox("Record Updated Succesfully")
            connection.Close()
            Form1.popul8Grid()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()

        End Try

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub


End Class

Public Class Form1



    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        Label8.Text = ComboBox1.SelectedItem

    End Sub

    Public Sub popul8Grid()
        Try
            connection.Open()

            Query = "SELECT `ID`, `First_Name`, `Last_Name`, `Sex`, `Date_of_Birth`, 
                `Phone_Number`, `Address` FROM testing.sus"
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(Query, connection)
            Dim table As New DataTable
            adapter.Fill(table)
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                DataGridView1.Rows.Add()
                DataGridView1.Rows(i).Cells(0).Value = table.Rows(i).Item(0).ToString
                DataGridView1.Rows(i).Cells(1).Value = table.Rows(i).Item(1).ToString
                DataGridView1.Rows(i).Cells(2).Value = table.Rows(i).Item(2).ToString
                DataGridView1.Rows(i).Cells(3).Value = table.Rows(i).Item(3).ToString
                DataGridView1.Rows(i).Cells(4).Value = Format(Convert.ToDateTime(table.Rows(i).
                          Item(4).ToString), "MM/dd/yyyy")
                DataGridView1.Rows(i).Cells(5).Value = table.Rows(i).Item(5).ToString
                DataGridView1.Rows(i).Cells(6).Value = table.Rows(i).Item(6).ToString

            Next
            connection.Close()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()
        End Try



    End Sub

    Private Sub search()
        Try
            connection.Open()

            Query = "Select * from testing.sus where " & Label8.Text & " Like '" & txtGet.Text & "%' "
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(Query, connection)
            Dim table As New DataTable
            adapter.Fill(table)
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                DataGridView1.Rows.Add()
                DataGridView1.Rows(i).Cells(0).Value = table.Rows(i).Item(0).ToString
                DataGridView1.Rows(i).Cells(1).Value = table.Rows(i).Item(1).ToString
                DataGridView1.Rows(i).Cells(2).Value = table.Rows(i).Item(2).ToString
                DataGridView1.Rows(i).Cells(3).Value = table.Rows(i).Item(3).ToString
                DataGridView1.Rows(i).Cells(4).Value = Format(Convert.ToDateTime(table.Rows(i).
                          Item(4).ToString), "MM/dd/yyyy")
                DataGridView1.Rows(i).Cells(5).Value = table.Rows(i).Item(5).ToString
                DataGridView1.Rows(i).Cells(6).Value = table.Rows(i).Item(6).ToString

            Next
            connection.Close()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()
        End Try


    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click, Label7.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        popul8Grid()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Function getImage(ByVal ID As String) As System.IO.MemoryStream
        Try
            connection.Open()
            Query = "Select `Photo` from `testing`.`sus` where `ID`=@ID"
            sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
            sqlcmd.Parameters.AddWithValue("@ID", ID)
            Reader = sqlcmd.ExecuteReader
            Reader.Read()
            Dim imgArr() As Byte = Reader.Item("Photo")
            Dim ms As New System.IO.MemoryStream(imgArr)
            sqlcmd.Dispose()
            connection.Close()
            Reader.Close()
            Return ms
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            connection.Open()
            Query = "INSERT INTO `testing`.`sus` (`ID`, `First_Name`, `Last_Name`, `Sex`, `Date_of_Birth`, `Phone_Number`, `Address`, `Photo`)
                VALUES (@ID, @First_Name, @Last_Name, @Sex, @Date_of_Birth, @Phone_Number, @Address, @Photo)"

            Dim fsize As UInt32
            Dim ms As New System.IO.MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgArray() As Byte = ms.GetBuffer
            fsize = ms.Length
            ms.Close()

            sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
            sqlcmd.Parameters.AddWithValue("@ID", intID.Text)
            sqlcmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text)
            sqlcmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text)
            sqlcmd.Parameters.AddWithValue("@Sex", txtSex.Text)
            sqlcmd.Parameters.AddWithValue("@Date_of_Birth", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
            sqlcmd.Parameters.AddWithValue("@Phone_Number", intPhoneNumber.Text)
            sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            sqlcmd.Parameters.AddWithValue("@Photo", imgArray)
            sqlcmd.ExecuteNonQuery()

            MessageBox.Show("Record Saved")
            clearControl()


        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
        End Try
        connection.Close()
        popul8Grid()

    End Sub

    Private Sub clearControl()
        Try
            For Each c In Me.Controls
                If TypeOf c Is TextBox Then
                    c.Text = String.Empty
                End If
            Next
            PictureBox1.Image = Nothing

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged

    End Sub

    Private Sub intID_TextChanged(sender As Object, e As EventArgs) Handles intID.TextChanged

    End Sub

    Private Sub txtSex_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtLast_Name_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged

    End Sub

    Private Sub txtFirst_Name_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged

    End Sub

    Private Sub intPhoneNumber_TextChanged(sender As Object, e As EventArgs) Handles intPhoneNumber.TextChanged

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            connection.Open()
            If DataGridView1.SelectedRows.Count > 0 Then
                intID.Text = DataGridView1.SelectedRows.Item(0).Cells(0).Value
                Dim message As String = "Do you want to delete the record?"
                Dim title As String = "Delete"
                Dim button As MessageBoxButtons = MessageBoxButtons.YesNo
                Dim result As DialogResult = MessageBox.Show(message, title, button)

                If (result = DialogResult.Yes) Then

                    Query = "delete from testing.sus where ID='" & intID.Text & "'"
                    sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
                    Reader = sqlcmd.ExecuteReader
                    Reader.Close()
                    connection.Close()
                Else
                    Reader.Close()
                    connection.Close()
                End If

            Else


            End If

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
            connection.Close()
        End Try
        popul8Grid()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        popul8Grid()
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                With Form2
                    .PictureBox1.Image = Image.FromStream(getImage(DataGridView1.SelectedRows.Item(0).Cells(0).Value))
                    .intID.Text = DataGridView1.SelectedRows.Item(0).Cells(0).Value
                    .txtFirstName.Text = DataGridView1.SelectedRows.Item(0).Cells(1).Value
                    .txtLastName.Text = DataGridView1.SelectedRows.Item(0).Cells(2).Value
                    .txtSex.Text = DataGridView1.SelectedRows.Item(0).Cells(3).Value
                    .DateTimePicker1.Value = Convert.ToDateTime(DataGridView1.SelectedRows.Item(0).Cells(4).Value)
                    .intPhoneNumber.Text = DataGridView1.SelectedRows.Item(0).Cells(5).Value
                    .txtAddress.Text = DataGridView1.SelectedRows.Item(0).Cells(6).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Here are they")
            End If
        Catch ex As Exception

        End Try
        connection.Close()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim fsize As UInt32
            Dim ms As New System.IO.MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgArray() As Byte = ms.GetBuffer
            fsize = ms.Length
            ms.Close()

            connection.Open()
            If DataGridView1.SelectedCells.Count > 0 Then
                Query = "UPDATE testing.sus SET `ID`=@ID, `First_Name`=@First_Name, `Last_Name`=@Last_name, `Sex`=@Sex,
                      `Date_of_Birth`=@Date_of_Birth, `Phone_Number`=@Phone_Number, `Address`=@Address, `Photo`=@Photo WHERE `ID`=@ID"
                sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
                sqlcmd.Parameters.AddWithValue("@ID", intID.Text)
                sqlcmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text)
                sqlcmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text)
                sqlcmd.Parameters.AddWithValue("@Sex", txtSex.Text)
                sqlcmd.Parameters.AddWithValue("@Date_of_Birth", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                sqlcmd.Parameters.AddWithValue("@Phone_Number", intPhoneNumber.Text)
                sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text)
                sqlcmd.Parameters.AddWithValue("@Photo", imgArray)
                sqlcmd.ExecuteNonQuery()
                MsgBox("Record Updated Succesfully")
                connection.Close()
                popul8Grid()

            End If


        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()

        End Try


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSex.SelectedIndexChanged

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtResult_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs)
        If ComboBox1.SelectedIndex > -1 Then
            Label8.Text = ComboBox1.SelectedText

        End If

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            search()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
            connection.Close()

        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_2(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
Public Class Form1



    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        Label8.Text = ComboBox1.SelectedItem

    End Sub

    Public Sub popul8Grid()
        Try
            connection.Open()

            Query = "SELECT `ID`, `First_Name`, `Last_Name`, `Sex`, `Date_of_Birth`, 
                `Phone_Number`, `Address` FROM testing.sus"
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(Query, connection)
            Dim table As New DataTable
            adapter.Fill(table)
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                DataGridView1.Rows.Add()
                DataGridView1.Rows(i).Cells(0).Value = table.Rows(i).Item(0).ToString
                DataGridView1.Rows(i).Cells(1).Value = table.Rows(i).Item(1).ToString
                DataGridView1.Rows(i).Cells(2).Value = table.Rows(i).Item(2).ToString
                DataGridView1.Rows(i).Cells(3).Value = table.Rows(i).Item(3).ToString
                DataGridView1.Rows(i).Cells(4).Value = Format(Convert.ToDateTime(table.Rows(i).
                          Item(4).ToString), "MM/dd/yyyy")
                DataGridView1.Rows(i).Cells(5).Value = table.Rows(i).Item(5).ToString
                DataGridView1.Rows(i).Cells(6).Value = table.Rows(i).Item(6).ToString

            Next
            connection.Close()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()
        End Try







    End Sub

    Private Sub search()
        Try
            connection.Open()

            Query = "Select * from testing.sus where " & Label8.Text & " Like '" & txtGet.Text & "%' "
            adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(Query, connection)
            Dim table As New DataTable
            adapter.Fill(table)
            DataGridView1.Rows.Clear()
            For i = 0 To table.Rows.Count - 1
                DataGridView1.Rows.Add()
                DataGridView1.Rows(i).Cells(0).Value = table.Rows(i).Item(0).ToString
                DataGridView1.Rows(i).Cells(1).Value = table.Rows(i).Item(1).ToString
                DataGridView1.Rows(i).Cells(2).Value = table.Rows(i).Item(2).ToString
                DataGridView1.Rows(i).Cells(3).Value = table.Rows(i).Item(3).ToString
                DataGridView1.Rows(i).Cells(4).Value = Format(Convert.ToDateTime(table.Rows(i).
                          Item(4).ToString), "MM/dd/yyyy")
                DataGridView1.Rows(i).Cells(5).Value = table.Rows(i).Item(5).ToString
                DataGridView1.Rows(i).Cells(6).Value = table.Rows(i).Item(6).ToString

            Next
            connection.Close()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()
        End Try


    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click, Label7.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        popul8Grid()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Function getImage(ByVal ID As String) As System.IO.MemoryStream
        Try
            connection.Open()
            Query = "Select `Photo` from `testing`.`sus` where `ID`=@ID"
            sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
            sqlcmd.Parameters.AddWithValue("@ID", ID)
            Reader = sqlcmd.ExecuteReader
            Reader.Read()
            Dim imgArr() As Byte = Reader.Item("Photo")
            Dim ms As New System.IO.MemoryStream(imgArr)
            sqlcmd.Dispose()
            connection.Close()
            Reader.Close()
            Return ms
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            connection.Open()
            Query = "INSERT INTO `testing`.`sus` (`ID`, `First_Name`, `Last_Name`, `Sex`, `Date_of_Birth`, `Phone_Number`, `Address`, `Photo`)
                VALUES (@ID, @First_Name, @Last_Name, @Sex, @Date_of_Birth, @Phone_Number, @Address, @Photo)"

            Dim fsize As UInt32
            Dim ms As New System.IO.MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgArray() As Byte = ms.GetBuffer
            fsize = ms.Length
            ms.Close()

            sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
            sqlcmd.Parameters.AddWithValue("@ID", intID.Text)
            sqlcmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text)
            sqlcmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text)
            sqlcmd.Parameters.AddWithValue("@Sex", txtSex.Text)
            sqlcmd.Parameters.AddWithValue("@Date_of_Birth", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
            sqlcmd.Parameters.AddWithValue("@Phone_Number", intPhoneNumber.Text)
            sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text)
            sqlcmd.Parameters.AddWithValue("@Photo", imgArray)
            sqlcmd.ExecuteNonQuery()

            MessageBox.Show("Record Saved")
            clearControl()


        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
        End Try
        connection.Close()
        popul8Grid()

    End Sub

    Private Sub clearControl()
        Try
            For Each c In Me.Controls
                If TypeOf c Is TextBox Then
                    c.Text = String.Empty
                End If
            Next
            PictureBox1.Image = Nothing

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged

    End Sub

    Private Sub intID_TextChanged(sender As Object, e As EventArgs) Handles intID.TextChanged

    End Sub

    Private Sub txtSex_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtLast_Name_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged

    End Sub

    Private Sub txtFirst_Name_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged

    End Sub

    Private Sub intPhoneNumber_TextChanged(sender As Object, e As EventArgs) Handles intPhoneNumber.TextChanged

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            connection.Open()
            If DataGridView1.SelectedRows.Count > 0 Then
                intID.Text = DataGridView1.SelectedRows.Item(0).Cells(0).Value
                Dim message As String = "Do you want to delete the record?"
                Dim title As String = "Delete"
                Dim button As MessageBoxButtons = MessageBoxButtons.YesNo
                Dim result As DialogResult = MessageBox.Show(message, title, button)

                If (result = DialogResult.Yes) Then

                    Query = "delete from testing.sus where ID='" & intID.Text & "'"
                    sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
                    Reader = sqlcmd.ExecuteReader
                    Reader.Close()
                    connection.Close()
                Else
                    Reader.Close()
                    connection.Close()
                End If

            Else


            End If

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
            connection.Close()
        End Try
        popul8Grid()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        popul8Grid()
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                With Form2
                    .PictureBox1.Image = Image.FromStream(getImage(DataGridView1.SelectedRows.Item(0).Cells(0).Value))
                    .intID.Text = DataGridView1.SelectedRows.Item(0).Cells(0).Value
                    .txtFirstName.Text = DataGridView1.SelectedRows.Item(0).Cells(1).Value
                    .txtLastName.Text = DataGridView1.SelectedRows.Item(0).Cells(2).Value
                    .txtSex.Text = DataGridView1.SelectedRows.Item(0).Cells(3).Value
                    .DateTimePicker1.Value = Convert.ToDateTime(DataGridView1.SelectedRows.Item(0).Cells(4).Value)
                    .intPhoneNumber.Text = DataGridView1.SelectedRows.Item(0).Cells(5).Value
                    .txtAddress.Text = DataGridView1.SelectedRows.Item(0).Cells(6).Value
                    .ShowDialog()

                End With
            Else
                MsgBox("Here are they")
            End If
        Catch ex As Exception

        End Try
        connection.Close()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim fsize As UInt32
            Dim ms As New System.IO.MemoryStream()
            PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgArray() As Byte = ms.GetBuffer
            fsize = ms.Length
            ms.Close()

            connection.Open()
            If DataGridView1.SelectedCells.Count > 0 Then
                Query = "UPDATE testing.sus SET `ID`=@ID, `First_Name`=@First_Name, `Last_Name`=@Last_name, `Sex`=@Sex,
                      `Date_of_Birth`=@Date_of_Birth, `Phone_Number`=@Phone_Number, `Address`=@Address, `Photo`=@Photo WHERE `ID`=@ID"
                sqlcmd = New MySql.Data.MySqlClient.MySqlCommand(Query, connection)
                sqlcmd.Parameters.AddWithValue("@ID", intID.Text)
                sqlcmd.Parameters.AddWithValue("@First_Name", txtFirstName.Text)
                sqlcmd.Parameters.AddWithValue("@Last_Name", txtLastName.Text)
                sqlcmd.Parameters.AddWithValue("@Sex", txtSex.Text)
                sqlcmd.Parameters.AddWithValue("@Date_of_Birth", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                sqlcmd.Parameters.AddWithValue("@Phone_Number", intPhoneNumber.Text)
                sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text)
                sqlcmd.Parameters.AddWithValue("@Photo", imgArray)
                sqlcmd.ExecuteNonQuery()
                MsgBox("Record Updated Succesfully")
                connection.Close()
                popul8Grid()

            End If


        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            connection.Close()

        End Try


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSex.SelectedIndexChanged

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtResult_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs)
        If ComboBox1.SelectedIndex > -1 Then
            Label8.Text = ComboBox1.SelectedText

        End If

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            search()

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)

        Finally
            connection.Dispose()
            connection.Close()

        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_2(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub


    Private Sub txtGet_TextChanged(sender As Object, e As EventArgs) Handles txtGet.TextChanged

    End Sub

    Private Sub txtResult_SelectedIndexChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class
    End Sub


    Private Sub txtGet_TextChanged(sender As Object, e As EventArgs) Handles txtGet.TextChanged

    End Sub

    Private Sub txtResult_SelectedIndexChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class
