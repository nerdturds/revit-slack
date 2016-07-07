using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KTM.RevitSlack.Utils
{
    public class Helpers
    {
        #region PUBLIC PROPERTIES

        // Public Static Properties (Globals)
        private static readonly Assembly assy = Assembly.GetExecutingAssembly();
        private static readonly Type t = assy.GetTypes().First();
        public static string assemblyName = assy.GetName().Name;
        public static string assemblyVersion = assy.GetName().Version.ToString();
        public static string projectClient = "IVL";
        public static string projectName = "FILL ME OUT"; //TODO: ProjectName
        public static string projectNamespace = t.Namespace;
        public static string projectIconPath = "icons";
        public static string projectRevitVersion = clsAppVersionHelpers.RevitVersion;
        public static string projectErrorUserString = "This Add-In was built for Revit ";

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Helpers()
        {
        }

        #region Flip SVG Y coordinates
        public static bool SvgFlip = true;

        /// <summary>
        /// Flip Y coordinate for SVG export.
        /// </summary>
        public static int SvgFlipY(int y)
        {
            return SvgFlip ? -y : y;
        }
        public static double SvgFlipYDouble(double y)
        {
            return SvgFlip ? -y : y;
        }
        #endregion // Flip SVG Y coordinates

        #region STATIC METHODS

        /// <summary>
        /// Fetches embedded string files using the filename
        /// Requires files to live in *.Resources
        /// </summary>
        /// <param name="filename">Name of file with extension</param>
        /// <returns>string contents of the embedded file.</returns>
        internal static string fetchEmbeddedFileStringContents(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var ns = assembly.GetTypes().First().Namespace;
            var fullPath = ns + ".Resources." + filename;
            using (Stream s = assembly.GetManifestResourceStream(fullPath))
            using (StreamReader sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Creates a custom sized UUID from
        /// an existing UUID or creates one if null
        /// </summary>
        /// <param name="uuidSize">size of resultant uuid</param>
        /// <param name="uuid">uuid if any to trim</param>
        /// <returns></returns>
        internal static string CreateUuid(int uuidSize, string uuid = null)
        {
            if (null == uuid)
                uuid = new Guid().ToString();

            return uuid.Trim().Substring(uuid.Length - uuidSize);
        }

        internal static string FormatKeyName(string paramName)
        {
            var sb = new StringBuilder();

            // check if paramName already contains underscores, then if so, remove
            // the ending including the underscore
            // before processing to get only the starting string
            if (paramName.Contains("_"))
            {
                paramName = paramName.Split('_').First();
            }

            //need to check for USF, RSF, GSF, NYSF in name
            if (paramName.StartsWith("USF"))
                paramName = paramName.Replace("USF", "Usf");
            if (paramName.StartsWith("RSF"))
                paramName = paramName.Replace("RSF", "Rsf");
            if (paramName.StartsWith("GSF"))
                paramName = paramName.Replace("GSF", "Gsf");
            if (paramName.StartsWith("NYSF"))
                paramName = paramName.Replace("NYSF", "Nysf");

            // split the parameter name at the uppercase,
            // insert underscore as separator
            // and make everything lowercase
            foreach (char c in paramName)
            {
                if (Char.IsUpper(c) && sb.Length > 1)
                {
                    sb.Append("_");
                }
                sb.Append(Char.ToLower(c));
            }
            return sb.ToString();
        }

        internal static string FormatParamNameFromKey(string keyName)
        {
            var sb = new StringBuilder();
            var newstring = keyName.Replace('_', ' ');
            TextInfo ti = new CultureInfo("en-us", false).TextInfo;
            newstring = ti.ToTitleCase(newstring);
            foreach (char c in newstring.Where(c => !c.Equals(' ')))
            {
                sb.Append(c);
            }
            return sb.ToString();

        }
        #endregion

    }
}
