namespace EppLib
{
    public static class Debug
    {
        private static IDebugger _debugger = new SimpleLogger();

        public static void Setup(IDebugger debugger)
        {
            _debugger = debugger;
        }

        public static void Log(byte[] bytes)
        {
            _debugger.Log(bytes);
        }

        public static void Log(string str)
        {
            _debugger.Log(str);
        }
    }
}