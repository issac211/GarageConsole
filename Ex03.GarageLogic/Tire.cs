using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private readonly float r_MaxTirePressure = 30;
        private readonly string r_ProducerName;
        private float m_CurrentTirePressure;

        internal Tire(float i_MaxTirePressure, string i_ProducerName = "")
        {
            if(i_MaxTirePressure < 0)
            {
                throw new ArgumentException(
                    string.Format("The Max Tire Pressure '{0}' is not possible, must be greater than 0", i_MaxTirePressure));
            }

            r_MaxTirePressure = i_MaxTirePressure;
            r_ProducerName = i_ProducerName;
        }

        public string ProducerName
        {
            get { return r_ProducerName; }
        }

        public float CurrentTirePressure
        {
            get { return m_CurrentTirePressure; }
        }

        public float MaxTirePressure
        {
            get { return r_MaxTirePressure; }
        }

        internal void SetCurrentTirePressure(float i_TirePressure)
        {
            if (i_TirePressure > MaxTirePressure || i_TirePressure < 0)
            {
                throw new ValueOutOfRangeException(i_TirePressure, 0, MaxTirePressure);
            }

            m_CurrentTirePressure = i_TirePressure;
        }

        internal bool IsMatchedTirePressure(float i_TirePressure)
        {
            return m_CurrentTirePressure == i_TirePressure;
        }

        internal void InflateATire(float i_AmountOfAirToInflate)
        {
            SetCurrentTirePressure(CurrentTirePressure + i_AmountOfAirToInflate);
        }

        public override string ToString()
        {
            StringBuilder tireDescription = new StringBuilder();

            tireDescription.AppendLine($"ProducerName: {ProducerName}");
            tireDescription.AppendLine($"Current Tire Pressure: {CurrentTirePressure}");
            tireDescription.AppendLine($"Max Tire Pressure: {MaxTirePressure}");

            return tireDescription.ToString();
        }
    }
}
