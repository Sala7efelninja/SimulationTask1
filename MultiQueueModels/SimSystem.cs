using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class SimSystem : SimulationSystem
    {
        public SimSystem()
        {
            simulationQueue = new Queue<int>();
            this.priorityID = -1;
            SetServersTimeTable();
            SetUsersTimeTable();
            setData();
        }
        Queue<int> simulationQueue { get; set; }
        int priorityID { get; set; }

        Random rnd = new Random();

        public void SetPriorityServer(int ID)
        {
            this.priorityID = ID;
        }

        int SelectServer(int time)
        {
            List<int> serverList = new List<int>();

            if (SelectionMethod == Enums.SelectionMethod.HighestPriority && priorityID != -1)
                for (int i = 0; i < Servers.Count; i++)
                {
                    if (Servers[i].ID == priorityID && Servers[i].FinishTime <= time)
                    {
                        return i;
                    }
                }
            // else if (SelectionMethod == Enums.SelectionMethod.Random
            for (int i = 0; i < Servers.Count; i++)
            {
                if (Servers[i].FinishTime <= time)
                    serverList.Add(i);
            }
            if (serverList.Count == 0)
                return -1;
            int index = new Random().Next(serverList.Count);
            return serverList[index];
        }

        int getTime(List<TimeDistribution> timeList, int value)
        {
            foreach (TimeDistribution T in timeList)
            {
                if (value >= T.MinRange && value <= T.MaxRange)
                    return T.Time;
            }
            return 0;
        }

        public void StartSimulation()
        {
            while (true)
            {
                if (SimulationTable.Count == 100)
                {
                    if (SimulationTable[99].EndTime != 0)
                        break;
                }
                AddUser();
                if (simulationQueue.Count > 0)
                {
                    int simulationIndex = simulationQueue.Peek();
                    int ServerIndex = SelectServer(SimulationTable[simulationIndex].ArrivalTime);
                    if (ServerIndex == -1)
                        continue;

                    simulationQueue.Dequeue();

                    if (Servers[ServerIndex].FinishTime >= SimulationTable[simulationIndex].InterArrival)
                        SimulationTable[simulationIndex].StartTime = Servers[ServerIndex].FinishTime;//F / I
                    else
                        SimulationTable[simulationIndex].StartTime = SimulationTable[simulationIndex].InterArrival;//F / I

                    SimulationTable[simulationIndex].ServiceTime = getTime(Servers[ServerIndex].TimeDistribution, SimulationTable[simulationIndex].RandomService); //G /J
                    SimulationTable[simulationIndex].EndTime = SimulationTable[simulationIndex].ServiceTime + SimulationTable[simulationIndex].StartTime;//H / K
                    Servers[ServerIndex].FinishTime = SimulationTable[simulationIndex].EndTime;

                    SimulationTable[simulationIndex].TimeInQueue = SimulationTable[simulationIndex].StartTime - SimulationTable[simulationIndex].ArrivalTime;
                }
            }
        }

        void AddUser()
        {
            if (SimulationTable.Count == 100)
                return;
            SimulationCase tmp = new SimulationCase();
            tmp.CustomerNumber = SimulationTable.Count + 1; //A
            if (tmp.CustomerNumber == 1)
            {
                tmp.RandomInterArrival = 1;//B
                tmp.InterArrival = 0;//C    
                tmp.ArrivalTime = 0;//D
            }
            else
            {
                tmp.RandomInterArrival = rnd.Next(100) + 1;//B
                tmp.InterArrival = getTime(InterarrivalDistribution, tmp.RandomInterArrival);// C   
                tmp.ArrivalTime = SimulationTable[SimulationTable.Count - 1].ArrivalTime + tmp.InterArrival; //D
            }
            tmp.RandomService = rnd.Next(100) + 1;//E
            simulationQueue.Enqueue(tmp.CustomerNumber-1);
            SimulationTable.Add(tmp);
        }
        void setData()
        {
            this.NumberOfServers = 2;
            this.StoppingNumber = 100;
            this.StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            this.SelectionMethod = Enums.SelectionMethod.HighestPriority;
        }
        void SetServersTimeTable()
        {

            Server tmp1 = new Server();
            tmp1.ID = 1;

            TimeDistribution S1T1 = new TimeDistribution();
            S1T1.Time = 2;
            S1T1.Probability = 0.30m;
            S1T1.CummProbability = 0.30m;
            S1T1.MinRange = 1;
            S1T1.MaxRange = 30;

            TimeDistribution S1T2 = new TimeDistribution();
            S1T2.Time = 3;
            S1T2.Probability = 0.28m;
            S1T2.CummProbability = 0.58m;
            S1T2.MinRange = 31;
            S1T2.MaxRange = 58;

            TimeDistribution S1T3 = new TimeDistribution();
            S1T3.Time = 4;
            S1T3.Probability = 0.25m;
            S1T3.CummProbability = 0.83m;
            S1T3.MinRange = 59;
            S1T3.MaxRange = 83;

            TimeDistribution S1T4 = new TimeDistribution();
            S1T4.Time = 5;
            S1T4.Probability = 0.17m;
            S1T4.CummProbability = 1m;
            S1T4.MinRange = 84;
            S1T4.MaxRange = 100;

            tmp1.TimeDistribution.Add(S1T1);
            tmp1.TimeDistribution.Add(S1T2);
            tmp1.TimeDistribution.Add(S1T3);
            tmp1.TimeDistribution.Add(S1T4);


            Server tmp2 = new Server();
            tmp2.ID = 2;

            TimeDistribution S2T1 = new TimeDistribution();
            S2T1.Time = 3;
            S2T1.Probability = 0.35m;
            S2T1.CummProbability = 0.35m;
            S2T1.MinRange = 1;
            S2T1.MaxRange = 35;

            TimeDistribution S2T2 = new TimeDistribution();
            S2T2.Time = 4;
            S2T2.Probability = 0.25m;
            S2T2.CummProbability = 0.60m;
            S2T2.MinRange = 36;
            S2T2.MaxRange = 60;

            TimeDistribution S2T3 = new TimeDistribution();
            S2T3.Time = 5;
            S2T3.Probability = 0.20m;
            S2T3.CummProbability = 0.80m;
            S2T3.MinRange = 61;
            S2T3.MaxRange = 80;

            TimeDistribution S2T4 = new TimeDistribution();
            S2T4.Time = 6;
            S2T4.Probability = 0.20m;
            S2T4.CummProbability = 1m;
            S2T4.MinRange = 81;
            S2T4.MaxRange = 100;

            tmp2.TimeDistribution.Add(S2T1);
            tmp2.TimeDistribution.Add(S2T2);
            tmp2.TimeDistribution.Add(S2T3);
            tmp2.TimeDistribution.Add(S2T4);

            Servers.Add(tmp1);
            Servers.Add(tmp2);
        }
        void SetUsersTimeTable()
        {

            TimeDistribution tmp1 = new TimeDistribution();
            tmp1.Time = 1;
            tmp1.Probability = 0.25m;
            tmp1.CummProbability = 0.25m;
            tmp1.MinRange = 1;
            tmp1.MaxRange = 25;

            TimeDistribution tmp2 = new TimeDistribution();
            tmp2.Time = 2;
            tmp2.Probability = 0.40m;
            tmp2.CummProbability = 0.65m;
            tmp2.MinRange = 26;
            tmp2.MaxRange = 65;

            TimeDistribution tmp3 = new TimeDistribution();
            tmp3.Time = 3;
            tmp3.Probability = 0.20m;
            tmp3.CummProbability = 0.85m;
            tmp3.MinRange = 66;
            tmp3.MaxRange = 85;

            TimeDistribution tmp4 = new TimeDistribution();
            tmp4.Time = 4;
            tmp4.Probability = 0.15m;
            tmp4.CummProbability = 1m;
            tmp4.MinRange = 86;
            tmp4.MaxRange = 100;

            this.InterarrivalDistribution.Add(tmp1);
            this.InterarrivalDistribution.Add(tmp2);
            this.InterarrivalDistribution.Add(tmp3);
            this.InterarrivalDistribution.Add(tmp4);
        }
        /*
        public SimulationSystem getSimulationSystem()
        {
            SimulationSystem s = new SimulationSystem();

            s.Servers = this.idleServers;
            s.InterarrivalDistribution = this.InterarrivalDistribution;
            s.NumberOfServers = 2;
            s.StoppingNumber = 100;
            s.StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            s.SelectionMethod = Enums.SelectionMethod.HighestPriority;

            s.SimulationTable = this.SimulationTable;
            return s;
        }
        */
    }
}
