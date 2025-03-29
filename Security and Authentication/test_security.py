import unittest
import html
from your_script import sanitize_input, get_user_info

class TestSecurity(unittest.TestCase):

    def test_sql_injection_attempt(self):
        """Test that SQL injection attempts are neutralized."""
        malicious_input = "' OR '1'='1' --"
        sanitized = sanitize_input(malicious_input)
        self.assertNotIn("'", sanitized)
        self.assertNotIn("--", sanitized)
        
        # Attempt SQL Injection
        result = get_user_info(malicious_input, "test@example.com")
        self.assertIsNone(result, "SQL Injection should not return data")

    def test_xss_attempt(self):
        """Test that XSS attempts are neutralized."""
        malicious_input = "<script>alert('Hacked!')</script>"
        sanitized = sanitize_input(malicious_input)
        
        # Ensure HTML is escaped
        self.assertNotEqual(malicious_input, sanitized)
        self.assertEqual(sanitized, html.escape(malicious_input))
        
    def test_valid_email(self):
        """Test valid email format."""
        valid_email = "user@example.com"
        self.assertRegex(valid_email, r"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")

if __name__ == "__main__":
    unittest.main()
