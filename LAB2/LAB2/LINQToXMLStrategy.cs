using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LAB2
{
    public class LINQToXMLStrategy : IStrategy
    {
        public List<Tournaments> Search(Tournaments tournament)
        {
            List<Tournaments> allResult = new List<Tournaments>();
            var doc = XDocument.Load(@"C:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.xml");
            var result = from obj in doc.Descendants("Tournament")
                         where
                         (
                         (obj.Attribute("Title").Value.Equals(tournament.Title) || tournament.Title.Equals(string.Empty)) &&
                         (obj.Attribute("Date").Value.Equals(tournament.Date) || tournament.Date.Equals(string.Empty)) &&
                         (tournament.PriceRange.Equals(string.Empty) || isPriceRange(obj.Attribute("PriceRange").Value, tournament.PriceRange)) &&
                         (obj.Attribute("Location").Value.Contains(tournament.Location) || tournament.Location.Equals(string.Empty)) &&
                         (obj.Attribute("Commentators").Value.Contains(tournament.Commentators) || tournament.Commentators.Equals(string.Empty)) &&
                         (obj.Attribute("Participants").Value.Contains(tournament.Participants) || tournament.Participants.Equals(string.Empty)) &&
                         (obj.Attribute("Type").Value.Equals(tournament.Type) || tournament.Type.Equals(string.Empty))
                         )
                         select new
                         {
                             title = (string)obj.Attribute("Title"),
                             date = (string)obj.Attribute("Date"),
                             priceRange = (string)obj.Attribute("PriceRange"),
                             location = (string)obj.Attribute("Location"),
                             commentators = (string)obj.Attribute("Commentators"),
                             participants = (string)obj.Attribute("Participants"),
                             type = (string)obj.Attribute("Type"),
                         };
            foreach (var n in result)
            {
                Tournaments myConcert = new Tournaments();
                myConcert.Title = n.title;
                myConcert.Date = n.date;
                myConcert.PriceRange = n.priceRange;
                myConcert.Location = n.location;
                myConcert.Commentators = n.commentators;
                myConcert.Participants = n.participants;
                myConcert.Type = n.type;

                allResult.Add(myConcert);
            }
            return allResult;
        }

        private bool isPriceRange(string value, string priceRange)
        {
            string[] _value = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] _priceRange = priceRange.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double.TryParse(_value[0], out double a1);
            double.TryParse(_value[2], out double a2);
            double.TryParse(_priceRange[0], out double b1);
            double.TryParse(_priceRange[2], out double b2);
            if ((a1 >= b1 || a2 <= b2) && b2 >= a1 && a2 >= b1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
