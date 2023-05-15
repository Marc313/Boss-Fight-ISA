# Boss Fight
Battle against a dangerous monster using mechanics like attack combos, blocking and parrying attacks, and dodging attacks!

With this project, I tried to expand my (limited) knowledge about AI, combat, and animation by combining all of these concepts in a 3D Boss Fight. In this Unity game, the player controls a knight avatar who is battling against a dangerous monster: Rakall, the deprived. This boss fight contains a variety of player and enemy mechanics, e.g. attack combos, blocking and parrying attacks, and dodging attacks.

The boss has four states:
Chase State: The boss walks towards the player using a NavMeshAgent. Switches to Melee Attack State when close to the player, and switches to Ranged Attack State if the boss failed to come close to the player for several seconds.
Melee Attack State: The boss will perform a melee attack combo on the player. Switches to Ranged Attack State after the attack combo is performed.
Ranged Attack State: The boss will perform a ranged attack combo on the player. Switches to Ranged Attack State after the attack combo is performed.
Resting State: The boss tries to maintain a specific distance from the player. Switches to Chase State after a specific cooldown.
