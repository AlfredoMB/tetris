using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly RectTransform[] _prefabs;

    private Dictionary<TetrisBlockGroup.EBlockType, Stack<RectTransform>> _availableInstances = new Dictionary<TetrisBlockGroup.EBlockType, Stack<RectTransform>>();
    private Dictionary<TetrisBlockGroup.EBlockType, Stack<RectTransform>> _instancesInUse = new Dictionary<TetrisBlockGroup.EBlockType, Stack<RectTransform>>();

    public ObjectPool(RectTransform[] prefabs)
    {
        _prefabs = prefabs;

        for (int i = 0; i < prefabs.Length; i++)
        {
            _availableInstances.Add((TetrisBlockGroup.EBlockType)i, new Stack<RectTransform>());
            _instancesInUse.Add((TetrisBlockGroup.EBlockType)i, new Stack<RectTransform>());
        }
    }

    public void RetrieveAll()
    {
        foreach(KeyValuePair<TetrisBlockGroup.EBlockType, Stack<RectTransform>> blockTypeInstancesInUse in _instancesInUse)
        {
            while (blockTypeInstancesInUse.Value.Count > 0)
            {
                var instance = blockTypeInstancesInUse.Value.Pop();
                instance.SetParent(null);
                instance.gameObject.SetActive(false);
                _availableInstances[blockTypeInstancesInUse.Key].Push(instance);
            }
        }
    }

    public RectTransform GetBlockView(TetrisBlockGroup.EBlockType blockType)
    {
        var blockTypeStack = _availableInstances[blockType];
        var instance = (blockTypeStack.Count == 0)
            ? Object.Instantiate(_prefabs[(int)blockType])
            : blockTypeStack.Pop();

        _instancesInUse[blockType].Push(instance);
        instance.gameObject.SetActive(true);
        return instance;
    }
}