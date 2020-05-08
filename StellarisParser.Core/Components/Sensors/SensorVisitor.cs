namespace StellarisParser.Core.Components.Sensors
{
    public class SensorVisitor : ComponentVisitor, IStellarisVisitor<Sensor>
    {
        private readonly SensorRangeVisitor _sensorRangeVisitor;
        private readonly HyperlaneRangeVisitor _hyperlaneRangeVisitor;
        
        public SensorVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, SensorRangeVisitor sensorRangeVisitor, HyperlaneRangeVisitor hyperlaneRangeVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _sensorRangeVisitor = sensorRangeVisitor;
            _hyperlaneRangeVisitor = hyperlaneRangeVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var sensor = base.VisitKeyval(context) as Sensor;
            if (sensor == null)
                return null;

            var sensorRange = _sensorRangeVisitor.Visit(context.val());
            var hyperlaneRange = _hyperlaneRangeVisitor.Visit(context.val());

            sensor.SensorRange = sensorRange;
            sensor.HyperlaneRange = hyperlaneRange;

            return sensor;
        }

        public override Component Create()
        {
            return new Sensor();
        }

        public new Sensor VisitContent(stellarisParser.ContentContext context)
        {
            return (Sensor) base.VisitContent(context);
        }
    }
}