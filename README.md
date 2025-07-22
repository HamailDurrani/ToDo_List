# 📝 To-Do List Application (ASP.NET MVC)

This is a basic web-based To-Do List application developed using **ASP.NET MVC** and **SQL Server**. The app allows users to manage their daily tasks efficiently with a clean interface.

---

## ✅ What This App Does

- Users can **add** new tasks with a title or description.
- Users can **view** all saved tasks in a list.
- Tasks can be **marked as complete** or **incomplete**.
- Tasks can be **edited** or **deleted**.
- The task data is stored in a **SQL Server** database using **Entity Framework**.

---

## 🔧 Technologies Used

- **ASP.NET MVC** – for building the web application structure (Model-View-Controller).
- **Entity Framework** – for communicating with the database (ORM).
- **SQL Server** – for storing task data (local or hosted).
- **Razor Views** – to design interactive web pages.
- **Bootstrap (optional)** – for responsive and clean UI.

---

## 📦 Features Summary

| Feature             | Description                          |
|---------------------|--------------------------------------|
| ➕ Add Task         | Create a new task with a title       |
| 🖊️ Edit Task        | Modify an existing task              |
| ✅ Complete Task    | Mark task as completed/incomplete    |
| 🗑️ Delete Task       | Remove tasks from the list           |
| 📋 Task List View/Filters   | View all pending and completed tasks

---

## 💾 Database

- The app uses a **SQL Server** database to store task data.
- It uses **Entity Framework Code First** to generate the database automatically.
- A sample model might look like:

```csharp
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
