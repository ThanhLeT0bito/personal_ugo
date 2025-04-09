using System;
using System.Collections.Generic;

namespace CloudBanking.Entities
{
    [System.Serializable]
    public abstract class BaseObject: IDisposable
    {
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
            }

            disposed = true;
        }

        protected object Copy()
        {
            return this.MemberwiseClone();
        }

        public virtual object DeepCopy()
        {
            return Copy();
        }
    }

    public static class ModelUtils
    {
        public static List<T> EntitiesDeepCopy<T>(this List<T> sources) where T : BaseObject
        {
            if (sources == null)
                return null;

            var list = new List<T>();
            foreach (var item in sources)
            {
                list.Add((T)item.DeepCopy());
            }

            return list;
        }

        public static List<T> ModelsDeepCopy<T>(this IList<T> sources) where T : BaseObject
        {
            if (sources == null)
                return null;

            var list = new List<T>();
            foreach (var item in sources)
            {
                list.Add((T)item.DeepCopy());
            }

            return list;
        }

        public static List<DateTime> ModelsDeepCopy(this List<DateTime> sources)
        {
            if (sources == null)
                return null;

            var list = new List<DateTime>();
            foreach (var item in sources)
            {
                list.Add(item);
            }

            return list;
        }

        public static T EntityDeepCopy<T>(this T source) where T : BaseObject
        {
            if (source == null)
                return null;

            return (T)source.DeepCopy();
        }

        public static T ModelDeepCopy<T>(this T source) where T : BaseObject
        {
            if (source == null)
                return null;

            return (T)source.DeepCopy();
        }
    }
}
