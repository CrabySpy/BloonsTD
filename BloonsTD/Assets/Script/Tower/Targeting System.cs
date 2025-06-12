using System.Collections.Generic;
using UnityEngine;

public enum TargetingMode
{
   First,
   Strongest,
   Closest,
   Farthest
}

public static class TargetingSystem
{
   public static Bloon GetTarget(List<Bloon> bloons, Transform towerTransform, TargetingMode mode)
   {
       if (bloons == null || bloons.Count == 0) return null;

       // Bubble sort
       for (int i = 0; i < bloons.Count - 1; i++)
       {
           for (int j = 0; j < bloons.Count - i - 1; j++)
           {
               bool shouldSwap = false;

               switch (mode)
               {
                   case TargetingMode.Closest:
                       float distA = Vector3.Distance(towerTransform.position, bloons[j].transform.position);
                       float distB = Vector3.Distance(towerTransform.position, bloons[j + 1].transform.position);
                       shouldSwap = distA > distB;
                       break;

                   case TargetingMode.Farthest:
                       float distA2 = Vector3.Distance(towerTransform.position, bloons[j].transform.position);
                       float distB2 = Vector3.Distance(towerTransform.position, bloons[j + 1].transform.position);
                       shouldSwap = distA2 < distB2;
                       break;

                   case TargetingMode.First:
                       shouldSwap = bloons[j].Progress < bloons[j + 1].Progress;
                       break;

                   case TargetingMode.Strongest:
                       shouldSwap = bloons[j].Health < bloons[j + 1].Health;
                       break;
               }

               if (shouldSwap)
               {
                   Bloon temp = bloons[j];
                   bloons[j] = bloons[j + 1];
                   bloons[j + 1] = temp;
               }
           }
       }

       return bloons[0];
   }
}
