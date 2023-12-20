<h1>Project Description</h1>

<p>A project I have worked on for an interview at GBET, then used as my master's thesis in University, and my first project that had unit tests. </p> 

<h1>Interview Project Requirements</h1>

<p>Using the OpenWeatherMap API at http://openweathermap.org/current, create a program that prompts for a city name and returns the current temperature for the city.</p>

Example Output
Where are you? Chicago IL
Chicago weather:
65 degrees Fahrenheit

Constraint
  - Keep the processing of the weather feed separate from the part of your program that displays the results.

Challenges
  - The API gives the sunrise and sunset times, as well as the humidity and a description of the weather. Display that data in a meaningful way.
  - The API gives the wind direction in degrees. Convert it to words such as “North,” “West,” “South,” “Southwest,” or even “South-southwest.”
  - Develop a scheme that lets the weather program tell you what kind of day it is. If it’s 70 degrees and clear skies, say that it’s a nice day out!
  - Display the temperature in both Celsius and Fahrenheit.
  - Based on the information, determine if the person needs a coat or an umbrella.
 
<p> Use .NET 5, C#, Visual Studio 2019 Community Edition </p>
<p> Can use either ConsoleApp or ASP.NET WebApi </p>

https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

<h1>Installation:</h1>

<h3> Download the solution from GitHub </h3>

<h3> Adding appsettings.json </h3>

<p>In the WeatherAPI folder, create a file called **appsettings.json** with the following text: </p>

<br>

    {
      "OpenWeatherAPIRequest": {
        "isEnabled": false,
        "weatherAPIUrl": "http://api.openweathermap.org/data/2.5/weather?q=",
        "options": "&units=imperial&appid=",
        "apiKey": ""
      }
    }

<h3> Optional - Fill in the blank api key with your OpenWeatherMap API Key </h3>

Create an account for the free version of [OpenWeatherMap](https://openweathermap.org/api) and fill in the blank API key with your own api key, so the field looks like the following:

    "isEnabled": true,
    [...]
    "apikey": "yourapikey123"

<h1> Project Runtime Explanation </h1>

In the appsettings.json, the values weatherAPIUrl, options and apiKey are combined together to form a link. An example link would be http://api.openweathermap.org/data/2.5/weather?q=&units=imperial&appid=apikey123 format, with "apikey123" being replaced by a proper api key. 
If the setting "isEnabled" is set to false, the app will use a default location and weather, and then process the settings based on that.

(More to be added)
