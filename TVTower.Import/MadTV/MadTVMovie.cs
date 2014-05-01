using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Importer.MadTV
{
    public class MadTVMovie
    {
        public string Title;
        public string Description;

        public int Genre; //1-10
        public int Pause; //1-5
        public int Blocks; //1-7
        public int Quality; //1-32
        public bool Auction;
        public bool XRated;
        public int Cost;
        public int Country; //0-9
        public int Year; //0-99
        public int Critics; //0-10
        public int Box; //0-10
    }
}
