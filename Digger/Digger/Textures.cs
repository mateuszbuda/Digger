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
    public class Textures
    {
        private const string BOMB = "bomb";
        private const string BOMB_ARTEFACT = "bomb_artefact";
        private const string BONUS_TIME = "bonustime";
        private const string CAPTAIN = "captain";
        private const string COLONEL = "colonel";
        private const string DIAMOND = "diamond";
        private const string FIELD = "field";
        private const string GENERAL = "general";
        private const string GOLD = "gold";
        private const string GOLDBAG = "goldbag";
        private const string GUY = "guy";
        private const string INVICLOAK = "invicloak";
        private const string MAJOR = "major";
        private const string MISSILE = "missile";
        private const string SERGEANT = "sergeant";
        private const string FIRE = "fire";

        private static ContentManager cm;
        public Textures(ContentManager contentManager)
        {
            cm = contentManager;
        }

        private static Texture2D bombTex;
        public static Texture2D getBombTex()
        {
            if (bombTex != null)
                return bombTex;

            return bombTex = cm.Load<Texture2D>(BOMB);
        }

        private static Texture2D bombArtefactTex;
        public static Texture2D getBombArtefactTex()
        {
            if (bombArtefactTex != null)
                return bombArtefactTex;

            return bombArtefactTex = cm.Load<Texture2D>(BOMB_ARTEFACT);
        }

        private static Texture2D bonusTimeTex;
        public static Texture2D getBonusTimeTex()
        {
            if (bonusTimeTex != null)
                return bonusTimeTex;

            return bonusTimeTex = cm.Load<Texture2D>(BONUS_TIME);
        }

        private static Texture2D captainTex;
        public static Texture2D getCaptainTex()
        {
            if (captainTex != null)
                return captainTex;

            return captainTex = cm.Load<Texture2D>(CAPTAIN);
        }

        private static Texture2D colonelTex;
        public static Texture2D getColonelTex()
        {
            if (colonelTex != null)
                return colonelTex;

            return colonelTex = cm.Load<Texture2D>(COLONEL);
        }

        private static Texture2D diamondTex;
        public static Texture2D getDiamondTex()
        {
            if (diamondTex != null)
                return diamondTex;

            return diamondTex = cm.Load<Texture2D>(DIAMOND);
        }

        private static Texture2D fieldTex;
        public static Texture2D getFieldTex()
        {
            if (fieldTex != null)
                return fieldTex;

            return fieldTex = cm.Load<Texture2D>(FIELD);
        }

        private static Texture2D fireTex;
        public static Texture2D getFireTex()
        {
            if (fireTex != null)
                return fireTex;

            return fireTex = cm.Load<Texture2D>(FIRE);
        }

        private static Texture2D generalTex;
        public static Texture2D getGeneralTex()
        {
            if (generalTex != null)
                return generalTex;

            return generalTex = cm.Load<Texture2D>(GENERAL);
        }

        private static Texture2D goldTex;
        public static Texture2D getGoldTex()
        {
            if (goldTex != null)
                return goldTex;

            return goldTex = cm.Load<Texture2D>(GOLD);
        }

        private static Texture2D goldbagTex;
        public static Texture2D getGoldbagTex()
        {
            if (goldbagTex != null)
                return goldbagTex;

            return goldbagTex = cm.Load<Texture2D>(GOLDBAG);
        }

        private static Texture2D guyTex;
        public static Texture2D getGuyTex()
        {
            if (guyTex != null)
                return guyTex;

            return guyTex = cm.Load<Texture2D>(GUY);
        }

        private static Texture2D invicloakTex;
        public static Texture2D getInvicloakTex()
        {
            if (invicloakTex != null)
                return invicloakTex;

            return invicloakTex = cm.Load<Texture2D>(INVICLOAK);
        }

        private static Texture2D majorTex;
        public static Texture2D getMajorTex()
        {
            if (majorTex != null)
                return majorTex;

            return majorTex = cm.Load<Texture2D>(MAJOR);
        }

        private static Texture2D missileTex;
        public static Texture2D getMissileTex()
        {
            if (missileTex != null)
                return missileTex;

            return missileTex = cm.Load<Texture2D>(MISSILE);
        }

        private static Texture2D sergeantTex;
        public static Texture2D getSergeantTex()
        {
            if (sergeantTex != null)
                return sergeantTex;

            return sergeantTex = cm.Load<Texture2D>(SERGEANT);
        }



    }
}
