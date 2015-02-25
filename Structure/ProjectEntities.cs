using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    public class ProjectEntities
    {
        public Sheet Sheet { get; private set; }

        public NormalDevice NormalDevice { get; private set; }

        public NetSegment NetSegment { get; private set; }

        public Net Net { get; private set; }

        public Connection Connection { get; private set; }

        public Symbol Symbol { get; private set; }

        public DevicePin Pin { get; private set; }

        public ProjectEntities(E3Project project)
        {
            Sheet = project.CreateSheet();
            NormalDevice = project.CreateNormalDevice();
            NetSegment = project.CreateNetSegment();
            Net = project.CreateNet();
            Connection = project.CreateConnection();
            Symbol = project.CreateSymbol();
            Pin = project.CreateDevicePin();
        }
    }
}
