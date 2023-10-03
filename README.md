# Vetenary Practice Admin System using Entity Framework and LINQ

For this assignment, I was tasked with designing, building and deploying an enterprise-style console application to manage a veterinary practice. 

I first designed a normalised DB model and used the model-first approach in Entity Framework to generate the schema from the model.

The entity relationship diagram:

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/d8931f73-d490-4fe7-badb-2a3271473183">

##
I then had to develop a console application to be used as an admin system for the veterinary practice and used LINQ to query the data to provide the following functionality:

##
List the Names and contact details of all pet owners (customers), sorted by surname.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/8a2b4e5c-f438-4202-ab2e-74d95526f78b">

##
List the pet name, type and breed of all pets registered with the practice.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/27b19fb6-2d50-44a4-b3cc-68f6a241d867">

##
Given the registration number, display the name and address of the practice and list the names of all vets working in the practice.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/866c2029-ce8c-466f-a3a3-1ff85e950d50">

##
Given a PetId, list the name, type and breed of the pet followed (in chronological order) by the date and reason for all visits by that pet to the practice.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/fdf8d3e9-3818-4986-ab26-d6e2937f97a0">

##
Given a VetId and a specified date, for all pet appointments with that vet on that date, first list the vet's name followed by the list of pets treated (i.e. the pet name) and also the pet owner’s name.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/90fec54d-8156-4225-881b-cd1405a73e99">

##
Given a PetNum calculate the cost of the most recent visit to the vet if the charge for an appointment is £40 and two or more medications (which are each an additional cost) have been given to the pet. The output should show the name of the pet and an itemised invoice.

<img src="https://github.com/zita94/VetenaryPracticeAdminSystem/assets/56891175/8cb69dec-2974-4eb4-8d86-acc9e8c25221">

##
Final Grade: 100  
COM580 Enterprise Computing
