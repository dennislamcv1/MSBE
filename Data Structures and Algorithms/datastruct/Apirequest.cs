using System;
using System.Collections.Generic;
using System.Threading;

public class ApiRequest
{
    public string Endpoint { get; set; }
    public int Priority { get; set; }

    public ApiRequest(string endpoint, int priority)
    {
        Endpoint = endpoint;
        Priority = priority;
    }
}

public class ApiRequestQueue
{
    private List<ApiRequest> _heap;
    private readonly object _lock = new();

    public ApiRequestQueue()
    {
        _heap = new List<ApiRequest>();
    }

    public void Enqueue(ApiRequest request)
    {
        lock (_lock)
        {
            _heap.Add(request);
            HeapifyUp(_heap.Count - 1);
        }
    }

    public void BatchEnqueue(IEnumerable<ApiRequest> requests)
    {
        lock (_lock)
        {
            foreach (var request in requests)
            {
                _heap.Add(request);
                HeapifyUp(_heap.Count - 1);
            }
        }
    }

    public ApiRequest Dequeue()
    {
        lock (_lock)
        {
            if (_heap.Count == 0) return null;

            var root = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);
            return root;
        }
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (_heap[index].Priority >= _heap[parent].Priority)
                break;

            Swap(index, parent);
            index = parent;
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = _heap.Count - 1;

        while (true)
        {
            int smallest = index;
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left <= lastIndex && _heap[left].Priority < _heap[smallest].Priority)
                smallest = left;

            if (right <= lastIndex && _heap[right].Priority < _heap[smallest].Priority)
                smallest = right;

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        var temp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = temp;
    }
}
