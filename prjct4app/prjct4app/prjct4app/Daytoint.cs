using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjct4app
{
    class DaytoInt
    {
        public static int daytoint(DayOfWeek dag)
        {
            if (dag == DayOfWeek.Sunday) { return 0; }
            else if (dag == DayOfWeek.Monday) { return 1; }
            else if (dag == DayOfWeek.Tuesday) { return 2; }
            else if (dag == DayOfWeek.Wednesday) { return 3; }
            else if (dag == DayOfWeek.Thursday) { return 4; }
            else if (dag == DayOfWeek.Friday) { return 5; }
            else if (dag == DayOfWeek.Saturday) { return 6; }
            else { return 0; }
        }
    }
}
