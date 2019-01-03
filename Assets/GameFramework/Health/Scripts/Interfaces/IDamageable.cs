namespace GameFramework
{
    public interface IDamageable<T>
    {
        void Hit  (T value, UnityEngine.GameObject damager);
        void Gain (T value);
        void Set  (T value);
    }
}
