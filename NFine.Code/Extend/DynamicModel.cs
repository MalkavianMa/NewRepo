using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code.Extend
{
    public class DynamicModel : DynamicObject
    {
        private string propertyName;
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        // The inner dictionary.
        Dictionary<string, object> dicProperty
            = new Dictionary<string, object>();
        public Dictionary<string, object> DicProperty
        {
            get
            {
                return dicProperty;
            }
        }


        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dicProperty.Count;
            }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name;

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dicProperty.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            if (binder.Name == "Property")
            {
                dicProperty[PropertyName] = value;
            }
            else
            {
                dicProperty[binder.Name] = value;
            }


            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }
}
