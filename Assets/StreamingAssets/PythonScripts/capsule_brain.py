import random
import sys

def get_next_position():
    # Generate a random direction and distance between 5 and 10 units
    distance = random.uniform(5, 10)
    x = random.uniform(-1, 1) * distance
    y = 0
    z = random.uniform(-1, 1) * distance
    return x, y, z

if __name__ == "__main__":
    x, y, z = get_next_position()
    print(f"{x},{y},{z}")  # Print coordinates in comma-separated format
    sys.stdout.flush()  # Force output to be written (important!)
