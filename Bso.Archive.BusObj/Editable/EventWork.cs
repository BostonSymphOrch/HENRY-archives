using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bso.Archive.BusObj
{
    partial class EventWork
    {
        public static EventWork AddEventWork(Event evt, Work work)
        {
            var eventWork = EventWork.NewEventWork();
            eventWork.Event = evt;
            eventWork.Work = work;
            eventWork.WorkPremiere = work.WorkPremiere;
          
            return eventWork;
        }
    }
}
