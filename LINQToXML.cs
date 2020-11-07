using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba2
{
    public class LINQToXML: IStrategy
    {
        private List<Films> find = null;
        XDocument doc = new XDocument();
        public List<Films> AnalyzeFile(Films mySearch, string path)
        {
            doc = XDocument.Load(@path);
            find = new List<Films>();
            List<XElement> matches = (from val in doc.Descendants("film")
                                      where ((mySearch.city == null || mySearch.city == val.Attribute("CITY").Value) &&
                                      (mySearch.cinema == null || mySearch.cinema == val.Attribute("CINEMA").Value) &&
                                      (mySearch.movie == null || mySearch.movie == val.Attribute("MOVIE").Value) &&
                                      (mySearch.date == null || mySearch.date == val.Attribute("DATE").Value) &&
                                      (mySearch.time == null || mySearch.time == val.Attribute("TIME").Value) &&
                                      (mySearch.price == null || mySearch.price == val.Attribute("PRICE").Value))
                                      select val).ToList();
            foreach(XElement match in matches)
            {
                Films res = new Films();
                res.city = match.Attribute("CITY").Value;
                res.cinema = match.Attribute("CINEMA").Value;
                res.movie = match.Attribute("MOVIE").Value;
                res.date = match.Attribute("DATE").Value;
                res.time = match.Attribute("TIME").Value;
                res.price = match.Attribute("PRICE").Value;
                find.Add(res);
            }
            return find;
        }
    }
}
