using System.Reflection;

namespace FastReflection
{
    public class FieldAccessorFactory : IFastReflectionFactory<FieldInfo, IFieldAccessor>
    {
        #region IFastReflectionFactory<FieldInfo,IFieldAccessor> Members

        IFieldAccessor IFastReflectionFactory<FieldInfo, IFieldAccessor>.Create(FieldInfo key)
        {
            return Create(key);
        }

        #endregion

        public IFieldAccessor Create(FieldInfo key)
        {
            return new FieldAccessor(key);
        }
    }
}