using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SET09120___NBMFS
{
    public class SIR
    {
        // Getters and setters
        public string header { get; set; }
        public string subject { get; set; }
        public string sortcode { get; set; }
        public string incident { get; set; }


        public SIR(string headerIn, string subjIn, string sortIn, string incidentIn)
        {
            header = headerIn;
            subject = subjIn;
            sortcode = sortIn;
            incident = incidentIn;
        }
    }

    public class IncidentReportList
    {
        public List<SIR> Incidents { get; set; }
    }
}
