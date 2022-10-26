using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignment1
{
    class Program
    {
        private static string strEnterSelect = "ENTER Your Selection: ";
        private static string strEnterPetID = "ENTER PET ID: ";
        private static string strEnterVetID = "ENTER VET ID: ";
        private static string strEnterDate = "ENTER DATE (DD/MM/YYYY): ";
        private static string strEnterPetNum = "ENTER PET ID: ";
        private static string strEnterRegNum = "ENTER REG NUM: ";
        private static string strLineBreak = "\n===========================================================\n";

        static void Main(string[] args)
        {
            using (var db = new VetPracticeContainer())
            {
                Console.WriteLine("VETERANIAN ADMINISTRATION SYSTEM");
                Console.WriteLine(strLineBreak);

                while (true)
                {
                    Console.WriteLine("1 - Pet Owner Details List");
                    Console.WriteLine("2 - Pet Details List");
                    Console.WriteLine("3 - Enter Registration Num to see Practice Details");
                    Console.WriteLine("4 - Enter Pet ID to see Pet Details");
                    Console.WriteLine("5 - Enter Vet ID and Date to see appointments for Vet");
                    Console.WriteLine("6 - Enter Pet Num to see cost of most recent Visit ");
                    Console.WriteLine("7 - LOG OUT");

                    int iSelection = 0;
                    Console.Write(strEnterSelect);
                    while (!(int.TryParse(Console.ReadLine(), out iSelection) && (iSelection >= 1 && iSelection <= 7)))
                    {
                        Console.WriteLine("ENTER integer 1..7 for selection");
                        Console.Write(strEnterSelect);
                    }

                    Console.WriteLine();
                    switch (iSelection)
                    {
                        case 1:
                            PetOwnerDetails(db);
                            break;
                        case 2:
                            AllPetDetails(db);
                            break;
                        case 3:
                            PracticeDetails(db);
                            break;
                        case 4:
                            PetDetails(db);
                            break;
                        case 5:
                            VetDetails(db);
                            break;
                        case 6:
                            VisitCostBreakdown(db);
                            break;
                        case 7:
                            Console.WriteLine("Logging Out");
                            return;
                        default:
                            Console.WriteLine("Invalid selection");
                            break;
                    }
                }
            }
        }

        private static void PetOwnerDetails(VetPracticeContainer db)
        {
            var OwnerQuery = from owner in db.Owners orderby owner.Surname select owner;
            if (OwnerQuery.Any())
            {
                Console.WriteLine("OWNER DETAILS");
                Console.WriteLine("|{0,10}|{1,10}|{2,25}|{3,15}|{4,20}|", "First Name", "Surname", "Address", "TelNo", "Email");
                foreach (var item in OwnerQuery)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                    Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,25}|{3,15}|{4,20}|", item.FirstName, item.Surname, item.Address, item.TelNo, item.Email));
                }
            }
            else
            {
                Console.WriteLine("NO OWNERS WERE FOUND IN THE DATABASE");
            }
                Console.WriteLine(strLineBreak);
        }

        private static void AllPetDetails(VetPracticeContainer db)
        {
            var PetQuery = from pet in db.Pets select pet;
            if (PetQuery.Any())
            {
                Console.WriteLine("PET DETAILS");
                Console.WriteLine("|{0,5}|{1,10}|{2,15}|{3,15}|{4,10}|", "ID", "Name", "Type", "Breed", "Owner ID");
                foreach (var item in PetQuery)
                {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine(String.Format("|{0,5}|{1,10}|{2,15}|{3,15}|{4,10}|", item.Id, item.Name, item.Type, item.Breed, item.OwnerId));
                }
            }
            else
            {
                Console.WriteLine("NO PETS WERE FOUND IN THE DATABASE");
            }
            Console.WriteLine(strLineBreak);
        }

        private static void PracticeDetails(VetPracticeContainer db)
        {
            Console.Write(strEnterRegNum);
            int iRegNum;
            bool isParsed = int.TryParse(Console.ReadLine(), out iRegNum);
            if (!isParsed)
            {
                Console.WriteLine("COULD NOT FIND REG NUM");
                Console.Write(strEnterRegNum);
                isParsed = int.TryParse(Console.ReadLine(), out iRegNum);
            }
            else
            {
                var PractQuery = from vetPrac in db.Practices where vetPrac.RegNum == iRegNum select vetPrac;
                if (PractQuery.Any())
                {
                    Console.WriteLine("VETERANIAN PRACTICE DETAILS");
                    Console.WriteLine("|{0,10}|{1,20}|{2,20}|{3,12}|", "Reg Number", "Name", "Address", "Telephone#");
                    foreach (var item in PractQuery)
                    {
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine(String.Format("|{0,10}|{1,20}|{2,20}|{3,12}|", item.RegNum, item.PracticeName, item.Address, item.TelNo));
                    }
                } else
                {
                    Console.WriteLine("NO PRACTICES WERE FOUND IN THE DATABASE");
                }
                Console.WriteLine(strLineBreak);
            }
        }

        private static void PetDetails(VetPracticeContainer db)
        {
            Console.Write(strEnterPetID);
            int iPetID;
            bool isParsed = int.TryParse(Console.ReadLine(), out iPetID);

            while (!isParsed)
            {
                Console.WriteLine("INVALID PET ID. ID CONSIST OF NUMBERS ONLY");
                Console.Write(strEnterPetID);
                isParsed = int.TryParse(Console.ReadLine(), out iPetID);
            }

            var PetQuery = from pet in db.Pets where pet.Id == iPetID select pet;
            if (PetQuery.Any())
            {
                string strName = PetQuery.FirstOrDefault().Name;
                string strType = PetQuery.FirstOrDefault().Type;
                string strBreed = PetQuery.FirstOrDefault().Breed;

                var VisitQuery = from visit in db.Visits where visit.PetId == iPetID orderby visit.Date select visit;

                Console.WriteLine("\nAPPOINTMENTS FOR NAME: " + strName + " TYPE: " + strType + " BREED: " + strBreed);
                Console.WriteLine("|{0,25}|{1,20}|", "Date", "Notes");

                foreach (var item in VisitQuery)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine(String.Format("|{0,25}|{1,20}|", item.Date, item.Notes));

                }
                Console.WriteLine(strLineBreak);
            }
            else
            {
                Console.WriteLine("NO PETS WERE FOUND WITH THAT ID");
                Console.WriteLine(strLineBreak);
            }
        }

        private static void VetDetails(VetPracticeContainer db)
        {
            Console.Write(strEnterVetID);
            int iVetID;
            bool isParsed = int.TryParse(Console.ReadLine(), out iVetID);
            string strDateTime = "";
            while (!isParsed)
            {
                Console.WriteLine("INVALID VET ID. ID CONSIST OF NUMBERS ONLY");
                Console.Write(strEnterVetID);
                isParsed = int.TryParse(Console.ReadLine(), out iVetID);
            }
            Console.Write(strEnterDate);

            string strDate = Console.ReadLine();

            while (!Regex.IsMatch(strDate, "[0-3][0-9]/[0-1][0-9]/[0-9]{4}"))
            {
                Console.WriteLine("Invalid date");
                Console.Write(strEnterDate);
                strDate = Console.ReadLine();
            }
            var VetQuery = from vet in db.Vets where vet.StaffNo == iVetID select vet;
            if (VetQuery.Any())
            {
                string strVetName = VetQuery.FirstOrDefault().FirstName;
                string strVetSurname = VetQuery.FirstOrDefault().Surname;

                Console.WriteLine("\nAPPOINTMENTS FOR VET: " + strVetSurname + ", " + strVetName);
                Console.WriteLine("|{0,20}|{1,10}|{2,15}|{3,10}|", "Date", "Pet", "Owner", "Notes");
                Console.WriteLine("----------------------------------------------------------------");

                var AppQuery = from app in db.Visits where app.VetId.Equals(iVetID) select app;
                if (AppQuery.Any())
                {
                    strDateTime = AppQuery.FirstOrDefault().Date.ToString();
                }
                foreach (var item in AppQuery)
                {
                    if (item.Date.ToString().Contains(strDate))
                    {
                        string strNotes = item.Notes;
                        var PetQuery = from pet in db.Pets where pet.Id == item.PetId select pet;
                        string strPetName = PetQuery.FirstOrDefault().Name;
                        int iOwnerId = PetQuery.FirstOrDefault().OwnerId;
                        var OwnerQuery = from owner in db.Owners where owner.Id == iOwnerId select owner;
                        string strOwnerName = OwnerQuery.FirstOrDefault().Surname + ", " + OwnerQuery.FirstOrDefault().FirstName;

                        Console.WriteLine("|{0,20}|{1,10}|{2,15}|{3,10}|", strDateTime, strPetName, strOwnerName, strNotes);
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("NO VETS WERE FOUND WITH THAT ID OR THERE ARE NO APPOINTMENTS ON THAT DATE");
                Console.WriteLine(strLineBreak);
            }
        }

        private static void VisitCostBreakdown(VetPracticeContainer db)
        {
            string strTreatmentName = "";
            List<int> iCostList = new List<int>();
            Console.Write(strEnterPetNum);
            int iTotalCost = 0;
            int iTreatmentCost = 0;
            int iPetID;
            bool isParsed = int.TryParse(Console.ReadLine(), out iPetID);

            while (!isParsed)
            {
                Console.WriteLine("INVALID PET ID. ID CONSIST OF NUMBERS ONLY");
                Console.Write(strEnterVetID);
                isParsed = int.TryParse(Console.ReadLine(), out iPetID);
            }

            var VisitQuery = from visit in db.Visits orderby visit.Date descending where visit.PetId == iPetID select visit;
            string strVisitNotes = VisitQuery.FirstOrDefault().Notes;
            int iVisitID = VisitQuery.FirstOrDefault().Id;
            string strDate = VisitQuery.FirstOrDefault().Date.ToString();
            int iVetID = VisitQuery.FirstOrDefault().VetId;
            iTotalCost += VisitQuery.FirstOrDefault().Cost;


            var VetQuery = from vet in db.Vets where vet.Id == iVetID select vet;
            string strVetName = VetQuery.FirstOrDefault().Surname + ", " + VetQuery.FirstOrDefault().FirstName;

            var PetQuery = from pet in db.Pets where pet.Id == iPetID select pet;
            string strPetName = PetQuery.FirstOrDefault().Name;

            Console.WriteLine("\nMOST RECENT VISIT FOR: " + strPetName + "\nDATE: " + strDate);

            var TreatmentQuery = from treatment in db.Treatments where treatment.VisitId == iVisitID select treatment;

            if (TreatmentQuery.Any())
            {
                foreach (var treatment in TreatmentQuery)
                {
                    int iTreatmentID = treatment.Id;
                    strTreatmentName = treatment.Name;
                    iTreatmentCost = treatment.Cost;
                    var MedQuery = from medication in db.Medications where medication.TreatmentId == iTreatmentID select medication;

                    Console.WriteLine("TREATMENT FOR: " + strTreatmentName + "\nITEMISED BILL:");
                    Console.WriteLine("============================================");
                    Console.WriteLine("|{0,20}|{1,10}|{2,10}|", "Medication", "Dose", "Cost");
                    Console.WriteLine("--------------------------------------------");

                    foreach (var medication in MedQuery)
                    {
                        int iMedCost = medication.Cost;
                        int iDose = medication.Dose;
                        iTotalCost += iMedCost;
                        string strMedication = medication.Name;
                        Console.WriteLine("|{0,20}|{1,10}|{2,10}|", strMedication, iDose.ToString()+"mg", "£" + iMedCost.ToString());
                        Console.WriteLine("--------------------------------------------");
                    }
                }
                Console.WriteLine("{0,43}", "Appointment Cost: £" + iTreatmentCost);
                Console.WriteLine("{0,43}", "TOTAL: £" + iTotalCost.ToString());
            }else
            {
                Console.WriteLine("THERE IS NO ITEMISED BILL FOR THIS VISIT");
                Console.WriteLine("{0,43}", "Appointment Cost: £" + iTreatmentCost);
                Console.WriteLine("{0,43}", "TOTAL: £" + iTotalCost.ToString());
            }
            Console.WriteLine(strLineBreak);
        }
    }
}
