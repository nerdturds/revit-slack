using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RestSharp;
using KTM.RevitSlack.API;
using KTM.RevitSlack.UI;
using KTM.RevitSlack.Utils;

namespace KTM.RevitSlack.Entry
{
    /// <summary>
    /// Interaction logic for IExternalCommand
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class CmdMain : IExternalCommand
    {
        public Document _doc;
        public static UIDocument _uidoc;
        //public Utils _utils;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Check to ensure Entry is acceptable. (Do Not Remove)
            if (!clsEntryChecks.RevitVersionCheck(commandData))
                return Result.Cancelled;

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            // widen scope
            _doc = doc;
            _uidoc = uidoc;

            //opt. Setup progress ui thread
            //Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));

            try
            {

                //TODO: CODE GOES HERE

                // Return Success if the Commands were properly executed. (Do Not Remove)
                return Result.Succeeded;
            }
            catch (Exception m_ex)
            {
                // Failure (Do Not Remove)
                message = m_ex.Message;
                Logger.WriteLine(message);
                //opt. newWindowThread.Abort();
                return Result.Failed;
            }
        }

        #region PRIVATE MEMBERS

        #endregion
    }
}