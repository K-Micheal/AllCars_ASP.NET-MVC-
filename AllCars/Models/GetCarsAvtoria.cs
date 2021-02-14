using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AllCars.Models
{
    public partial class GetCarsAvtoria
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int? PriceUSD { get; set; }
        public int? PriceUAH { get; set; }
        public int? PriceEUR { get; set; }
        public int? CarYear { get; set; }
        public int? YearForm { get; set; }
        public int? YearTo { get; set; }
        public string Odometer { get; set; } //------------- 
        public string GearBoxType { get; set; }//-------------
        public string Description { get; set; }
        public string Engine { get; set; }
        public string Img { get; set; }
        public string ImgSX { get; set; }
        public string ImgB { get; set; }
        public string ImgF { get; set; }
        public string LinkForCarPage { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string CarLocation { get; set; }

        //string mark, string model, string region, string category, int priceFrom, int priceto

        public List<GetCarsAvtoria> ListCarsAvtoria(Search searchparams)
        {
            List<GetCarsAvtoria> listCars = new List<GetCarsAvtoria>();

            HtmlWeb web = new HtmlWeb();
            string pathForListId = $"https://developers.ria.com/auto/search?api_key=TVjrh8VNnQKxVtqS4x4gen7trbUIh87gqyKaW1Ka&category_id={searchparams.Category}&marka_id%5B0%5D={searchparams.Mark}&model_id%5B0%5D={searchparams.Model}&s_yers%5B0%5D={searchparams.YearFrom}&po_yers%5B0%5D={searchparams.YearTo}&price_ot%5B0%5D={searchparams.PriceFrom}&price_do%5B0%5D={searchparams.PriceTo}&state%5B0%5D={searchparams.Region}&city%5B0%5D=0&countpage=100&engineVolumeFrom={searchparams.VoluemEngineFrom}&engineVolumeTo={searchparams.VoluemEngineTo}&gearbox%5B1%5D={searchparams.GearBox}";
            HtmlDocument document = web.Load(pathForListId);

            List<SearchResult> search_result = new List<SearchResult>();

            SearchResult search = new SearchResult();

            


            var searchresult = JsonConvert.DeserializeObject<Dictionary<string, SearchResult>>(document.Text);

            var result = JObject.Parse(document.Text);
            var items = result["result"].Children().ToList();




            List<SearchResult> listSearchResult = new List<SearchResult>();

            foreach (var subItem in items)
            {
                foreach (JToken result1 in subItem)
                {
                    SearchResult info = result1.ToObject<SearchResult>();
                    listSearchResult.Add(info);
                    break;
                }
                break;
            }

            foreach (SearchResult v in listSearchResult)
            {
                foreach (string s in v.ids)
                {
                    Console.WriteLine(s);
                }
            }


            //Получение информации о каждой машине-----------------------------------------------------------------------------------------------

            string jsonResult = "";
            List<string> listID = new List<string>();

            foreach (SearchResult v in listSearchResult)
            {
                foreach (string s in v.ids)
                {
                    listID.Add(s);
                }

            }


            foreach (string id in listID)
            {
                GetCarsAvtoria car = new GetCarsAvtoria();
                string path = "https://developers.ria.com/auto/info?api_key=TVjrh8VNnQKxVtqS4x4gen7trbUIh87gqyKaW1Ka&auto_id=" + id;



                try
                {
                    WebRequest request = WebRequest.Create(path);
                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                //Console.WriteLine(reader.ReadToEnd());
                                jsonResult = reader.ReadToEnd();
                            }
                        }
                    }
                }


                catch (WebException ex)
                {
                    // получаем статус исключения
                    WebExceptionStatus status = ex.Status;

                    if (status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                        Console.WriteLine("Статусный код ошибки: {0} - {1}",
                                (int)httpResponse.StatusCode, httpResponse.StatusCode);
                    }
                }

                //Досиаю AutoData из json ---------------------------------------------
                var Getresult = JObject.Parse(jsonResult);
                var Getitems = Getresult["autoData"];
                AutoData autoData = new AutoData();
                autoData = Getitems.ToObject<AutoData>();
                //Запись в конечный класс для передачи в View-----------------------------
                car.Odometer = autoData.race;
                car.CarYear = autoData.year;
                car.Engine = autoData.fuelName;
                car.GearBoxType = autoData.gearboxName;
                car.Description = autoData.description;
                car.Id = autoData.autoId;

                //get Car Price -------------------
                var GetUSD = Getresult["USD"];
                string price = GetUSD.ToString();
                car.PriceUSD = int.Parse(price);

                var GetUAH = Getresult["UAH"];
                price = GetUAH.ToString();
                car.PriceUAH = int.Parse(price);

                var GetEUR = Getresult["EUR"];
                price = GetEUR.ToString();
                car.PriceEUR = int.Parse(price);
                // get markName from json -----------------
                var GetMarkName = Getresult["markName"];
                car.MarkName = GetMarkName.ToString();
                // get modelName from json-----------------
                var GetModelName = Getresult["modelName"];
                car.ModelName = GetModelName.ToString();
                //get Link for looking CarPage-------------------
                var GetCarLink = Getresult["linkToView"];
                car.LinkForCarPage = "https://auto.ria.com/uk" + GetCarLink.ToString();
                //get location car -------------------
                var GetCarLocation = Getresult["locationCityName"];
                car.CarLocation = GetCarLocation.ToString();
                //get car photos ----------------------
                PhotoData photoData = new PhotoData();
                var GetCarphotos = Getresult["photoData"];
                photoData = GetCarphotos.ToObject<PhotoData>();
                car.Img = photoData.seoLinkM;
                car.ImgSX = photoData.seoLinkSX;
                car.ImgB = photoData.seoLinkB;
                car.ImgF = photoData.seoLinkF;
                // get phone number ------------------
                UserPhoneData phoneNumberData = new UserPhoneData();
                var GetPhoneNumber = Getresult["userPhoneData"];
                phoneNumberData = GetPhoneNumber.ToObject<UserPhoneData>();
                car.PhoneNumber = phoneNumberData.phone;
                // add to list wich sending to View
                listCars.Add(car);
            }
            return (listCars);
        }
    }
}
