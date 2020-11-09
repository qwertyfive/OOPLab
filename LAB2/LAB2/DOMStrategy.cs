using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LAB2
{
    public class DOMStrategy : IStrategy
    {
        public List<Tournaments> Search(Tournaments tournament)
        {
            List<Tournaments> result = new List<Tournaments>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.xml");

            XmlNode node = doc.DocumentElement;

            string xpath = "/Tournaments/Tournament[";
            int count = 0;

            if (tournament.Title != "")
            {
                xpath += "@Title=\"" + tournament.Title + "\"";
                count++;
            }

            if (tournament.Date != "")
            {
                if (count == 0)
                {
                    xpath += "@Date=\"" + tournament.Date + "\"";
                    count++;
                }
                else
                {
                    xpath += " and @Date=\"" + tournament.Date + "\"";
                }
            }


            if (tournament.PriceRange != "")
            {
                if (count == 0)
                {
                    xpath += "@PriceRange=\"" + tournament.PriceRange + "\"";
                    count++;
                }
                else
                {
                    xpath += " and @PriceRange=\"" + tournament.PriceRange + "\"";
                }
            }

            if (tournament.Location != "")
            {
                if (count == 0)
                {
                    xpath += "contains(@Location, \"" + tournament.Location + "\")";
                    count++;
                }
                else
                {
                    xpath += " and contains(@Location, \"" + tournament.Location + "\")";
                }
            }

            if (tournament.Commentators != "")
            {
                if (count == 0)
                {
                    xpath += "contains(@Commentators, \"" + tournament.Commentators + "\")";
                    count++;
                }
                else
                {
                    xpath += " and contains(@Commentators, \"" + tournament.Commentators + "\")";
                }
            }

            if (tournament.Participants != "")
            {
                if (count == 0)
                {
                    xpath += "contains(@Participants, \"" + tournament.Participants + "\")";
                    count++;
                }
                else
                {
                    xpath += " and contains(@Participants, \"" + tournament.Participants + "\")";
                }
            }

            if (tournament.Type != "")
            {
                if (count == 0)
                {
                    xpath += "@Type=\"" + tournament.Type + "\"";
                    count++;
                }
                else
                {
                    xpath += " and @Type=\"" + tournament.Type + "\"";
                }
            }

            xpath += "]";

            XmlNodeList res = doc.SelectNodes(xpath);
            if (res.Count != 0)
            {
                foreach (XmlNode nod in res)
                {
                    Tournaments temp = new Tournaments();

                    for (int i = 0; i < nod.Attributes.Count; i++)
                    {
                        if (nod.Attributes[i].Name.Equals("Title")) { temp.Title = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("Date")) { temp.Date = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("PriceRange")) { temp.PriceRange = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("Location")) { temp.Location = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("Commentators")) { temp.Commentators = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("Participants")) { temp.Participants = nod.Attributes[i].Value; }
                        if (nod.Attributes[i].Name.Equals("Type")) { temp.Type = nod.Attributes[i].Value; }
                    }
                    result.Add(temp);
                }
            }
            return result;
        }
    }
}
