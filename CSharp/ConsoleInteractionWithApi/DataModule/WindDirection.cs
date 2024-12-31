
namespace ConsoleInteractionWithApi.DataModule
{
    internal class WindDirection
    {
        public string Unit { get; set; }
        public float Value { get; set; }

        //TODO: For CsvHelper need public set property and constuctor without parameters
        public WindDirection()
        { }

        public WindDirection(string unit, float value)
        {
            Unit = unit;
            Value = value;
        }
    }
}
