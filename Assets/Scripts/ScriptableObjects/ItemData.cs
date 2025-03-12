using UnityEngine;

namespace ScriptableObjects
{
    public class ItemData : ScriptableObject
    {    
        public string Name;
        public Sprite Icon; 
        public string Description;
        public int Price;
    }
}