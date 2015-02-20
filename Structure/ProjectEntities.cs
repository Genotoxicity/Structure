using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    class ProjectEntities
    {
        public Sheet Sheet { get; private set; }

        public NormalDevice NormalDevice { get; private set; }

        public ProjectEntities(E3Project project)
        {
            Sheet = project.CreateSheet();
            NormalDevice = project.CreateNormalDevice();
        }
    }
}
