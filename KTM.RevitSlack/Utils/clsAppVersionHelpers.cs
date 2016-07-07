namespace KTM.RevitSlack.Utils
{
    public class clsAppVersionHelpers
    {
        //Global Versioning

#if Version2014
    
        public const string RevitVersion = "2014";
    
#elif Version2015
    
        public const string RevitVersion = "2015";
    
#elif Version2016
    
        public const string RevitVersion = "2016";
    
#endif
    }
}