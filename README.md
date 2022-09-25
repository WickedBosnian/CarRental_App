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
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

CarRental app allows users to reserve vehicles for clients.
CarRental app also allows for creating and manging clients, vehicles, vehicle types and vehicle manufacturers.


### Built With

CarRental consists of an application built in Visual Studio 2022 and a SQL database built with Microsoft SQL Server Management Studio 18 on a SQL server version 15.0.2000.5.</br>
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
Repository is public so there is no need for any authentication.

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
The Domain project contains the enterprise logic and entities. In my case there is not any enterprise logic since the project is small.</br>
![image](https://user-images.githubusercontent.com/105022465/192170847-f100448d-0d7a-4d7f-b0f4-5a9ef8689659.png)
</br></br>


