# Bid Calculation Tool

This project consists of two main parts: a backend API and a frontend application. The backend is built with .NET Core, and the frontend is developed using Vue.js.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)
- [Node.js](https://nodejs.org/) (version 14.0 or higher)
- npm (comes with Node.js)

## Running the Backend API

1. **Navigate to the Backend Project Directory**:
   ```bash
   cd path/to/BidCalculationTool/project

2. **Restore NuGet Packages**:
   dotnet restore

3. **Build the Project**:
   dotnet build

4. **Run the API**:
   dotnet run

The API should now be running on https://localhost:7206/api.

## Running the Frontend Application

1. **Navigate to the Frontend Project Directory**:
   ```bash
   cd path/to/BidCalculationTool/Web/bid-calculation-frontend

2. **Install Dependencies**:
   npm install

3. **Run the Development Server:**:
   npm run dev

## Configuring the API Base URL

If you need to change the base URL of the API that the frontend communicates with, follow these steps:

1. **Locate the config.ts file**:
   This file can be found in the frontend project directory, under src/.

2. **Update the API Base URL**:
   Open config.ts and modify the API_BASE_URL constant to reflect the desired API endpoint

3. **Save the File:**:
   After saving, the frontend will use the new API base URL for all requests.