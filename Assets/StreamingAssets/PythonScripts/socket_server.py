import socket
import json
import time
import sys
import threading

# Define the server's host and port
HOST = '127.0.0.1'  # Localhost (works only on your machine)
PORT = 65432        # Port number to listen on (this can be any valid port number)

# Separate communication logic for each team
def handle_team1_action(conn):
    action = {"team": "team1", "action": "MoveForward", "player_id": 1}
    conn.sendall(json.dumps(action).encode())  # Send action to Unity

def handle_team2_action(conn):
    action = {"team": "team2", "action": "MoveBackward", "player_id": 1}
    conn.sendall(json.dumps(action).encode())  # Send action to Unity

# Function to handle each team connection in a separate thread
def handle_client(conn, addr):
    print(f"Connected by {addr}")
    try:
        while True:
            data = conn.recv(1024)  # Receive data from Unity
            if not data:
                break  # If no data, break the loop

            team_request = data.decode()  # Decode the incoming request
            print(f"Received request: {team_request}")

            # Handle the received request based on the team
            if team_request == 'team1':
                handle_team1_action(conn)
            elif team_request == 'team2':
                handle_team2_action(conn)
            else:
                error_response = {"error": "Unknown team"}
                conn.sendall(json.dumps(error_response).encode())

            time.sleep(0.1)  # Prevent overloading the connection
    except Exception as e:
        print(f"Error with {addr}: {e}")
    finally:
        conn.close()

def start_server():
    # Create a TCP/IP socket
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen()

        print(f"Server listening on {HOST}:{PORT}...")

        # Accept connections for both teams
        while True:
            conn, addr = s.accept()
            threading.Thread(target=handle_client, args=(conn, addr)).start()  # Start a new thread for each connection

if __name__ == "__main__":
    start_server()
