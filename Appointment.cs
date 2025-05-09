using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
    public class Availability
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
   }

