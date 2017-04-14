using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJF_Honey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void frmSTF_Load(object sender, EventArgs e)
        {

        }

        List<SJFProcess> list;

        private void btnStart_Click(object sender, EventArgs e)
        {
            ReadValues();
            Process();
        }

        private void ReadValues()
        {
            int at, bt;

            list = new List<SJFProcess>();

            int.TryParse(txtArrivalTime1.Text, out at);
            int.TryParse(txtCPUBurst1.Text, out bt);
            list.Add(new SJFProcess('A', at, bt));

            int.TryParse(txtArrivalTime2.Text, out at);
            int.TryParse(txtCPUBurst2.Text, out bt);
            list.Add(new SJFProcess('B', at, bt));

            int.TryParse(txtArrivalTime3.Text, out at);
            int.TryParse(txtCPUBurst3.Text, out bt);
            list.Add(new SJFProcess('C', at, bt));

            int.TryParse(txtArrivalTime4.Text, out at);
            int.TryParse(txtCPUBurst4.Text, out bt);
            list.Add(new SJFProcess('D', at, bt));

            int.TryParse(txtArrivalTime5.Text, out at);
            int.TryParse(txtCPUBurst5.Text, out bt);
            list.Add(new SJFProcess('E', at, bt));

        }

        private void Process()
        {
            SJFProcess currentProcess;
            int totalTime = 0;
            int order = 0;
            int finalTime;

            //While I have process without an execution order
            while (list.Where(x => x.ExecutionOrder == -1).Count() > 0)
            {
                //Search if there is some process already waiting
                var waitingProcess = list.Where(x => x.ExecutionOrder == -1 & x.ArrivalTime <= totalTime);

                //If there is one or more process already waiting, get the smaller burst time
                if (waitingProcess.Count() > 0)
                {
                    currentProcess = waitingProcess.OrderBy(x => x.CPUBurst).ToList()[0];
                    currentProcess.WaitingTime = totalTime - currentProcess.ArrivalTime;
                }
                else //Else get the next to arrive (uses AT on ordenation, because will run the next first to arrives)
                {
                    currentProcess = list.Where(x => x.ExecutionOrder == -1).OrderBy(x => x.ArrivalTime).ThenBy(x => x.CPUBurst).ToList()[0];
                    currentProcess.WaitingTime = 0;
                }

                currentProcess.ExecutionOrder = order;
                order++;


                if (currentProcess.ArrivalTime > totalTime)
                    totalTime += currentProcess.ArrivalTime - totalTime;

                totalTime += currentProcess.CPUBurst;

            }


            //Show the waiting time 
            txtWaitingTime1.Text = list[0].WaitingTime.ToString();
            txtWaitingTime2.Text = list[1].WaitingTime.ToString();
            txtWaitingTime3.Text = list[2].WaitingTime.ToString();
            txtWaitingTime4.Text = list[3].WaitingTime.ToString();
            txtWaitingTime5.Text = list[4].WaitingTime.ToString();


            //Shows the Turn Around Time
            txtTurnAroundTime1.Text = list[0].TurnAroundTime.ToString();
            txtTurnAroundTime2.Text = list[1].TurnAroundTime.ToString();
            txtTurnAroundTime3.Text = list[2].TurnAroundTime.ToString();
            txtTurnAroundTime4.Text = list[3].TurnAroundTime.ToString();
            txtTurnAroundTime5.Text = list[4].TurnAroundTime.ToString();

            txtTurnAroundTimeTotal.Text = list.Sum(x => x.TurnAroundTime).ToString();
            txtTurnAroundTimeAverage.Text = (Convert.ToDecimal(list.Sum(x => x.TurnAroundTime)) / 5m).ToString();


            //Order the list by the execution order
            list = list.OrderBy(x => x.ExecutionOrder).ToList();

            //Show the execution order
            txtSequence1.Text = list[0].ProcessID.ToString();
            txtSequence2.Text = list[1].ProcessID.ToString();
            txtSequence3.Text = list[2].ProcessID.ToString();
            txtSequence4.Text = list[3].ProcessID.ToString();
            txtSequence5.Text = list[4].ProcessID.ToString();

            //Show the running time and final time for each process
            lblSequence0.Text = list[0].RunningTime.ToString();
            lblSequence1.Text = list[1].RunningTime.ToString();
            lblSequence2.Text = list[2].RunningTime.ToString();
            lblSequence3.Text = list[3].RunningTime.ToString();
            lblSequence4.Text = list[4].RunningTime.ToString();

            finalTime = list[4].RunningTime + list[4].CPUBurst;

            lblSequence5.Text = finalTime.ToString();

            txtWaitingTimeTotal.Text = list.Sum(x => x.WaitingTime).ToString();
            txtWaitingTimeAverage.Text = (Convert.ToDecimal(list.Sum(x => x.WaitingTime)) / 5m).ToString();


        }
    }
}
