# Assignment1

For this assignment I was tasked with designing, building and deploying an enterprise-style console application to manage a veterinary practice. 

I first designed a normalised DB model and used the model-first approach in Entity Framework to generate the schema from the model.

The entity relationship diagram:

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/db601c56-0ee0-4a99-b372-829d8405d671)

I then had to develop a console application to be used as an admin system for the veterinary practice and used LINQ to query the data to provide the following functionality:

##
List the Names and contact details of all pet owners (customers), sorted by surname.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/d2bdfb5f-1257-4874-bb74-de8d6ed01d98)

##
List the pet name, type and breed of all pets registered with the practice.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/9f5f5994-bd64-423b-82e6-824a6e07dec0)

##
Given the registration number, display the name and address of the practice and list the names of all vets working in the practice.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/a55b5ca5-8013-4823-aa28-6aceeb7d2b09)

##
Given a PetId, list the name, type and breed of the pet followed (in chronological order) by the date and reason for all visits by that pet to the practice.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/61edca71-601c-4b9a-a965-047906894a9b)

##
Given a VetId and a specified date, for all pet appointments with that vet on that date, first list the vets name followed by the list of pets treated (i.e. the pet name) and also the pet owner’s name.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/ce20a244-5426-4d25-b39d-5a0ecf166d1f)

##
Given a PetNum calculate the cost of the most recent visit to the vet if the charge for an appointment is £40 and two or more medications (which are each an additional cost) have been given to the pet. The output should show the name of the pet and an itemised invoice.

![image](https://github.com/zita94/COM580_Assignment1/assets/56891175/b38b3480-f02c-41aa-b108-33c67ef9061a)
