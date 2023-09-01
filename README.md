# 🚀 Issue Tracking System in C# 🛠️

**A comprehensive issue tracking system built in C# with Windows Forms.**

## Description

Welcome to the Issue Tracking System! This application is designed to simplify issue management and enhance team collaboration.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)
- [Author](#author)
- [Footer](#footer)

## Features 🌟



Add New Issue
Users can add new customer issues, providing details like client information, description, assigned technician, and estimated time.

Track Live Tickets
The application enables users to track live tickets, manage their details, and associate them with a technician.

Close Tickets
Users can mark tickets as closed when the issue is resolved. The closed tickets are then moved to the closed tickets list.

Export to Excel
The application offers the functionality to export closed tickets to an Excel spreadsheet for reporting purposes.



## Getting Started
1. **Clone the Repository**: Clone this repository to your local machine using Git or download the ZIP file.

    ```shell
    git clone https://github.com/your-username/Issue_Tracking_System
    ```

    Or download the ZIP file and extract it.

2. **Open Project**: Open the project in Visual Studio by navigating to the solution file (`*.sln`).

3. **Database Setup**: Modify the database connection string in the code to match your database server. Find the following line of code in the `Main` class within your project:

    ```csharp
    SqlConnection con = new SqlConnection(@"Place_Your_Database_Connection_String_Here");
    ```

    Replace `Place_Your_Database_Connection_String_Here` with your actual database connection string.

4. **Build and Run the Project**: Build the project and run it in Visual Studio to test the functionality.

## Troubleshooting
If you encounter any issues while setting up or running the project, consider the following steps:

- **Verify Visual Studio Installation**: Make sure Visual Studio is installed with the C# development tools.

- **Database Connection**: Double-check your database connection string. It should contain the necessary details to connect to your database server.

- **Build Errors**: If you encounter build errors, review the error messages in Visual Studio for clues on what went wrong.



## Prerequisites 🛠️
- Visual Studio (2017 or later)
- .NET Framework

## Usage 📖
1. **Launch Application**: Start the application and log in using your credentials.

2. **Add Customer Details**: Navigate to the "Customers" section and input customer details.

3. **Create Live Tickets**: In the "Live Tickets" section, generate live tickets.

4. **Close Live Tickets**: Once resolved, close live tickets and relocate them to the "Closed Tickets" section.

5. **Export to Excel**: Utilize the "Export to Excel" button to export closed tickets to an Excel format.


## User Authentication
The user authentication code manages the process of validating user credentials before granting access to the Issue Tracking System. This ensures that only authorized users are allowed to use the system.
![Screenshot 2023-08-28 200021]

```csharp
// Establish a connection to the SQL Server
SqlConnection sqlcon = new SqlConnection(@"Data Source=BLINDSPACE\SQLEXPRESS;Initial Catalog=TrackingSystem;Integrated Security=True");

// Create an SQL query to verify user credentials
string query = "select * from users where username = '" + cbUsername.Text.Trim() + "' and password = '" + tbPassword.Text.Trim() + "'";

// Create a SqlDataAdapter to execute the query
SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
DataTable dt = new DataTable();
sda.Fill(dt);

// Check if there's a single matching user
if (dt.Rows.Count == 1)
{
    // Authentication successful
    MessageBox.Show("Greetings! Welcome to the Issue Tracking System....");

    // Open the main application form
    Main ObjMain = new Main();
    this.Hide();
    ObjMain.Show();
}
else
{
    // Authentication failed
    MessageBox.Show("Incorrect username or password!");
}


```
# Contributing 🤝
Contributions are highly welcome! If you come across any issues or have ideas for improving the application, please don't hesitate to open an issue or submit a pull request.

# License 📄
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for more details.

# Author 🙋‍♂️
- Name: Usama Hussain
- Contact: usamahussaindev@gmail.com

# Footer ⬆️
[Back to Top](#project-name-readme)




