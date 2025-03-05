# ⚓ Battleship Game
In this game, a single player competes against the computer. Each player views the opponent's board without seeing any images of their fleet. <br/>
The player can see their own board with their fleet's layout and the opponent’s hits.

###  🛳 Ship Placement Instructions::
Before the game starts, the human player must arrange their fleet on their private **10x10** grid. The fleet consists of **4** ships of varying sizes. <br/>
The player can place ships by selecting grid squares, either by dragging and dropping or selecting squares (depending on the game interface). Once a ship is placed, it cannot be moved unless the player chooses to remove it and place it again.

### 🚢 Ship Placement Rules:
1. Ships can be placed **horizontally** or **vertically**, but **not diagonally**.
2. Ships must **not overlap** each other.
3. Ships must **not be placed adjacent** to each other.
4. Ships must be placed **entirely within the boundaries** of the grid, without extending beyond it.
     
     ![image](https://github.com/user-attachments/assets/6e4ee84a-79bc-4370-991f-eb99fae5a89e)
   

### 🎯 Game Flow
- After both players have confirmed their boards by clicking the **"START"** button, the game will begin, with the **human player going first**.
- The player will see a matrix with coordinates and must **click on a square** where they believe an opponent's ship is located. This click is an attempt to **hit**.
- Ensure that the click is **within the opponent's grid** and that the player has **not yet hit this square**.
- Each attempt, whether **successful or not**, will result in the square being blocked (**it cannot be clicked again**).
- If the player **hits a ship**, a **fire icon 🔥** will be displayed on the square. If the player **misses**, a **miss symbol ❌** will be shown.
- Additionally, if a player makes a correct guess, they will be granted an extra turn.
- A ship will be considered completely destroyed if all squares it occupies are marked.
- In every square **surrounding a completely destroyed ship**, a **special symbol 🚫** will appear to indicate that this square **cannot be hit**. This symbol also shows that the player did not hit a ship in the current square. <br/>
      **❌ (Miss Symbol) will appear when:** <br/>
       - The player attempted to hit a square but **missed**. <br/>
       - The player has **destroyed the entire ship**, so the symbol **❌** will appear around all adjacent squares.
     
     **🔥 (Hit Symbol) will appear when:**  <br/>
        - The player successfully **hits a square containing a ship**.
  
### 🏆 Win Condition
- The player who **successfully sinks all of the opponent's ships first** will be declared **the winner**. 🎉

### 💀 Loss Condition
- The player who **fails to discover and destroy all of the opponent's ships** will be **the loser**. ❌

---

### 🎮 Game Ending:
Once a player wins by sinking all the opponent's ships, the game will display the winner, and a prompt will appear to either **restart the game** or **exit**.

