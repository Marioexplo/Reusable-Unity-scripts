using UnityEngine;

///<summary>Move an object back and forth</summary>
public static class BackAndForth
{
    ///<summary>It moves the object within the given space</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="speed">Must be positive</param>
    ///<param name="min">The left limit</param>
    ///<param name="max">The right limit</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    ///<param name="direction">Must be 1 or -1</param>
    public static void Move(Transform obj, float speed, float min, float max, bool horizontal, ref int direction)
    {
        obj.position += Delta(speed, direction, horizontal) * Time.deltaTime;
        if (horizontal)
        {
            if (obj.position.x < min)
            {
                direction = 1;
            }
            if (obj.position.x > max)
            {
                direction = -1;
            }
        }
        else
        {
            if (obj.position.y < min)
            {
                direction = 1;
            }
            if (obj.position.y > max)
            {
                direction = -1;
            }
        }
    }

    ///<summary>It moves the object within the given space and store the movement of 1s</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="speed">Must be positive</param>
    ///<param name="min">The left limit</param>
    ///<param name="max">The right limit</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    ///<param name="direction">Must be 1 or -1</param>
    ///<param name="delta">Store the position variation of 1s</param>
    public static void Move(Transform obj, float speed, float min, float max, bool horizontal, ref int direction, ref Vector3 delta)
    {
        Move(obj, speed, min, max, horizontal, ref direction);
        delta = Delta(speed, direction, horizontal);
    }

    ///<summary>It moves the object horizontally flipping to face the direction of movement</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="speed">Must be positive</param>
    ///<param name="min">The left limit</param>
    ///<param name="max">The right limit</param>
    ///<param name="direction">Must be 1 or -1</param>
    ///<param name="faceRight">If the object faces right</param>
    ///<param name="originalScale">The scale of the object before this function is called for the first time</param>
    public static void Move(Transform obj, float speed, float min, float max, ref int direction, bool faceRight, Vector3 originalScale)
    {
        obj.position += Delta(speed, direction, true) * Time.deltaTime;
        if (obj.position.x < min)
        {
            direction = 1;
            if (!faceRight)
            {
                originalScale.x *= -1;
            }
            obj.localScale = originalScale;
        }
        if (obj.position.x > max)
        {
            direction = -1;
            if (faceRight)
            {
                originalScale.x *= -1;
            }
            obj.localScale = originalScale;
        }
    }

    private static Vector3 Delta(float speed, int dir, bool hor)
    {
        if (hor)
        {
            return new Vector3(speed * dir, 0, 0);
        }
        else
        {
            return new Vector3(0, speed * dir, 0);
        }
    }

    ///<summary>It moves the object within the given space, must already be moving</summary>
    ///<param name="rb">The rigidbody of the object to move</param>
    ///<param name="speed">Must be positive</param>
    ///<param name="min">The left limit</param>
    ///<param name="max">The right limit</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    ///<param name="direction">Must be 1 or -1</param>
    public static void Move(Rigidbody2D rb, float speed, float min, float max, bool horizontal, ref int direction)
    {
        Transform obj = rb.transform;
        if (horizontal)
        {
            if (obj.position.x < min)
            {
                direction = 1;
                rb.linearVelocityX = speed * direction;
            }
            if (obj.position.x > max)
            {
                direction = -1;
                rb.linearVelocityX = speed * direction;
            }
        }
        else
        {
            if (obj.position.y < min)
            {
                direction = 1;
                rb.linearVelocityY = speed * direction;
            }
            if (obj.position.y > max)
            {
                direction = -1;
                rb.linearVelocityY = speed * direction;
            }
        }
    }

    ///<summary>It moves the object horizontally flipping to face the direction of movement, must already be moving</summary>
    ///<param name="rb">The rigidbody of the object to move</param>
    ///<param name="speed">Must be positive</param>
    ///<param name="min">The left limit</param>
    ///<param name="max">The right limit</param>
    ///<param name="direction">Must be 1 or -1</param>
    ///<param name="faceRight">If the object faces right</param>
    ///<param name="originalScale">The scale of the object before this function is called for the first time</param>
    public static void Move(Rigidbody2D rb, float speed, float min, float max, ref int direction, bool faceRight, Vector3 originalScale)
    {
        Transform obj = rb.transform;
        if (obj.position.x < min)
        {
            direction = 1;
            if (!faceRight)
            {
                originalScale.x *= -1;
            }
            obj.localScale = originalScale;
            rb.linearVelocityX = speed * direction;
        }
        if (obj.position.x > max)
        {
            direction = -1;
            if (faceRight)
            {
                originalScale.x *= -1;
            }
            obj.localScale = originalScale;
            rb.linearVelocityX = speed * direction;
        }
    }
}

[System.Serializable]
/// <summary>Move an object <see cref="BackAndForth"/> more easily</summary>
public struct PBackAndForth
{
    public float speed;
    public float minPos;
    public float maxPos;
    public int direction;

    /// <param name="Speed">The speed on the axis. Should be positive</param>
    /// <param name="MinPos">The lower position value on the axis</param>
    /// <param name="MaxPos">The greater position value on the axis</param>
    /// <param name="Direction">-1 for left or down; 1 for right or up</param>
    public PBackAndForth(float Speed, float MinPos, float MaxPos, int Direction)
    {
        speed = Speed;
        minPos = MinPos;
        maxPos = MaxPos;
        direction = Direction;
    }

    ///<summary>It moves the object within the given space</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    public void Move(Transform obj, bool horizontal)
    {
        BackAndForth.Move(obj, speed, minPos, maxPos, horizontal, ref direction);
    }

    ///<summary>It moves the object within the given space and store the movement of 1s</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    ///<param name="delta">Store the position variation of 1s</param>
    public void Move(Transform obj, bool horizontal, ref Vector3 delta)
    {
        BackAndForth.Move(obj, speed, minPos, maxPos, horizontal, ref direction, ref delta);
    }

    ///<summary>It moves the object horizontally flipping to face the direction of movement</summary>
    ///<param name="obj">The transform of the object to move</param>
    ///<param name="faceRight">If the object faces right</param>
    ///<param name="originalScale">The scale of the object before this function is called for the first time</param>
    public void Move(Transform obj, bool faceRight, Vector3 originalScale)
    {
        BackAndForth.Move(obj, speed, minPos, maxPos, ref direction, faceRight, originalScale);
    }

    ///<summary>It moves the object within the given space</summary>
    ///<param name="rb">The rigidbody of the object to move</param>
    ///<param name="horizontal">If the movement is horizontal</param>
    public void Move(Rigidbody2D rb, bool horizontal)
    {
        BackAndForth.Move(rb, speed, minPos, maxPos, horizontal, ref direction);
    }

    ///<summary>It moves the object horizontally flipping to face the direction of movement</summary>
    ///<param name="rb">The rigidbody of the object to move</param>
    ///<param name="faceRight">If the object faces right</param>
    ///<param name="originalScale">The scale of the object before this function is called for the first time</param>
    public void Move(Rigidbody2D rb, bool faceRight, Vector3 originalScale)
    {
        BackAndForth.Move(rb, speed, minPos, maxPos, ref direction, faceRight, originalScale);
    }
}
