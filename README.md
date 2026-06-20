# School_Management_System
SchoolManagementSystem/
│
├── App_Start/
│   ├── BundleConfig.cs
│   └── RouteConfig.cs         <-- Handles application routing paths
│
├── Controllers/
│   ├── AccountController.cs   <-- Login & Access management
│   ├── AdminController.cs     <-- Administrative setups (Classes, Allocations)
│   ├── TeacherController.cs   <-- Marks entry, Attendance grids
│   └── StudentController.cs   <-- Report cards, personalized read-views
│
├── Models/
│   ├── Database Context & EF Entities (Generated or Built Code-First)
│   └── ViewModels/            <-- Lightweight custom UI wrappers
│       ├── LoginViewModel.cs
│       ├── AttendanceVM.cs
│       └── ReportCardVM.cs
│
├── Views/
│   ├── Account/
│   ├── Admin/
│   ├── Teacher/
│   ├── Student/
│   └── Shared/
│       └── _Layout.cshtml     <-- Base Bootstrap structural layouts
│
└── Web.config                 <-- Stores active database connection string parameters
