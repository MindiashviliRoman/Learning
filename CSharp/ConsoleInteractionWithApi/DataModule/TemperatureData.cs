
namespace ConsoleInteractionWithApi.DataModule
{
    internal class TemperatureData
    {
        public string Unit { get; set; }
        public float Value { get; set; }

        //TODO: For CsvHelper need public set property and constuctor without parameters
        public TemperatureData()
        { }

        public TemperatureData(string unit, float value)
        { 
            Unit = unit;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Value, Unit);
        }
    }
}
