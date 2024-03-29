---

# Simple Web Application Using ASP.NET CORE

<!--![Web Application Screenshot](screenshot.png)-->

## Overview
This web application allows users to input a comma-separated array of strings. The application performs operations on the input array according to the listed requirements and displays the output to the user. Additionally, it stores both the input and output in the database.

## Requirements
To divide the array of strings, follow these requirements:

1. **Divide the Array**: Divide the array into multiple arrays using a dash (`-`) as the separator.
    - Example: `["a", "b", "-", "c", "d", "e", "-", "f"]` would be divided into 3 arrays: `["a", "b"]`, `["c", "d", "e"]`, and `["f"]`.
2. **Select the Array**: Pick the array with the highest length. If multiple arrays have the same maximum length, select all of them.
3. **Select the Last Element**: From the selected array(s), pick the last element. This element will be the result.
4. **Handle Multiple Arrays with Same Max Length**: If there are multiple arrays with the same maximum length, select the last element of each of these arrays and return the string with the maximum number of characters.

## Usage
1. Enter a comma-separated array of strings into the input field.
2. Click the "Submit" button to perform the operation.
3. View the output displayed on the screen.
4. The input and output are stored in the database for future reference.
5. 
## Basic Calculator
In addition to array operations, this application also features a basic calculator. You can perform simple arithmetic operations using the calculator provided.

## Register
New users can create an account by navigating to the registration page and providing the required information, including username, email, and password. Upon successful registration, users gain access to additional features and personalized data.

To register:
1. Navigate to the registration page.
2. Fill out the registration form with your desired username, email, and password.
3. Click the "Register" or "Sign Up" button to create your account.
4. You can log in using your credentials and access the full functionality of the application.

## Login
To access features, users can log in using their credentials.

## Technologies Used
- ASP.NET Core
- C#
- HTML
- CSS
- JavaScript
- Entity Framework Core (for database interaction)

## Development
To run the application locally:
1. Clone this repository.
2. Navigate to the project directory.
3. Run `dotnet build` to build the project.
4. Run `dotnet run` to start the application.
5. Access the application in your web browser at `http://localhost:29457`.

---
