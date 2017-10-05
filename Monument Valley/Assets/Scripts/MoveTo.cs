using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {
    public class MoveOptions
    {
        public GameObject GameObject;
        public Vector3 Destination;
        public float Time;
        public float StartTime;
        public float RunTime;
        public Func<float, float> TimeFunction;
        public MoveOptions(GameObject gameObj, Vector3 offset, float time)
        {
            this.GameObject = gameObj;
            this.Destination = offset;
            this.Time = time;
            this.TimeFunction = (x) =>
            {
                return x;
            };
        }
        public MoveOptions(GameObject gameObj,Vector3 dst, float time, Func<float,float> timeFunction)
        {
            this.GameObject = gameObj;
            this.Destination = dst;
            this.Time = time;
            this.TimeFunction = timeFunction;
        }
        public void Move(float dt)
        {
            var x = (RunTime + dt) / Time;
            x = TimeFunction(x);
            var lastX = RunTime / Time;
            lastX = TimeFunction(lastX);
            if (x >= 1)
            {
                x = 1;
                var dx = (x - lastX) / (1 - lastX);
                var dv = (Destination - GameObject.transform.position) * dx;
                GameObject.transform.Translate(dv);
                if (OnFinished != null)
                    OnFinished.Invoke(this, new EventArgs());
            }
            else
            {
                var dx = (x - lastX) / (1 - lastX);
                var dv = (Destination - GameObject.transform.position) * dx;
                GameObject.transform.Translate(dv);
            }
            
            RunTime += dt;
        }
        public event EventHandler OnFinished;
    }
    public static Dictionary<GameObject, MoveOptions> MoveList = new Dictionary<GameObject, MoveOptions>();
    public static void Start(MoveOptions options)
    {
        MoveList[options.GameObject] = options;
        options.OnFinished += (sender, e) =>
        {
            MoveTo.MoveList.Remove((sender as MoveTo.MoveOptions).GameObject);
        };
    }
    public static void Stop(GameObject gameObj)
    {
        if (MoveList.ContainsKey(gameObj))
            MoveList.Remove(gameObj);
    }
	// Use this for initialization
	void Start () {
        //MoveList = new Dictionary<GameObject, MoveOptions>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var item in MoveList)
        {
            if(!item.Key)
            {
                MoveList.Remove(item.Key);
            }
            try
            {
                item.Value.Move(Time.deltaTime);
            }
            catch (Exception ex)
            {
                MoveList.Remove(item.Key);
            }
        }
	}
}
