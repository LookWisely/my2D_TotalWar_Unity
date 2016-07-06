using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class GAnalytics
    {
        static public Vector2 EquifyDZ(Vector2 obj, Vector2 subj, float DZsize)
        {
            if ((obj - subj).magnitude < DZsize)
                return obj;
            else
                return subj;
        }



    }
}
