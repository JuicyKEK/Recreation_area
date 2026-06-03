// StringDropdownAttribute.cs

using UnityEngine;

namespace Game.Scripts.Extensions.StringDropdownDrawer
{
    public class StringDropdownAttribute : PropertyAttribute
    {
        public string ArrayFieldName;
    
        public StringDropdownAttribute(string arrayFieldName)
        {
            ArrayFieldName = arrayFieldName;
        }
    }
}