using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable : IObservable<int>
{
    //購読されたIObserver<int>のリスト
    private List<IObserver<int>> m_observers = new List<IObserver<int>>();

    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!m_observers.Contains(observer))
            m_observers.Add(observer);
        //購読解除用のクラスをIDisposableとして返す
        return new Unsubscriber(m_observers, observer);
    }
    ///<summary>
    ///public関数を作り、それを必要なところに呼び出す
    ///<summary>
    
}
public class Unsubscriber : IDisposable
{
    //発行先リスト
    private List<IObserver<int>> m_observers;
    //DisposeされたときにRemoveするIObserver<int>
    private IObserver<int> m_observer;

    public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
    {
        m_observers = observers;
        m_observer = observer;
    }

    public void Dispose()
    {
        //Disposeされたら発行先リストから対象の発行先を削除する
        m_observers.Remove(m_observer);
    }
}