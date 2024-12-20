# FitTrack
Empowering you to achieve your fitness goals through intuitive tracking and personalized goals.

## Features
- **Workout Tracking**: Easily log your exercises and monitor your progress.
- **Goal Setting**: Set personalized fitness goals and track your achievements.

# Installation Instructions

To set up the Marvel API, install the appropriate versions of the necessary packages:
```
Install-Package Microsoft.EntityFrameworkCore -Version 8.0.10
Install-Package Pomelo.EntityFrameworkCore.MySql
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.10
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.10
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
```
These commands ensure that the API has the required dependencies for Entity Framework Core and MySQL integration.

After installing the necessary packages, run the following command to apply migrations and update the database schema:
```
Update-Database
```
This will apply migrations and ensure the database is up to date.