using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:ӵ�ж�ʱ���ܵĶ��У�ʱ�䵽�ͳ���
*/

public class PXTimerQueue<T>
{
	private float dur;
	Queue<T> _queue;
	public float Duration
	{
		get { return dur; }
		set { dur = value; }
	}

	public PXTimerQueue(float Dur)
	{
		this.Duration = Dur;
		_queue = new Queue<T>();
	}
}
