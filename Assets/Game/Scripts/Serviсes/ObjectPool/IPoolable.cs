namespace Game.Scripts.Serviсes.ObjectPool
{
    /// <summary>
    /// Интерфейс для объектов, которые могут быть использованы в пуле
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Вызывается при получении объекта из пула
        /// </summary>
        public void OnGetFromPool();
        
        /// <summary>
        /// Вызывается при возврате объекта в пул
        /// </summary>
        public void OnReturnToPool();
    }
}