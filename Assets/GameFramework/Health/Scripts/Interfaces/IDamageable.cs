namespace GameFramework
{
    public interface IDamageable<T>
    {
        void Hit  (DamageInfo info);
        void Gain (T value);
        void Set  (T value);
    }
}
