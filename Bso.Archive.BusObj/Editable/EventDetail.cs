using System;

namespace Bso.Archive.BusObj
{
    public partial class EventDetail
    {
        [System.ComponentModel.DataObjectField(false)]
        public string EventFullDate
        {
            get
            {
                if (String.IsNullOrEmpty(this.EventStart))
                    return EventStartDate;

                return String.Concat(EventStartDate, " - ", this.EventStart);
            }
        }

        [System.ComponentModel.DataObjectField(false)]
        public string EventStartDate
        {
            get
            {
                return String.Concat(EventDate.ToShortDateString(), " ", EventStart);
            }
        }
    }
}
