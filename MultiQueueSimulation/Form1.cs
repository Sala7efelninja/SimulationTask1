using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;


namespace MultiQueueSimulation
{
    public partial class Form1 : Form
    {

        SimSystem system=new SimSystem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            system.SetPriorityServer(1);
            system.StartSimulation();
            dataGridView1.DataSource = system.SimulationTable;
            
            String testResult = TestingManager.Test(system, Constants.FileNames.TestCase1);
            MessageBox.Show(testResult);
        }
    }
}
