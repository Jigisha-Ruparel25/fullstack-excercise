## Project Structure Description
backend - It contains the code developed in C# .Net Core 3.1.
    Controllers:
        MailController:
            It contains GET & POST Method. GET method returns the list of files (This is static as of now, for demo purpose).
            POST method returns true if mail sent successfully and false otherwise.

            SendEmailAsync: This method is used to send email via SMTP. To use email functionality, please set the fileds with "*** ***". Note: If you're using gmail, please allow less secure app. 
    Models:
        Request:
            All request models resides in this folder.
        Response:
            All response models resides in this folder.

        Other useful models are define here (root Models directory).


src - It contains the code developed in Angular 11 with Typescript.
    app/grid:
        It uses ag grid community package to demonstrate grid functionality.

        description:
            The grid contains list of files which is served from the backend via REST API. Upon share click, Modal popup appears which requires email parameters along with the file type checkbox. It will trigger mail on submitting the form. Based on the checkbox selected, attachements will be sent in mail.

            For example: If you select CSV, then all the CSV files displayed in grid will sent to the recepient.


## To Run the Project

Prerequisite: (Install the versions used to develop the code)

1. Node.js & Npm
2. Angular CLI
3. .Net Core 3.1 SDK
4. .NET Core 3.1 Hosting Bundle
5. IIS Server
6. Enable IIS hostable web core in Program Feature

Follow below steps to run project:

1. Run "npm install" in root directory.
2. In environment.ts file, change baseUrl to your dot net server.
3. Run "ng serve". Your application will start on localhost:4200 by default.
4. Preferred way to use the backend API is to host it in IIS via folder publish in dot net.

:)
