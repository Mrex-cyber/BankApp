# BankApp

# **Currency API with Monobank Integration**

This project provides a simple API to retrieve currency exchange rates using Monobank's API. It includes two main endpoints: 
- one for fetching all currencies, 
- and another for fetching a specific currency by its code.

It uses `ASP.NET Core Minimal API` and includes Swagger for API documentation.

---

## **Features**
- Fetch live currency exchange rates from Monobank's API.
- Fallback to local file storage if Monobank API is unavailable.
- Automatic file updates to maintain a local cache of currency data.
- API documentation using Swagger.

---

## **Requirements**
- .NET 6 SDK or higher.
- Internet connection for fetching live currency data.

---

## **Setup and Installation**

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
