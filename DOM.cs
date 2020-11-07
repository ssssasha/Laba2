using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Laba2
{
    public class DOM : IStrategy
    {
        XmlDocument document = new XmlDocument();
        public List<Films> AnalyzeFile(Films mySearch, string path)
        {
            document.Load(path);
            List<List<Films>> info = new List<List<Films>>();
            if (mySearch.city == null && mySearch.cinema == null && mySearch.movie == null && mySearch.date == null
                && mySearch.time == null && mySearch.price == null)
            {
                return ErrorCatch(document);
            }
            if (mySearch.city != null)
            {
                info.Add(SearchByAttribute("film", "CITY", mySearch.city, document));
            }
            if (mySearch.cinema != null)
            {
                info.Add(SearchByAttribute("film", "CINEMA", mySearch.cinema, document));
            }
            if (mySearch.movie != null)
            {
                info.Add(SearchByAttribute("film", "MOVIE", mySearch.movie, document));
            }
            if (mySearch.date != null)
            {
                info.Add(SearchByAttribute("film", "DATE", mySearch.date, document));
            }
            if (mySearch.time != null)
            {
                info.Add(SearchByAttribute("film", "TIME", mySearch.time, document));
            }
            if (mySearch.price != null)
            {
                info.Add(SearchByAttribute("film", "PRICE", mySearch.price, document));
            }
            return Cross(info, mySearch);
        }
        public List<Films> SearchByAttribute(string nodeName, string attribute, string myTemplate, XmlDocument document)
        {
            List<Films> find = new List<Films>();
            if(myTemplate != null)
            {
                XmlNodeList lst = document.SelectNodes("//" + nodeName + "[@" + attribute + "=\"" + myTemplate + "\"]");
                foreach(XmlNode e in lst)
                {
                    find.Add(Info(e));
                }
            }
            return find;
        }
        public List<Films> ErrorCatch(XmlDocument doc)
        {
            List<Films> result = new List<Films>();
            XmlNodeList lst = doc.SelectNodes("//" + "film");
            foreach(XmlNode elem in lst)
            {
                result.Add(Info(elem));
            }
            return result;
        }
        public Films Info(XmlNode node)
        {
            Films search = new Films();
            search.city = node.Attributes.GetNamedItem("CITY").Value;
            search.cinema = node.Attributes.GetNamedItem("CINEMA").Value;
            search.movie = node.Attributes.GetNamedItem("MOVIE").Value;
            search.date = node.Attributes.GetNamedItem("DATE").Value;
            search.time = node.Attributes.GetNamedItem("TIME").Value;
            search.price = node.Attributes.GetNamedItem("PRICE").Value;
            return search;
        }
        public List<Films> Cross(List<List<Films>> list, Films myTemplate)
        {
            List<Films> result = new List<Films>();
            List<Films> clear = CheckNodes(list, myTemplate);
            foreach (Films elem in clear)
            {
                bool isIn = false;
                foreach (Films s in result)
                {
                    if (s.Compare(elem))
                    {
                        isIn = true;
                    }
                }
                if (!isIn)
                {
                    result.Add(elem);
                }
            }
            return result;
        }
        public List<Films> CheckNodes(List<List<Films>> list, Films myTemplate)
        {
            List<Films> newResult = new List<Films>();
            foreach (List<Films> elem in list)
            {
                foreach (Films s in elem)
                {
                    if ((myTemplate.city == s.city || myTemplate.city == null) && (myTemplate.cinema == s.cinema || myTemplate.cinema == null)
                        && (myTemplate.movie == s.movie || myTemplate.movie == null) && (myTemplate.date == s.date || myTemplate.date == null)
                        && (myTemplate.time == s.time || myTemplate.time == null) && (myTemplate.price == s.price || myTemplate.price == null))
                    {
                        newResult.Add(s);
                    }
                }
            }
            return newResult;
        }
    }
}
