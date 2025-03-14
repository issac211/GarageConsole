using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class Garage
    {
        private const string k_SelectionIsNotOkMessage = "Choice not possible, You have to choose the appropriate number";
        private const string k_YesString = "y";
        private const string k_NoString = "n";
        private const string k_ExitString = "exit";
        private readonly string r_ExitMessage = string.Format("(To exit Enter '{0}')", k_ExitString);
        private readonly string r_NewLine = Environment.NewLine;
        private readonly VehicleFactory r_VehicleFactory;
        private readonly VehicleManager r_VehicleManager;
        private StoredVehicle m_CurrentStoredVehicle;

        private enum eGarageFeatures
        {
            EnterNewVehicle = 1,
            ShowLicenseNumbers,
            ChangeConditionOfVehicle,
            InflateVehicleTiresToMax,
            FillVehiclePowerInGarage,
            DisplaySpecificVehicleData
        }

        public Garage()
        {
            r_VehicleFactory = new VehicleFactory();
            r_VehicleManager = new VehicleManager();
        }

        public void OpenForCustomers()
        {
            string userInput;

            Console.WriteLine("Welcome to our Garage!!" + r_NewLine);
            while (true)
            {
                Console.WriteLine("Choose one of the following options:");
                Console.WriteLine(r_ExitMessage + r_NewLine);
                showUserSelections();
                userInput = Console.ReadLine();
                
                if (userInput.ToLower() == k_ExitString.ToLower())
                {
                    break;
                }

                eGarageFeatures userSelection;
                bool selectionIsOk = Enum.TryParse(userInput, out userSelection);

                Console.WriteLine(r_NewLine);
                if (selectionIsOk)
                {
                    switch (userSelection)
                    {
                        case eGarageFeatures.EnterNewVehicle:
                            enterNewVehicleInGarage();
                            break;
                        case eGarageFeatures.ShowLicenseNumbers:
                            showLicenseNumbersInGarage();
                            break;
                        case eGarageFeatures.ChangeConditionOfVehicle:
                            changeConditionOfVehicleInGarage();
                            break;
                        case eGarageFeatures.InflateVehicleTiresToMax:
                            inflateVehicleTiresToMaxInGarage();
                            break;
                        case eGarageFeatures.FillVehiclePowerInGarage:
                            fillVehiclePowerInGarage();
                            break;
                        case eGarageFeatures.DisplaySpecificVehicleData:
                            displaySpecificVehicleDataInGarage();
                            break;
                        default:
                            Console.WriteLine(k_SelectionIsNotOkMessage);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(k_SelectionIsNotOkMessage);
                }

                Console.WriteLine(r_NewLine);
            }
        }

        private void showUserSelections()
        {
            int selectionIndex = 1;

            foreach (eGarageFeatures garageFeature in Enum.GetValues(typeof(eGarageFeatures)))
            {
                Console.WriteLine("{0} {1}", selectionIndex, addSpacesToSentence(garageFeature.ToString()));
                selectionIndex++;
            }
        }

        private string addSpacesToSentence(string i_Text)
        {
            string newStringText;

            if (string.IsNullOrWhiteSpace(i_Text))
            {
                newStringText = "";
            }
            else
            {
                StringBuilder newText = new StringBuilder(i_Text.Length * 2);

                newText.Append(i_Text[0]);
                for (int i = 1; i < i_Text.Length; i++)
                {
                    if (char.IsUpper(i_Text[i]) && i_Text[i - 1] != ' ')
                    {
                        newText.Append(' ');
                    }

                    newText.Append(i_Text[i]);
                }

                newStringText = newText.ToString();
            }

            return newStringText;
        }

        private void displaySpecificVehicleDataInGarage()
        {
            bool isLicenseNumberInGarage;
            string currentLicenseNumber;

            Console.WriteLine("Display specific vehicle data in garage:" + r_NewLine);
            currentLicenseNumber = getLicenseNumber();
            isLicenseNumberInGarage = r_VehicleManager.TryGetStoredVehicle(currentLicenseNumber,
                                                                                out m_CurrentStoredVehicle);
            if (isLicenseNumberInGarage)
            {
                displayVehicleData();
            }
            else
            {
                Console.WriteLine("The vehicle was not not found");
            }
        }

        private void displayVehicleData()
        {
            Console.WriteLine(r_NewLine);
            Console.WriteLine(m_CurrentStoredVehicle);
            Console.WriteLine(r_NewLine);
        }

        private void fillVehiclePowerInGarage()
        {
            bool isLicenseNumberInGarage;
            string currentLicenseNumber;

            Console.WriteLine("Fill the vehicle power engine:" + r_NewLine);
            currentLicenseNumber = getLicenseNumber();

            isLicenseNumberInGarage = r_VehicleManager.TryGetStoredVehicle(currentLicenseNumber,
                                                                                out m_CurrentStoredVehicle);
            if (isLicenseNumberInGarage)
            {
                Console.WriteLine(r_NewLine);
                fillVehiclePowerEngine();
            }
            else
            {
                Console.WriteLine(r_NewLine + "This license number does not exist in storage");
            }
        }

        private void fillVehiclePowerEngine()
        {
            Engine currentVehicleEngine = m_CurrentStoredVehicle.Vehicle.VehicleEngine;
            List<string> fillParamsDescriptions = currentVehicleEngine.GetFillParamsDescriptions();
            List<string> fillParams = new List<string>();

            foreach (string message in fillParamsDescriptions)
            {
                Console.WriteLine(message);

                string userInput = Console.ReadLine();

                fillParams.Add(userInput);
            }

            try
            {
                currentVehicleEngine.FillPower(fillParams.ToArray());
                Console.WriteLine("The vehical power engine has been Successfully filled");
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(valueOutOfRangeException.Message);
                Console.WriteLine(r_NewLine);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(formatException.Message);
                Console.WriteLine(r_NewLine);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(argumentException.Message);
                Console.WriteLine(r_NewLine);
            }

        }

        private void inflateVehicleTiresToMaxInGarage()
        {
            bool isInflated;
            string currentLicenseNumber;

            Console.WriteLine("Inflate vehicle tires to maximum in garage:" + r_NewLine);
            currentLicenseNumber = getLicenseNumber();
            isInflated = r_VehicleManager.InflateVehicleTiresToMax(currentLicenseNumber);

            if (isInflated)
            {
                Console.WriteLine("The vehicle was inflated to maximum");
            }
            else
            {
                Console.WriteLine("The vehicle or tires was not not found");
            }
        }

        private void changeConditionOfVehicleInGarage()
        {
            bool isLicenseNumberInGarage;
            string currentLicenseNumber;

            Console.WriteLine("Changeing condition of vehicle in garage:" + r_NewLine);
            currentLicenseNumber = getLicenseNumber();
            isLicenseNumberInGarage = r_VehicleManager.TryGetStoredVehicle(currentLicenseNumber,
                                                                                out m_CurrentStoredVehicle);
            if (isLicenseNumberInGarage)
            {
                setVehicleCondition();
                changeVehicleStatus(currentLicenseNumber);
                Console.WriteLine("Vehicle Conditions has been changed successfully");
            }
            else
            {
                Console.WriteLine(r_NewLine + "This license number does not exist in storage");
            }
        }

        private void changeVehicleStatus(string i_LicenseNumber)
        {
            Console.WriteLine("Change the vehicle status? (Enter '{0}' for NO)", k_NoString);

            foreach (eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
            {
                Console.WriteLine($"{(int)status}) {status}");
            }

            string newVehicleStatusInput = Console.ReadLine();

            if (newVehicleStatusInput.ToLower() != k_NoString.ToLower())
            {
                bool inputIsOk = r_VehicleManager.ChangeVehicleStatus(i_LicenseNumber, newVehicleStatusInput);

                if (!inputIsOk)
                {
                    Console.WriteLine("Incorrect input, the status has not changed");
                }
            }
        }

        private void showLicenseNumbersInGarage()
        {
            Console.WriteLine("Showing all license numbers in garage:" + r_NewLine);

            foreach (string licenseNumber in r_VehicleManager.StoredVehiclesInGarage)
            {
                Console.WriteLine(licenseNumber);
            }

            Console.WriteLine(r_NewLine);

            Console.WriteLine(
                "If you want to filtered by values, Enter {0} (Otherwise enter any other key)", k_YesString);

            string userInput = Console.ReadLine();

            if (userInput.ToLower() == k_YesString.ToLower())
            {
                showFilteredLicenseNumbers();
            }

            Console.WriteLine(r_NewLine);
        }

        private void showFilteredLicenseNumbers()
        {
            string[] filteredLicenseNumbers;
            string[] sharedConditionsDescriptions = r_VehicleManager.GetSharedConditionsDescriptions();
            List<string> conditionsParams = new List<string>();

            Console.WriteLine("Set the filters:" + r_NewLine);
            foreach (string message in sharedConditionsDescriptions)
            {
                Console.WriteLine(message);

                string userInput = Console.ReadLine();

                conditionsParams.Add(userInput);
            }

            try
            {
                filteredLicenseNumbers = r_VehicleManager.GetFilteredVehiclesInGarage(conditionsParams.ToArray());
                Console.WriteLine("Showing filtered license numbers:" + r_NewLine);
                if (filteredLicenseNumbers != null)
                {
                    foreach (string licenseNumber in filteredLicenseNumbers)
                    {
                        Console.WriteLine(licenseNumber);
                    }
                }
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(valueOutOfRangeException.Message);
                Console.WriteLine(r_NewLine);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(formatException.Message);
                Console.WriteLine(r_NewLine);
            }
        }

        private void enterNewVehicleInGarage()
        {
            bool isLicenseNumberInGarage;
            string currentLicenseNumber;

            currentLicenseNumber = getLicenseNumber();
            isLicenseNumberInGarage = r_VehicleManager.TryGetStoredVehicle(currentLicenseNumber,
                                                                                out m_CurrentStoredVehicle);
            if (!isLicenseNumberInGarage)
            {
                storeTheVehicle(currentLicenseNumber);
                setVehicleCondition();
            }
            else
            {
                Console.WriteLine(r_NewLine + "This license number already exist in storage");
                r_VehicleManager.ChangeVehicleStatus(currentLicenseNumber, eVehicleStatus.InProgress.ToString());
                Console.WriteLine(r_NewLine + $"This vehicle has now moved to status {eVehicleStatus.InProgress}");
                Console.WriteLine(r_NewLine + $"Displaying Vehicle Data:");
                displayVehicleData();
            }
        }

        private void setVehicleCondition()
        {
            Vehicle currentVehicle = m_CurrentStoredVehicle.Vehicle;
            List<string> conditionsParamsDescriptions = currentVehicle.GetConditionsParamsDescriptions();
            List<string> conditionsParams = new List<string>();

            Console.WriteLine("Setting the Vehicle Condition:" + r_NewLine);
            foreach (string message in conditionsParamsDescriptions)
            {
                Console.WriteLine(message);
                
                string userInput = Console.ReadLine();

                conditionsParams.Add(userInput);
            }

            try
            {
                currentVehicle.SetVehicleConditions(conditionsParams.ToArray());
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(valueOutOfRangeException.Message);
                Console.WriteLine(r_NewLine);
                setVehicleCondition();
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(r_NewLine);
                Console.WriteLine(formatException.Message);
                Console.WriteLine(r_NewLine);
                setVehicleCondition();
            }
        }

        private void storeTheVehicle(string i_LicenseNumber)
        {
            string ownerName;
            string ownerPhone;

            Console.WriteLine("Enter Owner Name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Enter Owner Phone:");
            ownerPhone = Console.ReadLine();

            bool phoneIsInt = int.TryParse(ownerPhone, out int phoneNum);

            if (!phoneIsInt)
            {
                Console.WriteLine("The phone number needs to be int" + r_NewLine);
                storeTheVehicle(i_LicenseNumber);
            }
            else
            {
                Vehicle vehicleForStorage = getVehicleForStorage(i_LicenseNumber);

                m_CurrentStoredVehicle =
                    r_VehicleManager.StoreTheVehicleInGarage(vehicleForStorage, ownerName, ownerPhone);
            }
        }

        private Vehicle getVehicleForStorage(string i_LicenseNumber)
        {
            Vehicle vehicleForStorage;
            int vehicleNumber = 1;

            Console.WriteLine(
                "What type of vehicle do you want to put in the garage? (Enter the number of The vehicle)" + r_NewLine);
            foreach (string supportedVehicle in r_VehicleFactory.SupportedVehicles)
            {
                Console.WriteLine(supportedVehicle);
                vehicleNumber++;
            }

            string userChoice = Console.ReadLine();
            int choiceNumber;

            try
            {
                choiceNumber = int.Parse(userChoice);
                vehicleForStorage = r_VehicleFactory.MakeVehicleWithVehicleTypeIndex(choiceNumber, i_LicenseNumber);
            }
            catch(FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
                vehicleForStorage = getVehicleForStorage(i_LicenseNumber);
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(outOfRangeException.Message);
                vehicleForStorage =  getVehicleForStorage(i_LicenseNumber);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                vehicleForStorage = getVehicleForStorage(i_LicenseNumber);
            }

            return vehicleForStorage;
        }

        private string getLicenseNumber()
        {
            string licenseNumber;

            Console.WriteLine("What is the vehicle's License Number?");
            licenseNumber = Console.ReadLine();

            return licenseNumber;
        }
    }
}
