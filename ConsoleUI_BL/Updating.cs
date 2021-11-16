﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    class Updating
    {
        BL.BL BLProgram;
        public Updating(BL.BL _bLProgram)
        {
            BLProgram = _bLProgram;
        }
        public enum enumUpdatingOptions { EXIT = 0, UPDATE_NAME_OF_DRONE,STATION_DATA, PACKAGE_DELIVERY, CARGING_DRONE, RELEASE_DRONE };
        public void UpdatingOptions()
        {
            Console.WriteLine("enter 1 to  assigning a package to a drone");
            Console.WriteLine("enter 2 to  collection of a package by drone");
            Console.WriteLine("enter 3 to  delivery package to customer");
            Console.WriteLine("enter 4 to  sending a drone for charging at a base station");
            Console.WriteLine("enter 5 to  release drone from charging at base station");
            Console.WriteLine("enter 0 to EXIT");

            enumUpdatingOptions enumUpdating;
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            enumUpdating = (enumUpdatingOptions)choice;
            switch (enumUpdating)
            {
                case enumUpdatingOptions.UPDATE_NAME_OF_DRONE:
                    UpdateModelTheDrone();
                    break;
                case enumUpdatingOptions.STATION_DATA:
                    updatingStationData(); 
                    break;
                case enumUpdatingOptions.PACKAGE_DELIVERY:
                    updatingcustomerData();
                    break;
                case enumUpdatingOptions.CARGING_DRONE:
                    SendingDroneForCharging();
                    break;
                case enumUpdatingOptions.RELEASE_DRONE:
                    ReleasingDroneFromCharging();
                    break;
                case enumUpdatingOptions.EXIT:
                    return;
                default:
                    Console.WriteLine("ERROR!");
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateModelTheDrone()
        {
            Console.WriteLine("Enter the ID of the drone you want to change its name to");
            int IdDrone;
            int.TryParse(Console.ReadLine(), out IdDrone);
            Console.WriteLine("Enter the name you want to change");
            string newModel = Console.ReadLine();
            bool test = BLProgram.updateModelOfDrone(newModel, IdDrone);
            if (test)
                Console.WriteLine("Updated successfully");
            else
                Console.WriteLine("ERROR");
        }
        /// <summary>
        /// This function performs an update on packet collection by drone.
        /// </summary>
        public void updatingStationData()
        {
            Console.WriteLine("Enter the ID of a station you would like to update");
            int Idstation;
            int.TryParse(Console.ReadLine(), out Idstation);
            Console.WriteLine("Enter a new name for the station, if you do not want to update Enter X");
            string newName = Console.ReadLine();
            Console.WriteLine("Update the total amount of charging stations, if you do not want to update Enter -1");
            int ChargingSlots;
            int.TryParse(Console.ReadLine(), out ChargingSlots);
            bool testParcel = BLProgram.updateStationData(Idstation, newName, ChargingSlots);
            if (testParcel)
                Console.WriteLine("the station data has been updated successfully");
            else
                Console.WriteLine("ERROR");
        }
        /// <summary>
        /// This function performs an update on delivering a package to the customer.
        /// </summary>
        public void updatingcustomerData()
        {
            Console.WriteLine("enter ID number of the customer");
            int IdCustomer;
            int.TryParse(Console.ReadLine(), out IdCustomer);
            Console.WriteLine("Enter a new name for the customer, if you do not want to update Enter X");
            string newName = Console.ReadLine();
            Console.WriteLine("Enter a new phone number for the customer, if you do not want to update Enter X");
            string newPhone = Console.ReadLine();
            bool testCustomer = BLProgram.updateCustomerData(IdCustomer,newName,newPhone);
            if (testCustomer)
                Console.WriteLine("the transaction completed successfully");
            else
                Console.WriteLine("ERROR");
        }
        /// <summary>
        /// The function performs an update on sending a drone for charging.
        /// </summary>
        public void SendingDroneForCharging()
        {
            Console.WriteLine("enter ID number of the drone");
            int IdDrone;
            int.TryParse(Console.ReadLine(), out IdDrone);
            
            bool testStation = BLProgram.SendDroneForCharging(IdDrone);

            if (testStation)
                Console.WriteLine("the transaction completed successfully");
            else
                Console.WriteLine("ERROR");
        }
        /// <summary>
        /// The function performs an update on drone release from charging.
        /// </summary>
        public void ReleasingDroneFromCharging()
        {
            Console.WriteLine("enter ID number of the drone");
            int IdDrone;
            int.TryParse(Console.ReadLine(), out IdDrone);

            bool test = BLProgram. ReleaseDroneFromCharging(IdDrone);
            //bool testDrone = Dal.DalObject.makeAvailableTheDrone(IdDrone);
            if (testDroneCarge /*&& testDrone && testStation*/)
                Console.WriteLine("the transaction completed successfully");
            else
                Console.WriteLine("ERROR");
        }
    }
}
