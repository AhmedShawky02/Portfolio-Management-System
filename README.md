

# Portfolio Management System (Web API Project)

### **Overview**
A robust Web API built using ASP.NET Core for managing user investment portfolios, stock details, and comments. The system incorporates secure user authentication and role-based access control.

---

### **Key Features**
- **Authentication & Authorization:** Secure user management with ASP.NET Identity and JWT-based authentication.
- **Portfolio Management:** Add, view, and manage stocks in personalized user portfolios.
- **Stock Management:** CRUD operations for stock data, including company details and market insights.
- **Commenting System:** Users can leave notes on stocks.
- **API Documentation:** Comprehensive documentation with Swagger.

---

### **Technologies**
- **Backend:** ASP.NET Core Web API, Entity Framework Core  
- **Authentication:** ASP.NET Identity, JWT  
- **Database:** SQL Server  
- **Serialization:** Newtonsoft.Json  
- **Documentation:** Swagger/OpenAPI  

---

### **Challenges Addressed**
- Resolved JSON serialization reference loops.
- Designed efficient many-to-many database relationships using composite keys.

---

### **Endpoints Overview**

#### **Stock Management**
- `GET: api/Stock` – List all stocks with filtering and pagination. *(Admin)*  
- `GET: api/Stock/{id}` – Fetch stock details by ID. *(Admin)*  
- `POST: api/Stock` – Add a new stock. *(Admin)*  
- `PUT: api/Stock/{id}` – Update stock details. *(Admin)*  
- `DELETE: api/Stock/{id}` – Remove a stock by ID. *(Admin)*  

#### **Comment Management**
- `GET: api/Comment` – List all comments. *(Admin)*  
- `GET: api/Comment/{id}` – Fetch comment details by ID. *(Admin)*  
- `POST: api/Comment/{StockId}` – Add a comment to a stock. *(Admin)*  
- `PUT: api/Comment/{id}` – Update a comment. *(Admin)*  
- `DELETE: api/Comment/{id}` – Delete a comment. *(Admin)*  

#### **Account Management**
- `POST: api/Account/login` – Authenticate user and issue JWT token.  
- `POST: api/Account/register` – Register a new user with default "User" role.  

#### **Portfolio Management**
- `GET: api/Portfolio` – Retrieve the authenticated user's portfolio.  
- `POST: api/Portfolio` – Add a stock to the user's portfolio.  
- `DELETE: api/Portfolio` – Remove a stock from the user's portfolio.  

---

### **Security & Validation**
- **Authentication:** JWT-based token authentication.  
- **Authorization:** Role-based access control (e.g., Admin-only endpoints).  
- **Validation:** Input validation to ensure data integrity and prevent duplication.  
- **Error Handling:** Clear and consistent error messages for invalid operations.  

---

### **Outcome**
Delivered a scalable and secure Web API for managing user portfolios, emphasizing performance, maintainability, and user experience.

---
