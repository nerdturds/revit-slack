using System;
using Autodesk.Revit.UI;

namespace KTM.RevitSlack.Utils
{
    internal class clsEntryChecks
    {
        public static bool RevitVersionCheck(ExternalCommandData commandData)
        {
            if (commandData == null)
            {
                Logger.WriteLine("Problem with commandData");
                throw new ArgumentNullException("commandData");
            }

            //Version Checking
            string m_revitVersion = commandData.Application.Application.VersionName;

            if (m_revitVersion.Contains(clsAppVersionHelpers.RevitVersion)) return true;

            using (TaskDialog m_td = new TaskDialog("Cannot Continue"))
            {
                m_td.TitleAutoPrefix = false;
                m_td.MainInstruction = clsAppVersionHelpers.RevitVersion;
                m_td.MainContent += "Please contact INVIEWlabs for more information.";
                m_td.Show();
            }
            return false;
        }
    }
}