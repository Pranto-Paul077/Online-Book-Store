The Online Book Store is a full-stack web application designed to simulate a real-world e-commerce platform for purchasing books. The system is developed using ASP.NET Core MVC framework and follows a layered architecture to ensure maintainability and scalability.

The application provides two types of users: customers and administrators. Customers can register and log in securely, browse available books, view detailed information including images, add books to a shopping cart, and proceed to checkout to place orders. The system manages cart operations and calculates total pricing dynamically.

On the administrative side, the system includes a dedicated admin panel that allows authorized users to perform full CRUD (Create, Read, Update, Delete) operations on book records. Admins can add new books, update existing book details, delete books, and upload book images, which are stored and displayed through the web interface.

The backend is powered by Entity Framework Core, which handles database interactions using object-relational mapping (ORM). Microsoft SQL Server is used as the database management system to store user data, book information, orders, and cart details.

The project demonstrates key concepts such as authentication and authorization, MVC architecture, database integration, file handling (image upload), and basic e-commerce workflow. It is designed as a beginner-friendly yet practical project to build a strong foundation in backend web development using .NET technologies.
