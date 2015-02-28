using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    class StructureScheme
    {
        public StructureScheme(E3Project project, Location location, List<int> planSheetIds)
        {
            List<DevicePinIds> devicesPinIds = new List<DevicePinIds>(location.DeviceIds.Count);
            foreach (int deviceId in location.DeviceIds)
            {
                devicesPinIds.Add(GetDevicePinIds(project, planSheetIds, deviceId));
            }
        }

        private static DevicePinIds GetDevicePinIds(E3Project project, List<int> planSheetIds, int deviceId)
        {
            Symbol symbol = project.Symbol;
            NormalDevice device = project.NormalDevice;
            NetSegment netSegment = project.NetSegment;
            Net net = project.Net;
            DevicePin pin = project.DevicePin;
            List<int> inPinIds = new List<int>();
            List<int> outPinIds = new List<int>();
            device.Id = deviceId;
            List<int> totalDeviceIds = new List<int>() { device.Id };
            totalDeviceIds.AddRange(device.ConnectedDeviceIds);
            foreach (int id in totalDeviceIds)
            {
                device.Id = id;
                foreach (int pinId in device.PinIds)
                {
                    pin.Id = pinId;
                    if (planSheetIds.Contains(pin.SheetId))
                        continue;
                    net.Id = pinId;
                    foreach (int netSegmentId in net.NetSegmentIds)
                    {
                        netSegment.Id = netSegmentId;
                        foreach (int nodeId in netSegment.NodeIds)
                        {
                            symbol.Id = nodeId;
                            if (symbol.Id == 0)
                                continue;
                            SheetReferenceInfo info = symbol.SheetReferenceInfo;
                            if (info == null)
                                continue;
                            if (info.InOut == ReferenceDirection.Destination)
                            {
                                inPinIds.Add(pinId);
                                continue;
                            }
                            if (info.InOut == ReferenceDirection.Source)
                                outPinIds.Add(pinId);
                        }
                    }
                }
            }
            return new DevicePinIds(deviceId, inPinIds, outPinIds);
        }

        private struct DevicePinIds
        {
            int deviceId;
            List<int> inPinIds;
            List<int> outPinIds;

            public DevicePinIds(int deviceId, List<int> inPinIds, List<int> outPinIds)
            {
                this.deviceId = deviceId;
                this.inPinIds = inPinIds;
                this.outPinIds = outPinIds;
            }
        }
    }
}
