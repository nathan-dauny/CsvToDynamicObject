# 🧩 CsvDynamicParser

**CsvDynamicParser** is a C# library along with a demo app that allows you to load and parse **any CSV file without prior knowledge of its columns or types**, converting it into typed objects for easy manipulation in object-oriented programming.

---

## 🚀 Features

- 📂 Dynamically load CSV files with unknown schema  
- 🔍 Automatically detect column data types (int, double, bool, DateTime, string)  
- 🧱 Represent CSV rows as strongly-typed objects (`TypedCsvRow`)  
- 🛠️ Simple and reusable API for integration in any .NET project  
- 📁 Sample CSV files available in the `/input` folder of the demo app  
- ⚙️ Easily extensible for other input formats or UI frontends (console, web, etc.)

---

## 🛠️ Tech Stack

- .NET 8 (C#)  
- CsvHelper library for robust CSV parsing  
- Modular architecture:  
  - `CsvLib` : core parsing library (class library)  
  - `CsvDemoApp` : console demo app referencing the core library  
- Designed for testability and dependency injection

---

## ⚙️ Getting Started

### Clone and build

```bash
git clone https://github.com/yourusername/CsvDynamicParser.git
cd CsvDynamicParser
dotnet build
