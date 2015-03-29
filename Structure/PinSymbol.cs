using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ProELib;

namespace Structure
{
    public class PinSymbol
    {
        private PinInfo pinInfo;
        private double nameLength;

        public double NameLength
        {
            get
            {
                return nameLength;
            }
        }

        public string Name
        {
            get
            {
                return pinInfo.Name;
            }
        }

        public PinSymbol(E3Project project, PinInfo pinInfo, E3Font font)
        {
            this.pinInfo = pinInfo;
            nameLength = project.E3Text.GetTextLength(pinInfo.Name, font);
        }
    }
}
