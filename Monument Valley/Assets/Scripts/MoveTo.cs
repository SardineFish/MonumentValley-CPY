using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {
    public class MoveOptions
    {
        public GameObject GameObject;
        public Vector3 Offset;
        public float Time;
        public float StartTime;
        public float RunTime;
        public Func<float, float> TimeFunction;
        public MoveOptions(GameObject gameObj, Vector3 offset, float time)
        {
            this.GameObject = gameObj;
            this.Offset = offset;
            this.Time = time;
            this.TimeFunction = (x) =>
            {
                return x;
            };
        }
        public MoveOptions(GameObject gameObj,Vector3 offset, float time, Func<float,float> timeFunction)
        {
            this.GameObject = gameObj;
            this.Offset = offset;
            this.Time = time;
            this.TimeFunction = timeFunction;
        }
        public void Move(float dt)
        {
            var x = (RunTime + dt) / Time;
            x = TimeFunction(x);
            if (x >= 1)
            {
                x = 1;
                var lastX = RunTime / Time;
                lastX = TimeFunction(lastX);
                var dx = x - lastX;
                GameObject.transform.Translate(Offset * dx);
                if (OnFinished != null)
                    OnFinished.Invoke(this, new EventArgs());
            }
            else
            {
                var lastX = RunTime / Time;
                lastX = TimeFunction(lastX);
                var dx = x - lastX;
                GameObject.transform.Translate(Offset * dx);
            }
            RunTime += dt;
        }
        public event EventHandler OnFinished;
    }
    static Dictionary<GameObject, MoveOptions> MoveList = new Dictionary<GameObject, MoveOptions>();
    public static void Start(MoveOptions options)
    {
        MoveList[options.GameObject] = options;
        options.OnFinished += (sender, e) =>
        {
            MoveList.Remove(options.GameObject);
        };
    }
    public static void Stop(GameObject gameObj)
    {
        if (MoveList.ContainsKey(gameObj))
            MoveList.Remove(gameObj);
    }
	// Use this for initialization
	void Start () {
        MoveList = new Dictionary<GameObject, MoveOptions>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var item in MoveList)
        {
            item.Value.Move(Time.deltaTime);
        }
	}
}
