using System;
using Autodesk.Revit.DB;
using ArgumentNullException = Autodesk.Revit.Exceptions.ArgumentNullException;

namespace KTM.RevitSlack.Utils
{
    public class clsParameterHelpers
    {
        #region GetParameterHelper

        /// <summary>
        ///   Universal Revit Parameter Selector
        ///   Currently Configured for Revit 2014,2015,2016
        /// </summary>
        /// <param name="element">Host Element</param>
        /// <param name="builtInParameter">Built-In Parameter ENUM</param>
        /// <returns>Revit Parameter Object or Null</returns>
        public static Parameter ParameterSelector(
          Element element,
          BuiltInParameter builtInParameter)
        {
            Parameter m_param;
            try
            {
                m_param = element.get_Parameter(builtInParameter);
            }
            catch (ArgumentNullException m_ex)
            {
                Logger.WriteLine(m_ex.Message);
                throw;
            }
            return m_param;
        }

        /// <summary>
        ///   Universal Revit Parameter Selector
        ///   Currently Configured for Revit 2014,2015,2016
        /// </summary>
        /// <param name="element">Host Element</param>
        /// <param name="definition">Parameter Definition Object </param>
        /// <returns>Revit Parameter Object or Null</returns>
        public static Parameter ParameterSelector(
          Element element,
          Definition definition)
        {
            Parameter m_param;
            try
            {
                m_param = element.get_Parameter(definition);
            }
            catch (ArgumentNullException m_ex)
            {
                Logger.WriteLine(m_ex.Message);
                throw;
            }
            return m_param;
        }

        /// <summary>
        ///   Universal Revit Parameter Selector
        ///   Currently Configured for Revit 2014,2015,2016
        /// </summary>
        /// <param name="element">Host Element</param>
        /// <param name="guid">GUID of Parameter Object</param>
        /// <returns>Revit Parameter Object or Null</returns>
        public static Parameter ParameterSelector(
          Element element,
          Guid guid)
        {
            Parameter m_param;
            try
            {
                m_param = element.get_Parameter(guid);
            }
            catch (ArgumentNullException m_ex)
            {
                Logger.WriteLine(m_ex.Message);
                throw;
            }
            return m_param;
        }

        /// <summary>
        ///   Universal Revit Parameter Selector
        ///   Currently Configured for Revit 2014,2015,2016
        /// </summary>
        /// <param name="element">Host Element</param>
        /// <param name="name">String of the Parameter Name</param>
        /// <returns>Revit Parameter Object or Null</returns>
        public static Parameter ParameterSelector(
          Element element,
          string name)
        {
            Parameter m_param;
            try
            {
#if Version2014
            m_param = element.get_Parameter(name);
#else
                m_param = element.LookupParameter(name);
#endif
            }
            catch (ArgumentNullException m_ex)
            {
                Logger.WriteLine(m_ex.Message);
                throw;
            }

            return m_param;
        }

        /// <summary>
        /// Write the parameter value
        /// based on storage type
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static object WriteParameter(Parameter p)
        {
            if (p == null || !p.HasValue)
                return null;

            switch (p.StorageType)
            {
                case StorageType.Double:
                    return p.AsDouble();
                    break;
                case StorageType.Integer:
                    return p.AsInteger();
                    break;
                case StorageType.String:
                    return p.AsString();
                    break;
                default:
                    return p.AsString();
                    break;
            }
        }
        #endregion
    }
}