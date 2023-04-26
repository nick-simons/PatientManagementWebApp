## Local Setup Instructions

Starting the APIs
* Open Visual Studio and open the ./PatientManagementWebApp/PatientManagementSystem/PatientManagementSystem.csproj file to load the project
* Once open, simply press play to start the service, it should be hosted on port 7074
* Navigate to https://localhost:7074/swagger/index.html to ensure it has started
* The service is configured to communicate with the SQL server running in a docker container hosted locally. Complete the instructions for setting up the docker container prior to executing the APIs in the patient management service.

Starting the SQL Server Docker Container 
* Ensure that you have docker installed 
* Open a new cmd line window and navigate to the following directory: ./PatientManagementWebApp/PatientManagementSystem/DockerConfig
* Run the following command to start the docker container that hosts the patient records SQL database: docker compose up --build
* The database is initally loaded with 4 patient records

Starting the angular web application
* Open up a new cmd line window
* Navigate to the following directory: ./PatientManagementWebApp/angular-patient-manager
* Run the following command initially: npm install --force
* Run the following command to start the web application: ng serve
* Once started you can navigate to the web application at: http://localhost:4200/#

Using the application
* After doing all of the setup locally for the application outlined above, you can access the web application at http://localhost:4200/#
* On the page you can upload a csv file of patient records and review the table of ALL patient records in the db
* When uploading the patient records csv, if you leave the 'id' column of the csv blank that will indicate to the system to create a new patient record. Including an 'id' in the csv will indicate to the system to update a patient record.
* There is client side validation for the csv file that will help call out errors in the csv file to upload.
* The Patient Records Table is paginated, includes all data columns from the db, and all columns can be sorted 
