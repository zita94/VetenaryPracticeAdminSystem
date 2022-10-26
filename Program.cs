using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var query = from owner in db.Owners orderby owner.Surname select owner;
            Console.WriteLine("OWNER DETAILS");
            Console.WriteLine("|{0,10}|{1,10}|{2,25}|{3,15}|{4,20}|", "First Name", "Surname", "Address", "TelNo", "Email");
            foreach (var item in query)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,25}|{3,15}|{4,20}|", item.FirstName, item.Surname, item.Address, item.TelNo, item.Email));
            }

            Console.WriteLine(strLineBreak);

        }

        private static void AllPetDetails(VetPracticeContainer db)
        {
            var query = from pet in db.Pets select pet;
            Console.WriteLine("PET DETAILS");
            Console.WriteLine("|{0,5}|{1,10}|{2,15}|{3,15}|{4,10}|", "ID", "Name", "Type", "Breed", "Owner ID");
            foreach (var item in query)
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine(String.Format("|{0,5}|{1,10}|{2,15}|{3,15}|{4,10}|", item.Id, item.Name, item.Type, item.Breed, item.OwnerId));
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
                var query = from vetPrac in db.Practices where vetPrac.RegNum == iRegNum select vetPrac;
                Console.WriteLine("VETERANIAN PRACTICE DETAILS");
                Console.WriteLine("|{0,10}|{1,20}|{2,20}|{3,12}|", "Reg Number", "Name", "Address", "Telephone#");
                foreach (var item in query)
                {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine(String.Format("|{0,10}|{1,20}|{2,20}|{3,12}|", item.RegNum, item.PracticeName, item.Address, item.TelNo));
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

            var query = from pet in db.Pets where (pet.Id == iPetID) select pet;

            if (query.Any())
            {
                string strName = query.FirstOrDefault().Name;
                string strType = query.FirstOrDefault().Type;
                string strBreed = query.FirstOrDefault().Breed;

                var query1 = from visit in db.Visits orderby visit.Date select visit;

                Console.WriteLine("\nAPPOINTMENTS FOR NAME: " + strName + " TYPE: " + strType + " BREED: " + strBreed);
                Console.WriteLine("|{0,15}|{1,20}|", "Date", "Notes");

                foreach (var item in query1)
                {
                    Console.WriteLine("---------------------------------------------------------------------------");
                    Console.WriteLine(String.Format("|{0,15}|{1,20}|", item.Date, item.Notes));

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

            while (!isParsed)
            {
                Console.WriteLine("INVALID VET ID. ID CONSIST OF NUMBERS ONLY");
                Console.Write(strEnterVetID);
                isParsed = int.TryParse(Console.ReadLine(), out iVetID);
            }

            Console.Write(strEnterDate);
            string strDateTime = Console.ReadLine();

            while (!Regex.IsMatch(strDateTime, "[0-3][0-9]/[0-1][0-9]/[0-9]{4}"))
            {
                Console.WriteLine("Invalid date");
                Console.Write(strEnterDate);
                strDateTime = Console.ReadLine();
            }


            var query = from vet in db.Vets where vet.StaffNo == iVetID select vet;
            if (query.Any())
            {
                string strVetName = query.FirstOrDefault().FirstName;
                string strVetSurname = query.FirstOrDefault().Surname;

                Console.WriteLine("\nAPPOINTMENTS FOR VET: " + strVetSurname + ", " + strVetName + ", DATE: " + strDateTime);
                Console.WriteLine("|{0,10}|{1,15}|{2,20}|", "Pet", "Owner", "Notes");
                Console.WriteLine("----------------------------------------------------");

                var query1 = from app in db.Visits where app.VetId.Equals(iVetID) select app;
                foreach (var item in query1)
                {
                    if (item.Date.ToString().Contains(strDateTime))
                    {
                        string strNotes = item.Notes;
                        var query2 = from pet in db.Pets where pet.Id == item.PetId select pet;
                        string strPetName = query2.FirstOrDefault().Name;
                        int iOwnerId = query2.FirstOrDefault().OwnerId;
                        var query3 = from owner in db.Owners where owner.Id == iOwnerId select owner;
                        string strOwnerName = query3.FirstOrDefault().Surname + ", " + query3.FirstOrDefault().FirstName;

                        Console.WriteLine("|{0,10}|{1,15}|{2,20}|", strPetName, strOwnerName, strNotes);

                    }
                }

                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("NO VETS WERE FOUND WITH THAT ID");
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
                    iTotalCost += treatment.Cost;
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
                //Console.WriteLine("{0,43}", "£" + iTotalCost.ToString());
            }

            Console.WriteLine(strLineBreak);

        }
    }
}
