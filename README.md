**Notes:**
1. Unit Testing using xUnit
2. The exception at the API level is handled by custom middleware in the **ErrorMiddleware.cs** file where Exception errors will be translated into different types of HTTP response errors.
3. Based on the requirements, since the .NET project uses .NET 3.1, some packages may be shown as vulnerable or deprecated.
4. The API data for countries and cities uses mocked data at the service level

**Service and API Projects**

1. Clone the repository
2. Open the project solution located in **OpenWeathers\OpenWeather.Core** folder using Visual Studio
3. Target Project is Net 3.1
4. Build the solution, make sure all dependencies are downloaded and there are no errors.
5. There are two unit test projects: **OpenWeathers.Core.Test** and **OpenWeathers.Api.Tests.** To execute them, right-click on each project and select 'Run Tests'.

![image](https://github.com/user-attachments/assets/33a16b6e-3f65-46c2-8403-1dd12d1d66fc)


   
6. Run API Project **OpenWeathers.Api**

![image](https://github.com/user-attachments/assets/a241e62f-28ea-4bc6-9ea7-e594393c2553)


**FE ReactJs**
1. Install NodeJs and NPM
2. Navigate to the **OpenWeathers\FE\weather-app** folder 
3. Change the value of the **baseUrl** variable in the **WeatherComponent.js** file to match the base URL of the API that is currently running.
   
![image](https://github.com/user-attachments/assets/197bacc1-90c9-47bc-8729-c4757ccda165)

   
5. Open terminal that folder and type **npm start**. App will running in browser
![image](https://github.com/user-attachments/assets/5048d375-1234-4d85-ba44-9e01823d37bb)
