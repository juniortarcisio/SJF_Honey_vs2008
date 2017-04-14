namespace SJF_Honey
{
    public class SJFProcess
    {
        public char ProcessID { get; set; }
        public int ArrivalTime { get; set; }
        public int CPUBurst { get; set; }
        public int ExecutionOrder { get; set; }
        public int WaitingTime { get; set; }

        public int RunningTime
        {
            get { return this.ArrivalTime + this.WaitingTime; }
        }

        public int TurnAroundTime
        {
            get { return this.CPUBurst + this.WaitingTime; }
        }

        public SJFProcess(char id, int at, int bt)
        {
            ProcessID = id;
            ArrivalTime = at;
            CPUBurst = bt;
            this.WaitingTime = 0;
            ExecutionOrder = -1;
        }
    }
}
