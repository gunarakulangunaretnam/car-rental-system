using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestWindowService
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;

        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 5000; //every 10 secs
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            Library.WriteErrorLog("The CRMS Online system is started");
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            //Write code here to do some job depends on your requirement
            Library.WriteErrorLog("Searching for the specified server IP 198.67.36.55");
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            Library.WriteErrorLog("The server is not found Network error 404!");
        }
    }
}
