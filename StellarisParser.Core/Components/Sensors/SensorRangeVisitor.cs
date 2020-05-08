namespace StellarisParser.Core.Components.Sensors
{
    public class SensorRangeVisitor : SpecVisitorDouble
    {
        public SensorRangeVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SENSOR_RANGE_ID;
    }
}