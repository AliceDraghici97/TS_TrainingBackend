using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traning.Repository.Helpers
{
    public static class SQLHelper
    {
        private static string _connString;

        public static void Initialize(string connString)
        {
            _connString = connString;
        }

        public static IEnumerable<DataRow> Get(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {tableName}", conn))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    return dataSet.Tables.Cast<DataTable>().FirstOrDefault().AsEnumerable();
                }
            }
        }

        public static DataRow GetById(string tableName, int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {tableName} WHERE Id={id}", conn))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    IEnumerable<DataRow> dataRows = dataSet.Tables.Cast<DataTable>().FirstOrDefault().AsEnumerable();
                    return dataRows.FirstOrDefault();
                }
            }
        }

        public static void Delete(string tableName, int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM {tableName} WHERE Id={id}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Insert(string tableName, IEnumerable<string> tableColumns, IEnumerable<string> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"INSERT INTO {tableName}(");

            var columnId = tableColumns.FirstOrDefault(tc => tc.ToUpper() == "ID");
            var columnIdIndex = tableColumns.ToList().IndexOf(columnId);
            List<string> valueList = values.ToList();
            valueList.RemoveAt(columnIdIndex);
            List<string> columnList = tableColumns.ToList();
            columnList.RemoveAt(columnIdIndex);
            var strInsertQuery = $"INSERT INTO {tableName}({String.Join(",", columnList)}) VALUES({ String.Join(",", valueList)});";

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand(strInsertQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(string tableName, IEnumerable<string> tableColumns, IEnumerable<string> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"UPDATE {tableName} SET");
            for (int i = 0; i < tableColumns.Count(); i++)
            {
                if (tableColumns.ToArray()[i].ToUpper() != "ID")
                {
                    stringBuilder.AppendLine($"{tableColumns.ToArray()[i]} = {values.ToArray()[i]}");
                    if (tableColumns.Count() - 1 != i)
                    {
                        stringBuilder.Append(",");
                    }
                }
            }
            stringBuilder.AppendLine($"FROM {tableName}");
            stringBuilder.AppendLine($"WHERE Id={id};");

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand cmd = new SqlCommand(stringBuilder.ToString(), conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AssureDatabase(string tableName)
        {
            string strFields = string.Empty;
            string strOptions = string.Empty;

            //string checkDBQuery = $@"if not exists(select * from sys.tables where name='{tableName}')
            //                     begin
            //                      CREATE TABLE [dbo].[{tableName}](
            //                            [Id] [int] IDENTITY(1,1) NOT NULL,
            //                            [CreatedOn] [datetime] NOT NULL,
            //                            [ModifiedOn] [datetime],
            //                            {"{0}"}
            //                            CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED 
            //                      (
            //                       [Id] ASC
            //                      )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
            //                     ) ON [PRIMARY]
            //                            {"{1}"}                                     
            //                     end";
            //switch (tableName)
            //{
            //    case "Students":
            //        strFields = "[Name][varchar](30) NOT NULL," +
            //            " [Surname] [varchar] (30) NOT NULL," +
            //            "[PhoneNo] [varchar] (30)";
            //        checkDBQuery = string.Format(checkDBQuery, strFields, strOptions);
            //        break;

            //    case "Subjects":
            //        strFields = "[Description] [varchar](100) NOT NULL," +
            //            "[CoursesNo] [int]";
            //        checkDBQuery = string.Format(checkDBQuery, strFields, strOptions);
            //        break;

            //    case "StudentsXSubjects":
            //        strFields = "[StudentId] [int] NOT NULL, [SubjectId] [int] NOT NULL, [Grade] [decimal] NOT NULL";
            //        strOptions = "ALTER TABLE [dbo].[StudentsXSubjects]  WITH NOCHECK ADD  CONSTRAINT [FK_Students] FOREIGN KEY([StudentId]) " +
            //            "REFERENCES [dbo].[Students] ([Id]) NOT FOR REPLICATION " +
            //            "ALTER TABLE [dbo].[StudentsXSubjects]  WITH NOCHECK ADD  CONSTRAINT [FK_Subjects] FOREIGN KEY([SubjectId]) " +
            //            "REFERENCES [dbo].[Subjects] ([Id])" +
            //            "NOT FOR REPLICATION";
            //        checkDBQuery = string.Format(checkDBQuery, strFields, strOptions);
            //        break;
            //}

            string checkDBQuery = $@"alter procedure createTable " +
                "@TableName varchar(50)	" +
                "as " +
                "begin " +
                "set transaction isolation level Read uncommitted " +
                "declare " +
                "@sql nvarchar(MAX), " +
                "@fields nvarchar(MAX), " +
                "@opt nvarchar(MAX) " +
                "if @TableName = 'Students' " +
                "begin " +
                "set @fields = N'[Name][varchar](30) NOT NULL, [Surname] [varchar] (30) NOT NULL, [PhoneNo] [varchar] (30)'; " +
                "set @opt = ' '; " +
                "end " +
                "if @TableName = 'Subjects' " +
                "begin " +
                "set @fields = N'[Description] [varchar](100) NOT NULL, [CoursesNo] [int]'; " +
                "set @opt = ' '; " +
                "end " +
                "if @TableName = 'StudentsXSubjects' " +
                "begin " +
                "set @fields = N'[StudentId] [int] NOT NULL, [SubjectId] [int] NOT NULL, [Grade] [decimal] NOT NULL'; " +
                "set @opt = 'ALTER TABLE [dbo].[StudentsXSubjects] WITH NOCHECK ADD  CONSTRAINT [FK_Students] " +
                "FOREIGN KEY([StudentId]) REFERENCES [dbo].[Students] ([Id]) NOT FOR REPLICATION " +
                "ALTER TABLE [dbo].[StudentsXSubjects] WITH NOCHECK ADD CONSTRAINT [FK_Subjects] " +
                "FOREIGN KEY([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]) NOT FOR REPLICATION'; " +
                "end " +
                "set @sql = N'if not exists(select * from sys.tables where name=''' + @TableName + ''')' + CHAR(13) + 'begin' + CHAR(13) + 'CREATE TABLE [dbo].[' + @TableName + ']" +
                "([Id][int] IDENTITY(1, 1) NOT NULL, " +
                "[CreatedOn] [datetime] NOT NULL, [ModifiedOn] [datetime],'+ CHAR(13)+ @fields+ CHAR(13)+ 'CONSTRAINT [PK_{'+@TableName+'}] PRIMARY KEY CLUSTERED" +
                "([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]'" +
                "+') ON [PRIMARY]'+ CHAR(13)+@opt+'end'; " +
                "EXEC sp_executesql @sql; " +
                "end; "; 
               // "go ";
                //"exec createTable '{tableName}';";
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(checkDBQuery, conn))
                {
                    sqlCmd.ExecuteNonQuery();
                }
            }
        }
    }
}