# 🏠 HomeFix — Home Services Booking Platform

> An ASP.NET Core MVC web application built as part of the ITI (Information Technology Institute) training program. HomeFix connects homeowners with trusted service providers for repairs, maintenance, and home improvement needs.

---

## 🔍 About the Project

**HomeFix** is a full-stack home services platform developed using the **ASP.NET Core MVC** framework. The application allows users to browse, book, and manage home maintenance and repair services, while providing an admin panel to manage service providers, categories, and orders.

This project was built as part of the **ITI MVC track**, applying real-world software engineering principles including the **Repository Pattern**, **Unit of Work**, **Identity-based Authentication**, and **N-Tier Architecture**.

---


## 🧰 Tech Stack

<p align="center">
  <img src="https://img.shields.io/badge/ASP.NET_Core_MVC-.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-Language-239120?style=flat-square&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-ORM-6B46C1?style=flat-square&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Identity-Authentication-black?style=flat-square" />
  <img src="https://img.shields.io/badge/Razor_Views-Frontend-7c3aed?style=flat-square" />
  <img src="https://img.shields.io/badge/Bootstrap_5-UI-7952B3?style=flat-square&logo=bootstrap&logoColor=white" />
  <img src="https://img.shields.io/badge/jQuery-JavaScript_Library-0769AD?style=flat-square&logo=jquery&logoColor=white" />
  <img src="https://img.shields.io/badge/N--Tier_Architecture-Scalable-e11d48?style=flat-square" />
  <img src="https://img.shields.io/badge/Repository_Pattern-Clean_Code-9333EA?style=flat-square" />
  <img src="https://img.shields.io/badge/Unit_Of_Work-Architecture-8b5cf6?style=flat-square" />
</p>

---

## ✨ Features

### 👤 Customer Side
- Register and log in with ASP.NET Core Identity
- Browse service categories and available providers
- Book a service with scheduling
- Track booking status
- Manage profile and order history

### 🛠️ Admin Panel
- Dashboard with system overview
- Manage service categories
- Manage service providers and their listings
- View and update order statuses
- Role-based access control (Admin / Customer)

---


## 🗂️ Project Structure

```
HomeFix.ITIMVC_Project/
│
├── Bulky.DataAccess/          # Data layer — DbContext, Repositories, Migrations
│   ├── Data/
│   ├── Repository/
│   └── Migrations/
│
├── Bulky.Models/              # Domain models and ViewModels
│   ├── Models/
│   └── ViewModels/
│
├── Bulky.Utility/             # Constants, email sender, helper classes
│
├── BulkyWeb/                  # Main MVC web application
│   ├── Areas/
│   │   ├── Admin/             # Admin area controllers & views
│   │   └── Customer/          # Customer area controllers & views
│   ├── wwwroot/               # Static files (CSS, JS, images)
│   └── Program.cs
│
└── BulkyWebRazor_temp/        # Razor Pages prototype (temporary/reference)
```

## 🎯 Usage

- **Home page** — Browse service categories
- **Register/Login** — Create an account or sign in
- **Book a service** — Choose a category, select a provider, and schedule
- **Admin panel** — Navigate to `/Admin` (requires admin role)

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## 👥 Team

| Name | GitHub |
|---|---|
| Mustafa Saad | [@mustafas3aad](https://github.com/mustafas3aad) |
| Mahmoud Ali | [@mahmoudali2429](https://github.com/mahmoudali2429) |
| Menna Mohamed | [@Menna-mohamed-18](https://github.com/Menna-mohamed-18) |
| Sohaila wael | [@sohailawael1](https://github.com/sohailawael1) |
| Nour Khataan | [@Nourkhataan](https://github.com/Nourkhataan) |

---


> *"Fix your home, fix your life."* 🔧


---

<p align="center">
  <img 
    src="https://github.com/user-attachments/assets/fc6ab60f-47fa-4a1e-a713-8351e6545640"
    width="850"
    alt="Project Screenshot"
  />
</p>

