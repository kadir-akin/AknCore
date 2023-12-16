using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Utilities
{
    public static class TypeUtilities
    {
        public static IEnumerable<Type> GetAllAssembyTypes() 
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                   .SelectMany(s => s.GetTypes());
        }
        public static List<Type> GetAllAssembysTypeFromAssignableInterface(Type interfacetype,bool isNotInterfaceAssignable) 
        {
           var assgnableList= GetAllAssembyTypes().Where(p => interfacetype.IsAssignableFrom(p));

            if (isNotInterfaceAssignable)
                return assgnableList?.Where(x => !x.IsInterface)?.ToList();
            else
                return assgnableList?.ToList();
        }

        public static TAttribute GetAttributeValueByType<TAttribute>(Type classtype)
        {
            var attributes = classtype.GetCustomAttributes(false)?.Where(x => x.GetType() == typeof(TAttribute))?.FirstOrDefault();

            if (attributes != null && attributes is TAttribute Treturnattribute)
                return Treturnattribute;
            
            return default(TAttribute);
        }
    }
}
