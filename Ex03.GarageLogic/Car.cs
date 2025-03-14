using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public enum eCarColor
        {
            Blue = 1,
            White,
            Red,
            Yellow
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        internal Car(Engine i_Engine, string i_LicenseNumber, int i_NumberOfTires, float i_MaxEnergyAmount,
                            float i_MaxTirePressure, string i_TiresProducerName = "", string i_NameOfModel = "")
            : base (i_Engine, i_LicenseNumber, i_NumberOfTires, i_MaxEnergyAmount,
                            i_MaxTirePressure, i_TiresProducerName, i_NameOfModel)
        { }

        public eCarColor CarColor
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eNumberOfDoors NumOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public override void SetVehicleConditions(params string[] i_Args)
        {
            base.SetVehicleConditions(i_Args);

            int firstValueCarColor = (int)((eCarColor[])Enum.GetValues(typeof(eCarColor)))[0];
            int amountOfCarColor = Enum.GetValues(typeof(eCarColor)).Length;
            int lastValueCarColor = amountOfCarColor - 1 + firstValueCarColor;
            int firstValueNumberOfDoors = (int)((eNumberOfDoors[])Enum.GetValues(typeof(eNumberOfDoors)))[0];
            int amountOfNumberOfDoors = Enum.GetValues(typeof(eNumberOfDoors)).Length;
            int lastValueNumberOfDoors = amountOfNumberOfDoors - 1 + firstValueNumberOfDoors;

            CarColor = (eCarColor)GetParsedIntInRange(i_Args[2], firstValueCarColor, lastValueCarColor);
            NumOfDoors = (eNumberOfDoors)GetParsedIntInRange(i_Args[3], firstValueNumberOfDoors, lastValueNumberOfDoors);
        }

        public override List<string> GetConditionsParamsDescriptions()
        {
            List<string> allConditionsParams = base.GetConditionsParamsDescriptions();
            StringBuilder carColorMessage = new StringBuilder();
            StringBuilder numberOfDoorsMessage = new StringBuilder();
            int index = (int)((eCarColor[])Enum.GetValues(typeof(eCarColor)))[0];

            carColorMessage.AppendLine("Car Color:");
            foreach (eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
            {
                carColorMessage.AppendLine($"{index}) {carColor}");
                index++;
            }

            index = (int)((eNumberOfDoors[])Enum.GetValues(typeof(eNumberOfDoors)))[0];
            numberOfDoorsMessage.AppendLine("Number of doors:");
            foreach (eNumberOfDoors numberOfDoors in Enum.GetValues(typeof(eNumberOfDoors)))
            {
                numberOfDoorsMessage.AppendLine($"{index}) {numberOfDoors}");
                index++;
            }

            allConditionsParams.Add(carColorMessage.ToString());
            allConditionsParams.Add(numberOfDoorsMessage.ToString());

            return allConditionsParams;
        }

        public override bool IsMatchedConditions(params string[] i_Args)
        {
            bool isBaseMatched = base.IsMatchedConditions(i_Args);
            int firstValueCarColor = (int)((eCarColor[])Enum.GetValues(typeof(eCarColor)))[0];
            int amountOfCarColor = Enum.GetValues(typeof(eCarColor)).Length;
            int lastValueCarColor = amountOfCarColor - 1 + firstValueCarColor;
            int firstValueNumberOfDoors = (int)((eNumberOfDoors[])Enum.GetValues(typeof(eNumberOfDoors)))[0];
            int amountOfNumberOfDoors = Enum.GetValues(typeof(eNumberOfDoors)).Length;
            int lastValueNumberOfDoors = amountOfNumberOfDoors - 1 + firstValueNumberOfDoors;
            bool isCarColorMatched = true;
            bool isNumOfDoorsMatched = true;

            if (i_Args[2] != null)
            {
                isCarColorMatched =
                    CarColor == (eCarColor)GetParsedIntInRange(i_Args[2], firstValueCarColor, lastValueCarColor);
            }

            if (i_Args[3] != null)
            {
                isNumOfDoorsMatched =
                    NumOfDoors == (eNumberOfDoors)GetParsedIntInRange(i_Args[3], firstValueNumberOfDoors, lastValueNumberOfDoors);
            }

            return isBaseMatched && isCarColorMatched && isNumOfDoorsMatched;
        }

        public override string ToString()
        {
            StringBuilder carDescription = new StringBuilder();

            carDescription.AppendLine("Car:");
            carDescription.AppendLine(base.ToString());
            carDescription.AppendLine();
            carDescription.AppendLine($"Car Color: {CarColor}");
            carDescription.AppendLine($"Num Of Doors: {NumOfDoors}");

            return carDescription.ToString();
        }
    }
}
