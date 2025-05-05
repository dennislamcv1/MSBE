-- Analyze the Provided SQL Query

SELECT p.ProductName, SUM(o.Quantity) AS TotalSold
FROM Orders o
JOIN Products p ON o.ProductID = p.ProductID
WHERE p.Category = 'Electronics'
GROUP BY p.ProductName
ORDER BY TotalSold DESC;

-- Copilot’s Optimization Suggestions

SELECT p.ProductName, SUM(o.Quantity) AS TotalSold
FROM Orders o
INNER JOIN Products p ON o.ProductID = p.ProductID
WHERE p.Category = 'Electronics'
GROUP BY p.ProductName
ORDER BY TotalSold DESC;
