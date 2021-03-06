﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsManager.Contract.Interfaces;
using ThreadsManager.Contract.Models;

namespace ThreadsManager.DataSql.DbManager
{
    public class DbManager : IDatabaseManager
    {
        private readonly string ConnectionString;
        private OleDbConnection con = null;
        private OleDbCommand cmd = null;

        public const string CREATE_QUERY =
            @"CREATE TABLE Thread
                (
                    ID int IDENTITY(1,1) NOT NULL,
                    Sequence nvarchar(10) NOT NULL,
                    Thread_ID int NOT NULL,
                    DateT datetime NOT NULL,
                    CONSTRAINT pk_id PRIMARY KEY (ID)
                );";

        public DbManager()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory + "MDB_File\\Thread121.mdb";

            if (dir.Contains("\\bin\\Debug"))
            {
                dir = dir.Replace("\\bin\\Debug", "");
            }

            ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                               + dir + ";\r\nPersist Security Info=False;";
        }

        public void OpenConnection()
        {
            con = new OleDbConnection(ConnectionString);
            cmd = new OleDbCommand
            {
                Connection = con
            };

            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public void InitializeDatabase()
        {
            OleDbConnection Connection = new OleDbConnection(ConnectionString); /*possible way to wrapping into 'using()' for dispose method for mem leaks */

            OleDbCommand Command = new OleDbCommand(CREATE_QUERY, Connection);

            Connection.Open();
            Command.ExecuteNonQuery();
        }

        public void InsertInformationToDb(ThreadInformation data)
        {
            data.DateTime =  new DateTime(data.DateTime.Year, data.DateTime.Month, data.DateTime.Day,
                data.DateTime.Hour, data.DateTime.Minute, data.DateTime.Second, data.DateTime.Kind);
           
            cmd.CommandText = @"INSERT INTO Thread(Sequence, Thread_ID, DateT) Values(@FN, @LN, @GN)";
            cmd.Parameters.AddWithValue("@FN", data.Sequence);
            cmd.Parameters.AddWithValue("@LN", data.ThreadId);
            cmd.Parameters.Add(new OleDbParameter("@GN", data.DateTime));

            cmd.ExecuteNonQuery();
            
        }
    }
}
