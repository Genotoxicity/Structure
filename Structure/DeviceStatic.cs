using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProELib;

namespace Structure
{
    static class DeviceStatic
    {
        public static DeviceType GetDeviceType(E3Project project, int deviceId)
        {
            Device device = project.NormalDevice;
            device.Id = deviceId;
            if (device.IsConnector)
                return DeviceType.Connector;
            string purposeAttribute = "PurposeEquipment";
            Component component = project.Component;
            component.Id = deviceId;
            string purpose = component.GetAttributeValue(purposeAttribute);
            if (purpose.Equals("Медиаконвертеры") || purpose.Equals("SFP"))
                return DeviceType.Connector;
            return DeviceType.NetWork;
        }
    }
}
