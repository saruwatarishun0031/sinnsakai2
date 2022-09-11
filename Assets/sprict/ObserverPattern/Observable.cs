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
    public void SendNotice()
    {
        //すべての発行先に対して1,2,3を発行する
        foreach (var observer in m_observers)
        {
            if (Player.Instance.p == 3)
            {
                observer.OnNext(2);
            }
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
        }
        

        //observer.OnNext("Player2あと2つ");
        //observer.OnNext("Player3あと2つ");
        //observer.OnNext("Player4あと2つ");
    }
}
 class Unsubscriber : IDisposable
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