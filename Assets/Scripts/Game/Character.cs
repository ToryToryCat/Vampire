using UnityEngine;

public class Character : MonoBehaviour
{
   public static float Speed
   {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f;}
   }

   public static float WeaponSpeed
   {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
   }
}
