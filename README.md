# Unity Slot Machine

A Unity slot machine assignment in which the player selects a bet and spins three reels. A win occurs when all three reels stop on the same symbol.

## Features

- Three animated slot reels
- Continuous looping reel movement while spinning
- Random symbol outcome generation
- Sequential reel stopping and target alignment
- Winning-combination detection
- Betting and virtual wallet/credit system
- Symbol-based payout multipliers
- Win and loss messages
- Betting buttons disabled while the machine is spinning
- Button animation and input feedback

## Architecture

| Class | Responsibility |
| --- | --- |
| `MachineBrain` | Coordinates the game flow, starts the reels, generates results, stops reels sequentially, and handles the win/loss flow. |
| `ReelSpin` | Controls the movement, looping, and stopping/alignment of one reel. |
| `SlotOutcomeGenerator` | Generates random symbol IDs using Unity's random number generator. |
| `WinManager` | Checks whether all three reel results form a winning combination. |
| `PayManager` | Manages the bet amount, wallet/credits, payout calculation, and symbol multipliers. |
| `UiManager` | Handles betting button input, UI interaction, wallet and message display, and button animation. |

## Payout System

The player starts with virtual credits. The selected bet is debited from the wallet before the spin. When all three reels match, the payout is calculated as:

```text
Payout = Bet Amount × Symbol Multiplier
```

| Symbol ID | Symbol | Multiplier |
| ---: | --- | ---: |
| 0 | Seven | 10x |
| 1 | Cherry | 2x |
| 2 | Bell | 3x |
| 3 | BAR | 5x |

This project uses virtual credits only and does not involve real-money gambling.

## Technical Approach

- Each reel continuously moves its symbols downward while spinning. Symbols that pass the lower boundary are moved above the highest symbol to create a loop.
- `SlotOutcomeGenerator` selects a random symbol ID for each reel using Unity's random number generator.
- When a result is assigned, `ReelSpin` moves the complete reel by the required offset so the selected symbol aligns with the target position.
- `MachineBrain` starts all reels together, assigns results with a delay between reels, and uses `WinManager` to check whether all three IDs match.
- `PayManager` debits bets, tracks wallet credits, calculates winning payouts, and applies the symbol multiplier.
- Events connect the UI and gameplay systems: betting input is sent from `UiManager` to `MachineBrain`, while messages, wallet updates, and machine interaction state are sent back to the UI.

## Project Structure

Relevant project files are organized as follows:

```text
Assets/
├── Animations/
├── Prefabs/
├── Scenes/
├── Scripts/
│   ├── MachineBrain.cs
│   ├── ReelSpin.cs
│   ├── SlotOutcomeGenerator.cs
│   ├── WinManager.cs
│   ├── PayManager.cs
│   └── UiManager.cs
├── Sounds/
└── UI/
```

The current Unity scripts are located under `_Scripts` in this repository. The structure above is a simplified overview of the relevant asset categories.

## How to Run

1. Clone or download this repository.
2. Open the project in Unity `[Unity Version]`.
3. Open the project's main scene from the `Scenes` folder, or open the scene configured for the project in Build Settings.
4. Press the Unity Play button.
5. Select a bet to spin the machine. The buttons are unavailable until the spin sequence finishes.

### WebGL Build

A WebGL build is included, open it in a browser using the published build link below:

**WebGL build:** https://punitsehrawat16.itch.io/jackpot-machine


## Design / Thought Process

- Responsibilities are separated across gameplay, reel, outcome, win, payment, and UI systems.
- `MachineBrain` acts as the coordinator instead of containing every piece of logic.
- Reel movement stays inside `ReelSpin`, while random outcome generation is separate from visual movement.
- Win checking and payout calculation are handled by their own classes.
- Events allow the UI and machine coordinator to communicate without tightly coupling their implementation details.

## Bonus Features

No bonus features are currently included beyond the core slot machine gameplay described above.

## Git / Development

The project was developed with meaningful commits and organized into separate systems for gameplay flow, reel movement, payments, and UI interaction.

## Links

- **WebGL build:** (https://punitsehrawat16.itch.io/jackpot-machine)

## Author

Punit Sehrawat
