using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProELib;

namespace Structure
{
    class Script
    {
        public void Start(int processId)
        {
            E3Project project = new E3Project(processId);
            Dictionary<string, Location> locationByName = GetLocationByName(project);
            AddressWindow addressWindow = new AddressWindow(project, locationByName);
            addressWindow.Show();
            project.Release();
        }
 
        /*private static List<int> GetNetSegmentPinIds(E3Project project, ProjectEntities projectEntities)
        {
            List<int> netSegmentPinIds = new List<int>();
            Net net = projectEntities.Net;
            NetSegment netSegment = projectEntities.NetSegment;
            foreach (int netId in project.NetIds)
            {
                net.Id = netId;
                foreach (int netSegmentId in net.NetSegmentIds)
                {
                    netSegment.Id = netSegmentId;
                    netSegmentPinIds.AddRange(netSegment.NodeIds);
                }
            }
            return netSegmentPinIds;
        }*/

        private Dictionary<string, Location> GetLocationByName(E3Project project)
        {
            Sheet sheet = project.Sheet;
            NormalDevice device = project.NormalDevice;
            Dictionary<string, Location> locationByName = new Dictionary<string, Location>();
            List<int> planSheetIds = project.GetSheetIdsByType((int)SchemeType.Plan);
            foreach (int sheetId in planSheetIds)
            {
                sheet.Id = sheetId;
                foreach (int symbolId in sheet.SymbolIds)
                {
                    device.Id = symbolId;
                    string location = String.Intern(device.Location);
                    if (!String.IsNullOrEmpty(location))
                        if (!locationByName.ContainsKey(location))
                            locationByName.Add(location, new Location(location));
                }
            }
            List<int> deviceIds = project.DeviceIds;
            deviceIds.AddRange(project.TerminalIds);
            foreach (int deviceId in deviceIds)
            {
                device.Id = deviceId;
                if (device.IsView)
                    continue;
                if (DeviceStatic.GetDeviceType(project, deviceId) == DeviceType.Connector)
                    continue;
                string location = String.Intern(device.Location);
                if (locationByName.ContainsKey(location))
                    locationByName[location].AddDeviceId(deviceId);
            }
            return locationByName;
        }
    }


}
