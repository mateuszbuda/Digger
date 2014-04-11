using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digger.Objects;

namespace Digger
{
    class HighScores
    {
        private const string HS_FNAME = "HighScores";

        public bool update(Guy guy)
        {
            return false;
        }
        public List<Tuple<string, int>> getHighScores()
        {
            return new List<Tuple<string, int>>();
        }
    }
}
