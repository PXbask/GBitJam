using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:拥有定时功能的队列，时间到就出列
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
