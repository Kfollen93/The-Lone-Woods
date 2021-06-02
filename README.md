# The Lone Woods
<img src=https://github.com/Kfollen93/The-Lone-Woods/blob/main/Images/MainWoods.PNG alt="Title Screen">

Play the game online in a web browser (no download necessary) here: <a href="https://kfollen.itch.io/the-lone-woods">The Lone Woods</a> <br>
## Description
<i>*The private repository containing all project files and commit messages may be available upon request.</i> <br>
<br>
You've wandered through a forest for days and arrived at a clearing in the woods. Meet the lone local and assist with a few tasks before continuing your journey onward.

## Development
This project was intended to be a prototype-build to learn about <a href="https://docs.unity3d.com/ScriptReference/Physics.Raycast.html">Raycasts</a>, and more specifically <a href="https://docs.unity3d.com/Manual/class-ScriptableObject.html">Scriptable Objects</a>, in order to create a questing and dialogue system. I wanted to make a questing system for a long time now, and I knew Scriptable Objects would be a great way to do it, but I was having a hard time understanding them from watching tutorials. Therefore, I decided it was best to dive in and learn by doing.

## Learning Outcome
I was able to figure out the Raycast portion of the project quick enough. However, I definitely struggled with implementing the questing system, but I was able to get it done. I spent so much time and energy on this, that I then felt like I got a bit sloppy with my other classes. Although everything works, there would be some things that I would change for future projects. One of those things is how I get/set my references to objects. For example, I had developed the entire game with the player already dragged into the world scene. This caused several headaches and issues when the player would transition into the home, which is a new scene. All of my references had to be reset (the guns on the player's back, the animals detecting the player, etc). This is something I will certainly be aware of for future projects. <br>
<br>
I also found it was useful to go back to my scripts and spend some time cleaning them up. Slowing down and focusing on cleaning the code helped make myself aware of times where I was setting variables as public, when they didn't need to be. This then transitioned into me spending some time reading about the <a href="https://csharpindepth.com/articles/singleton">Singleton Pattern</a> and its uses, and how to properly implement it. Taking the extra time made this project a better experience for the players, and taught me how to become a better developer. Overall, the project was a success and I will continue to use Scriptable Objects whenever I am in need of swapping in/out data.

## Additional Information
<ul>
  <li>Made with: Unity 2020.3.0</li>
</ul>
