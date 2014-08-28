using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HelpDesk.Utileria
{
    public class hdk_Utileria
    {
        public void limitarFechas(DatePicker dp, DateTime li, DateTime lf)
        {
            dp.BlackoutDates.Add(new CalendarDateRange(
                li, lf));
        }
    }
}
