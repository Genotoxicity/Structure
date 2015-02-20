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
        private const int PlanCode = 102;

        public void Start(int processId)
        {
            E3Project project = new E3Project(processId);
            ProjectEntities projectEntities = new ProjectEntities(project);
            List<int> communicationDeviceIds = GetCommunicationDeviceIds(project, projectEntities);
            project.Release();
        }

        private static List<int> GetCommunicationDeviceIds(E3Project project, ProjectEntities projectEntities)
        {
            Sheet sheet = projectEntities.Sheet;
            NormalDevice device = projectEntities.NormalDevice;
            List<int> planSheetIds = project.GetSheetIdsByType(PlanCode);
            List<int> symbolIds = new List<int>();
            foreach (int sheetId in planSheetIds)
            {
                sheet.Id = sheetId;
                symbolIds.AddRange(sheet.SymbolIds);
            }
            List<int> communicationDeviceIds = new List<int>();
            foreach (int symbolId in symbolIds)
            {
                device.Id = symbolId;
                if (!String.IsNullOrEmpty(device.Location))
                    communicationDeviceIds.Add(device.Id);
            }
            return communicationDeviceIds;
        }
    }


}
