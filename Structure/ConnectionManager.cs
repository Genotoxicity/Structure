using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    public class ConnectionManager
    {
        private DevicePin pin;
        private NormalDevice device;
        private NetSegment netSegment;
        private Core core;
        private E3Project project;
        private Sheet sheet;
        private Symbol symbol;
        private HashSet<IdConnection> IdConnections;
        private Dictionary<int, IdConnection> IdConnectionById;
        private int connectionId;

        public ConnectionManager(E3Project project)
        {
            pin = project.DevicePin;
            device = project.NormalDevice;
            core = project.CableCore;
            sheet = project.Sheet;
            symbol = project.Symbol;
            netSegment = project.NetSegment;
            IdConnectionById = new Dictionary<int, IdConnection>();
            IdConnections = new HashSet<IdConnection>();
            connectionId = 0;
            this.project = project;
        }

        public void GetConnectionIdsAndType(int pinId, out List<int> connectionIds, out ConnectionType type)
        {
            List<int> netSegmentIds = new List<int>();
            List<int> pinIds = GetAllConnectedPinId(pinId, netSegmentIds);
            connectionIds = new List<int>();
            foreach(int pinId2 in pinIds)
            {
                IdConnection connection = new IdConnection(connectionId++, pinId, pinId2);
                if (!IdConnections.Contains(connection))
                {
                    IdConnections.Add(connection);
                    IdConnectionById.Add(connection.Id, connection);
                    connectionIds.Add(connection.Id);
                }
            }
           type = GetConnectionType(pinId, netSegmentIds);
        }

        private ConnectionType GetConnectionType(int pinId, List<int> netSegmentIds)
        {
            if (netSegmentIds.Count > 0)
            {
                netSegmentIds = netSegmentIds.Distinct().ToList();
                pin.Id = pinId;
                sheet.Id = pin.SheetId;
                List<int> sheetNetSegmentIds = sheet.NetSegmentIds;
                netSegmentIds = netSegmentIds.Intersect(sheetNetSegmentIds).ToList();
                foreach (int netSegmentId in netSegmentIds)
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
                        if (info.Direction == ReferenceDirection.Destination)
                            return ConnectionType.In;
                        else
                            return ConnectionType.Out;
                    }
                }
            }
            return ConnectionType.Normal;
        }

        private List<int> GetAllConnectedPinId(int pinId, List<int> netSegmentIds)
        {
            List<int> pinIds = new List<int>();
            pin.Id = pinId;
            int connectedPinId = pin.ConnectedPinId;
            if (connectedPinId!=0)
            {
                if (IsConnectorPin(connectedPinId))
                {
                    List<int> connectorPinIds = GetConnectorPinIds(connectedPinId, netSegmentIds);
                    foreach (int pId in connectorPinIds)
                        pinIds.AddRange(GetAllConnectedPinId(pId,netSegmentIds));
                }
                else
                    pinIds.Add(connectedPinId);
            }
            List<int> coreConnectedPinIds = GetCoreConnectedPinIds(pinId, netSegmentIds);
            foreach (int pId in coreConnectedPinIds)
                pinIds.AddRange(GetAllConnectedPinId(pId,netSegmentIds));
            pinIds.Remove(pinId);
            return pinIds;
        }

        private bool IsConnectorPin(int pinId)
        {
            return DeviceStatic.GetDeviceType(project, pinId) == DeviceType.Connector;
        }

        private List<int> GetConnectorPinIds(int connectorPinId, List<int> netSegmentIds)
        {
            Symbol symbol = project.Symbol;
            symbol.Id = connectorPinId;
            List<int> pinIds = new List<int>();
            foreach (int pinId in symbol.PinIds)
            {
                pin.Id = pinId;
                int id = pin.Id;
                pinIds.Add(id);
                pinIds.AddRange(GetCoreConnectedPinIds(id, netSegmentIds));
            }
            pinIds.Remove(connectorPinId);
            return pinIds;
        }
        
        private List<int> GetCoreConnectedPinIds(int pinId, List<int> netSegmentIds)
        {
            List<int> pinIds = new List<int>();
            foreach(int coreId in pin.CoreIds)
            {
                core.Id = coreId;
                netSegmentIds.AddRange(core.NetSegmentIds);
                pinIds.Add(core.StartPinId);
                pinIds.Add(core.EndPinId);
            }
            pinIds = pinIds.Distinct().ToList();
            pinIds.Remove(pinId);
            return pinIds;
        }
    }
}
