
namespace ConsoleInteractionWithApi.DataModule
{
    internal class WindData
    {
        public WindSpeed Speed { get; set; }
        public WindDirection Direction { get; set; }

        //TODO: For CsvHelper need public set property and constuctor without parameters
        public WindData()
        { }

        public WindData(WindSpeed speed, WindDirection direction)
        {
            Speed = speed;
            Direction = direction;
        }

        public override string ToString()
        {
            return String.Format("WindSpeed: {0} ({1})  |   WindDirection: {2} ({3})", 
                                    Speed.Value, Speed.Unit, Direction.Value, Direction.Unit);
        }
    }
}
