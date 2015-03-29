using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ProELib;

namespace Structure
{
    class StructureScheme
    {
        private Dictionary<int, Equipment> equipmentById;
        
        public StructureScheme(E3Project project, Location location)
        {
            ConnectionManager connectionManager = new ConnectionManager(project);
            equipmentById = new Dictionary<int, Equipment>();
            foreach (int deviceId in location.DeviceIds)
                equipmentById.Add(deviceId, new Equipment(project, deviceId, connectionManager));
            /*IEnumerable<Equipment> destinationEquipments = equipmentById.Values.Where(e => e.PinInfos.Any(p => p.Type == ConnectionType.In));
            while (destinationEquipments.Count()>0)
            {

            }*/
            foreach (Equipment equipment in equipmentById.Values)
            {
                EquipmentSymbol equipmentSymbol = new EquipmentSymbol(project, equipment);
                Sheet sheet = project.Sheet;
                int sheetId = sheet.Create(equipment.Name, "DINA3");
                equipmentSymbol.Place(project, sheet, sheetId, new Point(50, 50));
            }
        }

        /*
        private static DevicePinIds GetDevicePinIds(E3Project project, int deviceId, ConnectionManager connectionManager)
        {
            Symbol symbol = project.Symbol;
            NormalDevice device = project.NormalDevice;
            NetSegment netSegment = project.NetSegment;
            DevicePin pin = project.DevicePin;
            Core core = project.CableCore;
            List<int> inPinIds = new List<int>();
            List<int> outPinIds = new List<int>();
            device.Id = deviceId;
            List<PinInfo> pinInfos = new List<PinInfo>();
            device.PinIds.ForEach(pId => { if (!planSheetIds.Contains(pId)) pinInfos.Add(new PinInfo(project, pId, connectionManager)); });
            device.Id = deviceId;
            List<int> totalDeviceIds = new List<int>() { device.Id };
            totalDeviceIds.AddRange(device.ConnectedDeviceIds);
            foreach (int id in totalDeviceIds)
            {
                device.Id = id;
                foreach (int pinId in device.PinIds)
                {
                    pin.Id = pinId;
                    int pinSheetId = pin.SheetId;
                    if (planSheetIds.Contains(pinSheetId))
                        continue;
                    foreach (int coreId in pin.CoreIds)
                    {
                        core.Id = coreId;
                        foreach (int netSegmentId in core.NetSegmentIds)
                        {
                            netSegment.Id = netSegmentId;
                            foreach (int nodeId in netSegment.NodeIds)
                            {
                                symbol.Id = nodeId;
                                if (symbol.Id == 0)
                                    continue;
                                if (symbol.SheetId != pinSheetId)
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
            }
        }*/

    }
}
