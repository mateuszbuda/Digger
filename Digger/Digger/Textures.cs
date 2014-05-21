using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Digger
{
    /// <summary>
    /// Singleton udostępniający tekstury dla obiektów gry
    /// </summary>
    public class Textures
    {
        /// <summary>
        /// Nazwa pliku z teksturą dla bomby
        /// </summary>
        private const string BOMB = "bomb";
        /// <summary>
        /// Nazwa pliku z teksturą dla  artefaktu bomby
        /// </summary>
        private const string BOMB_ARTEFACT = "bomb_artefact";
        /// <summary>
        /// Nazwa pliku z teksturą dla czasu bonusowego
        /// </summary>
        private const string BONUS_TIME = "bonustime";
        /// <summary>
        /// Nazwa pliku z teksturą dla Captaina
        /// </summary>
        private const string CAPTAIN = "captain";
        /// <summary>
        /// Nazwa pliku z teksturą dla Colonela
        /// </summary>
        private const string COLONEL = "colonel";
        /// <summary>
        /// Nazwa pliku z teksturą dla diamentu
        /// </summary>
        private const string DIAMOND = "diamond";
        /// <summary>
        /// Nazwa pliku z teksturą dla pola
        /// </summary>
        private const string FIELD = "field";
        /// <summary>
        /// Nazwa pliku z teksturą dla Generala
        /// </summary>
        private const string GENERAL = "general";
        /// <summary>
        /// Nazwa pliku z teksturą dla złota
        /// </summary>
        private const string GOLD = "gold";
        /// <summary>
        /// Nazwa pliku z teksturą dla worka ze złotem
        /// </summary>
        private const string GOLDBAG = "goldbag";
        /// <summary>
        /// Nazwa pliku z teksturą dla bohatera Guya
        /// </summary>
        private const string GUY = "guy";
        /// <summary>
        /// Nazwa pliku z teksturą dla peleryny niewidki
        /// </summary>
        private const string INVICLOAK = "invicloak";
        /// <summary>
        /// Nazwa pliku z teksturą dla Majora
        /// </summary>
        private const string MAJOR = "major";
        /// <summary>
        /// Nazwa pliku z teksturą dla pocisku
        /// </summary>
        private const string MISSILE = "missile";
        /// <summary>
        /// Nazwa pliku z teksturą dla Sergeanta
        /// </summary>
        private const string SERGEANT = "sergeant";
        /// <summary>
        /// Nazwa pliku z teksturą dla wystrzału
        /// </summary>
        private const string FIRE = "fire";

        /// <summary>
        /// Menadże udostępniający zasoby graficzne
        /// </summary>
        private static ContentManager cm;
        
        /// <summary>
        /// Konstruktor tworzący obiekt przed rozpoczęciem gry
        /// </summary>
        /// <param name="contentManager">Menadżer zasobów do ładowania tekstur</param>
        public Textures(ContentManager contentManager)
        {
            cm = contentManager;
        }

        /// <summary>
        /// Tekstura bomby
        /// </summary>
        private static Texture2D bombTex;
        /// <summary>
        /// Metoda udostępniająca teksturę bomby
        /// </summary>
        /// <returns>Tekstura bomby</returns>
        public static Texture2D getBombTex()
        {
            if (bombTex != null)
                return bombTex;

            return bombTex = cm.Load<Texture2D>(BOMB);
        }

        /// <summary>
        /// Tekstura artefaktu bomby
        /// </summary>
        private static Texture2D bombArtefactTex;
        /// <summary>
        /// Metoda udostępniająca teksturę bomby artefaktu
        /// </summary>
        /// <returns>Tekstura bomby artefaktu</returns>
        public static Texture2D getBombArtefactTex()
        {
            if (bombArtefactTex != null)
                return bombArtefactTex;

            return bombArtefactTex = cm.Load<Texture2D>(BOMB_ARTEFACT);
        }

        /// <summary>
        /// Tekstura casu bonusowego
        /// </summary>
        private static Texture2D bonusTimeTex;
        /// <summary>
        /// Metoda udostępniająca teksturę czasu bonusowego
        /// </summary>
        /// <returns>Tekstura czasu bonusowego</returns>
        public static Texture2D getBonusTimeTex()
        {
            if (bonusTimeTex != null)
                return bonusTimeTex;

            return bonusTimeTex = cm.Load<Texture2D>(BONUS_TIME);
        }

        /// <summary>
        /// Tekstura Captaina
        /// </summary>
        private static Texture2D captainTex;
        /// <summary>
        /// Metoda udostępniająca teksturę Captaina
        /// </summary>
        /// <returns>Tekstura Captaina</returns>
        public static Texture2D getCaptainTex()
        {
            if (captainTex != null)
                return captainTex;

            return captainTex = cm.Load<Texture2D>(CAPTAIN);
        }

        /// <summary>
        /// Tekstura Colonela
        /// </summary>
        private static Texture2D colonelTex;
        /// <summary>
        /// Metoda udostępniająca teksturę Colonela
        /// </summary>
        /// <returns>Tekstura Colonela</returns>
        public static Texture2D getColonelTex()
        {
            if (colonelTex != null)
                return colonelTex;

            return colonelTex = cm.Load<Texture2D>(COLONEL);
        }

        /// <summary>
        /// Tekstura diamentu
        /// </summary>
        private static Texture2D diamondTex;
        /// <summary>
        /// Metoda udostępniająca teksturę diamentu
        /// </summary>
        /// <returns>Tekstura diamentu</returns>
        public static Texture2D getDiamondTex()
        {
            if (diamondTex != null)
                return diamondTex;

            return diamondTex = cm.Load<Texture2D>(DIAMOND);
        }

        /// <summary>
        /// Tekstura pola na mapie
        /// </summary>
        private static Texture2D fieldTex;
        /// <summary>
        /// Metoda udostępniająca teksturę pola
        /// </summary>
        /// <returns>Tekstura pola</returns>
        public static Texture2D getFieldTex()
        {
            if (fieldTex != null)
                return fieldTex;

            return fieldTex = cm.Load<Texture2D>(FIELD);
        }

        /// <summary>
        /// Tekstura wystrzału
        /// </summary>
        private static Texture2D fireTex;
        /// <summary>
        /// Metoda udostępniająca teksturę strzału
        /// </summary>
        /// <returns>Tekstura strzału</returns>
        public static Texture2D getFireTex()
        {
            if (fireTex != null)
                return fireTex;

            return fireTex = cm.Load<Texture2D>(FIRE);
        }

        /// <summary>
        /// Tekstura Generala
        /// </summary>
        private static Texture2D generalTex;
        /// <summary>
        /// Metoda udostępniająca teksturę Generala
        /// </summary>
        /// <returns>Tekstura Generala</returns>
        public static Texture2D getGeneralTex()
        {
            if (generalTex != null)
                return generalTex;

            return generalTex = cm.Load<Texture2D>(GENERAL);
        }

        /// <summary>
        /// Tekstura złota
        /// </summary>
        private static Texture2D goldTex;
        /// <summary>
        /// Metoda udostępniająca teksturę złota
        /// </summary>
        /// <returns>Tekstura złota</returns>
        public static Texture2D getGoldTex()
        {
            if (goldTex != null)
                return goldTex;

            return goldTex = cm.Load<Texture2D>(GOLD);
        }

        /// <summary>
        /// Tekstura worka ze złotem
        /// </summary>
        private static Texture2D goldbagTex;
        /// <summary>
        /// Metoda udostępniająca teksturę worka ze złotem
        /// </summary>
        /// <returns>Tekstura worka ze złotem</returns>
        public static Texture2D getGoldbagTex()
        {
            if (goldbagTex != null)
                return goldbagTex;

            return goldbagTex = cm.Load<Texture2D>(GOLDBAG);
        }

        /// <summary>
        /// Tekstura bohatera Guya
        /// </summary>
        private static Texture2D guyTex;
        /// <summary>
        /// Metoda udostępniająca teksturę bohatera
        /// </summary>
        /// <returns>Tekstura bohatera</returns>
        public static Texture2D getGuyTex()
        {
            if (guyTex != null)
                return guyTex;

            return guyTex = cm.Load<Texture2D>(GUY);
        }

        /// <summary>
        /// Tekstura peleryny niewidki
        /// </summary>
        private static Texture2D invicloakTex;
        /// <summary>
        /// Metoda udostępniająca teksturę peleryny niewidki
        /// </summary>
        /// <returns>Tekstura peleryny niewidki</returns>
        public static Texture2D getInvicloakTex()
        {
            if (invicloakTex != null)
                return invicloakTex;

            return invicloakTex = cm.Load<Texture2D>(INVICLOAK);
        }

        /// <summary>
        /// Tekstura Majora
        /// </summary>
        private static Texture2D majorTex;
        /// <summary>
        /// Metoda udostępniająca teksturę Majora
        /// </summary>
        /// <returns>Tekstura Majora</returns>
        public static Texture2D getMajorTex()
        {
            if (majorTex != null)
                return majorTex;

            return majorTex = cm.Load<Texture2D>(MAJOR);
        }

        /// <summary>
        /// Tekstura pocisku
        /// </summary>
        private static Texture2D missileTex;
        /// <summary>
        /// Metoda udostępniająca teksturę pocisku
        /// </summary>
        /// <returns>Tekstura pocisku</returns>
        public static Texture2D getMissileTex()
        {
            if (missileTex != null)
                return missileTex;

            return missileTex = cm.Load<Texture2D>(MISSILE);
        }

        /// <summary>
        /// Tekstura Sergeanta
        /// </summary>
        private static Texture2D sergeantTex;
        /// <summary>
        /// Metoda udostępniająca teksturę Sergeanta
        /// </summary>
        /// <returns>Tekstura Sergeanta</returns>
        public static Texture2D getSergeantTex()
        {
            if (sergeantTex != null)
                return sergeantTex;

            return sergeantTex = cm.Load<Texture2D>(SERGEANT);
        }
    }
}
