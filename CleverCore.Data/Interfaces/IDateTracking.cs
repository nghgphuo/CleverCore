using System;
using System.Collections.Generic;
using System.Text;

namespace CleverCore.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }

        DateTime DateModified { set; get; }
    }
}
