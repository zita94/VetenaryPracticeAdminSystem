using System;
using System.Collections.Generic;
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
        private static string strEnterDate = "ENTER DATE (YYYY/MM/DD): ";
        private static string strEnterPetNum = "ENTER PET NUM: ";
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
            int petID;
            bool isParsed = int.TryParse(Console.ReadLine(), out petID);

            while (!isParsed)
            {
                Console.WriteLine("INVALID PET ID. ID CONSIST OF NUMBERS ONLY");
                Console.Write(strEnterPetID);
                isParsed = int.TryParse(Console.ReadLine(), out petID);
            }

            var query = from pet in db.Pets where (pet.Id == petID) select pet;

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

            while (!Regex.IsMatch(strDateTime, "[0-9]{4}/[0-9]{2}/[0-9]{2}"))
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

                var query1 = from app in db.Visits where app.Date.Equals(strDateTime) where app.VetId.Equals(iVetID) select app;
                string strNotes = query1.FirstOrDefault().Notes;


                Console.WriteLine("\nAPPOINTMENTS FOR VET: " + strVetSurname + ", " + strVetName + ", DATE: " + strDateTime);
                Console.WriteLine("|{0,10}|{1,15}|{2,20}|", "Pet", "Owner", "Notes");
                Console.WriteLine("----------------------------------------------------");

                foreach (var item in query1)
                {
                    var query2 = from pet in db.Pets
                                 where pet.Id == item.PetId
                                 select pet;
                    string strPetName = query2.FirstOrDefault().Name;
                    int iOwnerId = query2.FirstOrDefault().OwnerId;
                    var query3 = from owner in db.Owners where owner.Id == iOwnerId select owner;
                    string strOwnerName = query3.FirstOrDefault().Surname + ", " + query3.FirstOrDefault().FirstName;

                    Console.WriteLine("|{0,10}|{1,15}|{2,20}", strPetName, strOwnerName, strNotes);
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
            List<int> iTreatmentIDList = new List<int>();
            List<string> strTreatmentNameList = new List<string>();
            List<int> iCostList = new List<int>();
            Console.Write(strEnterPetNum);
            string strPetNum = Console.ReadLine();

            var query = from visit in db.Visits orderby visit.Date select visit;

            int iVisitID = query.FirstOrDefault().Id;
            int iPetID = query.FirstOrDefault().PetId;

            var query1 = from pet in db.Pets where pet.Id == iPetID select pet;
            string strPetName = query1.FirstOrDefault().Name;

            var query2 = from treatment in db.Treatments where treatment.VisitId == iVisitID select treatment;

            if (query2.Any())
            {
                foreach (var item in query2)
                {
                    iTreatmentIDList.Add(query2.FirstOrDefault().Id);
                    strTreatmentNameList.Add(query2.FirstOrDefault().Name);
                    iCostList.Add(query2.FirstOrDefault().Cost);
                }

                foreach (int treatmentID in iTreatmentIDList)
                {
                    var query3 = from medication in db.Medications where medication.TreatmentId == treatmentID select medication;
                    int iMedCost = query3.FirstOrDefault().Cost;

                    Console.WriteLine();
                }
            }

            Console.WriteLine(strLineBreak);

        }
    }
}
