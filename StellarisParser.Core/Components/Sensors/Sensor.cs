namespace StellarisParser.Core.Components.Sensors
{
    public class Sensor : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.SENSOR;

        public double SensorRange { get; set; }
        public double HyperlaneRange { get; set; }
    }
}