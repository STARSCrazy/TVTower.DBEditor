using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
    public class TVTEpisodePersister<T> : TVTEntityPersister<T>
        where T : TVTEpisode
    {
        public override void Load(XmlNode xmlNode, T episode, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            base.Load(xmlNode, episode, database, dbVersion, dataStructure);

            foreach (var movieChild in xmlNode.ChildNodes)
            {
                if (movieChild is XmlLinkedNode)
                {
                    var linkedNode = (XmlLinkedNode)movieChild;

                    LoadNames(linkedNode, episode, dbVersion, dataStructure);

                    switch (linkedNode.Name)
                    {
                        case "data":
                            episode.BettyBonus = linkedNode.GetAttributeInteger("betty");
                            episode.PriceMod = linkedNode.GetAttributeInteger("price");
                            episode.CriticsRate = linkedNode.GetAttributeInteger("critics");
                            episode.ViewersRate = linkedNode.GetAttributeInteger("speed");
                            episode.BoxOfficeRate = linkedNode.GetAttributeInteger("outcome");
                            break;
                    }
                }
            }
        }


        public override void Save(XmlNode xmlNode, T episode, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            base.Save(xmlNode, episode, dbVersion, dataStructure);

            SaveNames(xmlNode, episode, dbVersion, dataStructure);

            if ((int)dbVersion > 2)
                xmlNode.AddAttribute("betty", episode.BettyBonus.ToString());

            xmlNode.AddAttribute("price", episode.PriceMod.ToString());
            xmlNode.AddAttribute("critics", episode.CriticsRate.ToString());
            xmlNode.AddAttribute("speed", episode.ViewersRate.ToString());
            xmlNode.AddAttribute("outcome", episode.BoxOfficeRate.ToString());
        }


        private void LoadNames(XmlLinkedNode linkedNode, T episode, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            switch (linkedNode.Name)
            {
                case "title":
                case "title_de":
                    if (dataStructure == DataStructure.FakeData)
                        episode.FakeTitleDE = linkedNode.GetElementValue();
                    else
                        episode.TitleDE = linkedNode.GetElementValue();
                    break;
                case "title_en":
                    if (dataStructure == DataStructure.FakeData)
                        episode.FakeTitleEN = linkedNode.GetElementValue();
                    else
                        episode.TitleEN = linkedNode.GetElementValue();
                    break;
                case "description":
                case "description_de":
                    episode.DescriptionDE = linkedNode.GetElementValue();
                    break;
                case "description_en":
                    episode.DescriptionEN = linkedNode.GetElementValue();
                    break;
            }
        }

        private void SaveNames(XmlNode xmlNode, T episode, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            if (dbVersion == DatabaseVersion.V2)
            {
                if (dataStructure == DataStructure.FakeData)
                    xmlNode.AddElement("title", episode.FakeTitleDE);
                else
                    xmlNode.AddElement("title", episode.TitleDE);

                xmlNode.AddElement("description", episode.DescriptionDE);
            }
            else if (dbVersion == DatabaseVersion.V3)
            {
                xmlNode.AddElement("title_de", episode.TitleDE);
                xmlNode.AddElement("title_en", episode.TitleEN);
                xmlNode.AddElement("description_de", episode.DescriptionDE);
                xmlNode.AddElement("description_en", episode.DescriptionEN);
            }

            if (dataStructure == DataStructure.Full)
            {
                var additionalNode = xmlNode.OwnerDocument.CreateElement("name_full");

                xmlNode.AddElement("fake_title_de", episode.FakeTitleDE);
                xmlNode.AddElement("fake_title_en", episode.FakeTitleEN);

                xmlNode.AddElement("original_title_de", episode.TitleDE);
                xmlNode.AddElement("original_title_en", episode.TitleEN);
                xmlNode.AddElement("description_de", episode.DescriptionDE);
                xmlNode.AddElement("description_en", episode.DescriptionDE);

                xmlNode.AddElement("description_movie_db", episode.DescriptionMovieDB);

                xmlNode.AppendChild(additionalNode);
            }
        }
    }
}