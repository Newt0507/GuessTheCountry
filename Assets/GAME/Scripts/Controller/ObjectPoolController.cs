using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public int size;
        public GameObject prefab;
        public string name => prefab.name;
    }

    public static ObjectPoolController Instance { get; private set; }

    public List<Pool> pools;
    private Dictionary<string, ObjectPool> _poolDict = new Dictionary<string, ObjectPool>();


    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (var pool in pools)
        {
            ObjectPool objectPool = new ObjectPool();
            objectPool.Init(pool.prefab, pool.size, transform);
            _poolDict.Add(pool.name, objectPool);
        }
    }
    
    public GameObject Get(string name)
    {
        if (!_poolDict.ContainsKey(name)) return null;

        return _poolDict[name].Get();
    }

    public List<GameObject> GetAll(string name)
    {
        if (!_poolDict.ContainsKey(name)) return null;
        
        return _poolDict[name].GetAll();
    }

    public void Return(GameObject gameObject)
    {
        //remove (clone) from gameObject name
        string name = gameObject.name.Substring(0, gameObject.name.Length - 7);
        
        if (!_poolDict.ContainsKey(name)) return;
        
        _poolDict[name].Return(gameObject);
    }

    public int GetPoolSize(string name)
    {
        if (!_poolDict.ContainsKey(name)) return 0;

        return _poolDict[name].GetSize();
    }
}

public class ObjectPool
{
    private GameObject _prefab;
    private int _size;
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    public void Init(GameObject prefab, int size, Transform parent = null)
    {
        _prefab = prefab;
        _size = size;

        for (int i = 0; i < size; i++)
        {
            var obj = GameObject.Instantiate(_prefab, parent);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (_objectPool.Count <= 0) return null;

        GameObject obj = _objectPool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public List<GameObject> GetAll()
    {
        var objList = new List<GameObject>();
        for (int i = 0; i < _size; i++)
        {
            var obj = _objectPool.Dequeue();
            obj.SetActive(true);
            objList.Add(obj);
        }

        return objList;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
    }

    public int GetSize()
    {
        return _size;
    }
}