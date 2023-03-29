using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_1_Facade
{
    //Facade Class
    class InsuranceFacade
    {
        private DrivingLicense ssA;
        private Accident ssB;
        private Claim ssC;

        public InsuranceFacade()
        {
            this.ssA = new DrivingLicense();
            this.ssB = new Accident();
            this.ssC = new Claim();
        }

        public void SetRate(Driver driver)
        {
            bool disountRateEligibility = true;
            Console.WriteLine("Checking discount rate eligibility...");

            if (!ssA.HasDrivingLicense(driver))
            {
                disountRateEligibility = false;
            }

            if (!ssB.HasNoAccidents(driver))
            {
                disountRateEligibility = false;
            }

            if (!ssC.HasNoClaim(driver))
            {
                disountRateEligibility = false;
            }

            if (disountRateEligibility)
            {
                Console.WriteLine("{0} is eligible to the discount rate", driver.DriverName);
            }
            else
            {
                Console.WriteLine("{0} is only eligible to the standard rate", driver.DriverName);
            }

        }
    }

    // A 'Subsystem' class
    class DrivingLicense
    {
        public bool HasDrivingLicense(Driver driver)
        {
            Console.WriteLine("Checking License Information for {0}....", driver.LicenseNumber);
            return true;
        }
    }

    // A 'Subsystem' class
    class Accident
    {
        public bool HasNoAccidents(Driver driver)
        {
            Console.WriteLine("Checking Accidents History for {0}....", driver.DriverName);
            return true;
        }
    }

    // A 'Subsystem' class
    class Claim
    {
        public bool HasNoClaim(Driver driver)
        {
            Console.WriteLine("Checking Previous Claims for {0}....", driver.LicenseNumber);
            return true;
        }
    }



    //Helper Class
    public class Driver
    {

        public string LicenseNumber { get; set; }
        public string DriverName { get; set; }

        public Driver(string licenseNumber, string driverName)
        {
            LicenseNumber = licenseNumber;
            DriverName = driverName;
        }
    }
}
