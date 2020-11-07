using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Laba2
{
    public class SAX : IStrategy
    {
        private List<Films> lastResult = null;
        public List<Films> AnalyzeFile(Films mySearch, string path)
        {
            XmlReader reader = XmlReader.Create(path);
            List<Films> result = new List<Films>();
            Films find = null;
            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    if(reader.Name == "film")
                    {
                        find = new Films();
                        while (reader.MoveToNextAttribute())
                        {
                            if(reader.Name == "CITY")
                            {
                                find.city = reader.Value;
                            }
                            if(reader.Name == "CINEMA")
                            {
                                find.cinema = reader.Value;
                            }
                            if(reader.Name == "MOVIE")
                            {
                                find.movie = reader.Value;
                            }
                            if (reader.Name == "DATE")
                            {
                                find.date = reader.Value;
                            }
                            if (reader.Name == "TIME")
                            {
                                find.time = reader.Value;
                            }
                            if (reader.Name == "PRICE")
                            {
                                find.price = reader.Value;
                            }
                        }
                        result.Add(find);
                    }
                }
            }
            lastResult = Filter(result, mySearch);
            return lastResult;
        }
        private List<Films> Filter(List<Films> allRes, Films myTemplate)
        {
            List<Films> newResult = new List<Films>();
            if (allRes != null)
            {
                foreach(Films i in allRes)
                {
                    if (( myTemplate.city == i.city || myTemplate.city == null) && (myTemplate.cinema == i.cinema || myTemplate.cinema == null)
                        && (myTemplate.movie == i.movie || myTemplate.movie == null) && (myTemplate.date == i.date || myTemplate.date == null)
                        && (myTemplate.time == i.time || myTemplate.time == null) && (myTemplate.price == i.price || myTemplate.price == null))
                    {
                        newResult.Add(i);
                    }
                }
            }
            return newResult;
        }
    }
}
