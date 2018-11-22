using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block
{
    public static float COLLISION_SIZE = 1.1f;
    public static float VANISH_TIME = 3.0f;



    public struct iPosition
    {
        public int x;
        public int y;
    }

   

    public enum COLOR
    {
        NONE = -1,
        PINK = 0,
        BLUE,
        YELLOW,
        GREEN,
        MAGENTA,
        GRAY,
        NORMAL_COLOR_NUM=GRAY,
    }

    public enum STEP {
        NONE=-1,
        IDLE=0,
        GRABBED,
        RELEASED,
        SLIDE,
        VACANT,
        RESPAWN,
        FALL,
        LONG_SLIDE,
        NUM,
    }
 

    public static int BLOCK_NUM_X = 5;
    public static int BLOCK_NUM_Y = 5;

    
}

public class BlockControl : MonoBehaviour {

    public Block.COLOR color = (Block.COLOR)0;

    public BlockRoot block_root = null;

    public Block.iPosition i_pos;

    public Block.STEP step = Block.STEP.NONE;

    public Block.STEP next_step = Block.STEP.NONE;

    private Vector3 position_offset_initial = Vector3.zero;

    public Vector3 position_offset = Vector3.zero;





    




    // Use this for initialization
    void Start () {

        this.setColor(this.color);
        this.next_step = Block.STEP.IDLE;
        


    }
	
	// Update is called once per frame
	void Update () {


        Vector3 mouse_position;
        this.block_root.unprojectMousePosition(out mouse_position, Input.mousePosition);

        Vector2 mouse_position_xy = new Vector2(mouse_position.x, mouse_position.y);

        while (this.next_step != Block.STEP.NONE)
        {
            this.step = this.next_step;
            this.next_step = Block.STEP.NONE;

            switch (this.step)
            {
                case Block.STEP.IDLE:
                    this.position_offset = Vector3.zero;
                    this.transform.localScale = Vector3.one * 1.0f;
                    break;
                case Block.STEP.GRABBED:
                    this.transform.localScale = Vector3.one * 1.2f;

                   // Debug.Log("" + BlockRoot.Instance.DeIndex);
                    Destroy(this.gameObject);
                    //Debug.Log("" + BlockRoot.Instance.BIndex);


                    break;
                case Block.STEP.RELEASED:
                    this.position_offset = Vector3.zero;

                    this.transform.localScale = Vector3.one * 1.0f;
                    break;
            }
        }

      
        Vector3 position = BlockRoot.calcBlockPosition(this.i_pos) + this.position_offset;


        this.transform.position = position;

    }

    public void beginGrab()
    {
        this.next_step = Block.STEP.GRABBED;

    }

    public void endGrab()
    {
        this.next_step = Block.STEP.IDLE;

    }

    public bool isGrabbable()
    {
        bool is_grabble = false;
        switch (this.step)
        {
            case Block.STEP.IDLE:
                is_grabble = true;
                break;
        }
        return (is_grabble);
    }

    public bool isContainedPosition(Vector2 position)
    {
        bool ret = false;
        Vector3 center = this.transform.position;
        float h = Block.COLLISION_SIZE / 2.0f;
        do
        {

            if (position.x < center.x - h || center.x + h < position.x)
            {
                break;
            }

            if (position.y < center.y - h || center.y + h < position.y)
            {
                break;
            }

            ret = true;
        } while (false);
        return (ret);
    }





    public void setColor(Block.COLOR color)
    {
        this.color = color;

        Color color_value;
        switch (this.color)
        {
            default:
            case Block.COLOR.PINK:
                color_value = new Color(1.0f, 0.5f, 0.5f);
                break;
            case Block.COLOR.BLUE:
                color_value = Color.blue;
                break;
            case Block.COLOR.YELLOW:
                color_value = Color.yellow;
                break;
            case Block.COLOR.GREEN:
                color_value = Color.green;
                break;
            case Block.COLOR.MAGENTA:
                color_value = Color.magenta;
                break;
        }

       

        this.GetComponent<Renderer>().material.color = color_value;
    }
    /*
    public void setNumder()
    {

        number_Index += 1;
        num.Add(number_Index);
    }
    */
  


   
}
