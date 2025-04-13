from flask import Flask, jsonify
import os

app = Flask(__name__)

# Change this to your Grasshopper file location
FILE_PATH = r"/Users/ksu/Desktop/status.txt"


# Check for file on startup
def ensure_state_file():
    if os.path.exists(FILE_PATH):
        print("File exists")
    else:
        try:
            os.makedirs(os.path.dirname(FILE_PATH), exist_ok=True)
            with open(FILE_PATH, "w") as f:
                f.write("off")
            print(f"Created file at {FILE_PATH} with default state 'off'")
        except Exception as e:
            print(f"Error creating file: {e}")


# Call this once when the app starts
ensure_state_file()


@app.route("/turn_on", methods=["POST"])
def turn_on():
    try:
        with open(FILE_PATH, "w") as f:
            f.write("on")
        return jsonify({"status": "success", "state": "on"}), 200
    except Exception as e:
        return jsonify({"status": "error", "message": str(e)}), 500


@app.route("/turn_off", methods=["POST"])
def turn_off():
    try:
        with open(FILE_PATH, "w") as f:
            f.write("off")
        return jsonify({"status": "success", "state": "off"}), 200
    except Exception as e:
        return jsonify({"status": "error", "message": str(e)}), 500


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5555, debug=True)
