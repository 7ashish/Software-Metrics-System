using Backend.Implementations;
using System;

namespace Backend
{
    public enum FPImplementation { BasicFP }
    public static class FPFactory
    {
        public static IFP CreateFP(FPImplementation imp)
        {
            switch(imp)
            {
                case FPImplementation.BasicFP:
                    return new BasicFP();
                default:
                    throw new ArgumentException("Unknown implementation", nameof(imp));
            }
        }
    }
}
