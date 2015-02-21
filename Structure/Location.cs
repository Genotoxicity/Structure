using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    public class Location
    {
        private List<int> deviceIds;

        public string Name { get; private set; }
                
        public Location(string name)
        {
            Name = name;
            deviceIds = new List<int>(); 
        }

        public void AddDeviceId(int deviceId)
        {
            deviceIds.Add(deviceId);
        }
    }
}
