# JobBoardAPI
## Description
A WEB REST API imitating a job board platform, in which users can create job advertisements and apply for them.

## Features
- **CRUD controllers:** API allows you to send different requests to create, display, delete or update job advertisements, as well as creating job applications for them.
- **Database**: Application uses EntityFramework to work with an MS SQL database that stores all of the portal data and seeds some starting values.
- **Error logging:** NLog library is utilized for logging all of the errors occuring within the API.
- **Custom middleware:** API has implemented middleware to catch custom exceptions occuring or to log information about requests that took too much time to complete.
- **User authentication:** API uses JSON Web Tokens for the authentication of the users that guarantees secure access to the application.
- **Authorization policy:** Implementation of custom authorization policy ensures that no user can modify data not belonging to him.
- **Pagination:** Data returned to the user is paginated, making the result he receives more transparent.
- **Data validation:** Data within the API is validated using FluentValidation, which ensures accuracy and integrity of the processed information.
- **Documentation:** API uses Swagger for providing detailed project documentation.
- **Static files:** API allows to download static files from the server, as well as to upload them to a server by authorized users.
- **Azure cloud:** Application is deployed to the Azure cloud and can be found under that link: https://job-board-api.azurewebsites.net/swagger/index.html
