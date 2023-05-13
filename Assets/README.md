While I have written generic pooling systems before, I used the pooling system found at https://liamederzeel.com/a-generic-object-pool-for-unity3d/ for this project.
However, I developed the lazy pooling script during the duration of this project.

At some points in this project, I call GameObject.Find() without caching (but never in Update); I am aware that this will become unoptomized in medium and larger projects. 
If this were a bigger project, I would refactor the code to hold references to the objects which I wish to find, so I am not searching through all loaded objects. I would also cache the results.