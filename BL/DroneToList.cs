﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList : abstractDrone
    {
        public int parcelNumber { get; set; }
        public override string ToString()
        {
            return string.Format(base.ToString() + "parcelNumber {0}: ", parcelNumber);
        }
    }
}

