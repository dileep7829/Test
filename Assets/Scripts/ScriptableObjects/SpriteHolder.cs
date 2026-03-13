using UnityEngine;

namespace ScriptableObjects
{
   
   [CreateAssetMenu(fileName = "SpriteHolder", menuName = "ScriptableObjects/SpriteHolder")]
   public class SpriteHolder : ScriptableObject
   {
      public Sprite[] Sprites;
   }
}
