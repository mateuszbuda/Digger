using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Digger.Objects;
using Digger.Objects.Artefacts;

namespace Digger
{
    class GameState
    {
        private Map map;
        private Guy guy;
        private List<Artefact> artefacts;
        private List<Enemy> enemies;

        public void pause()
        {
            return;
        }
        public void resume()
        {
            return;
        }
        public void saveGame(string filename)
        {
            return;
        }
        public void loadGame(string filename)
        {
            return;
        }
    }
}
