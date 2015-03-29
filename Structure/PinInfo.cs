using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    public class PinInfo
    {
        private List<int> connectionIds;

        public List<int> ConnectionIds
        {
            get
            { 
                return connectionIds;
            }
        }

        public ConnectionType Type { get; private set; }

        public string Name { get; private set; }

        public PinInfo(E3Project project, int pinId, ConnectionManager connectionManager)
        {
            DevicePin pin = project.DevicePin;
            pin.Id = pinId;
            Name = pin.Name;
            ConnectionType type;
            connectionManager.GetConnectionIdsAndType(pinId, out connectionIds, out type);
            Type = type;
        }
    }
}
