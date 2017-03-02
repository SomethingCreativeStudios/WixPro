using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game_Controls.Scripts.Enums;
using CodeBureau;
using UnityEngine;

namespace Assets.Utils
{
    public static class ControllerHelper
    {
        /// <summary>
        /// Based on location return the gameObject. This is for player 1 fields
        /// </summary>
        /// <param name="location">The location you want</param>
        /// <returns></returns>
        public static GameObject FindGameObject(Location location, bool opField = false)
        {
            GameObject gameObject = null;
            String locationName = StringEnum.GetStringValue(location);
            locationName = opField ? "OP_" + locationName : locationName;

            if ( location.ToString().StartsWith("SIGNI") )
            {
                GameObject[] signiZones = GameObject.FindGameObjectsWithTag(locationName);
                foreach ( var signiZone in signiZones )
                {
                    SIGNIController signiController = gameObject.GetComponent<SIGNIController>();
                    
                    if ( signiController != null && location.ToString().Contains(signiController.zone.ToString()) )
                    {
                        gameObject = signiZone;
                        break;
                    }
                }
            } else
            {
                gameObject = GameObject.FindGameObjectWithTag(locationName);
            }

            return gameObject;
        }

        /// <summary>
        /// Given gameObject will return the location enum
        /// </summary>
        /// <param name="gameObject">Object that has a poolscriptviewer</param>
        /// <returns></returns>
        public static Location GameObjectToLocation(GameObject gameObject)
        {
            Location location = Location.Hand;

            if ( gameObject.GetComponent<PoolViewerScript>() != null )
            {
                location = gameObject.GetComponent<PoolViewerScript>().location;
            }

            return location;
        }
    }
}
