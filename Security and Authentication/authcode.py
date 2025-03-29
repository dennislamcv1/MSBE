import bcrypt
import mysql.connector
from flask import Flask, request, jsonify

app = Flask(__name__)

# Secure database connection
db = mysql.connector.connect(
    host="localhost",
    user="your_user",
    password="your_password",
    database="SafeVault"
)
cursor = db.cursor()

# Hash password securely
def hash_password(password):
    salt = bcrypt.gensalt()
    return bcrypt.hashpw(password.encode('utf-8'), salt)

# Verify user credentials
def verify_user(username, password):
    """✅ Secure Authentication with Hashed Passwords"""
    query = "SELECT Password FROM Users WHERE Username = %s"
    cursor.execute(query, (username,))
    result = cursor.fetchone()

    if result:
        stored_hash = result[0].encode('utf-8')
        return bcrypt.checkpw(password.encode('utf-8'), stored_hash)  # ✅ Secure password verification
    return False

# User login endpoint
@app.route("/login", methods=["POST"])
def login():
    data = request.json
    username = data.get("username")
    password = data.get("password")

    if verify_user(username, password):
        return jsonify({"message": "Login successful"}), 200
    else:
        return jsonify({"error": "Invalid credentials"}), 401

if __name__ == "__main__":
    app.run(debug=True)
