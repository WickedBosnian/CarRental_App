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
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

CarRental app allows users to reserve vehicles for clients.
CarRental app also allows for creating and manging clients, vehicles, vehicle types and vehicle manufacturers.


### Built With

CarRental consists of an application built in Visual Studio 2022 and a SQL database built with Microsoft SQL Server Management Studio 18 on a SQL server version 15.0.2000.5.

CarRental app was written in .NET 6.0 using C#. 


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
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```js
   const API_KEY = 'ENTER YOUR API';
   ```
   
CarRental_DB_SchemaAndData.sql script creates the database along with test data.</br>
CarRental_DB_Schema.sql script creates database without the data.</br>
CarRental_DB_Data.sql script inserts test data into the database.</br>

<p align="right">(<a href="#readme-top">back to top</a>)</p>
