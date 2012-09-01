namespace UserActivity
{
    internal static class Constants
    {
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;
        public const int HC_ACTION = 0; 
        
        public const uint MAPTO_VSC = 0x00;
		public const uint MAPVSC_TO_VK = 0x01;
		public const uint MAPTO_CHAR = 0x02;
		public const uint MAPVSC_TO_EX = 0x03;
		public const uint MAPTO_VSC_EX = 0x04;
    }
}