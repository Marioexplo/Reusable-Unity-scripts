# Reusable-Unity-scripts
Here I leave some Unity scripts I think can be useful. I'm only at the beginning of my journey with Unity and C# and, until now, the only games I made were experiments, so I haven't published anything, yet.
Anyway, before focusing on the next game I have in mind, I wanted to make some scripts I can reuse in the future. I'm sure I haven't created anything new but I can't find any reason why I shouldn't share my code here.

### Description and how-to-use:
- **BackAndForth:** this class contains static functions you can always use without needing to store it in a variable. You can:
  * Move back and forth on the vertical or horizzontal axis. Perfect for moving platforms.
  * Do that and store the movement value in a variable. Perfect if you need to move a character on the moving platform.
  * Move back and forth horizzontally and mirror the object to face the movement direction.
  * Do the first and the third with a rigidbody.
- **PBackAndForth:** the *P* stands for *Personal*, in fact it works as (and with) BackAndForth but you can set speed and minimum and maximum postion, and you don't need a direction variable to reference.
- **FollowCamera**(2D)**:** it makes the given camera follow the object the MonoBehaviour is on. You can decide the ratio, a number between 0 and 1 which defines how fast the acceleration will be, and you can set borders outside of which the camera can't go.
- **TextScroll:** a component to scroll texts of different kinds, for all situations. You just have to set the time to scroll a letter and then set the text(s), which are of different types:
  * A simple string ("simple");
  * An array of strings ("array");
  * An array of Branches ("structure").
  - **AutoArray:** when you use the *array* type, you can enable this to automatically pass from a text of the array to the next one. You can also set the time this action will take.
  - **Branch:** this is a custom struct which contains a name (of the branch) and the text which will be scrolled. It works as the *array* but instead of setting the array's number of the text to scroll, you'll set the branch' name.
