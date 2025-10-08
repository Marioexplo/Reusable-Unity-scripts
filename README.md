# Reusable Unity scripts
Here I leave some scripts for Unity I think can be useful.  
I have to warn that I'm not an experienced developer but I still hope that by creating this repository, I'll help someone.
## FollowCamera
It makes the given camera follow the object the __*MonoBehaviour*__ is on. You can decide the ratio, a number between 0 and 1 which defines how fast the acceleration will be, and you can set borders outside of which the camera can't go.
## TextScroll
This namespace contains a main __*MonoBehaviour*__ which does the text-scrolling, and some other extensions:
### TextScroller
A __*MonoBehaviour*__ to scroll texts for all situations. You simply have to set the time to scroll one letter and then the text(s) to be scrolled. There are 3 modes:
* Simple: just load one *simple* string.
* Array: if you have a bunch of texts that you want to be scrolled, group them in an *array*: you can now change the text to be scrolled by just knowing its index in the *array*, or you can activate *AutoArray* to automatically pass from one text to the following one.
* Branches: *Branch* is a *struct* containing a string for the name of the branch, and another one for the actual text. By loading a list of *Branch*es, you can change the active text by giving the name of the branch.
### AutoScroll
Use **AutoScroll** and **AutoScrollTMPro** to automatically apply the scrolled text respectively to *legacy text* or *TextMeshProUGUI*. If you need to pass the scrolled text to something else, you can make your own auto-scroller with **AutoScrollBase**.
### ScrollActions
Use this ***MonoBehaviour*** to pass methods to be executed when **TextScroller** has finished scrolling.
### Editor
This script is needed to manage the **TextScroller**'s *Inspector* view.
## SafeDestroy
Destroy a *Behaviour* or a *GameObject* after deactivating it, this way you ensure no more actions will be executed by these.
## ImageFiller
You can use this ***MonoBehaviour*** to visually fill an *Image* (in *Fill* mode). You can set two speeds: one for when the fill amount is increasing, and one for when it's decreasing.
## SimpleSlider
Am I the only one, or the UI components of Unity are incredibly complicated and full of options you usually don't need? I don't know but I made this slider ***MonoBehaviour***, which doesn't have as many options as the one provided by Unity but I think it's much more straightforward to use.  
There is also an ***Editor*** script which manages the target image' fill amount from the **SimpleSlider** and it provides an option to create a *GameObject* with all the components put ideally to work with **SimpleSlider**.
## Timer
Does it ever happen to you to declare a `float` and just use it like this:
```
timer += Time.deltaTimer;
if (timer > time)
{
  ...
  timer = 0;
}
```
To me, a lot. So I created this ***struct*** to shorten it into this:
```
if (timer++ > time)
{
  ...
  timer--;
}
```
## Direction
It happens to me often to use an `int` to only store `1` and `-1`, and then having to compare it to get the direction as a boolean. So I made this ***struct*** to store a `sbyte` (lighter than `int`) and then just use it as an `int` or return a `bool`.
___
You can use these scripts as you wish. You can also modify them to suit your needs or just take inspiration from them.  

By Marioexplo.