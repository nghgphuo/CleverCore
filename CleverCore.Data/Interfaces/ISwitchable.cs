using System;
using System.Collections.Generic;
using System.Text;
using CleverCore.Data.Enums;

namespace CleverCore.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
