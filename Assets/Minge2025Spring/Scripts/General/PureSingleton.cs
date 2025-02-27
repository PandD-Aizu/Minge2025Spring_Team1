using System;

namespace General
{
    public abstract class PureSingleton<T> : IDisposable where T : class, new()
    {
        private static T instance = null;

        public static T I => Instance;

        public static T Instance
        {
            get
            {
                CreateInstance();
                return instance;
            }
        }

        public static void CreateInstance()
        {
            if (instance == null)
                instance = new T();
        }

        public static bool IsExist()
        {
            return instance != null;
        }

        public virtual void Dispose()
        {
            instance = null;
        }
    }
}