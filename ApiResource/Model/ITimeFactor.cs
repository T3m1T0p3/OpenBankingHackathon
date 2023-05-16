namespace ApiResource.Model
{
    public interface ITimeFactor
    {
        public double GetTimeFactorInDays();

        public double GetTimeFactorInSeconds();

        public double GetTimeFactorInHours();
    }
}