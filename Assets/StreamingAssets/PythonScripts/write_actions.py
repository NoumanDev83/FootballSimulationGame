import os
import json

# Sample actions for players (update as per your needs)
actions = [
    {"team": "team1", "action": "MoveForward", "player_index": 0},
    {"team": "team1", "action": "Shoot", "player_index": 1},
    {"team": "team2", "action": "Pass", "player_index": 2},
    {"team": "team2", "action": "MoveBackward", "player_index": 3}
]

# Define the directory where the JSON file will be saved
directory = 'D:/Projects/FootballGame/Assets/StreamingAssets/PythonScripts'
file_path = os.path.join(directory, 'player_actions.json')

# Ensure the directory exists. If not, create it.
if not os.path.exists(directory):
    os.makedirs(directory)

# Write the actions to the player_actions.json file
with open(file_path, 'w') as json_file:
    json.dump(actions, json_file, indent=4)

print(f"Actions successfully saved to {file_path}")
