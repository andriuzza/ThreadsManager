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
using ThreadsManager.Helper;

namespace ThreadsManager
{
    public partial class Form1 : Form
    {
        private readonly IDatabaseManager _manager;
        private readonly IThreadsHelper _helper;

        public Form1(IDatabaseManager manager,
            IThreadsHelper helper)
        {
            _manager = manager;
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

        private void InsertListItems(ThreadInformation information)
        {
            string[] row = { information.ThreadId.ToString(), information.Sequence };
            var listViewItem = new ListViewItem(row);
            listView1.Items.Insert(0, listViewItem);

            if (listView1.Items.Count >= 20)
            {
                listView1.Items.RemoveAt(19);
            }

            
            var msg = _manager.InsertInformationToDb(information);

            if (!msg.Equals("Success"))
            {
                MessageBox.Show(msg);
            }
        }

        private void DoThreadWork()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var thread = _helper.GetPairOfIdAndSequence();

                    if (listView1.InvokeRequired)
                    {
                        listView1.Invoke((MethodInvoker) delegate {
                            InsertListItems(thread);
                        });
                    }
                    else
                    {
                        InsertListItems(thread);
                    }
                }
            }).Start();
        }
    }
}
