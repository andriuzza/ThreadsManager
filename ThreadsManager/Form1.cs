using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadsManager.Contract.Interfaces;
using ThreadsManager.Contract.Models;
using ThreadsManager.DataSql.DbManager;
using ThreadsManager.Helper;

namespace ThreadsManager
{
    public partial class Form1 : Form
    {
        private readonly IThreadsHelper _helper;

        public Form1(
            IThreadsHelper helper)
        {
            _helper = helper;
            InitializeComponent();
            listView1.View = View.Details;

            listView1.Columns.Add("Thread ID", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Sequence", 120, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var numberOfThreads = textBox1.Text.ValidateInput();

            button1.Enabled = false;

            for (var i = 0; i < numberOfThreads; i++)
            {
                DoThreadWork();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void InsertListItems(ThreadInformation information, DbManager db)
        {
            string[] row = { information.ThreadId.ToString(), information.Sequence };
            var listViewItem = new ListViewItem(row);
            listView1.Items.Insert(0, listViewItem);

            if (listView1.Items.Count >= 20)
            {
                listView1.Items.RemoveAt(19);
            }
        }

        private void DoThreadWork()
        {
            new Thread(() =>
            {
                var manager = new DbManager();
                manager.OpenConnection();
                while (true)
                {
                    var thread = _helper.GetPairOfIdAndSequence();
                  
                    listView1.Invoke((MethodInvoker) delegate
                    {
                        InsertListItems(thread, manager);
                        Application.DoEvents();
                    });

                    string responseMessage;

                    try
                    {
                        responseMessage = manager.InsertInformationToDb(thread);
                    }
                    catch (Exception ex)
                    {
                        responseMessage = "Failed!";
                    }

                    if (!responseMessage.Equals("Success"))
                    {
                        manager.CloseConnection();
                        MessageBox.Show(responseMessage);
                    }
                }

            }).Start();

        }
    }
}
