namespace Common.CustomNaming
{
    public static class CustomLogInformation
    {
        public static string LogInfo(string MethodName)
        {
            return "Executing " + MethodName + " service method";
        }
        public static string ErrorInfo(string MethodName, string Message)
        {
            return "Error in  " + MethodName + " - " + Message;
        }
    }
}
