# CarRental_App
Web app for renting cars and managing clients.

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li>
      <a href="#project-structure">Project Structure</a>
      <ul>
        <li><a href="#domain-project">Domain Project</a></li>
        <li><a href="#application-project">Application Project</a></li>
        <li><a href="#infrastructure-project">Infrastructure Project</a></li>
        <li><a href="#dto-project">DTO Project</a></li>
        <li><a href="#ui-project">UI Project</a></li>
      </ul>
    </li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

CarRental app allows users to reserve vehicles for clients.
CarRental app also allows for creating and manging clients, vehicles, vehicle types and vehicle manufacturers.

User documentation is included in the git files and can be regarded as sort of a manual for using the web app.

### Built With

CarRental consists of an application built in Visual Studio 2022 and a SQL database built with Microsoft SQL Server Management Studio 18 on a SQL Server 2019 version 15.0.2000.5.</br>
</br>
CarRental app was written in .NET 6.0 using C#. 
Frontend was created using MVC project.
Views were written using HTML and CSS.
Bootstrap was used as well.

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

To set up the Visual Studio project use Visual Studio version 2022 with latest updates as of 25.09.2022. Earlier versions of Visual Studio haven't been tested.</br>
</br>
To set up the database use SQL Server version 15.0.2000.5.</br>
It is recommended to use Microsoft SQL Server Management Studio 18.</br>


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/WickedBosnian/CarRental_App.git
   ```
<!-- PROJECT STRUCTURE -->
### Project structure

Clean architecture was used for solution structure.</br>
![image](https://user-images.githubusercontent.com/105022465/192170492-5f651669-5a1f-4374-93be-0202fec34268.png)

</br>
Unlike the standard MVC project where .NET Framework is used for communicating with the database nd controllers do most of the backend logic, in clean architecture a project is divided into several components that seperate frontend from backend in a more meaningfull way.</br>
</br>

For this project I have decided to seperate my solution into 5 different projects as seen in the above picture.</br>
</br>
### Domain Project
The Domain project contains the enterprise logic and entities. In my case there isn't any enterprise logic since the project is small but it contains entities.</br>
![image](https://user-images.githubusercontent.com/105022465/192170847-f100448d-0d7a-4d7f-b0f4-5a9ef8689659.png)
</br></br>
### Application Project
Application project contains business logic. Repository containing interfaces for business logic can be seen in the picture below </br>
![image](https://user-images.githubusercontent.com/105022465/192171058-fa2fe597-2469-4c67-854b-9a4426aefcd2.png)
</br></br>
### Infrastructure Project
The Infrastructure project will implement interfaces from the Application project to provide functionality to access external systems. It also contains connection to data source, it could be a databse or some other external or internal(mock data) data source. All of the reading and writing data into the databas is done in this project. SQL Stored Procedures and SQL Scalar Functions have been used for communicating with the database.</br>
![image](https://user-images.githubusercontent.com/105022465/192171208-c1f4c06c-68dc-4fe1-b7f0-f90a48506daa.png)
</br></br>
### DTO Project
DTO project contains entity objects that are used to accomodate data from the data source. Domain contains pure entities used for the enterprise and DTO objects are used for business logic and in my case as View Model for the MVC UI project because there wouldn't be much difference between View Models and DTO objects.</br>
![image](https://user-images.githubusercontent.com/105022465/192171348-8cf1dd90-b7c6-4c79-b9e6-ffe911c8df98.png)
</br></br>
### UI Project
User Interface project is the final part in clean architecture design pattern. I have used MVC .NET 6.0 project for my solution and I have suplemented View Models with DTO objects. UI project uses dependency injection to access Infrastructure layer and to do business logic, to read and write data into the database. HTML Razor pages and CSS with boostrap have been used for frontend. Controllers call methods trough interfaces to get and write data.</br>
![image](https://user-images.githubusercontent.com/105022465/192171450-f12225e5-d107-4de9-80e7-d95590bd9154.png)
</br></br>


