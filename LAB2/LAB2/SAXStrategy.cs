using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LAB2
{
    public class SAXStrategy : IStrategy
    {
        public List<Tournaments> Search(Tournaments tournament)
        {
            List<Tournaments> AllResult = new List<Tournaments>();
            var xmlReader = new XmlTextReader(@"C:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.xml");

            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        string Title = "";
                        string Date = "";
                        string PriceRange = "";
                        string Location = "";
                        string Commentators = "";
                        string Participants = "";
                        string Type = "";

                        if (xmlReader.Name.Equals("Title") && (xmlReader.Value.Equals(tournament.Title) || tournament.Title.Equals(String.Empty)))
                        {
                            Title = xmlReader.Value;

                            xmlReader.MoveToNextAttribute();

                            if (xmlReader.Name.Equals("Date") && (xmlReader.Value.Equals(tournament.Date) || tournament.Date.Equals(String.Empty)))
                            {
                                Date = xmlReader.Value;

                                xmlReader.MoveToNextAttribute();

                                if (xmlReader.Name.Equals("PriceRange") && (tournament.PriceRange.Equals(String.Empty) || isPriceRange(xmlReader.Value, tournament.PriceRange)))
                                {
                                    PriceRange = xmlReader.Value;

                                    xmlReader.MoveToNextAttribute();

                                    if (xmlReader.Name.Equals("Location") && (xmlReader.Value.Contains(tournament.Location)) || tournament.Location.Equals(String.Empty))
                                    {
                                        Location = xmlReader.Value;

                                        xmlReader.MoveToNextAttribute();

                                        if (xmlReader.Name.Equals("Commentators") && (xmlReader.Value.Contains(tournament.Commentators) || tournament.Commentators.Equals(String.Empty)))
                                        {
                                            Commentators = xmlReader.Value;

                                            xmlReader.MoveToNextAttribute();

                                            if (xmlReader.Name.Equals("Participants") && (xmlReader.Value.Contains(tournament.Participants) || tournament.Participants.Equals(String.Empty)))
                                            {
                                                Participants = xmlReader.Value;

                                                xmlReader.MoveToNextAttribute();

                                                if (xmlReader.Name.Equals("Type") && (xmlReader.Value.Equals(tournament.Type) || tournament.Type.Equals(String.Empty)))
                                                {
                                                    Type = xmlReader.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (Title != "" && Date != "" && PriceRange != "" && Location != "" && Commentators != "" && Participants != "" && Type != "")
                        {
                            Tournaments myConcert = new Tournaments();
                            myConcert.Title = Title;
                            myConcert.Date = Date;
                            myConcert.PriceRange = PriceRange;
                            myConcert.Location = Location;
                            myConcert.Commentators = Commentators;
                            myConcert.Participants = Participants;
                            myConcert.Type = Type;

                            AllResult.Add(myConcert);
                        }
                    }
                }
            }

            xmlReader.Close();
            return AllResult;
        }
        public bool isPriceRange(string value, string priceRange)
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
