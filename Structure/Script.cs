﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProELib;

namespace Structure
{
    class Script
    {
        private const int PlanCode = 102;

        public void Start(int processId)
        {
            E3Project project = new E3Project(processId);
            ProjectEntities projectEntities = new ProjectEntities(project);
            //Dictionary<string, Location> locationByName = GetLocationByName(project, projectEntities);
            //AddressWindow addressWindow = new AddressWindow(projectEntities, locationByName);
            //addressWindow.Show();
            /*Connection connection = projectEntities.Connection;
            List<int> netSegmentPinIds = GetNetSegmentPinIds(project, projectEntities);
            Symbol symbol = projectEntities.Symbol;
            NetSegment netSegment = projectEntities.NetSegment;
            foreach (int symbolId in project.SymbolIds)
            {
                symbol.Id = symbolId;
                SheetReferenceInfo info = symbol.SheetReferenceInfo;
                if (info != null)
                {
                    if (netSegmentPinIds.Contains(symbol.PinIds.First()))
                    {
                        netSegment.Id = symbol.PinIds.First();
                        netSegment.Highlight();
                    }
                }
            }*/
            NormalDevice device = projectEntities.NormalDevice;
            NetSegment netSegment = projectEntities.NetSegment;
            DevicePin pin = projectEntities.Pin;
            device.Id = 74880;
            foreach (int id in device.ConnectedDeviceIds)
            {
                device.Id = id;
                foreach (int pinId in device.PinIds)
                {
                    pin.Id = pinId; 
                }
            }
            project.Release();
        }

        private static List<int> GetNetSegmentPinIds(E3Project project, ProjectEntities projectEntities)
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
        }

        private Dictionary<string, Location> GetLocationByName(E3Project project, ProjectEntities projectEntities)
        {
            Sheet sheet = projectEntities.Sheet;
            NormalDevice device = projectEntities.NormalDevice;
            List<int> planSheetIds = project.GetSheetIdsByType(PlanCode);
            HashSet<int> addedDeviceIds = new HashSet<int>();
            Dictionary<string, Location> locationByName = new Dictionary<string, Location>();
            foreach (int sheetId in planSheetIds)
            {
                sheet.Id = sheetId;
                foreach (int symbolId in sheet.SymbolIds)
                {
                    device.Id = symbolId;
                    string location = String.Intern(device.Location);
                    if (!String.IsNullOrEmpty(location))
                    {
                        int deviceId = device.Id;
                        if (!locationByName.ContainsKey(location))
                            locationByName.Add(location, new Location(location));
                        locationByName[location].AddDeviceId(deviceId);
                        addedDeviceIds.Add(deviceId);
                    }
                }
            }
            List<int> deviceIds = project.DeviceIds;
            deviceIds.AddRange(project.TerminalIds);
            foreach (int deviceId in deviceIds)
            {
                if (!addedDeviceIds.Contains(deviceId))
                {
                    device.Id = deviceId;
                    string location = String.Intern(device.Location);
                    if (locationByName.ContainsKey(location))
                        locationByName[location].AddDeviceId(deviceId);
                }
            }
            return locationByName;
        }
    }


}
