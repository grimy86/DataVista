using DataVista.Database;
using DataVista.System;
using System.Windows.Controls; //For the DataGrid & TextBlock

class UsingDatabase
{
    DataGrid dataGrid = new DataGrid();
    string query = "SELECT TOP (50) * FROM [Northwind].[dbo].[Customers]";

    void FillWithOperation()
    {
        Operation operation = new Operation();
        DataTable dataTable = databaseOperation.ExecuteSQL<DataTable>(query, databaseOperation.ExecuteReader);

        dataGrid.ItemsSource = dataTable.DefaultView;
    }

    void FillWithExtensionMethod()
    {
        DataTable dataTable = new DataTable();
        dataTable.ExecuteReader(query);

        dataGrid.ItemsSource = dataTable.DefaultView;
    }
}

class UsingSystem
{
    TextBlock textBlock = new TextBlock();

    void ShowFPS()
    {
        //Framerate.Count.GetType() returns double.
        textBlock.Text = $"[ FPS: {Framerate.Count.ToString("0")} ]";
    }

    void ShowEnvironment()
    {
        Hardware hardware = new Hardware();
        textBlock.Text = hardware.ToString();
    }

    void ShowUniqueProcesses()
    {
        textBlock.Text = WinProcess.UniqueProcesses;
    }
}
