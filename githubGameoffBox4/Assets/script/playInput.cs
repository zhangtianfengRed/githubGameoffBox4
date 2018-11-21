using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playInput : MonoBehaviour {
    ControlObject controlObject;
    Box ghost;
    public GameObject GhostGameobject;
    Receiver receiver = new Receiver();
    Commad commadup;
    Commad commadleft;
    Commad commadright;
    Commad commaddown;
    // Use this for initialization
    void Start () {
        ghost = new Ghost(GhostGameobject);
        controlObject = new ControlObject(ghost);
        commadup = new Moveup(receiver);
        commadleft = new Moveleft(receiver);
        commadright = new MoveRight(receiver);
        commaddown = new MoveDown(receiver);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.W))
        {
            InvokeCommad InvokeCommad = new InvokeCommad(commadup);
            InvokeCommad.Excutecommad();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            InvokeCommad InvokeCommad = new InvokeCommad(commadleft);
            InvokeCommad.Excutecommad();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            InvokeCommad InvokeCommad = new InvokeCommad(commaddown);
            InvokeCommad.Excutecommad();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            InvokeCommad InvokeCommad = new InvokeCommad(commadright);
            InvokeCommad.Excutecommad();
        }
    }
    #region 控制状态的枚举
    enum Control
    {
        Ghost,
        Box
    }
    #endregion

    public abstract class Box
    {
        protected GameObject Object;
        public Box(GameObject gameObject)
        {
            Object = gameObject;
        }
        public GameObject UsegameObject
        {
            get
            {
                return Object;
            }
        }
    }

    public class Ghost : Box
    {
        public Ghost(GameObject gameObject):base(gameObject) { }
    }

    public class BoxObject : Box
    {
        public BoxObject(GameObject gameObject) : base(gameObject) { }
    }

    public class ControlObject
    {
        public Box Box;
        public ControlObject(Box box)
        {
            Box = box;
        }
        public GameObject UseObject()
        {
            return Box.UsegameObject;
        }
    }


    #region 调用对象
    public class InvokeCommad
    {
        public Commad Commad;
        public InvokeCommad(Commad commad)
        {
            Commad = commad;
        }
        public void Excutecommad()
        {
            Commad.Action();
        }
    }
    #endregion

    #region 行为方法
    public class Receiver
    {
        public void up()
        {
            Debug.Log("up");
        }
        public void Down()
        {
            Debug.Log("down");
        }
        public void left()
        {
            Debug.Log("left");
        }
        public void right()
        {
            Debug.Log("right");
        }
    }
    #endregion 
    #region 行为对象化 
    public abstract class Commad
    {
        protected Receiver PlayRecever;

        public Commad(Receiver playRecever )
        {
            PlayRecever = playRecever;
        }
        public virtual void Action() { }
    }
    public class MoveDown : Commad
    {
        public MoveDown(Receiver receiver) : base(receiver) { }
        public override void Action()
        {
            PlayRecever.Down();
        }
    }
    public class Moveup : Commad
    {
        public Moveup(Receiver receiver) : base(receiver) { }
        public override void Action()
        {
            PlayRecever.up();
        }
    }
    public class Moveleft : Commad
    {
        public Moveleft(Receiver receiver) : base(receiver) { }
        public override void Action()
        {
            PlayRecever.left();
        }
    }
    public class MoveRight : Commad
    {
        public MoveRight(Receiver receiver) : base(receiver) { }
        public override void Action()
        {
            PlayRecever.right();
        }
    }
    #endregion
}
