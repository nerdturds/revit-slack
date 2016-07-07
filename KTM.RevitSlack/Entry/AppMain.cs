using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using KTM.RevitSlack.Utils;

namespace KTM.RevitSlack.Entry
{
    /// <summary>
    ///   Revit Application
    /// </summary>
    internal class AppMain : IExternalApplication
    {
        private readonly string _path = Path.GetDirectoryName(
          Assembly.GetExecutingAssembly().Location);

        public static string _assemblyName = Helpers.assemblyName + ".dll";

        public static string _version = string.Empty;

        private UIControlledApplication _uiApp;

        /// <summary>
        ///   System Startup
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication a)
        {
            //Ribbon Element Creation
            try
            {
                //Set global uiapp
                _uiApp = a;

                //Ribbon panels
                string m_tabName = Helpers.projectClient;

                try
                {
                    //New tab
                    a.CreateRibbonTab(m_tabName);
                }
                catch (Exception m_e)
                {
                }

                //Create Path to Icons Folder         
                string m_iconPath = string.Join(".",
                  Helpers.projectName,
                  Helpers.projectIconPath) + ".";

                //Tab info
                RibbonPanel m_mPanel = _uiApp.CreateRibbonPanel(
                  m_tabName,
                  Helpers.projectName + " v:" + Helpers.assemblyVersion);

                //Add Cmd_01_Button : Should be attached to First Executed Command.
                AddButton(m_mPanel,
                  "ButtonName",
                  "ButtonText",
                  string.Concat(m_iconPath, "icon_16.png"),
                  string.Concat(m_iconPath, "icon_32.png"),
                  Path.Combine(_path, Helpers.assemblyName + ".dll"),
                  Helpers.projectName + ".Entry.CmdMain",
                  "DESCRIPTION",
                  "",
                  false);
            }
            catch (Exception m_e)
            {
                //throw new Exception(m_e.Message);
            }

            return Result.Succeeded;
        }


        /// <summary>
        ///   System Shutdown
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        #region private members

        /// <summary>
        ///   Add a pushbutton to a panel
        /// </summary>
        /// <param name="rPanel"></param>
        /// <param name="buttonName"></param>
        /// <param name="buttonText"></param>
        /// <param name="imagePath16"></param>
        /// <param name="imagePath32"></param>
        /// <param name="dllPath"></param>
        /// <param name="dllClass"></param>
        /// <param name="toolTip"></param>
        /// <param name="pbAvail"></param>
        /// <param name="separatorBeforeButton"></param>
        private void AddButton(RibbonPanel rPanel,
          string buttonName,
          string buttonText,
          string imagePath16,
          string imagePath32,
          string dllPath,
          string dllClass,
          string toolTip,
          string pbAvail,
          bool separatorBeforeButton)
        {
            //File path must exist
            if (!File.Exists(dllPath)) return;

            //Separator??
            if (separatorBeforeButton) rPanel.AddSeparator();

            try
            {
                //Create the pbData
                PushButtonData m_mPushButtonData = new PushButtonData(
                  buttonName,
                  buttonText,
                  dllPath,
                  dllClass);

                if (!string.IsNullOrEmpty(imagePath16))
                {
                    try
                    {
                        m_mPushButtonData.Image = LoadPngImageSource(imagePath16);
                    }
                    catch (Exception m_e)
                    {
                        throw new Exception(m_e.Message);
                    }
                }

                if (!string.IsNullOrEmpty(imagePath32))
                {
                    try
                    {
                        m_mPushButtonData.LargeImage = LoadPngImageSource(imagePath32);
                    }
                    catch (Exception m_e)
                    {
                        throw new Exception(m_e.Message);
                    }
                }

                m_mPushButtonData.ToolTip = toolTip;

                //Availability?
                if (!string.IsNullOrEmpty(pbAvail))
                {
                    m_mPushButtonData.AvailabilityClassName = pbAvail;
                }

                //Add button to the ribbon
                rPanel.AddItem(m_mPushButtonData);
            }
            catch (Exception m_e)
            {
                throw new Exception(m_e.Message);
            }
        }

        /// <summary>
        ///   Load the PNG image from file
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        private ImageSource LoadPngImageSource(string sourceName)
        {
            try
            {
                //Assembly and stream
                Assembly m_assembly = Assembly.GetExecutingAssembly();
                Stream m_icon = m_assembly.GetManifestResourceStream(sourceName);

                //Decode
                if (m_icon != null)
                {
                    PngBitmapDecoder m_decoder = new PngBitmapDecoder(
                      m_icon,
                      BitmapCreateOptions.PreservePixelFormat,
                      BitmapCacheOption.Default);

                    //Source
                    ImageSource m_source = m_decoder.Frames[0];
                    return (m_source);
                }
            }
            catch (Exception m_e)
            {
                throw new Exception(m_e.Message);
            }
            return null;
        }

        #endregion
    }
}