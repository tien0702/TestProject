using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] protected string objectPoolerID;
        [SerializeField] protected bool canExpand = true;
        [SerializeField] protected int poolSize = 20;
        [SerializeField] protected GameObject objectToPool;
        protected Pool poolHolder;

        public bool CanExpand => canExpand;
        public int PoolSize => poolSize;
        public string ObjectPoolerID => objectPoolerID;
        public Pool PoolHolder => poolHolder;

        protected virtual void Awake()
        {
            poolHolder = CreatePoolHolder();
            this.FillPool();
        }

        protected virtual Pool CreatePoolHolder()
        {
            GameObject obj = new GameObject(DeterminePoolName());
            Pool pool = obj.AddComponent<Pool>();
            return pool;
        }

        public virtual string DeterminePoolName()
        {
            return "[Pooler] " + this.name;
        }

        public virtual void FillPool()
        {
            int amount = poolSize - poolHolder.PooledGameObjects.Count;
            for (int i = 0; i < amount; i++)
                AddOneObjectToPool();
        }

        public virtual GameObject GetObject()
        {
            if (poolHolder == null) return null;
            foreach (GameObject obj in poolHolder.PooledGameObjects)
                if (!obj.activeInHierarchy) return obj;

            if (canExpand) return AddOneObjectToPool();

            return null;
        }

        protected virtual GameObject AddOneObjectToPool()
        {
            GameObject newObject = Instantiate(objectToPool, poolHolder.transform);
            newObject.SetActive(false);
            newObject.name = objectToPool.name;
            poolHolder.PooledGameObjects.Add(newObject);

            return newObject;
        }

        private void OnDestroy()
        {
            if (poolHolder != null) Destroy(poolHolder.gameObject);
        }
    }
}
