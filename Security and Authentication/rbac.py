import bcrypt
import mysql.connector
from flask import Flask, request, jsonify, abort

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

# Verify user credentials and fetch role
def verify_user(username, password):
    query = "SELECT Password, Role FROM Users WHERE Username = %s"
    cursor.execute(query, (username,))
    result = cursor.fetchone()

    if result:
        stored_hash, role = result
        if bcrypt.checkpw(password.encode('utf-8'), stored_hash.encode('utf-8')):
            return role  # Return the user's role if credentials are correct
    return None

# Role-based access control decorator
def requires_role(role):
    def decorator(f):
        def wrapper(*args, **kwargs):
            username = request.json.get("username")
            password = request.json.get("password")
            
            role = verify_user(username, password)
            if role != role:
                abort(403, description="Forbidden: You don't have permission to access this resource.")
            return f(*args, **kwargs)
        return wrapper
    return decorator

# Admin Dashboard - protected route
@app.route("/admin_dashboard", methods=["GET"])
@requires_role("admin")  # Only allow access for 'admin' role
def admin_dashboard():
    return jsonify({"message": "Welcome to the Admin Dashboard!"})

# User Dashboard - protected route
@app.route("/user_dashboard", methods=["GET"])
@requires_role("user")  # Only allow access for 'user' role
def user_dashboard():
    return jsonify({"message": "Welcome to your User Dashboard!"})

# User login endpoint
@app.route("/login", methods=["POST"])
def login():
    data = request.json
    username = data.get("username")
    password = data.get("password")

    role = verify_user(username, password)
    if role:
        return jsonify({"message": "Login successful", "role": role}), 200
    else:
        return jsonify({"error": "Invalid credentials"}), 401

if __name__ == "__main__":
    app.run(debug=True)
