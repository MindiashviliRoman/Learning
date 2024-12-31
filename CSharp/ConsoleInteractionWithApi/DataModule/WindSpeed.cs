
namespace ConsoleInteractionWithApi.DataModule
{
    internal class WindSpeed
    {
        public string Unit { get; set; }
        public float Value { get; set; }

        //TODO: For CsvHelper need public set property and constuctor without parameters
        public WindSpeed()
        { }

        public WindSpeed(string unit, float value)
        {
            Unit = unit;
            Value = value;
        }
    }
}
