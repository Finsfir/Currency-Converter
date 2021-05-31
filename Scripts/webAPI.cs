using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Currency_Converter.Scripts
{
    class webAPI
    {

        public delegate void getJsonData(Data a);

        //Событие OnCount c типом делегата MethodContainer.
        public event getJsonData JsonLoaded;

        private static string SOURCE_URL = "https://www.cbr-xml-daily.ru/daily_json.js";

        HttpWebRequest webRequest;

        public async void RequestAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SOURCE_URL);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
           
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dynamic data = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    Data res = new Data();
                    res.Date = data.Date;
                    res.PreviousDate = data.PreviousDate;
                    res.PreviousURL = data.PreviousURL;
                    res.Timestamp = data.Timestamp;
                    res.Valute = new List<Currency>();
                    foreach (dynamic a in data.Valute)
                    {
                        Currency cur = new Currency();
                        cur.CharCode = a.First.CharCode;
                        cur.Name = a.First.Name;
                        cur.Value = a.First.Value;
                        res.Valute.Add(cur);

                    }
                    JsonLoaded(res);
                }
            }
            response.Close();
        }
    }
}
