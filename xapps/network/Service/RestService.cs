﻿#define USE_PARSE_XML

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace xapps
{
    public class RestService : INetworkManager
    {
        HttpClient client;
        public MovieListData items = new MovieListData();


        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<MovieListData> requestMovieList() {
                 
#if USE_PARSE_XML
            string url = MovieList.localeListRestUrl + MovieList.responseType_xml + MovieList.key + MovieList.targetDt;
#else
            string url = MovieList.localeListRestUrl + MovieList.responseType_json + MovieList.key + MovieList.targetDt;
#endif

            Debug.WriteLine(url);
            var uri = new Uri(string.Format(url, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    
                    var content = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("==========================================================");
                    Debug.WriteLine(content);
                    Debug.WriteLine("==========================================================");

#if USE_PARSE_XML
                    MovieListParser parser = new MovieListParser();
                    items = parser.parseXml(content);
#else
                    JObject obj = JObject.Parse(content);

                    List<MovieListItem> model = obj["dailyBoxOfficeList"].ToObject<List<MovieListItem>>();

                    items = JsonConvert.DeserializeObject<boxOfficeResult>(content);
                    Debug.WriteLine("items = " + items);
#endif
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items; 
        }

        public async Task<MovieDetailData> requestMovieDetail(string movieCd)
        {
#if USE_PARSE_XML
            string url = "";
            url = MovieDetail.localeListRestUrl + MovieDetail.responseType_xml + MovieDetail.key + MovieDetail.movieCd + movieCd;
#else
            string url = "";
            url = MovieDetail.localeListRestUrl + MovieDetail.responseType_json + MovieDetail.key + MovieDetail.movieCd + movieCd;
#endif

            Debug.WriteLine(url);
            var uri = new Uri(string.Format(url, string.Empty));
            MovieDetailData items = new MovieDetailData();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine("==========================================================");
                    Debug.WriteLine(content);
                    Debug.WriteLine("==========================================================");
#if USE_PARSE_XML
                    MovieDetailParser parser = new MovieDetailParser();
                    items = parser.parseXml(content);
#else
                    items = JsonConvert.DeserializeObject<MovieDetailItem>(content);
#endif
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items; 
        }
    }
}
