DISCLAIMERS:
1: While I have written generic pooling systems before, I used the pooling system found at https://liamederzeel.com/a-generic-object-pool-for-unity3d/ for this project.
However, I developed the lazy pooling script during the duration of this project.

2: At some points in this project, I call GameObject.Find() without caching (but never in Update); I am aware that this will become unoptomized in medium and larger projects. 
If this were a bigger project, I would refactor the code to hold references to the objects which I wish to find, so I am not searching through all loaded objects. I would also cache the results.

3: I pulled a couple assets from my library to make this project. For sake of speed, I pulled the whole asset in. I am aware that this bloats projects, and for bigger projects, I would create a new project, pull in the asset, and copy over the files I need from the new project to the main one.