using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class PlatformContainer
    {
        private GameObject leftPlat;
        private GameObject rightPlat;

        public GameObject LeftPlatform
        {
            get { return leftPlat; }
            set { leftPlat = value; }
        }

        public GameObject RightPlatform
        {
            get { return rightPlat; }
            set { rightPlat = value; }
        }

        GameObject[] GetBothPlatforms()
        {
            /*
            GameObject[] retArray = new GameObject[2];

            retArray[0] = leftPlat;
            retArray[1] = rightPlat;

            return retArray;
            */

            return new GameObject[] { leftPlat, rightPlat };
        }
    }
}
