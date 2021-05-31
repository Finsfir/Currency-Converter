using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency_Converter.Scripts
{

    /*

        {
   "Date": "2021-05-29T11:30:00+03:00",
   "PreviousDate": "2021-05-28T11:30:00+03:00",
   "PreviousURL": "\/\/www.cbr-xml-daily.ru\/archive\/2021\/05\/28\/daily_json.js",
   "Timestamp": "2021-05-28T16:00:00+03:00",
   "Valute": {
       "AUD": {
           "ID": "R01010",
           "NumCode": "036",
           "CharCode": "AUD",
           "Nominal": 1,
           "Name": "Австралийский доллар",
           "Value": 56.8828,
           "Previous": 56.9667
       },

        */
    public class Data
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        /*public class Valute {
            [JsonProperty("name")]
            
        }*/
        //public Currency[] Valute { get; set; }
        public List<Currency> Valute { get; set; }

    }

    public class Currency
    {
        public string ID { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Previous { get; set; }
    }
}
