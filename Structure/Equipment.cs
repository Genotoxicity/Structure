using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    public class Equipment
    {
        private List<PinInfo> pinInfos;

        public List<PinInfo> PinInfos
        {
            get
            {
                return pinInfos;
            }
        }
        
        public DeviceType Type { get; private set; }

        public string Name { get; private set; }

        public int Id { get; private set; }

        public Equipment(E3Project project, int id, ConnectionManager connectionManager)
        {
            NormalDevice device = project.NormalDevice;
            Type = DeviceStatic.GetDeviceType(project, id); // device.Id = id внутри функции
            Id = id;
            Name = device.Name;
            pinInfos = new List<PinInfo>();
            List<int> placedSymbolIds = device.GetSymbolIds(SymbolReturnParameter.Placed);
            Symbol symbol = project.Symbol;
            foreach (int symbolId in placedSymbolIds)
            {
                symbol.Id = symbolId;
                if (symbol.IsSchematicTypeOf((int)SchemeType.Electric))
                    foreach(int pinId in symbol.PinIds)
                    {
                        PinInfo pinInfo = new PinInfo(project, pinId, connectionManager);
                        if (pinInfo.ConnectionIds.Count > 0)
                            pinInfos.Add(pinInfo);
                    }
            }
        }

    }
}
