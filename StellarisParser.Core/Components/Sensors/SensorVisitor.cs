namespace StellarisParser.Core.Components.Sensors
{
    public class SensorVisitor : ComponentVisitor<Sensor>
    {
        private readonly SensorRangeVisitor _sensorRangeVisitor;
        private readonly HyperlaneRangeVisitor _hyperlaneRangeVisitor;
        

        public override Sensor VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var sensor = base.VisitKeyval(context);
            if (sensor == null)
                return null;

            var sensorRange = _sensorRangeVisitor.Visit(context.val());
            var hyperlaneRange = _hyperlaneRangeVisitor.Visit(context.val());

            sensor.SensorRange = sensorRange;
            sensor.HyperlaneRange = hyperlaneRange;

            return sensor;
        }

        public SensorVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, SizeVisitor sizeVisitor, Localisation.Localisation localisation, SensorRangeVisitor sensorRangeVisitor, HyperlaneRangeVisitor hyperlaneRangeVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor, sizeVisitor, localisation)
        {
            _sensorRangeVisitor = sensorRangeVisitor;
            _hyperlaneRangeVisitor = hyperlaneRangeVisitor;
        }
    }
}