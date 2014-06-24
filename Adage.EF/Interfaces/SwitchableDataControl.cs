using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adage.EF.Interfaces
{
    public interface SwitchableDataControl
    {
        string CurrentKey { get; }
        string QueryStringKey { get; }
        string BindingControl { get; }
        bool IsDirty { get; }

        void Update(object sender, EventArgs e);
        string ReturnURL(string ReturnControl);
    }
}
