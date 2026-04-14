# States of the Craps Table

|                                    | Puck OFF      | Puck ON       |
|------------------------------------|:-------------:|:-------------:|
| Accepting players                  |               |               |
| Accepting bets                     |               |               |
| Rolling dice, announcing outcomes  |               |               |
| Cashing out / removing players     |               |               |
| Removing bets                      |               |               |
| Pausing bets                       |               |               |

Asking for roller



# States of the Player

|                                    | Puck OFF      | Puck ON       |
|------------------------------------|:-------------:|:-------------:|
| CRUD BETS                          |               |               |
|  - take down bets                  |               |               |
|  - add bets                        |               |               |
|  - parlay bets                     |               |               |
| Removing bets                      |               |               |
| Pausing bets                       |               |               |



What should the bets be able to do?
States:
Return winnings (the base state) (evaluate bets and payout "like normal")
Be paused (not affected by play) (don't evaluate bets)
Be parlayed (until acted upon otherwise) (evaluate bets and add winnings to committed amount)

Property
IsEnabled (at the table level) (e.g. no place bets allowed when the point is off)
(if the player must have anotehr bet active, then we check the player's bet list for that bet, e.g. standing pass line bet needed to bet on points)

Methods:

Return some amount (instigated by player)
Be taken down (a method.i.e. I am done) / lose


Rather than each bet itself being a subscriber, the states of the bet will manage the bet's subscription.
Thus, by virtue of being subscribed or unsubscribed, a bet will become active or paused, respectively.



To do:
main game loop (e.g. add two players)
one is the roller 

rolling/announcing results
CRUD-ing bets (creating bets for now)
(switching rollers can come later)