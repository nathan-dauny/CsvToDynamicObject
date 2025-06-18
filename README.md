# 🧩 CsvToDynamicObject

**CsvToDynamicObject** is a C# library along with a demo app that allows you to load and parse **any CSV file without prior knowledge of its columns or types**, converting it into typed objects for easy manipulation in object-oriented programming.

---

## 🚀 Features

- 📂 Dynamically load CSV files with unknown schema  
- 🔍 Automatically detect column data types (int, double, bool, DateTime, string)  
- 🧱 Represent CSV rows as strongly-typed objects (`FinalObject`)  
- 🛠️ Simple and reusable API for integration in any .NET project  
- 📁 Sample  files available in the `/input` folder of the demo app  
- ⚙️ Easily extensible for other input formats or UI frontends (console, web, etc.)

---

## 🛠️ Tech Stack

- .NET 8 (C#)  
- CsvHelper library for robust CSV parsing  
- Modular architecture:  
  - `CsvToDynamicObjectLib` : core parsing library (class library)  
  - `CsvToDynamicObjectDemoApp` : console demo app referencing the core library  
- Designed for testability and dependency injection

---

## 📦 Packages Used

- 📂 CsvHelper — Robust CSV parsing library supporting dynamic and strongly-typed records  

---

## ⚙️ Getting Started

### Clone and build

```bash
git clone https://github.com/nathan-dauny/CsvToDynamicObject.git
cd CsvToDynamicObject
dotnet build
