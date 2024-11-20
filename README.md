# Travel Editor

## Overview
The Travel Editor is a desktop application designed to manage and organize travel-related information. It enables users to create, edit, and view trips, destinations, attractions, travelers, and reviews. Built using the WPF framework and the MVVM design pattern, the application ensures a clean and maintainable architecture while providing an intuitive user interface.

## Features
- Trips Management:

  - Create, view, edit, and delete trips.
  - Associate destinations, travelers, and attractions with a trip.
- Destinations Management:

  - Add and edit destinations with details like city, country, climate, and description.
  - View and manage attractions related to each destination.

- Attractions Management:

  - Add, edit, and remove attractions with details such as name, description, price, and location.

- Travelers Management:

  - Maintain a list of travelers associated with trips or accommodations.

- Reviews Management:

  - Add reviews for trips and manage traveler feedback.

 - Export and Import:

   - Export data to Excel with entities stored on separate sheets.
Import data from Excel files to populate the database.
Plugins Support: Extend functionality through plugins, such as custom export/import formats.

## Installation
Prerequisites: Visual Studio (2019 or later) with the following workloads:
   - .NET Desktop Development
   - Entity Framework Tools
   - SQL Server LocalDB: LocalDB is included with Visual Studio and will be used for the application's database.