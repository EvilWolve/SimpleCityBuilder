I'll try to keep track of my design decisions and the reasons behind them in this document.

One thing I learnt a long time ago is that nothing in game development is constant, everything can always change at any time. This informs a lot of my decisions. I won't hard-code anything if I can avoid it, I'll try to expose as much as I can to designers and I won't write any restrictions into my code that will prohibit designers from changing things themselves but instead makes it necessary to involve a programmer.

There are a few such restrictions in the design I was given.
1. All common structures have a grid size of 1x1, all unique structures 2x2.
-> I could hard-code this directly, or I could have all structures of a certain type get their grid size from a specific asset. Experience tells me that in a real project, there will be an exception at some point that requires one structure to have a different grid size. I've decided to just expose the grid size in the Unity editor, then I can set the grid size to fit the design but it's easier to change it later if (or when) design changes.

2. All common structures share a color.
-> Again, that's a nice limitation, I could just use a shared material in that case. Instead, I'll expose it to keep my options open.

3. All unique structures have individual, unique popups.
-> This is the reverse of the above points. In my experience, good UI/UX design requires at least some common layout for functionally and contextually similar popups. For example, all building popups should have a close button and it should always be in the same place. I'll go a bit further and also assume that the location of the name and the boundary of the popups will be the same.

I'll probably need access to the BuildingManager and the BuildingConfigurationService in a lot of places. I don't like singletons, but this project is a bit too small to throw an IoC Container at it. I'll make do with a Service Locator.

I don't want to use an enum to identify buildings because then I'd need to add a new value for each new building type. Ideally the behaviour of a building is defined by its components, not by an enum (and hopefully neither by the Building-class alone). Using the BuildingConfiguration ScriptableObject's name, we have an easy identifier that also allows us to directly connect the runtime class and the config. Renaming a ScriptableObject is also far more visible in version control than any other ID-attribute I could pick, so it's easy to figure out which building identifiers have changed between releases.