
namespace Backend
{
    public interface IFP
    {
        void SetUFPValue(int criteria, int complexity, int value);
        int GetUFPMultiplier(int criteria, int complexity);
        void SetDI(int DI);
        void SetAverageLineOfCodes(int AVC);
        int CalculateUFP();
        double CalculateTCF();
        int CalculateLOC();
        double CalculateFP();
    }
}
