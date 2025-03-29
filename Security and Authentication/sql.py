import mysql.connector

# Establish a secure database connection
db = mysql.connector.connect(
    host="localhost",
    user="your_user",
    password="your_password",
    database="SafeVault"
)
cursor = db.cursor()

def get_user_info(username, email):
    """✅ Secure: Uses Parameterized Queries"""
    query = "SELECT UserID, Username, Email FROM Users WHERE Username = %s AND Email = %s"
    cursor.execute(query, (username, email))  # ✅ Secure against SQL Injection
    result = cursor.fetchone()
    return result


# Example Usage
username = input("Enter username: ")  # User input
email = input("Enter email: ")

user_info = get_user_info(username, email)

if user_info:
    print(f"User found: {user_info}")
else:
    print("User not found.")

# Close the database connection
cursor.close()
db.close()
