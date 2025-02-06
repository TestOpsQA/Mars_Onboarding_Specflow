using System;
using NUnit.Framework;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Helpers
{
    public static class SoftAssert
    {
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                // Log the error without throwing an exception
                TestContext.WriteLine($"[SoftAssert Failure] {message}");

                // Optionally, write to console for debugging purposes
                Console.WriteLine($"[SoftAssert Failure] {message}");
            }
        }
    }
}