import unittest
from flask import app, verify_user  # Import your Flask app and functions

class TestAuthSystem(unittest.TestCase):

    def setUp(self):
        """Set up test client before each test"""
        self.client = app.test_client()
        self.client.testing = True

    def test_valid_login(self):
        """Test valid login with correct credentials"""
        response = self.client.post("/login", json={
            "username": "admin_user",
            "password": "securepassword"
        })
        self.assertEqual(response.status_code, 200)
        self.assertIn("Login successful", response.get_json()["message"])

    def test_invalid_login(self):
        """Test login with incorrect credentials"""
        response = self.client.post("/login", json={
            "username": "non_existent_user",
            "password": "wrongpassword"
        })
        self.assertEqual(response.status_code, 401)
        self.assertIn("Invalid credentials", response.get_json()["error"])

    def test_admin_access_denied_for_user(self):
        """Ensure a normal user cannot access the admin dashboard"""
        response = self.client.get("/admin_dashboard", json={
            "username": "regular_user",
            "password": "userpassword"
        })
        self.assertEqual(response.status_code, 403)
        self.assertIn("Forbidden", response.get_json()["description"])

    def test_admin_access_granted(self):
        """Ensure an admin user can access the admin dashboard"""
        response = self.client.get("/admin_dashboard", json={
            "username": "admin_user",
            "password": "securepassword"
        })
        self.assertEqual(response.status_code, 200)
        self.assertIn("Welcome to the Admin Dashboard!", response.get_json()["message"])

    def test_user_dashboard_access(self):
        """Ensure a regular user can access the user dashboard"""
        response = self.client.get("/user_dashboard", json={
            "username": "regular_user",
            "password": "userpassword"
        })
        self.assertEqual(response.status_code, 200)
        self.assertIn("Welcome to your User Dashboard!", response.get_json()["message"])

if __name__ == "__main__":
    unittest.main()
