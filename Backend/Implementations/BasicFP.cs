
namespace Backend.Implementations
{
    internal class BasicFP : IFP
    {
        private readonly int[,] UFPMultipliers = new int[5, 3]
        {
            { 5, 7, 10 },
            { 7, 10, 15 },
            { 3, 4, 6 },
            { 4, 5, 7 },
            { 3, 4, 6 }
        };
        private readonly int[,] UFPValues = new int[15, 3];
        private int AVCValue;
        private int DI;
        private double FP;
        private double TCF;
        private int UFP;
        public void SetUFPValue(int criteria, int complexity, int value)
        {
            UFPValues[criteria, complexity] = value;
        }
        public int GetUFPMultiplier(int criteria, int complexity)
        {
            return UFPMultipliers[criteria, complexity];
        }
        public void SetDI(int DI)
        {
            this.DI = DI;
        }
        public void SetAverageLineOfCodes(int AVC)
        {
            AVCValue = AVC;
        }
        public int CalculateUFP()
        {
            UFP = 0;
            for (int i = 0; i < UFPValues.GetLength(0); i++)
            {
                for (int j = 0; j < UFPValues.GetLength(1); j++)
                {
                    UFP += UFPValues[i, j] * UFPMultipliers[i / 3, j];
                }
            }
            return UFP;
        }
        public double CalculateTCF()
        {
            TCF = 0.65 + 0.01 * DI;
            return TCF;
        }
        public int CalculateLOC()
        {
            return (int)(FP * AVCValue);
        }
        public double CalculateFP()
        {
            FP = UFP * TCF;
            return FP;
        }
    }
}
