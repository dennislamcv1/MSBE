import re
import html
from flask import Flask, request, jsonify

app = Flask(__name__)

def sanitize_input(user_input):
    """Sanitizes user input by escaping HTML and removing malicious characters."""
    user_input = html.escape(user_input)  # Prevents XSS
    user_input = re.sub(r"[;'\"--]", "", user_input)  # Removes SQL injection attempts
    return user_input.strip()

@app.route("/submit", methods=["POST"])
def submit():
    username = sanitize_input(request.form.get("username", ""))
    email = sanitize_input(request.form.get("email", ""))

    # Simple email validation
    if not re.match(r"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", email):
        return jsonify({"error": "Invalid email format"}), 400

    return jsonify({"message": "Input sanitized successfully", "username": username, "email": email})

if __name__ == "__main__":
    app.run(debug=True)
