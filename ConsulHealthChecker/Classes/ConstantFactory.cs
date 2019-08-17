using System;

namespace ConsulHealthChecker.Classes
{
    public static class ConstantFactory
    {
        public static string DefaultHost { get; set; } = "http://localhost:8500";
        public static ConsoleKey ReturnKey { get; set; } = ConsoleKey.Enter;
    }
}