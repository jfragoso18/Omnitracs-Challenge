
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Omnitracs_APICSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Omnitracs
{
    public partial class OmniService : ServiceBase
    {
        private int eventId = 1; 
        public OmniService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog(); 
            if (!System.Diagnostics.EventLog.SourceExists("Omnitracs Service"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "Omnitracs Service", "Weather Service"); 
            }
            eventLog1.Source = "Omnitracs Service ";
            eventLog1.Log = "Weather Service"; 
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Omnitracs Weather monitoring ...");
            Timer timer = new Timer();
            timer.Interval = 60000 * 5; // 60 seconds by 5, 5 minutes timer 
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start(); 

        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service Stopped"); 
        }
        public void OnTimer( object sender, ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("Weather API Call @OpenWeather", EventLogEntryType.Information, eventId++);

            /*
             * 
             * Ask for weather 
             * */
            var response = Weather.RetrieveWeather();
     

            /** Json deserialization for grabbing parameters*/
            var WeatherJson = JsonConvert.DeserializeObject<WeatherJson>(response.Content);
         

            /*
             * Preparing information for CSV 
             */
            var precipitation = Weather.isRaining(WeatherJson.Weather[0]["main"]);
            var temperature = Weather.kelvinToCelsius(Convert.ToDouble(WeatherJson.Main["temp"]));
            var units = "C";

            // File to write 
            //  string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string path = "C:\\";
            path += "Weather.csv";

            /**
             * Create the new record to write 
             */
            var records = new List<Weather>();
            records.Add(item: new Weather { Temperature = temperature, Units = units, Precipitation = precipitation });


            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = !File.Exists(path)
            };

      

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {

                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(records);

                        }
                    }
                }
            }
            catch (IOException)
            {

            }


        }


    }
}
