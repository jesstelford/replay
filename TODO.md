- Split game objects into 3 parts:
  1. a "Controller"
    - Responsible for all the redux stuff
  2. a "Input"
    - Responsible for handling iput and updating physics stuff. Triggers actions
      on the "Controller"
  3. a "Renderer"
    - Responsible for subscribing to state changes (via "Controller") and
      re-rendering

- Add correct scripts to correct game objects
- Find way to not access methods on Game.Instance from deep in Renderer




- Separate physics updates from position of shooter displayed on screen (make
  them child objects of a parent object)
