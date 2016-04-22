using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DostavItClient
{
    public static class DostavItService
    {
        public static async Task<IEnumerable<CarrierInfo>> GetCarriersAsync()
        {
            string responseContent = await GetRequestAsync("http://dostav.it/api/carriers", "application/json");
            return JsonConvert.DeserializeObject<IEnumerable<CarrierInfo>>(responseContent);
        }

        public static async Task<CarrierInfo> GetCarrierAsync(string carrierName)
        {
            string responseContent = await GetRequestAsync(string.Format("http://dostav.it/api/carriers/{0}", carrierName), "application/json");
            return JsonConvert.DeserializeObject<CarrierInfo>(responseContent);
        }

        public static async Task<IEnumerable<Rate>> GetRatesAsync(string fromCity, string toCity, decimal lengthCm, decimal widthCm, decimal heightCm, decimal weightKg, decimal costRub, DateTime? shipDate = null)
        {
            return await GetRatesAsync(string.Empty, fromCity, toCity, lengthCm, widthCm, heightCm, weightKg, costRub, shipDate);
        }

        public static async Task<IEnumerable<Rate>> GetRatesAsync(string carrier, string fromCity, string toCity, decimal lengthCm, decimal widthCm, decimal heightCm, decimal weightKg, decimal costRub, DateTime? shipDate = null)
        {
            string uri = string.Format("http://dostav.it/api/rates/{0}", carrier);
            var request = new
            {
                Parcel = new
                {
                    Cost = new { Amount = costRub, Currency = "RUB" },
                    Length = new { Value = lengthCm, Units = "Cm" },
                    Width = new { Value = widthCm, Units = "Cm" },
                    Height = new { Value = heightCm, Units = "Cm" },
                    Weight = new { Value = weightKg, Units = "Kg" },
                },
                From = new { Country = "RU", City = fromCity },
                To = new { Country = "RU", City = toCity },
                ShipDate = shipDate,
            };
            string requestContent = JsonConvert.SerializeObject(request);
            string responseContent = await PostRequestAsync(uri, "application/json", requestContent);
            return JsonConvert.DeserializeObject<IEnumerable<Rate>>(responseContent);
        }

        private static async Task<string> GetRequestAsync(string uri, string contentType)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = "GET";
            request.ContentType = contentType;
            WebResponse response = await request.GetResponseAsync();
            using (StreamReader contentReader = new StreamReader(response.GetResponseStream()))
            {
                return await contentReader.ReadToEndAsync();
            }
        }

        private static async Task<string> PostRequestAsync(string uri, string contentType, string content)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = contentType;
            using (StreamWriter contentWriter = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await contentWriter.WriteAsync(content);
            }
            WebResponse response = await request.GetResponseAsync();
            using (StreamReader contentReader = new StreamReader(response.GetResponseStream()))
            {
                return await contentReader.ReadToEndAsync();
            }
        }
    }
}
