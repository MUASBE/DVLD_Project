# DVLD System

> DVLD System is a desktop application for managing driving licenses, applying practical skills in C#, OOP, SQL Server, ADO.NET, and a 3-Tier Architecture design.

## ◈ 1. Project Overview

DVLD is a desktop system built to handle all driving license operations. It helps employees manage the entire process in one place — from registering people and booking test appointments to issuing licenses and keeping a full history for every driver.

## ◈ 2. Project Purpose

This project was built as part of a structured learning roadmap to apply real-world software development concepts, including layered architecture, database-driven workflows, and enterprise-style desktop application design.

## ◈ 3. Screenshots

### 🔹 Main Interface
<p align="center">
  <img src="Screens/Main-Interface.png" width="85%" alt="Main Interface">
</p>

### 🔹 Applications Menu (Expanded)
<p align="center">
  <img src="Screens/Applications-Menu-Expanded.png" alt="Applications Menu">
</p>

### 🔹 System Management
| ![Login](Screens/Login-Screen.png) | ![People](Screens/People-Screen.png) | ![Users](Screens/Users-Screen.png) |
|:---:|:---:|:---:|
| **Login Screen** | **People Management** | **Users Management** |
| ![Applications](Screens/Local-License-Applications.png) | ![Tests](Screens/Tests-and-Appointments.png) | ![History](Screens/License-History-Screen.png) |
| **Local Applications** | **Tests & Appointments** | **License History** |

<details>
  <summary><b>Click to view more screenshots (4+ more)</b></summary>
  <br>
  <p align="center">
    <img src="Screens/Detained-Licenses-Screens.png" width="32%" alt="Detain & Release" />
    <img src="Screens/New-Local-Driving-License.png" width="32%" alt="New License" />
    <img src="Screens/Replacement.png" width="32%" alt="Replacement" />
  </p>
   <p align="center">
    <img src="Screens/Menu-Expanded.png" alt="Expanded Menu Overview" />
  </p>
</details>

---

## ◈ 4. Key Features

* **People & Users Management:** Manage personal records, user accounts, and secure login settings.
* **Application Services**
  * Full support for Local and International license types.
  * Handle first-time issuance based on passing all required tests.
  * Simplified workflows for License Renewal and Replacements (Lost/Damaged).
  * Flexible configuration for application types and test fees.
* **Tests & Appointments**
  * End-to-end scheduling for Vision, Written, and Practical tests.
  * Real-time result tracking and status updates.
  * Dynamic re-take logic for failed attempts.
* **Driver Profiles:** Automatic tracking of driver identities with a detailed history of all issued, active, and expired licenses.
* **Detain & Release:** Manage detained licenses, record violations, and process release workflow after settling requirements. 

## ◈ 5. Technology Stack

- **Framework:** C# / .NET Framework 4.8
- **Database:** Microsoft SQL Server
- **Architecture:** 3-Tier Architecture (Data, Business, and Presentation Layers)
- **Data Access:** ADO.NET
- **UI:** Windows Forms (WinForms)

## ◈ 6. Technical Highlights
- **Modular Design:** Divided into logical modules (People, Users, Applications, etc.) for easier maintenance.
- **Logic Integration:** Implemented solid validation rules and multi-stage workflows to ensure data integrity across the system.
- **Git Best Practices:** Clean and traceable feature-based commit history.

## ◈ 7. Installation and Setup

1. **Database:** Restore `Database/DVLD-System-Database.bak` via SSMS.
2. **Run:** Open the `.sln` file and press **F5**.

> **Note:** If the connection fails, update `Data Source` in `App.config` to match your SQL Server name (e.g., `.` or `.\SQLEXPRESS`).

## ◈ 8. Skills & Learnings

For more details on skills gained during this project, see: [DVLD_SKILLS.md](DVLD_SKILLS.md)

> This project represents a major milestone in my transition from Civil Engineering to Software Development.
