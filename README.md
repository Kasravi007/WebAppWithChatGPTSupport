# WebAppWithChatGPTSupport

A full-stack AI-enabled web application built with **.NET 9 Web API** and **Angular 20**. This project integrates **OpenAI's ChatGPT** API to offer intelligent conversational capabilities, suitable for building modern chatbot interfaces or AI-powered services.

---

## 📦 Tech Stack

| Layer       | Technology               |
|-------------|--------------------------|
| Backend     | ASP.NET Core 9 (REST API)|
| Frontend    | Angular 20               |
| AI Service  | OpenAI GPT (ChatGPT API) |
| Data Format | JSON                     |
| Auth        | (Optional) JWT or OAuth2 |
| Hosting     | IIS / Azure App Service / Docker |
| Build Tool  | Vite / Angular CLI       |

---

## 📁 Project Structure

```bash
WebAppWithChatGPTSupport/
├── DotnetApi/               # .NET 9 Web API Project
│   ├── Controllers/         # API Endpoints (Chat, Auth, etc.)
│   ├── Services/            # ChatGPT Service integration
│   ├── Models/              # DTOs and Data Models
│   └── appsettings.json     # Configuration (DO NOT store secrets here)
│
├── AngularApp/              # Angular 20 Frontend App
│   ├── src/app/             # Angular modules & components
│   ├── environments/        # API URL config
│   └── index.html
│
└── README.md                # You're here
