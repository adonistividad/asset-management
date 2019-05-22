using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace restapii.Controllers
{
    public static class ObjectExtension
    {

        /// <summary>
        /// Copies the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        private static void CopyCollection<T>(IEnumerable<T> from, ICollection<T> to)
        {
            if (from == null || to == null || to.IsReadOnly)
            {
                return;
            }

            to.Clear();
            foreach (T element in from)
            {
                to.Add(element);
            }
        }

        /// <summary>
        /// Copies the object.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void ObjectCopy(object from, object to, bool poplists)
        {
            try
            {

                //Process_ cProcess = new Process_();
                //if (!cProcess.GetInfo() && System.Diagnostics.Debugger.IsAttached) return;

                if (from == null || to == null)
                {
                    return;
                }

                PropertyDescriptorCollection toProperties = TypeDescriptor.GetProperties(to);
                foreach (PropertyDescriptor fromProperty in TypeDescriptor.GetProperties(from))
                {
                    PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true /* ignoreCase */);

                    if (toProperty != null && !toProperty.IsReadOnly && !(toProperty.PropertyType == typeof(System.Object)))
                    {
                        // Can from.Property reference just be assigned directly to to.Property reference?
                        bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);
                        // Is from.Property just the nullable form of to.Property?
                        bool liftedValueType = (isDirectlyAssignable) ? false : (Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType);

                        object fromValue = fromProperty.GetValue(from);
                        if (isDirectlyAssignable || liftedValueType)
                        {
                            if (isDirectlyAssignable || (fromValue != null && liftedValueType))
                            {
                                toProperty.SetValue(to, fromValue);
                            }
                        }
                        else if (poplists && fromProperty.GetValue(from) as IList != null)
                        {
                            foreach (var fromItem in (IList)fromValue)
                            {
                                ObjectCopy(fromItem, ((IList)toProperty.GetValue(to))[((IList)fromValue).IndexOf(fromItem)], poplists);
                            }
                        }
                        else
                        {
                            ObjectCopy(fromValue, toProperty.GetValue(to), poplists);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //_modelError.FormName = "Base";
                //_modelError.FunctionName = " Save";
            }

        }


    }
}

