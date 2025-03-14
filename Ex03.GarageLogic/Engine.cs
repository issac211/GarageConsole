using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private float m_MaxEnergyAmount;
        private float m_CurrentEnergyAmount;

        internal float MaxEnergyAmount
        {
            get { return m_MaxEnergyAmount; }
            set
            {
                if (value < CurrentEnergyAmount || value < 0)
                {
                    throw new ValueOutOfRangeException(value, CurrentEnergyAmount, float.MaxValue);
                }

                m_MaxEnergyAmount = value;
            }
        }

        internal float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set
            {
                if (value > MaxEnergyAmount || value < 0)
                {
                    throw new ValueOutOfRangeException(value, 0, MaxEnergyAmount);
                }

                m_CurrentEnergyAmount = value;
            }
        }

        public float PrecentOfCurrentEnergy
        {
            get
            {
                return CurrentEnergyAmount / MaxEnergyAmount;
            }
        }

        public virtual void FillPower(params string[] i_Args)
        {
            float addedPower = float.Parse(i_Args[0]);

            CurrentEnergyAmount += addedPower;
        }

        public abstract List<string> GetFillParamsDescriptions();

        public abstract List<string> GetConditionsParamsDescriptions();

        public override string ToString()
        {
            string engineDescription = string.Format("Precent of current energy is: {0}", PrecentOfCurrentEnergy);

            return engineDescription;
        }
    }
}
