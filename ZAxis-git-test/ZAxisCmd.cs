using System;


namespace ZAxis {
    public static class Cmd {
        public static class IO {
            public static void Out(string str) => Console.WriteLine(str);
        }
        public static class Math {
            public static int Clamp(int value, int min, int max) =>
                    (value < min) ? min : (value > max) ? max : value;  
        
            public static float ClampFloat(float value, float min, float max) =>
                (value < min) ? min : (value > max) ? max : value;  
        }
    }
}