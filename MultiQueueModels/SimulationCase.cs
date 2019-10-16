using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class SimulationCase
    {
        //slide 38
        public SimulationCase()
        {
            this.AssignedServer = new Server();
        }

        public int CustomerNumber { get; set; }  // A
        public int RandomInterArrival { get; set; } //B
        public int InterArrival { get; set; }//C  prob. in range (interarrival Distribution of calls)
        public int ArrivalTime { get; set; } //D Service Start Time
        public int RandomService { get; set; }//E
        public int ServiceTime { get; set; } // G / J
        public Server AssignedServer { get; set; }  //choose between G / j
        public int AssignedServerIndex { get; set; }  //choose between G / j
    
        public int StartTime { get; set; } // F / I
        public int EndTime { get; set; }//H / K
        public int TimeInQueue { get; set; } // L
    }
}
