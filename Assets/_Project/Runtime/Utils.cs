namespace _Project.Runtime
{
    public static class Utils
    {
        public static T[] Shuffle<T>(T[] a)
        {
            T[] b = new T[a.Length];
            a.CopyTo(b, 0);
            for (int i = b.Length - 1; i > 0; i--)
            {
                int rnd = UnityEngine.Random.Range(0, i);
                object temp = b.GetValue(i);
                
                b.SetValue(b.GetValue(rnd), i);
                b.SetValue(temp, rnd);
            }

            return b;
        }
    }
}