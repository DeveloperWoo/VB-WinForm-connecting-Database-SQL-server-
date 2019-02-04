Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connectionDB.Open()
        query = "SELECT * FROM dbo.jobs"
        sqlCommand = New SqlCommand(query, connectionDB)
        dataAdapter = New SqlDataAdapter(sqlCommand)
        dataSet = New DataSet
        dataAdapter.Fill(dataSet, "Job")

        ListBox1.DataSource = dataSet.Tables("Job")
        ListBox1.DisplayMember = "job_id"

        bindingSource = New BindingSource
        bindingSource.DataSource = dataSet.Tables("Job")

        txtJobID.DataBindings.Add("Text", bindingSource, "job_id")
        txtJobTitle.DataBindings.Add("Text", bindingSource, "job_title")
        txtMinSal.DataBindings.Add("Text", bindingSource, "min_salary")
        txtMaxSal.DataBindings.Add("Text", bindingSource, "max_salary")

        ListBox1.SelectedIndex = 0
        DisplayEmployees()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Not IsNothing(bindingSource) Then
            bindingSource.Position = ListBox1.SelectedIndex
        End If
        DisplayEmployees()
    End Sub

    Private Sub DisplayEmployees()
        If Not IsNothing(dataSet.Tables("Employees")) Then
            dataSet.Tables("Employees").Clear()
        End If
        query = "SELECT job_id, employee_id, first_name, last_name, hire_date, salary " &
                "FROM employees " &
                "WHERE job_id = '" & txtJobID.Text & "'"

        sqlCommand.CommandText = query
        dataAdapter.SelectCommand = sqlCommand
        dataAdapter.Fill(dataSet, "Employees")

        DataGridView1.DataSource = dataSet.Tables("Employees")
    End Sub

End Class
