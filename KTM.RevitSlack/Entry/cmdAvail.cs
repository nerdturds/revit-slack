using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KTM.RevitSlack.Entry
{
    public class cmdAvail : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            //check param
            Document doc = applicationData.ActiveUIDocument.Document;
            var p = Utils.clsParameterHelpers.ParameterSelector(doc.ProjectInformation, "slackChannel");
            if (p != null && p.HasValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
