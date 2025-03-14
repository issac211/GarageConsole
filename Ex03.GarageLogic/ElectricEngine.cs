using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public override List<string> GetFillParamsDescriptions()
        {
            return new List<string>() { "How much Battery Time to fill (minutes)" };
        }

        public override List<string> GetConditionsParamsDescriptions()
        {
            return new List<string>() { "Battery time remaining (hours)" };
        }

        public override void FillPower(params string[] i_Args)
        {
            float addedPower = float.Parse(i_Args[0]);
            string[] newArgs = new string[i_Args.Length];
            newArgs[0] = (addedPower / 60).ToString();

            base.FillPower(newArgs);
        }

        public override string ToString()
        {
            StringBuilder electricEngineDescription = new StringBuilder();

            electricEngineDescription.AppendLine("Electric Engine");
            electricEngineDescription.AppendLine(base.ToString());
            electricEngineDescription.AppendLine($"Battery time remaining (hours): {CurrentEnergyAmount}");
            electricEngineDescription.AppendLine($"Max Battery time (hours): {MaxEnergyAmount}");

            return electricEngineDescription.ToString();
        }
    }
}
