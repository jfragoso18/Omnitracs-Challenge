using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Omnitracs_APICSV
{

    /**
     *
     * @author : Jesus Fragoso 
     * @description: Class for Weather data. It has the attributes that are needed in the CSV
     * 
     */
    public class Weather
    {
        public double Temperature { get; set; }
        public string Units { get; set; }
        public bool Precipitation { get; set;  }


        /**
         * @name : RetrieveWeather
         * @params : void
         * @return : IRestResponse
         * @brief: Static method that handles the REST API call for the service
         * includes the URI, and query string
         * The API KEY and query strings are hard coded for the challenge purposes 
         * 
         */
        public static IRestResponse RetrieveWeather()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/");
            var request = new RestRequest("weather/", Method.GET);
            request.AddParameter("q", "Dallas");
            request.AddParameter("appid", "f0ce17382c9b48f3717c29cc73ecae0f");
            var response =  client.Execute(request);
            return response;
        }


        /**
       * @name : kelvinToCelsius
       * @params : double
       * @return : double
       * @brief: Static method that converts the temperature in Kelvin to Celsius as
       * defined on this service
       * 
       */
        public static double kelvinToCelsius ( double kelvin)
        {
            return kelvin - 273.15;
        }


        /**
       * @name : isRaining
       * @params : string
       * @return : bool
       * @brief: Static method that handles the Weather parameter. It has many options including
       * those that are related with raining. For the purposes of this challenge, there is an
       * array of objects that have the types of raining. The types of raining are being considered as precipitaion
       * 
       * For Drizzle, Rain, etc, the precipitation will always be true. 
       * 
       */
        public static bool isRaining ( string weather)
        {
            string [] rainingEnum = { "Drizzle", "Rain", "Thunderstorm", "Snow" };
            List<string> rainingTypes = new List<string>(rainingEnum);
            return rainingTypes.Contains(weather);
        }
    }

   
}
