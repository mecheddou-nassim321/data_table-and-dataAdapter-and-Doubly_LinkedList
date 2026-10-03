using System;

using System.Linq;

using System.Data;

namespace DataTableExample
{
   public class Program
    {
        static void Main(string[] args)
        {
            DataTable employeeTable = new DataTable("EmployesDataTable");
          

            DataColumn dtcolumn;
            dtcolumn = new DataColumn();
            dtcolumn.ColumnName = "ID";
            dtcolumn.DataType = typeof(int);
            dtcolumn.AutoIncrement = true;
            dtcolumn.AutoIncrementStep=1;
            dtcolumn.Caption = "ID";
            dtcolumn.ReadOnly = true;
            dtcolumn.Unique = true;
            employeeTable.Columns.Add(dtcolumn);
            dtcolumn = new DataColumn();
            dtcolumn.ColumnName = "Name";
            dtcolumn.DataType = typeof(string);
            dtcolumn.AutoIncrement = false;
            dtcolumn.Unique = false;
            dtcolumn.ReadOnly = false;
            dtcolumn.Caption = "Name";
            employeeTable.Columns.Add(dtcolumn);

            dtcolumn = new DataColumn();
            dtcolumn.ColumnName = "country";
            dtcolumn.DataType = typeof(string);
            dtcolumn.AutoIncrement = false;
            dtcolumn.Unique = false;
            dtcolumn.ReadOnly = false;
            dtcolumn.Caption = "country";
            employeeTable.Columns.Add(dtcolumn);

            dtcolumn = new DataColumn();
            dtcolumn.ColumnName = "Salary";
            dtcolumn.DataType = typeof(Double);
            dtcolumn.AutoIncrement = false;
            dtcolumn.Unique = false;
            dtcolumn.ReadOnly = false;
            dtcolumn.Caption = "Salary";
            employeeTable.Columns.Add(dtcolumn);


            dtcolumn = new DataColumn();
            dtcolumn.ColumnName = "Date";
            dtcolumn.DataType = typeof(DateTime);
            dtcolumn.AutoIncrement = false;
            dtcolumn.Unique = false;
            dtcolumn.ReadOnly = false;
            dtcolumn.Caption = "Date";
            employeeTable.Columns.Add(dtcolumn);




            DataTable DepartmentDataTable = new DataTable("DeparmentsDataTabel");
            DepartmentDataTable.Columns.Add("ID", typeof(int));
            DepartmentDataTable.Columns.Add("Name", typeof(string));

            DepartmentDataTable.Rows.Add(1, "Marketing");
            DepartmentDataTable.Rows.Add(2, "IT");
            DepartmentDataTable.Rows.Add(3, "HR");
            
            foreach(DataRow reccordRow in DepartmentDataTable.Rows)
            {
                Console.WriteLine("ID:{0},Name:{1}", reccordRow["ID"], reccordRow["Name"]);
            }



            //primary key
            DataColumn[] primartKeyColumns = new DataColumn[1];
            primartKeyColumns[0]=employeeTable.Columns["ID"];
            employeeTable.PrimaryKey = primartKeyColumns;

            employeeTable.Rows.Add(1, "Ali", "Algeria", 50000, DateTime.Now);
            employeeTable.Rows.Add(2, "Sara", "slovinia", 60000, DateTime.Now);
            employeeTable.Rows.Add(3, "Yahya", "kuwait", 70000, DateTime.Now);

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(employeeTable);
            dataSet.Tables.Add(DepartmentDataTable);

            foreach(DataRow recordrow in dataSet.Tables["EmployesDataTable"].Rows)
            {
                Console.WriteLine("ID: {0},Name:{1},Countery:{2},salary:{3},Date:{4}", recordrow["ID"], recordrow["Name"], recordrow["Country"], recordrow["Salary"], recordrow["Date"]);
            }
            foreach(DataRow reccordRow  in dataSet.Tables["DeparmentsDataTabel"].Rows)
            {
                Console.WriteLine("ID:{0},Name:{1}", reccordRow["ID"], reccordRow["Name"]);
            }

            DataView EmployeeDataView1 = employeeTable.DefaultView;
            for(int i = 0; i < EmployeeDataView1.Count; i++)
            {
                Console.WriteLine("{0} {1}  {2}  {3}", EmployeeDataView1[i][0], EmployeeDataView1[i][1], EmployeeDataView1[i][2], EmployeeDataView1[i][3]);
            }
            //Data View Filtering
            EmployeeDataView1.RowFilter = "country='Algeria' or country='Kuwait'";
            Console.WriteLine("Filter by Country");
            for (int i = 0; i < EmployeeDataView1.Count; i++)
            {
                Console.WriteLine("{0} {1}  {2}  {3}", EmployeeDataView1[i][0], EmployeeDataView1[i][1], EmployeeDataView1[i][2], EmployeeDataView1[i][3]);
            }
            EmployeeDataView1.Sort = "Name ASC";

            Console.WriteLine("sorting by Name ASC");
            for (int i = 0; i < EmployeeDataView1.Count; i++)
            {
                Console.WriteLine("{0} {1}  {2}  {3}", EmployeeDataView1[i][0], EmployeeDataView1[i][1], EmployeeDataView1[i][2], EmployeeDataView1[i][3]);
            }

            /*     int employeeCount = 0;
                 double totalSalries = 0;
                 double averageSalary = 0;
                 double MaxSalary = 0;
                 double MinSalary = 0;
                 employeeCount = employeeTable.Rows.Count;
                 totalSalries = Convert.ToDouble(employeeTable.Compute("SUM(Salary)", string.Empty));
                 averageSalary = Convert.ToDouble(employeeTable.Compute("AVG(Salary)", string.Empty));
                 MaxSalary = Convert.ToDouble(employeeTable.Compute("Max(Salary)", string.Empty));
                 MinSalary = Convert.ToDouble(employeeTable.Compute("Min(Salary)", string.Empty));

                 foreach (DataRow row in employeeTable.Rows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 Console.WriteLine("\t EmployeeCount:" + employeeCount);
                 Console.WriteLine("total Employee Slaries:" + totalSalries);
                 Console.WriteLine("Minimum salary:" + MinSalary);
                 Console.WriteLine("Maximum salary:" + MaxSalary);

                 int resultCount = 0;
                 DataRow[] ResultRows;
                 ResultRows = employeeTable.Select("country='Algeria'");
                 resultCount = ResultRows.Count();
                 totalSalries = Convert.ToDouble(employeeTable.Compute("SUM(Salary)", "country='Algeria'"));
                 averageSalary = Convert.ToDouble(employeeTable.Compute("AVG(Salary)", "country='Algeria'"));
                 MaxSalary = Convert.ToDouble(employeeTable.Compute("Max(Salary)", "country='Algeria'"));
                 MinSalary = Convert.ToDouble(employeeTable.Compute("Min(Salary)", "country='Algeria'"));
                 Console.WriteLine("Filter by Algeria Employees");
                 foreach (DataRow row in ResultRows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 Console.WriteLine("\t EmployeeCount:" + resultCount);
                 Console.WriteLine("total Employee Slaries:" + totalSalries);
                 Console.WriteLine("Minimum salary:" + MinSalary);
                 Console.WriteLine("Maximum salary:" + MaxSalary);
                 Console.WriteLine("Average salary:" + averageSalary);

                 ResultRows = employeeTable.Select("country='Algeria' or country='slovinia'");
                 resultCount=ResultRows.Count();
                 totalSalries = Convert.ToDouble(employeeTable.Compute("SUM(Salary)", "country = 'Algeria' or country = 'slovinia'"));
                 averageSalary = Convert.ToDouble(employeeTable.Compute("AVG(Salary)", "country = 'Algeria' or country = 'slovinia'"));
                 MaxSalary = Convert.ToDouble(employeeTable.Compute("Max(Salary)", "country = 'Algeria' or country = 'slovinia'"));
                 MinSalary = Convert.ToDouble(employeeTable.Compute("Min(Salary)", "country = 'Algeria' or country = 'slovinia'"));

                 Console.WriteLine("Filter by Algeria or slovenia Employees");

                 foreach (DataRow row in ResultRows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 Console.WriteLine("\t EmployeeCount:" + resultCount);
                 Console.WriteLine("total Employee Slaries:" + totalSalries);
                 Console.WriteLine("Minimum salary:" + MinSalary);
                 Console.WriteLine("Maximum salary:" + MaxSalary);
                 Console.WriteLine("Average salary:" + averageSalary);
                 ResultRows = employeeTable.Select("ID=1");
                 resultCount=ResultRows.Count();
                 totalSalries = Convert.ToDouble(employeeTable.Compute("SUM(Salary)", "ID=1"));
                 averageSalary = Convert.ToDouble(employeeTable.Compute("AVG(Salary)", "ID=1"));
                 MaxSalary = Convert.ToDouble(employeeTable.Compute("Max(Salary)", "ID=1"));
                 MinSalary = Convert.ToDouble(employeeTable.Compute("Min(Salary)", "ID=1"));
                 Console.WriteLine("Filter by ID=1");

                 foreach (DataRow row in ResultRows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 Console.WriteLine("\t EmployeeCount:" + resultCount);
                 Console.WriteLine("total Employee Slaries:" + totalSalries);
                 Console.WriteLine("Minimum salary:" + MinSalary);
                 Console.WriteLine("Maximum salary:" + MaxSalary);
                 Console.WriteLine("Average salary:" + averageSalary);
                 //sorting
                 employeeTable.DefaultView.Sort = "ID Desc";
                 employeeTable = employeeTable.DefaultView.ToTable();
                 Console.WriteLine("sorting by ID Desc");
                 foreach(DataRow row in employeeTable.Rows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 //Delete
                 DataRow[] Result = employeeTable.Select("ID=3");

                 foreach (var recordRow in Result)
                 {
                     recordRow.Delete();
                 }
                 Console.WriteLine("Delete ID=3");
                 foreach (DataRow row in employeeTable.Rows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 employeeTable.AcceptChanges();
                 DataRow[] resultrow = employeeTable.Select("ID=2");
                 foreach (var recordRow in resultrow)
                 {
                     recordRow["Name"] = "ff";
                     recordRow["Salary"] = "46447";
                 }

                 Console.WriteLine("Update ID=2");
                 foreach (DataRow row in employeeTable.Rows)
                 {
                     Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]},country:{row["country"]}, Salary: {row["Salary"]},Date:{row["Date"]}");
                 }
                 employeeTable.AcceptChanges();*/

        }
    }
}
